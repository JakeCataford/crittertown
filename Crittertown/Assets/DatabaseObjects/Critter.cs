using UnityEngine;
using System.Collections;
using SQLite4Unity3d;

public class Critter : ORM.Model<Critter> {

	public Critter() {}

	public enum Signs {
		Iba,
		Onu,
		Arah,
		Zebble
	}

	public bool Hatched = false;

	public string Name { get; set; }	
	public int FitnessPoints { get; set; }
	public int IntelligencePoints { get; set; }
	public int CharmPoints { get; set; }
	public int LoyaltyPoints { get; set; }

	public float ColorR { get; set; }
	public float ColorG { get; set; }
	public float ColorB { get; set; }

	public void Hatch() {
		Hatched = true;
	}

	[Ignore]
	public Color color {
		get {
			return new Color(ColorR, ColorG, ColorB, 1.0f);
		}

		set {
			ColorR = value.r;
			ColorG = value.g;
			ColorB = value.b;
		}
	}
	
	public Signs Sign = Signs.Zebble;
	
	public int Fitness {
		get {
			return FitnessPoints/100;
		}
		private set {}
	}
	
	public int Intelligence {
		get {
			return IntelligencePoints/100;
		}
		private set {}
	}
	
	public int Charm {
		get {
			return CharmPoints/100;
		}
		private set {}
	}
	
	public int Loyalty {
		get {
			return LoyaltyPoints/100;
		}
		private set {}
	}
	
	public float FitnessProgress {
		get {
			return (FitnessPoints % 100) / 100f;
		}
		private set {}
	}
	
	public float CharmProgress {
		get {
			return (CharmPoints % 100) / 100f;
		}
		private set {}
	}
	
	public float IntelligenceProgress {
		get {
			return (IntelligencePoints % 100) / 100f;
		}
		private set {}
	}
	
	public float LoyaltyProgress {
		get {
			return (LoyaltyPoints % 100) / 100f;
		}
		private set {}
	}
	
	public void AddFitnessPoints(int points) {
		int cachedFitnessLevel = Fitness;
		FitnessPoints += points;
		if (cachedFitnessLevel < Fitness) {
			UI.ToastDebug("Fitness LEVEL UP! NOW IT AT LEVEL " + Fitness);
		}
	}
	
	public void AddCharmPoints(int points) {
		int cachedCharmLevel = Charm;
		CharmPoints += points;
		if (cachedCharmLevel < Charm) {
			UI.ToastDebug("Charm LEVEL UP! NOW IT AT LEVEL " + Charm);
		}
	}
	
	public void AddIntelligencePoints(int points) {
		int cachedIntelligenceLevel = Intelligence;
		IntelligencePoints += points;
		if (cachedIntelligenceLevel < Intelligence) {
			UI.ToastDebug("Intelligence LEVEL UP! NOW IT AT LEVEL " + Intelligence);
		}
	}
	
	public void AddLoyaltyPoints(int points) {
		int cachedLoyaltyLevel = Loyalty;
		LoyaltyPoints += points;
		if (cachedLoyaltyLevel < Loyalty) {
			UI.ToastDebug("Loyalty LEVEL UP! NOW IT AT LEVEL " + Loyalty);
		}
	}


	private CritterController cachedController;
	public CritterController GetController() {
		if (cachedController != null) return cachedController;
		CritterController[] allControllers = GameObject.FindObjectsOfType<CritterController> ();
		foreach (CritterController controller in allControllers) {
			if(controller.critter.Id == Id) {
				cachedController = controller;
				return cachedController;
			}
		}

		return null;
	}
	
	public float Hunger { get; set; }
	public float Fatigue { get; set; }
	public float Fun { get; set; }
	
	public Critter(string name) {
		Name = name;
	}

	public CritterController Spawn(Vector3 spawn) {
		CritterController controller = ((GameObject) GameObject.Instantiate (Resources.Load<GameObject> ("Critter"))).GetComponent<CritterController>();

		Vector3 random = Random.insideUnitSphere * 30f;
		NavMeshHit hit;
		NavMesh.SamplePosition (spawn, out hit, 300f, 1);
		controller.transform.position = hit.position;
		controller.critter = this;
		return controller;
	}

	public void DecreaseHunger() {
		Hunger -= (0.02f * (1 + (Fitness * 0.1f)));
	}
	
	public void DecreaseFatigue() {
		Fatigue -= (0.02f * (1f / (Fitness + 1 * 0.5f)));
	}
	
	public void DecreaseFun() {
		Fun -= (0.02f * (1f / (Loyalty + 1 * 0.5f)));
	}
}
