using UnityEngine;
using System.Collections;

public abstract class Menu : MonoBehaviour {

	public GUISkin skin = Resources.Load<GUISkin>("MenuSkin");

	public void Show() {
		StartCoroutine (OnShowMenu());
	}

	public void Hide() {
		StartCoroutine (OnHideMenu());
	}

	void Start() {
		Show ();
	}

	public void OnGUI() {
		GUI.skin = skin;
		DrawMenu ();
	}

	public virtual IEnumerator OnShowMenu() { yield break; }
	public virtual IEnumerator OnHideMenu() { 
		Done ();
		yield break; 
	}
	public virtual void DrawMenu() { }

	public void Done() {
		Destroy (this);
	}
}
