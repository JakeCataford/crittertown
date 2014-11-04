using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NeedsAlert : Alert {
	public enum Needs {
		HUNGER,
		FATIGUE,
		FUN
	}

	public CritterController critter;
	public TankMeter meter;
	
	public Needs need = Needs.FATIGUE;

	public override IEnumerator OnShowMenu () {
		critter = GetComponentInParent<CritterController> ();
		float progress = 0.0f;
		Color endColor = meter.image.color;
		Color clearColor = new Color (endColor.r, endColor.g, endColor.b, 0);
		while (progress < 1.0f) {
			progress += Time.deltaTime * 1.5f;
			Vector3 position =  meter.image.rectTransform.localPosition;
			position.y = Mathfx.Berp(-1,0,progress);
			meter.image.rectTransform.localPosition = position;
			meter.image.color = Color.Lerp(clearColor, endColor, progress);
			yield return null;
		}
	}

	public override void OnUpdate () {
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

	public override IEnumerator OnHideMenu () {
		float progress = 0.0f;
		Color startColor = meter.image.color;
		Color clearColor = new Color (startColor.r, startColor.g, startColor.b, 0);
		while (progress < 1.0f) {
			progress += Time.deltaTime * 1.5f;
			meter.image.color = Color.Lerp(startColor, clearColor, progress);
			yield return null;
		}

		Done ();
	}

}
