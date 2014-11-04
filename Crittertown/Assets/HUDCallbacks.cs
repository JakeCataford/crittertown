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

	public void SpawnBall() {
		Placeable p = Placeable.Create("TestBall", Random.insideUnitSphere + Vector3.up * 5f, Quaternion.identity);
		p.Spawn();
	}
}
