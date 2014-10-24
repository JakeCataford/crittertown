using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

	CameraController cameraController;

	void Start() {
		cameraController = GetComponent<CameraController> ();
	}

	void Update() {
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;
			Ray ray = camera.ScreenPointToRay (new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.1f));
			if(Physics.Raycast(ray, out hit, 100f)) {
				CritterController critter = hit.collider.gameObject.GetComponent<CritterController>();
				if(critter != null) {
					if(critter.transform != cameraController.focusCritter) cameraController.SelectCritter(critter);
				} else {
					cameraController.Deselect();
				}
			}
		}

		if (Input.GetKeyDown (KeyCode.KeypadPlus)) {
			UI.ToastDebug("New Critter Created!");
			CritterFactory.SpawnNewCritter();
		}
	}
}
