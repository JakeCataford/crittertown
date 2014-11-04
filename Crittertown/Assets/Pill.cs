using UnityEngine;
using System.Collections;

public class Pill : Consumable {
	public Type PillType = Type.FOOD;
	override public Type Cures {
		get {
			return PillType;
		}
		set {
		}
	}

	override public IEnumerator Effect(Critter critter) {
		switch(Cures) {
		case Type.FOOD:
			critter.Hunger = 1.0f;
			break;
		case Type.BED:
			critter.Fatigue = 1.0f;
			break;
		case Type.TOY:
			critter.Fun = 1.0f;
			break;
		}
		yield break;
	}
}
