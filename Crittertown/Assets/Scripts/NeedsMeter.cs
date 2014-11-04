using UnityEngine;
using System.Collections;

public class NeedsMeter : MonoBehaviour {
	public enum Needs {
		HUNGER,
		FATIGUE,
		FUN
	}

	public CritterController critter;
	public TankMeter meter;

	public Needs need = Needs.FATIGUE;

	void Update () {
		switch (need) {
		case Needs.HUNGER:
			meter.Amount = critter.critter.Hunger;
			break;
		case Needs.FATIGUE:
			meter.Amount = critter.critter.Fatigue;
			break;
		case Needs.FUN:
			meter.Amount = critter.critter.Fun;
			break;
		}
	}
}
