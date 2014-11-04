using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CritterMenu : Menu {

	public Critter critter;
	public float showHide = 0.0f;
	private const float SHOW_HIDE_SPEED = 1.2f;


	public Image panel;
	private float offset = Screen.width * 3;
	private Dictionary<RectTransform, float> timeScatter; 
	private Dictionary<RectTransform, float> desiredPositions;
	private float hidden = 1.0f;

	public new Text name;
	public Text intelligenceLevel;
	public Text fitnessLevel;
	public Text charmLevel;
	public Text loyaltyLevel;

	public Image intelligenceProgress;
	public Image fitnessProgress;
	public Image charmProgress;
	public Image loyaltyProgress;

	public TankMeter hunger;
	public TankMeter fatigue;
	public TankMeter fun;


	public static void ShowForCritter(Critter critter) {
		CritterMenu c = Menu.Show<CritterMenu>();
		c.critter = critter;
	}

	public override IEnumerator OnShowMenu () {
		timeScatter = new Dictionary<RectTransform, float> ();
		desiredPositions = new Dictionary<RectTransform, float> ();
		
		foreach (RectTransform t in transform) {
			timeScatter.Add(t, Random.Range(1.0f, 1.5f));
			desiredPositions.Add (t, t.position.x);
		}
		
		timeScatter.Remove (panel.rectTransform);
		desiredPositions.Remove (panel.rectTransform);
		
		float progress = 0.0f;
		while (progress < 1.0f) {
			hidden = Mathfx.Berp(1, 0f, progress);
			progress += Time.deltaTime * SHOW_HIDE_SPEED;
			
			Color panelColor = panel.color;
			panelColor.a = 0.8f - hidden;
			panel.color = panelColor;
			
			foreach (RectTransform t in timeScatter.Keys) {
				Vector3 position = t.position;
				float scaledProgress = Mathf.Clamp(progress * timeScatter[t], 0, 1f);
				position.x = Mathfx.Sinerp (offset, desiredPositions[t], scaledProgress);
				t.position = position;
			}
			
			yield return null;
		}
		
		
	}


	public override IEnumerator OnHideMenu () {
		timeScatter = new Dictionary<RectTransform, float> ();
		desiredPositions = new Dictionary<RectTransform, float> ();
		
		foreach (RectTransform t in transform) {
			timeScatter.Add(t, Random.Range(1.0f, 1.5f));
			desiredPositions.Add (t, t.position.x);
		}
		
		timeScatter.Remove (panel.rectTransform);
		desiredPositions.Remove (panel.rectTransform);
		
		float progress = 1.0f;
		while (progress > 0.0f) {
			hidden = Mathfx.Berp(0, 0.8f, progress);
			progress -= Time.deltaTime * SHOW_HIDE_SPEED;
			
			Color panelColor = panel.color;
			panelColor.a = hidden;
			panel.color = panelColor;
			
			foreach (RectTransform t in timeScatter.Keys) {
				Vector3 position = t.position;
				float scaledProgress = Mathf.Clamp(progress * timeScatter[t], 0, 1f);
				position.x = Mathfx.Sinerp (offset, desiredPositions[t], scaledProgress);
				t.position = position;
			}
			
			yield return null;
		}

		Done ();
	}
	public void Update() {
		name.text = critter.Name;
		intelligenceLevel.text = critter.Intelligence.ToString();
		fitnessLevel.text = critter.Fitness.ToString();
		charmLevel.text = critter.Charm.ToString();
		loyaltyLevel.text = critter.Loyalty.ToString();


		intelligenceProgress.fillAmount = Mathf.Lerp (intelligenceProgress.fillAmount, critter.IntelligenceProgress, 0.1f);
		fitnessProgress.fillAmount = Mathf.Lerp (fitnessProgress.fillAmount, critter.FitnessProgress, 0.1f);
		charmProgress.fillAmount = Mathf.Lerp (charmProgress.fillAmount, critter.CharmProgress, 0.1f);
		loyaltyProgress.fillAmount = Mathf.Lerp (loyaltyProgress.fillAmount, critter.LoyaltyProgress, 0.1f);

		hunger.Amount = critter.Hunger;
		fatigue.Amount = critter.Fatigue;
		fun.Amount = critter.Fun;


	}

	public void DebugIncreaseHunger() {
		critter.Hunger += 0.1f;
	}

	public void DebugIncreaseFatigue() {
		critter.Fatigue += 0.1f;
	}

	public void DebugIncreaseFun() {
		critter.Fun += 0.1f;
	}



}
