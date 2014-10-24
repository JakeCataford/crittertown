using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public bool debug = true;
	public float acceleration = 2.0f;

	float deadZone = Screen.height/3f;
	Vector2 speed = Vector2.zero;

	public Transform focusCritter;

	float initialFov;
	float initialHeight;

	AudioClip selectSound;

	public void SelectCritter(CritterController critter) {
		if (focusCritter != null) UI.Unhighlight (focusCritter.gameObject);
		Soundtrack.PlayOneShot (selectSound);
		UI.Highlight (critter.gameObject, Palette.Blue);
		focusCritter = critter.transform;
		CritterMenu.ShowForCritter (critter.critter);
	}
	
	public void Deselect() {
		if(focusCritter != null) {
			Soundtrack.PlayOneShot (selectSound);
			UI.Unhighlight (focusCritter.gameObject);
			focusCritter = null;
		}
		if(UI.ActiveMenu != null) UI.ActiveMenu.Hide ();
	}

	void Start() {
		selectSound = Resources.Load<AudioClip> ("select");
		initialFov = camera.fieldOfView;
		initialHeight = transform.position.y;
	}

	void Update() {
		if(focusCritter == null) {
			camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, initialFov, 0.1f);
			Vector2 screenCenter = new Vector2(Screen.width/2, Screen.height/2);

			if (Vector2.Distance (screenCenter, Input.mousePosition) > deadZone) {
				speed += new Vector2(Mathf.Clamp(Input.mousePosition.x/Screen.width - 0.5f, -0.5f, 0.5f) * acceleration, Mathf.Clamp(Input.mousePosition.y/Screen.height - 0.5f, -0.5f, 0.5f) * acceleration);
			}

			transform.position += new Vector3 (speed.x, 0, speed.y) * Time.deltaTime;
			speed *= 0.8f;

			Vector3 xzClamp = new Vector2(Mathf.Clamp(transform.position.x, -20, 20), Mathf.Clamp(transform.position.z, -20, 20));
			transform.position = new Vector3 (xzClamp.x, transform.position.y, xzClamp.y);
		} else {
			camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, initialFov - 30f, 0.1f);
			float distanceToTarget = Vector3.Distance (transform.position, focusCritter.position);
			Vector3 screenPositioningOffset = focusCritter.position - camera.ScreenToWorldPoint(new Vector3(100f, 3 * (Screen.height / 4), distanceToTarget));

			Vector3 targetWithOffset = focusCritter.position;
			targetWithOffset.y = initialHeight + (focusCritter.position.y/2f);
			targetWithOffset.z -= 10f;
			targetWithOffset.x += screenPositioningOffset.x; 
			transform.position = Vector3.Lerp(transform.position, targetWithOffset, 0.1f);
		}

		if(Input.GetKeyDown(KeyCode.Tab)) {
			if(focusCritter == null) {
				UI.ToastDebug("Focusing Random Critter");
				SelectCritter(GameObject.FindObjectOfType<CritterController>());
			} else {
				Deselect();
			}
		}
	}
}
