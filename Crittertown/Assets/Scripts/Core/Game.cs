using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class Game : SVBLM.Core.Singleton<Game> {
	public List<Consumable> Food = new List<Consumable>();
	public List<Consumable> Toys = new List<Consumable>();
	public List<Consumable> Beds = new List<Consumable>();

	public Dictionary<Transform, ORM.WorldItem> WorldItems = new Dictionary<Transform, ORM.WorldItem> ();

	public bool DragIsHappening = false;
	public static bool ObjectIsBeingDragged() {
		return Instance.DragIsHappening;
	}

	public static void StartDrag() {
		Instance.DragIsHappening = true;
	}

	public static void EndDrag() {
		Instance.DragIsHappening = false;
	}

	public bool _BlockingMenuActive = false;
	public static bool BlockingMenuActive {
		get {
			return Instance._BlockingMenuActive;
		}
		set {
			Instance._BlockingMenuActive = value;
		}
	}

	public void OnApplicationQuit() {
		Save ();
	}

	public void OnApplicationPause(bool paused) {
		if (paused) {
			Save ();
		}
	}

	public static new Camera camera {
		get {
			return Camera.main;
		}
		private set {}
	}

	#region UI
	public static bool UIEnabled = true;
	#endregion

	#region promode
	public static bool SupportsImageEffects() { return SystemInfo.supportsImageEffects && SystemInfo.supportsRenderTextures; }
	#endregion


	public static void RegisterFood(Consumable food) {
		if (Instance.Food.Contains (food)) {
			UI.ToastWarning("Food item was registered twice. Ignoring");
		} else {
			Instance.Food.Add(food);
		}
	}

	public static void DeregisterFood(Consumable food) {
		Instance.Food.Remove(food);
	}

	public static Consumable GetClosestFood(Vector3 position) {
		Instance.Food.RemoveAll ((f) => f == null);
		Instance.Food.Sort ((food1, food2) => Vector3.Distance (food1.transform.position, position).CompareTo(Vector3.Distance (food2.transform.position, position)));
		if(Instance.Food.Count > 0) {
			return Instance.Food.First();
		} else {
			return null;
		}
	}

	public static void RegisterToy(Consumable toy) {
		if (Instance.Toys.Contains (toy)) {
			UI.ToastWarning("Toy item was registered twice. Ignoring");
		} else {
			Instance.Toys.Add(toy);
		}
	}
	
	public static void DeregisterToy(Consumable toy) {
		Instance.Toys.Remove(toy);
	}
	
	public static Consumable GetClosestToy(Vector3 position) {
		Instance.Toys.RemoveAll ((f) => f == null);
		Instance.Toys.Sort ((toy1, toy2) => Vector3.Distance (toy1.transform.position, position).CompareTo(Vector3.Distance (toy2.transform.position, position)));
		if(Instance.Toys.Count > 0) {
			return Instance.Toys.First();
		} else {
			return null;
		}
	}

	public static void RegisterBed(Consumable bed) {
		if (Instance.Beds.Contains (bed)) {
			UI.ToastWarning("Bed item was registered twice. Ignoring");
		} else {
			Instance.Beds.Add(bed);
		}
	}
	
	public static void DeregisterBed(Consumable bed) {
		Instance.Beds.Remove(bed);
	}
	
	public static Consumable GetClosestBed(Vector3 position) {
		Instance.Beds.RemoveAll ((f) => f == null);
		Instance.Beds.Sort ((bed1, bed2) => Vector3.Distance (bed1.transform.position, position).CompareTo(Vector3.Distance (bed2.transform.position, position)));
		if(Instance.Beds.Count > 0) {
			return Instance.Beds.First();
		} else {
			return null;
		}
	}

	public static void RegisterWorldItem(Transform t, ORM.WorldItem worldItem) {
		Instance.WorldItems [t] = worldItem;
	}

	public static ORM.WorldItem QueryWorldItemForTransform(Transform t) {
		if (Instance.WorldItems.ContainsKey (t)) return Instance.WorldItems [t];
		return null;
	}

	public static void Save() {
		Debug.Log ("Saving " + Instance.WorldItems.Count + " WorldItems");
		foreach (KeyValuePair<Transform, ORM.WorldItem> kv in Instance.WorldItems) {
			if(kv.Key != null && kv.Value != null) {
				kv.Value.Position = kv.Key.position;
				kv.Value.Rotation = kv.Key.rotation;
				kv.Value.Save();
			}
		}
	}
}