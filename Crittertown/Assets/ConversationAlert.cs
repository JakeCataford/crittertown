using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ConversationAlert : Alert {
	public Image image;

	public override IEnumerator OnShowMenu () {
		float progress = 0.0f;
		Color endColor = image.color;
		Color clearColor = new Color (endColor.r, endColor.g, endColor.b, 0);
		while (progress < 1.0f) {
			progress += Time.deltaTime * 1.5f;
			Vector3 position =  image.rectTransform.localPosition;
			position.y = Mathfx.Berp(-1,0,progress);
			image.rectTransform.localPosition = position;
			image.color = Color.Lerp(clearColor, endColor, progress);
			yield return null;
		}
	}

	public override IEnumerator OnHideMenu () {
		float progress = 0.0f;
		Color startColor = image.color;
		Color clearColor = new Color (startColor.r, startColor.g, startColor.b, 0);
		while (progress < 1.0f) {
			progress += Time.deltaTime * 1.5f;
			image.color = Color.Lerp(startColor, clearColor, progress);
			yield return null;
		}
		
		Done ();
	}

}
