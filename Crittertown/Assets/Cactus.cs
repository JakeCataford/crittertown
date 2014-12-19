using UnityEngine;
using System.Collections;

public class Cactus : MonoBehaviour {

	private float actualSize;
	private Color actualColor;

	public CactusSpot spot;

	IEnumerator Start() {
		yield return new WaitForSeconds (1.0f);
		transform.localScale = Vector3.one * actualSize;
		renderer.material.color = actualColor;
		transform.position = spot.position;
		name = "Cactus";
	}

	void Update () {
		if (spot.CactusColor () != actualColor || spot.CactusSize () != actualSize) {
			actualSize = Mathf.Lerp(actualSize, spot.CactusSize(), 0.1f);
			actualColor = Color.Lerp(actualColor, spot.CactusColor(), 0.1f);

			transform.localScale = Vector3.one * actualSize;
			renderer.material.color = actualColor;
		}
	}
}
