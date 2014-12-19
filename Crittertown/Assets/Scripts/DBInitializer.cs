using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DBInitializer : MonoBehaviour {

	public Menu loadingMenu;

	void Start() {
		StartCoroutine (DoLoad ());
	}

	void Finish() {
		loadingMenu.Hide ();
		Destroy (this);
	}

	IEnumerator DoLoad() {
		DataService.Init ();

		while (!DataService.IsReady()) {
			yield return null;
		}

		Migrate ();

		IEnumerable<ORM.WorldItem> worldItems = ORM.WorldItem.All ();
		foreach (ORM.WorldItem item in worldItems) {
			item.Spawn();
		}

		IEnumerable<Critter> critters = Critter.All ();
		foreach (Critter critter in critters) {
			critter.Spawn(new Vector3(Random.Range(-10f,10f), 0, Random.Range (-10f,10f)));
		}

		Finish ();
	}

	void Migrate() {
		DataService.GetConnection ().CreateTable<ORM.Item> ();
		DataService.GetConnection ().CreateTable<ORM.WorldItem> ();
		DataService.GetConnection ().CreateTable<Critter> ();
		DataService.GetConnection ().CreateTable<CactusSpot> ();
	}
}
