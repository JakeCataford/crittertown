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

		IEnumerable<ORM.WorldItem> worldItems = ORM.WorldItem.All ();
		foreach (ORM.WorldItem item in worldItems) {
			item.Spawn();
		}

		Finish ();
	}
}
