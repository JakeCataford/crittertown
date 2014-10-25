using UnityEngine;
using System.Collections;

public class Critter {

	public enum Signs {
		Iba,
		Onu,
		Arah,
		Zebble
	}

	public readonly string Name = "Horsel";
	private int FitnessPoints = 0;
	private int IntelligencePoints = 0;
	private int CharmPoints = 0;
	private int LoyaltyPoints = 0;

	public Color color = Color.cyan;
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

	public Critter(string name) {
		Name = name;
	}

	public static void LoadByName(string name) {

	}
}
