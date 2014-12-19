using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinMeter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Globals.OnCoinsChangedEvent += OnCoinsChanged;
		GetComponent<Text>().text = "" + Globals.GetCoins();
	}

	void OnDestroy() {
		Globals.OnCoinsChangedEvent -= OnCoinsChanged;
	}
	
	void OnCoinsChanged(int newAmount) {
		GetComponent<Text>().text = "" + newAmount;
	}
}
