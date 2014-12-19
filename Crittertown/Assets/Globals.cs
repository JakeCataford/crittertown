using UnityEngine;
using System.Collections;

public class Globals : SVBLM.Core.Singleton<Globals> {
	public delegate void CoinsChangedHandler(int newAmount);
	public static event CoinsChangedHandler OnCoinsChangedEvent;


	private static string COINS_KEY = "COINS";

	
	public static void Reset() {
		PlayerPrefs.DeleteAll ();
	}

	public static int GetCoins() {
		if (!PlayerPrefs.HasKey (COINS_KEY)) {
			PlayerPrefs.SetInt(COINS_KEY, 300);
		}

		return PlayerPrefs.GetInt(COINS_KEY);
	}

	public static int AddCoins(int amount) {
		int coins = GetCoins ();
		coins += amount;
		PlayerPrefs.SetInt(COINS_KEY, coins);
		if(OnCoinsChangedEvent != null) OnCoinsChangedEvent(coins);
		return coins;
	}

	public static int RemoveCoins(int amount) {
		int coins = GetCoins ();
		coins -= amount;
		PlayerPrefs.SetInt(COINS_KEY, coins);
		if(OnCoinsChangedEvent != null) OnCoinsChangedEvent(coins);
		return coins;
	}
}
