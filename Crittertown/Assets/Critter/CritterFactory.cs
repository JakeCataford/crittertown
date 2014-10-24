using UnityEngine;
using System.Collections;

public class CritterFactory : MonoBehaviour {
	private static string[] firstNameParts = {
		"Mar", "Car", "Mat", "Bu", "Pep", "Po", "Prep", "Lo", "Pip", "Morp"
	};

	private static string[] middleNameParts = {
		"on", "be", "me", "o", "e", "en", "er", "re", "aer", "ry", "yr", "io"
	};

	private static string[] lastNameParts = {
		"tle", "el", "ter", "tee", "tum", "per", "par", "re", "ler", "tler"
	};


	public static void SpawnNewCritter() {
		Critter critter = new Critter (GetRandomName());

		critter.color = new HSBColor (Random.value, 0.5f, 0.8f).ToColor();

		System.Array signs = System.Enum.GetValues (typeof(Critter.Signs));
		critter.Sign = (Critter.Signs) signs.GetValue (Mathf.FloorToInt (Random.value * signs.Length));

		CritterController c = ((GameObject) Instantiate (Resources.Load<GameObject> ("Critter"))).GetComponent<CritterController>();
		c.critter = critter;
	}

	public static string GetRandomName() {
		string name = firstNameParts[Mathf.FloorToInt(Random.value * firstNameParts.Length)];

		for (int i = 0; i < Random.Range(0,3); i++) {
			name += middleNameParts[Mathf.FloorToInt(Random.value * middleNameParts.Length)];
		}

		name += lastNameParts[Mathf.FloorToInt(Random.value * lastNameParts.Length)];
		return name;
	}
}
