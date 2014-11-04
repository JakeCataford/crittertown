using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InventoryMenu : Menu {
	public static string GetMenuName() {
		return "PauseMenu";
	}

	public Text selectedTitle;
	public Text selectedDescription;

	private string defaultTitle;
	private string defaultDescription;
	
	private const float SHOW_HIDE_SPEED = 0.7f;
	public Image bag;
	public Image panel;
	public GridLayoutGroup grid;
	private float offset = Screen.width * 3;
	
	private Dictionary<RectTransform, float> timeScatter; 
	private Dictionary<RectTransform, float> desiredPositions;
	
	private float hidden = 1.0f;

	public ORM.Item selectedItem;
	
	public override IEnumerator OnShowMenu () {
		Game.BlockingMenuActive = true;
		defaultTitle = selectedTitle.text;
		defaultDescription = selectedDescription.text;

		LoadGrid ();
		Soundtrack.Quiet ();
		timeScatter = new Dictionary<RectTransform, float> ();
		desiredPositions = new Dictionary<RectTransform, float> ();
		
		foreach (RectTransform t in transform) {
			timeScatter.Add(t, Random.Range(1.0f, 1.5f));
			desiredPositions.Add (t, t.position.x);
		}
		
		timeScatter.Remove (panel.rectTransform);
		desiredPositions.Remove (panel.rectTransform);

		timeScatter.Remove (bag.rectTransform);
		desiredPositions.Remove (bag.rectTransform);
		
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

	void Update() {
		if (selectedItem != null) {
			selectedTitle.text = selectedItem.Title;
			selectedDescription.text = selectedItem.Description;
		} else {
			selectedTitle.text = defaultTitle;
			selectedDescription.text = defaultDescription;
		}
	}

	public override IEnumerator OnHideMenu() {
		Game.BlockingMenuActive = false;
		Soundtrack.Loud ();
		offset = -Screen.height;
		timeScatter = new Dictionary<RectTransform, float> ();
		desiredPositions = new Dictionary<RectTransform, float> ();
		
		foreach (RectTransform t in transform) {
			timeScatter.Add(t, Random.Range(1.0f, 1.5f));
			desiredPositions.Add (t, t.position.y);
		}
		
		float progress = 1.0f;
		while (progress > 0.0f) {
			hidden = Mathfx.Berp(1, 0f, progress);
			progress -= Time.deltaTime * SHOW_HIDE_SPEED * 3.0f;
			
			Color panelColor = panel.color;
			panelColor.a = 0.8f - hidden;
			panel.color = panelColor;
			
			foreach (RectTransform t in timeScatter.Keys) {
				Vector3 position = t.position;
				float scaledProgress = Mathf.Clamp(progress * timeScatter[t], 0, 1f);
				position.y = Mathfx.Sinerp (offset, desiredPositions[t], scaledProgress);
				t.position = position;
			}
			
			yield return null;
		}
		Done ();
		
	}

	public void LoadGrid() {
		foreach (ORM.Item item in ORM.Item.All()) {
			Image background = new GameObject().AddComponent<Image>();
			background.sprite = Resources.Load<Sprite>("back-circle");
			background.transform.parent = grid.transform;
			Image image = new GameObject().AddComponent<Image>();
			image.name = item.Title;
			image.rectTransform.localScale = new Vector3(0.75f,0.75f,0.75f);
			Shadow shadow = image.gameObject.AddComponent<Shadow>();
			shadow.effectDistance = new Vector2(0,-4f);
			shadow.effectColor = new Color(0,0,0,0.2f);
			image.sprite = Resources.Load<Sprite>(item.Preview);
			InventoryMenuItem inventoryItem = image.gameObject.AddComponent<InventoryMenuItem>();
			inventoryItem.Host = this;
			inventoryItem.item = item;
			image.transform.parent = background.transform;
		}
	}
}
