using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class InventoryMenuItem : MonoBehaviour {
	public InventoryMenu Host;
	public ORM.Item item;
	public EventTrigger eventTrigger;
	public bool isSpawning = false;

	public void Start() {
		eventTrigger = gameObject.AddComponent<EventTrigger> ();
		eventTrigger.delegates = new System.Collections.Generic.List<EventTrigger.Entry> ();

		EventTrigger.Entry onPointerClickEntry = new EventTrigger.Entry ();
		onPointerClickEntry.eventID = EventTriggerType.PointerClick;
		onPointerClickEntry.callback.AddListener(new UnityEngine.Events.UnityAction<BaseEventData> (OnPointerClick));
		eventTrigger.delegates.Add (onPointerClickEntry);

		EventTrigger.Entry onDragEntry = new EventTrigger.Entry ();
		onDragEntry.eventID = EventTriggerType.Drag;
		onDragEntry.callback.AddListener(new UnityEngine.Events.UnityAction<BaseEventData> (OnDrag));		
		eventTrigger.delegates.Add (onDragEntry);
	}

	public void OnDrag(BaseEventData data) {
		if(!isSpawning) {
			isSpawning = true;
			GameObject go = item.SpawnInWorld ();
			Draggable d = go.GetComponent<Draggable> ();
			d.ForceDragStart ();
			Host.Hide ();
		}
	}

	public void OnPointerClick(BaseEventData data) {
		Host.selectedItem = item;
	}
}