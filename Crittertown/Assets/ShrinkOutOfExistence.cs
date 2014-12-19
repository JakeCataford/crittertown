using UnityEngine;
using System.Collections;

public class ShrinkOutOfExistence : MonoBehaviour {
	IEnumerator Start () {
		rigidbody.velocity = Random.onUnitSphere * 10f;
		rigidbody.angularVelocity = Random.onUnitSphere;
		yield return new WaitForSeconds(3.0f);
		while(true) {
			yield return null;
			transform.localScale = Vector3.Lerp (transform.localScale, Vector3.zero, 0.01f);
			if (transform.localScale.x < 0.01f) {
				Destroy(gameObject);
			}
		}
	}
}
