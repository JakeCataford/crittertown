using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TankMeter : MonoBehaviour {

	public float Amount = 0.5f;
	public float Disturbance = 0.3f;

	private float realAmount = 0.5f;
	private float previousAmount;
	private Material material;

	public Image image;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();
		material = (Material) Instantiate(image.material);
		image.material = material;
	}
	
	// Update is called once per frame
	void Update () {
		material.SetColor ("_Color", image.color);
		realAmount = Mathf.Lerp (realAmount, Amount, 0.1f);
		material.SetFloat ("_Level", realAmount);
		Disturbance += Mathf.Abs ((realAmount - previousAmount) * 0.2f);
		Disturbance = Mathf.Lerp (Disturbance, 0.01f, 0.05f);
		Disturbance = Mathf.Clamp (Disturbance, 0, 0.1f);
		material.SetFloat ("_WaveHeight", Disturbance);
		previousAmount = Amount;
	}
}
