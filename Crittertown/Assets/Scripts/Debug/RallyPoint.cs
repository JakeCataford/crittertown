using UnityEngine;
using System.Collections;

public class RallyPoint : MonoBehaviour {

	public Vector3 position = Vector3.zero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		if (Input.GetMouseButton (1) && Physics.Raycast(Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100f)) {
			transform.position = hit.point;
			particleSystem.Emit(10);
			CritterController[] controllers = GameObject.FindObjectsOfType<CritterController>();
			foreach(CritterController c in controllers) {
				c.WanderToTarget(hit.point);
			}
		} else {
			particleSystem.Stop();
		}
	}
}
