using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class MarketMenuItem : MonoBehaviour {
	public MarketMenu Host;
	public ORM.Item item;
	public EventTrigger eventTrigger;
	
	public void Start() {
		eventTrigger = gameObject.AddComponent<EventTrigger> ();
		eventTrigger.delegates = new System.Collections.Generic.List<EventTrigger.Entry> ();
		
		EventTrigger.Entry onPointerClickEntry = new EventTrigger.Entry ();
		onPointerClickEntry.eventID = EventTriggerType.PointerClick;
		onPointerClickEntry.callback.AddListener(new UnityEngine.Events.UnityAction<BaseEventData> (OnPointerClick));
		eventTrigger.delegates.Add (onPointerClickEntry);
	}

	public void OnPointerClick(BaseEventData data) {
		Host.selectedItem = item;
	}
}