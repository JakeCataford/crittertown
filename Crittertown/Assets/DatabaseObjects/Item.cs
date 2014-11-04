using UnityEngine;
using System.Collections;
using SQLite4Unity3d;

namespace ORM {
	public class Item : Model<Item> {
		public string Prefab { get; set; }
		[Unique]
		public string Preview { get; set; }
		[Unique]
		public string Title { get; set; }
		public string Description { get; set; }
		public int Price { get; set; }

		public GameObject editPrefab;
		public Texture2D editTexture;

		public Item() {}

		public Item(string title, string description, string prefab, string preview, int price) {
			Prefab = prefab;
			Title = title;
			Description = description;
			Preview = preview;
			Price = price;
			Save ();
		}

		public GameObject SpawnInWorld() {
			WorldItem w = new WorldItem (this);
			GameObject go = w.Spawn ();
			return go;
		}

		public void AddToInventory() {}
	}
}
