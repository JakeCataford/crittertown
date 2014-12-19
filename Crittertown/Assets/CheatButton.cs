using UnityEngine;
using System.Collections;

public class CheatButton : MonoBehaviour {
	public void AddCash(int amount) {
		Globals.AddCoins (amount);
	}

}
