using UnityEngine;
using System.Collections;

public class PlaceableController : MonoBehaviour {
	public Placeable Model;

	void OnApplicationQuit() {
		if(Model != null) SerializeAndSave ();
	}

	void OnApplicationPause() {
		if(Model != null) SerializeAndSave ();
	}

	void SerializeAndSave() {
		Model.PositionX = transform.position.x;
		Model.PositionY = transform.position.y;
		Model.PositionZ = transform.position.z;
		
		Model.RotationX = transform.rotation.x;
		Model.RotationY = transform.rotation.y;
		Model.RotationZ = transform.rotation.z;
		Model.RotationW = transform.rotation.w;
		
		Model.Save ();
	}

}
