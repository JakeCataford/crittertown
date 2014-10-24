using UnityEngine;
using System.Collections;

public abstract class Trigger : MonoBehaviour {
	public delegate void OnTriggeredHandler(Trigger trigger);
	public event OnTriggeredHandler OnTriggeredEvent;

	public void Activate() {
		OnTriggered();
		if(OnTriggeredEvent != null) OnTriggeredEvent(this);
	}

	public virtual void OnHoverEnter() {}
	public virtual void OnHoverExit()  {}
	public virtual void OnTriggered()  {}
}
