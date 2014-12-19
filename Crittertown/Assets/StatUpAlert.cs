using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatUpAlert : Alert {
	public string stat;
	public Text text;

	public override IEnumerator OnShowMenu () {
		text.text = stat;
		float progress = 0;
		Color cachedTextColor = text.color;
		Color clearTextColor = text.color;
		clearTextColor.a = 0;
		while (progress < (Mathf.PI/2f)) {
			text.color = Color.Lerp(clearTextColor, cachedTextColor, Mathf.Sin ((Mathf.PI/2f) + progress));
			text.rectTransform.localPosition = Vector3.up * progress;
			progress += Time.deltaTime * 0.5f;
			yield return null;
		}

		Done ();
	}

	public override IEnumerator OnHideMenu ()
	{
		UI.ToastWarning ("You cannot hide a stat up alert, it will disapear on its own.");
		yield return null;
	}

}
