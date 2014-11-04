using UnityEngine;
using System.Collections;

/***
 * Menu<T>: Base class for ingame menus.
 * The T Generic is simply a reference to the current type
 * 
 * For example, a PauseMenu class would be declared as
 * PauseMenu : Menu<PauseMenu>
 * 
 * Menu prefabs need to share the same name as the class, 
 * and be placed in a resources folder.
 **/

public abstract class Menu : MonoBehaviour {

	public virtual void Hide() {
		StartCoroutine (OnHideMenu());
	}

	void Start() {
		StartCoroutine (OnShowMenu());

	}

	public virtual IEnumerator OnShowMenu() { yield break; }
	public virtual IEnumerator OnHideMenu() { 
		Done ();
		yield break; 
	}

	public void Done() {
		Destroy (gameObject);
	}
	
	public static T Show<T>(bool hideActiveMenu = true) where T : Menu {
		if (hideActiveMenu && UI.ActiveMenu != null) {
			UI.ActiveMenu.Hide ();
		}

		UI.CreateEventSystem ();
		string MenuName = typeof(T).FullName;
		GameObject go = Resources.Load<GameObject>(MenuName);
		go = (GameObject) Instantiate (go);
		go.name = MenuName;

		if (hideActiveMenu) {
			UI.ActiveMenu = go.GetComponent<Menu> ();
		}

		return go.GetComponent<T> ();
	}
}
