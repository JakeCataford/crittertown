using UnityEngine;
using System.Collections;

public class TriggerChecker : MonoBehaviour {
	public Trigger currentHover = null;

	void Update() {
		RaycastHit hit;
		if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 10f)) {
			Trigger t = hit.transform.gameObject.GetComponent<Trigger>();
			if( t != null && currentHover != t) {
				if(currentHover) {
					currentHover.OnHoverExit();
					UI.Unhighlight(currentHover.gameObject);
				}
				currentHover = t;
				UI.Highlight(t.gameObject, Palette.Blue);
				t.OnHoverEnter();
			} else if(currentHover != t) {
				if(currentHover) currentHover.OnHoverExit();
				if(currentHover) UI.Unhighlight(currentHover.gameObject);
				currentHover = null;
			}
		} else {
			if(currentHover) currentHover.OnHoverExit();
			if(currentHover) UI.Unhighlight(currentHover.gameObject);
			currentHover = null;
		}

		if (currentHover != null && Input.GetButtonDown ("Use")) {
			currentHover.Activate ();
		}
	}
}
