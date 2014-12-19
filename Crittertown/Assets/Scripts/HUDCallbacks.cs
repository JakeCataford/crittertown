using UnityEngine;
using System.Collections;

public class HUDCallbacks : MonoBehaviour {
	void Start() {
		UI.CreateEventSystem ();
	}

	public void OpenInventory() {
		Menu.Show<InventoryMenu> ();
	}

	public void OpenPauseMenu() {
		Menu.Show<PauseMenu> ();
	}

	public void OpenMarketMenu() {
		Menu.Show<MarketMenu> ();
	}
}
