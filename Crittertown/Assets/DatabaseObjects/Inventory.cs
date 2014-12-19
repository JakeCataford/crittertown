using UnityEngine;
using System.Collections;
using SQLite4Unity3d;
using System.Linq;

public class Inventory : ORM.Model<Inventory> {
	public Inventory() {}

	[Unique]
	public int ItemId { get; set; }
	public int quantity { get; set; }

	public static void Aquire(ORM.Item item) {
		Inventory inventory = Inventory.FindByItemId(item.Id);
		if (inventory == null) {
			UI.ToastDebug(item.Title + " was not tracked, creating a new entry now");
			inventory = new Inventory();
			inventory.ItemId = item.Id;
		} 

		inventory.quantity ++;
		inventory.Save ();
	}

	public static void Spend(ORM.Item item) {
		Inventory inventory = Inventory.FindByItemId(item.Id);

		if (inventory == null) {
			UI.ToastDebug(item.Title + " was not tracked, creating a new entry now");
			inventory = new Inventory();
			inventory.ItemId = item.Id;
		}

		if (inventory.quantity > 0) { 
			inventory.quantity --;
		}

		inventory.Save ();
	}

	public ORM.Item Item() {
		return ORM.Item.FindById(ItemId);
	}

	public static Inventory FindByItemId(int id) {
		return DataService.GetConnection ().Query<Inventory> ("SELECT * FROM Inventory WHERE ItemId=?", id).FirstOrDefault();
	}
}
