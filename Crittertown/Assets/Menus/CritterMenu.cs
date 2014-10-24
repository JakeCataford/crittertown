using UnityEngine;
using System.Collections;

public class CritterMenu : Menu {

	public Critter Critter;
	public float showHide = 0.0f;

	public static void ShowForCritter(Critter critter) {
		CritterMenu c = UI.ShowMenu<CritterMenu> ();
		c.Critter = critter;
	}

	public override IEnumerator OnShowMenu () {
		float progress = 0f;
		while (progress < 1.0f) {
			showHide = Mathfx.Hermite(0,1f,progress);
			progress += Time.deltaTime * 2.0f;
			yield return null;
		}

		showHide = 1.0f;
	}

	public override IEnumerator OnHideMenu () {
		float progress = 0f;
		while (progress < 1.0f) {
			showHide = Mathfx.Coserp(1,0f,progress);
			progress += Time.deltaTime * 2.0f;
			yield return null;
		}
		
		showHide = 0.0f;
		Done ();
	}

	public override void DrawMenu () {
		Matrix4x4 temp = GUI.matrix;
		GUIUtility.RotateAroundPivot(3f, new Vector2(Screen.width/2f, Screen.height/2f));

		GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, showHide);

		string content = "" + Critter.Name + "";
		content += "\nFitness: lv " + Critter.Fitness + "<i> (" + (Critter.FitnessProgress * 100f) + "%)</i>";
		content += "\nCharm: lv " + Critter.Charm + "<i> (" + (Critter.CharmProgress * 100f) + "%)</i>";
		content += "\nLoyalty: lv " + Critter.Loyalty + "<i> (" + (Critter.LoyaltyProgress * 100f) + "%)</i>";
		content += "\nIntelligence: lv " + Critter.Intelligence + "<i> (" + (Critter.IntelligenceProgress * 100f) + "%)</i>";

		content += "\n\n";

		content += "\nSign: " + Critter.Sign.ToString ();

		content += "\n\n";

		GUI.Box (new Rect (Screen.width / 2f + (600f * (1f - showHide)), -30f, Screen.width / 2f + 20f, Screen.height + 40f), content);
		GUI.matrix = temp;
		GUI.color = Color.white;
	}


}
