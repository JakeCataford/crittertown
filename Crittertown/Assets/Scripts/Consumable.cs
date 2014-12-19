using UnityEngine;
using System.Collections;
using System;

public abstract class Consumable : MonoBehaviour{
	public enum Type {
		FOOD,
		TOY,
		BED
	}

	public bool IsBeingUsed = false;
	public int Uses = 1;
	public float TimeRequiredPerUse = 3.0f;

	private Critter consumer;
	private Action callback;

	public abstract Type Cures { get; set; }

	void Start() {
		switch(Cures) {
		case Type.FOOD:
			Game.RegisterFood (this);
			break;
		case Type.BED:
			Game.RegisterBed (this);
			break;
		case Type.TOY:
			Game.RegisterToy (this);
			break;
		}
	}

	public void Use(Critter consumer, Action onDone) {
		this.consumer = consumer;
		callback = onDone;
		IsBeingUsed = true;
		StartCoroutine (UseLoop());
	}

	private IEnumerator UseLoop() {
		float time = 0;
		while (time < TimeRequiredPerUse) {
			time += Time.deltaTime;
			yield return null;
		}
		Uses --;

		if (Uses <= 0) {
			StartCoroutine(OnBecomeUnusable());
		}

		StartCoroutine (Effect (consumer));
		IsBeingUsed = false;
		callback ();
	}

	abstract public IEnumerator Effect (Critter critter);

	virtual public IEnumerator OnBecomeUnusable() {
		switch(Cures) {
		case Type.FOOD:
			Game.DeregisterFood (this);
			break;
		case Type.BED:
			Game.DeregisterBed (this);
			break;
		case Type.TOY:
			Game.DeregisterToy (this);
			break;
		}

		ORM.WorldItem item = Game.QueryWorldItemForTransform (transform);
		if (item != null) {
			item.Destroy();
		}

		Destroy (gameObject);
		yield break;
	}
}
