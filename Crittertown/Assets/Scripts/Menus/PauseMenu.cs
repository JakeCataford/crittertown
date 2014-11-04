using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PauseMenu : Menu{
	public static string GetMenuName() {
		return "PauseMenu";
	}

	private const float SHOW_HIDE_SPEED = 0.7f;
	public Text title;
	public Image panel;
	private float offset = Screen.width * 3;

	private Dictionary<RectTransform, float> timeScatter; 
	private Dictionary<RectTransform, float> desiredPositions;

	private float hidden = 1.0f;


	public override IEnumerator OnShowMenu () {
		Game.BlockingMenuActive = true;
		Soundtrack.Quiet ();
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

	public void SpawnACritter() {
		UI.ToastDebug("New Critter Created!");
		CritterFactory.SpawnNewCritter();
	}

	public void IncreaseStats() {

	}

	public override IEnumerator OnHideMenu () {
		Game.BlockingMenuActive = false;
		Soundtrack.Loud ();
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
			hidden = Mathfx.Berp(1, 0f, progress);
			progress -= Time.deltaTime * SHOW_HIDE_SPEED;
			
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
		Done ();
		
	}
}
