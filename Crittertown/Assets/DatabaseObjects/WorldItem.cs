using UnityEngine;
using System.Collections;
using SQLite4Unity3d;

namespace ORM {
	public class WorldItem : Model<WorldItem> {

		[Indexed]
		public int ItemId { get; set; }
		public float PositionX { get; set; }
		public float PositionY { get; set; }
		public float PositionZ { get; set; }
		
		public float RotationX { get; set; }
		public float RotationY { get; set; }
		public float RotationZ { get; set; }
		public float RotationW { get; set; }

		private Item cachedAssociation;

		[Ignore]
		public Vector3 Position {
			get {
				return new Vector3(PositionX, PositionY, PositionZ);
			}

			set {
				PositionX = value.x;
				PositionY = value.y;
				PositionZ = value.z;
			}
		}

		[Ignore]
		public Quaternion Rotation {
			get {
				return new Quaternion(RotationX, RotationY, RotationZ, RotationW);
			}
			
			set {
				RotationX = value.x;
				RotationY = value.y;
				RotationZ = value.z;
				RotationW = value.w;
			}
		}

		public WorldItem() {}

		public WorldItem(Item item) {
			Position = Vector3.zero;
			Rotation = Quaternion.identity;
			ItemId = item.Id;
			Save ();
		}

		public Item GetItem() {
			if(cachedAssociation == null) {
				cachedAssociation = DataService.GetConnection().Find<Item>(ItemId);
			}

			return cachedAssociation;
		}

		public GameObject Spawn() {
			if (GetItem () == null) {
				Debug.Log("Unrecognized item instance (id: " + ItemId + "). Deleting...");
				Destroy();
				return null;
			}

			GameObject go = (GameObject) Game.Instantiate(Resources.Load<GameObject> (GetItem ().Prefab));
			go.name = GetItem ().Title;

			go.transform.position = Position;
			go.transform.rotation = Rotation;

			Game.RegisterWorldItem(go.transform, this);
			return go;
		}

	}
}