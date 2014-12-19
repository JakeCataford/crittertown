using UnityEngine;
using System.Collections;

public class EyeBlink : MonoBehaviour {

	public bool blink = false;
	public SkinnedMeshRenderer otherEye;

	void Update () {
		if (blink) {
			renderer.enabled = false;
			otherEye.enabled = false;
		} else {
			renderer.enabled = true;
			otherEye.enabled = true;
		}

		if (Random.value < 0.01f) {
			StartCoroutine(Blink());
		}
	}
	
	IEnumerator Blink() {
		blink = true;
		yield return new WaitForSeconds(0.1f);
		blink = false;
	}
}
