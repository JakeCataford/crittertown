using UnityEngine;
using System.Collections;

public class Draggable : MonoBehaviour {
	public bool IsBeingDragged = false;

	private Vector3 LastPosition = Vector3.zero;

	void Start() {
		gameObject.layer = LayerMask.NameToLayer ("Draggables");
	}

	void Update() {

		if (Input.touchCount > 0) {
			foreach(Touch touch in Input.touches) {
				if(!IsBeingDragged && touch.phase == TouchPhase.Began) {
					RaycastHit hit;
					if(Physics.SphereCast(Camera.main.ScreenPointToRay((Vector3) touch.position + Vector3.forward), 1f, out hit, 100f, (1 << LayerMask.NameToLayer ("Draggables")))) {
						Debug.DrawLine(Vector3.up * 10f, hit.point);
						if(hit.collider.gameObject == collider.gameObject) {
							IsBeingDragged = true;
							//UI.Highlight(gameObject, Palette.Blue);
							rigidbody.useGravity = false;
							Game.StartDrag();
						}
					}
				} else if (IsBeingDragged && touch.phase == TouchPhase.Ended) {
					IsBeingDragged = false;
					rigidbody.useGravity = true;
					rigidbody.velocity = (transform.position - LastPosition) * 50f;
					//UI.Unhighlight(gameObject);
					Game.EndDrag();
				} else if (IsBeingDragged && (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)) {
					LastPosition = transform.position;
					RaycastHit hit;
					if(Physics.Raycast(Camera.main.ScreenPointToRay(((Vector3) (touch.position + (Vector2.up * 20f))) + Vector3.forward), out hit, 100f)) {
						rigidbody.MovePosition(Vector3.Lerp(transform.position, hit.point + Vector3.up * 1f, 0.1f));
					}
				}
			}
		}
	}

	public void ForceDragStart() {
		IsBeingDragged = true;
		rigidbody.useGravity = false;
		Game.StartDrag();
	}
}
