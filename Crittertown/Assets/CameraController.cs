using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public bool debug = true;
	public float acceleration = 2.0f;

	float deadZone = Screen.height/3f;
	Vector2 speed = Vector2.zero;

	void Update() {
		Vector2 screenCenter = new Vector2(Screen.width/2, Screen.height/2);

		if (Vector2.Distance (screenCenter, Input.mousePosition) > deadZone) {
			speed += new Vector2(Mathf.Clamp(Input.mousePosition.x/Screen.width - 0.5f, -0.5f, 0.5f) * acceleration, Mathf.Clamp(Input.mousePosition.y/Screen.height - 0.5f, -0.5f, 0.5f) * acceleration);
		}

		transform.position += new Vector3 (speed.x, 0, speed.y) * Time.deltaTime;
		speed *= 0.8f;
	}
}
