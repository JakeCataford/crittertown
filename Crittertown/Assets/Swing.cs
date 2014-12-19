using UnityEngine;
using System.Collections;

public class Swing : MonoBehaviour {

	public float frequency = 1f;
	public float amplitude = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.rotation = Quaternion.Euler(new Vector3 (0, 0, Mathf.Sin (Time.time * frequency) * amplitude));
	}
}
