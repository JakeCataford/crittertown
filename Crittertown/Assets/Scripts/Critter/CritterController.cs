using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public class CritterController : SVBLM.Core.FSM  {

	#region Interface
	public enum States {
		IDLE,
		WANDER,
		SEEK_CONSUMABLE,
		CONSUMING,
		EVENT
	}

	public void WanderToTarget(Vector3 target) {
		wanderTarget = target;
		currentState = States.WANDER;
	}

	public Critter critter = new Critter ("Horsel");
	#endregion


	#region Implementation
	NavMeshAgent agent;
	Vector3 wanderTarget =  Vector3.zero;

	Alert lowFunAlert;
	Alert lowFatigueAlert;
	Alert lowHungerAlert;
	
	Consumable consumableTarget;

	void Start() {
		agent = GetComponent<NavMeshAgent> ();
		currentState = States.IDLE;
		renderer.material.color = critter.color;
		StartCoroutine (NeedsDecayLoop ());
		TargetNewConsumable (Game.GetClosestFood (transform.position));
	}

	IEnumerator WANDER_EnterState() {
		agent.destination = wanderTarget;
		yield break;
	}

	void WANDER_Update() {
		if (agent.remainingDistance < 0.01f) {
			currentState = States.IDLE;
		}
	}

	void SEEK_CONSUMABLE_Update() {
		if (consumableTarget == null || consumableTarget.IsBeingUsed || consumableTarget.Uses <= 0) {
			currentState = States.IDLE;
			agent.SetDestination(transform.position);
			return;
		}

		agent.SetDestination (consumableTarget.transform.position);

		Debug.DrawLine (transform.position, consumableTarget.transform.position);

		if (Vector3.Distance (consumableTarget.transform.position, transform.position) < 1.5f) {
			agent.SetDestination(transform.position);
			consumableTarget.Use (critter, OnFinishedConsuming);
			currentState = States.CONSUMING;
		}
	}

	void IDLE_Update() {
		if (Random.value < 0.005f) {
			WanderToTarget(new Vector3(Random.Range (-10,10), 0, Random.Range(-10,10)));
		}

		if (Random.value < 0.01f) {
			AttemptToFulfillNeed();
		}
	}
	#endregion

	#region Debug
	override protected void Always_AfterUpdate() {
		if(Input.GetKeyDown(KeyCode.Alpha1)) {
			critter.AddCharmPoints(10);
			UI.ToastDebug("Added 10 Charm!");
		}

		if(Input.GetKeyDown(KeyCode.Alpha2)) {
			critter.AddFitnessPoints(10);
			UI.ToastDebug("Added 10 Fitness!");
		}

		if(Input.GetKeyDown(KeyCode.Alpha3)) {
			critter.AddIntelligencePoints(10);
			UI.ToastDebug("Added 10 Intelligence!");
		}

		if(Input.GetKeyDown(KeyCode.Alpha4)) {
			critter.AddLoyaltyPoints(10);
			UI.ToastDebug("Added 10 Loyalty!");
		}
	}
	#endregion

	IEnumerator NeedsDecayLoop() {
		while (gameObject.activeSelf) {
			yield return new WaitForSeconds(Random.Range(10.0f, 15.0f));

			if(Random.value < 0.5f) {
				critter.DecreaseFatigue();
			} 

			if(Random.value < 0.5f) { 
				critter.DecreaseHunger();
			} 

			if(Random.value < 0.5f) {
				critter.DecreaseFun();
			}

			if(lowHungerAlert == null) {
				if(critter.Hunger < 0.4f) {
					lowHungerAlert = Alert.Attach<LowHungerAlert>(transform);
				}
			} else {
				if(critter.Hunger > 0.4f) {
					lowHungerAlert.Hide ();
				}
			}

			if(lowFatigueAlert == null) {
				if(critter.Fatigue < 0.4f) {
					lowFatigueAlert = Alert.Attach<LowFatigueAlert>(transform);
				}
			} else {
				if(critter.Fatigue > 0.4f) {
					lowFatigueAlert.Hide ();
				}
			}

			if(lowFunAlert == null) {
				if(critter.Fun < 0.4f) {
					lowFunAlert = Alert.Attach<LowFunAlert>(transform);
				}
			} else {
				if(critter.Fun > 0.4f) {
					lowFunAlert.Hide ();
				}
			}
		}
	}

	private void AttemptToFulfillNeed() {
		if(critter.Fun < 0.4f) {
			TargetNewConsumable (Game.GetClosestToy (transform.position));
		} else if (critter.Fatigue < 0.4f) {
			TargetNewConsumable (Game.GetClosestBed (transform.position));
		} else if (critter.Hunger < 0.4f) {
			TargetNewConsumable (Game.GetClosestFood (transform.position));
		}
	}

	private void TargetNewConsumable(Consumable consumable) {
		consumableTarget = consumable;
		currentState = States.SEEK_CONSUMABLE;
	}

	private void OnFinishedConsuming() {
		currentState = States.IDLE;
	}
}
