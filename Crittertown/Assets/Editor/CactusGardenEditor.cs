using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(CactusGarden))]
public class CactusGardenEditor : Editor {

	void OnEnable() {
		CactusGarden garden = (CactusGarden) target;
		CactusSpot.editMode = true;
		IEnumerable<CactusSpot> spots = CactusSpot.All ();
		foreach (CactusSpot spot in spots) {
			garden.spots.Add(spot);
		}
	}

	void OnSceneGUI() {
		CactusGarden garden = (CactusGarden) target;
		int id = 0;

		for(int i = 0; i < garden.spots.Count; i++) {
			garden.spots[i].position = Handles.FreeMoveHandle(garden.spots[i].position, Quaternion.identity, 0.1f, Vector2.zero, Handles.CircleCap);
			id ++;
		}

		if (GUI.changed) {
			CactusSpot.editMode = true;
			garden.spots.ForEach(x => x.Save());
			EditorUtility.SetDirty(target);
		}
	}

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		
		CactusGarden garden = (CactusGarden)target;
		if(GUILayout.Button("New Spot")) {
			garden.spots.Add(new CactusSpot(garden.transform.position + Vector3.up * 2f));
		}
	}
}
