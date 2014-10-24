using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public abstract class ContactTrigger : MonoBehaviour {
	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player") {
			OnTriggered();
		}
	}

	public abstract void OnTriggered();
}
