using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class InputController : MonoBehaviour {

	CameraController cameraController;

	void Start() {
		cameraController = GetComponent<CameraController> ();
		Input.simulateMouseWithTouches = true;
	}

	void Update() {
		if (Input.GetMouseButtonDown (0) && !EventSystem.current.IsPointerOverGameObject() && !Game.BlockingMenuActive) {
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

		if (Input.GetKeyDown (KeyCode.P)) {
			UI.ToastDebug("New Critter Created!");
			CritterFactory.SpawnNewCritter();
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			Menu.Show<PauseMenu>(false);
		}

		if(Input.GetKeyDown(KeyCode.B)) {
			Placeable p = Placeable.Create("TestBall", Random.insideUnitSphere + Vector3.up * 5f, Quaternion.identity);
			p.Spawn();
		}
	}
}
