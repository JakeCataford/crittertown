using UnityEngine;
using System.Collections;

public abstract class TriggerObserver<T> : MonoBehaviour where T : Trigger {

	public T[] Triggers;

	void Awake () {
		foreach(T t in Triggers) {
			t.OnTriggeredEvent += TriggerWasActivated;
		}
	}

	protected abstract void TriggerWasActivated(Trigger trigger);
}
