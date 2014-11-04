using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Alert : Menu {

	public Transform target;
	public AudioClip clip;
	public int Position = 0;
	public AudioClip hideSound;

	public virtual void OnUpdate () {}

	public static Dictionary<Transform, List<Alert>> activeAlerts = new Dictionary<Transform, List<Alert>> ();

	public static Alert Attach<T>(Transform target) where T : Alert {
		UI.CreateEventSystem ();
		GameObject alert = (GameObject) Instantiate(Resources.Load<GameObject> (typeof(T).FullName));
		alert.name = typeof(T).FullName;
		alert.transform.position = target.position + Vector3.up * 1.6f;
		alert.transform.parent = target;

		Alert alertComponent = alert.GetComponent<Alert> ();
		if (alertComponent == null) {
			UI.ToastError("Alert prefab did not have an alert componenet. Shit is likely broken.");
			return null;
		}

		alertComponent.target = alert.transform.parent;
		Soundtrack.PlayOneShot(alertComponent.clip);

		if (!activeAlerts.ContainsKey (target)) {
			activeAlerts[target] = new List<Alert>();
		}

		activeAlerts [target].Add(alertComponent);
		UpdateOrderFor (target);

		return (T) alertComponent;
	}

	void Update() {
		transform.position = Vector3.Lerp (transform.position, target.position + Vector3.up * 1.6f + (Vector3.up * Position * 1.1f), 0.1f);
		OnUpdate ();
	}

	public static void UpdateOrderFor(Transform t) {
		activeAlerts[t].RemoveAll(item => item == null);
		for (int i = 0; i < activeAlerts[t].Count; i++) {
			activeAlerts[t][i].Position = i;
		}
	}

	public override void Hide () {
		Soundtrack.PlayOneShot (hideSound);
		activeAlerts [target].Remove (this);
		UpdateOrderFor (target);
		base.Hide ();
	}


}
