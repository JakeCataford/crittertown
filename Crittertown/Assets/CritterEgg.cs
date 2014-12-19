using UnityEngine;
using System.Collections;

public class CritterEgg : MonoBehaviour {
	public GameObject top;
	public GameObject bottom;
	public ParticleSystem particle;
	public int tapsLeft = Mathf.FloorToInt(Random.Range(3.0f, 5.0f));

	void Start() {
		rigidbody.centerOfMass = Vector3.down * 0.5f;
	}

	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit hit = new RaycastHit();
			if(Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f)), out hit, 100f)) {
				if(hit.collider.transform == transform) {
					rigidbody.angularVelocity = Vector3.right * 5.0f + Random.onUnitSphere;
					tapsLeft --;

					if(tapsLeft <= 0) Hatch();
				}
			}                                     
		}
	}

	void Hatch() {
		//Spawn Halfs and critter
		Instantiate (top, transform.position, transform.rotation);
		Instantiate (bottom, transform.position, transform.rotation);
		CritterController controller = CritterFactory.SpawnNewCritter(transform.position);
		controller.animator.SetTrigger("Born");
		controller.transform.forward = -Vector3.forward;
		Destroy (gameObject);
	}
}
