using UnityEngine;
using System.Collections;
using SQLite4Unity3d;

public class Placeable  {
	[PrimaryKey, AutoIncrement]
	public int Id { get; set; }
	public float PositionX { get; set; }
	public float PositionY { get; set; }
	public float PositionZ { get; set; }

	public float RotationX { get; set; }
	public float RotationY { get; set; }
	public float RotationZ { get; set; }
	public float RotationW { get; set; }

	public string Prefab   { get; set; }

	public static Placeable Create(string prefabResource, Vector3 position, Quaternion rotation) {
		Placeable p = new Placeable {
			PositionX = position.x,
			PositionY = position.y,
			PositionZ = position.z,

			RotationX = rotation.x,
			RotationY = rotation.y,
			RotationZ = rotation.z,
			RotationW = rotation.w,

			Prefab 	  = prefabResource
		};

		p.Save(true);

		return p;
	}

	public void Save(bool isNewRecord = false) {
		if (isNewRecord) {
			DataService.GetConnection().Insert (this);
		} else {
			int num = DataService.GetConnection().Update(this);
			Debug.Log (num);
		}
	}

	public void Obliterate() {
		DataService.GetConnection ().Delete (this);
	}

	public PlaceableController Spawn() {
		GameObject go = (GameObject) Game.Instantiate(Resources.Load<GameObject> (Prefab));
		go.name = "Placeable (" + Prefab + ")";
		go.transform.position = new Vector3 (PositionX, PositionY, PositionZ);
		go.transform.rotation = new Quaternion (RotationX, RotationY, RotationZ, RotationW);
		PlaceableController pc = go.AddComponent<PlaceableController> ();
		pc.Model = this;
		return pc;
	}
}
