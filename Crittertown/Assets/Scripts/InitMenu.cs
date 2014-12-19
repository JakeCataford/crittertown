using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InitMenu : Menu {
	public Image panel;
	public Text loading;

	void Update() {
		Vector3 position = loading.rectTransform.position;
		position.y = Mathf.Lerp (position.y, 10, 0.1f);
		loading.rectTransform.position = position;
	}

	public override IEnumerator OnHideMenu () {
		Color initialColor = panel.color;
		float progress = 0f;
		while (progress < 1.0f) {
			progress += Time.deltaTime * 2.0f;
			panel.color = Color.Lerp(initialColor, Color.clear, progress);
			loading.rectTransform.position = Vector3.Lerp(loading.rectTransform.position, Vector3.down * 100f, 0.1f);
			yield return null;
		}

		Done ();
	}


}
