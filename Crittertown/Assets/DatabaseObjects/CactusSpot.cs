using UnityEngine;
using System.Collections;
using SQLite4Unity3d;

public class CactusSpot : ORM.Model<CactusSpot> {
	public CactusSpot() {}
	public CactusSpot(Vector3 position) {
		this.position = position;
		Owner = 0;
	}
	public float positionX { get; set; }
	public float positionY { get; set; }
	public float positionZ { get; set; }
	
	public int Owner { get; set; }

	public float CactusQuality = 0.0f;
	public float CactusVitality = 1.0f;

	[Ignore]
	public Vector3 position {
		get {
			return new Vector3(positionX, positionY, positionZ);
		}
		set {
			positionX = value.x;
			positionY = value.y;
			positionZ = value.z;
		}
	}

	private Cactus cachedCactusObject;

	[Ignore]
	public Cactus CactusObject {
		get {
			if(cachedCactusObject == null) {
				init ();
			}

			return cachedCactusObject;
		}
	}

	public bool hasInit = false;

	public void init() {
		if(!hasInit) {
			cachedCactusObject = SpawnCactusObject();
		}
	}

	public Color CactusColor() {
		return Color.Lerp(Color.gray, Color.green, CactusVitality);
	}

	public float CactusSize() {
		return CactusQuality * 0.8f;
	}

	public Cactus SpawnCactusObject() {
		GameObject go = GameObject.CreatePrimitive (PrimitiveType.Capsule);
		Cactus cactus = go.AddComponent<Cactus> ();
		cactus.spot = this;

		return cactus;
	}

	public void TheCruelAndInevitableDecayOfTime() {
		CactusVitality -= 0.1f;

		if (CactusVitality < 0) {
			Owner = 0;
			CactusVitality = 0;
			CactusQuality = 0;
		}
	}

	public void StartGrowing() {
		CactusQuality = 0.1f;
		CactusVitality = 1.0f;
	}

	public bool IsFree() {
		return Owner == 0;
	}

	public void Finish() {
		Owner = 0;
		CactusVitality = 0;
		CactusQuality = 0;
	}
}
