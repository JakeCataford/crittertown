using UnityEngine;
using System.Collections;

public class UIPulse : MonoBehaviour {

	Vector3 originalScale;

	// Use this for initialization
	void Start () {
		originalScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = originalScale + Vector3.one * (Mathf.Sin (Time.time * 3f) * 0.05f);
	}
}
