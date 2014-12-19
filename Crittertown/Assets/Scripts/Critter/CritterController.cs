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
		EVENT,
		TALKING,
		GARDENING
	}

	public ConversationAlert activeConversationAlert;

	public void WanderToTarget(Vector3 target) {
		wanderTarget = target;
		currentState = States.WANDER;
	}

	public Critter critter = new Critter ("Horsel");

	public GameObject critterMesh;
	public Animator animator;
	#endregion


	#region Implementation
	public NavMeshAgent agent;
	Vector3 wanderTarget =  Vector3.zero;

	Alert lowFunAlert;
	Alert lowFatigueAlert;
	Alert lowHungerAlert;
	
	Consumable consumableTarget;

	Cactus myCactus;

	void Start() {
		critter.Save ();
		agent = GetComponent<NavMeshAgent> ();
		currentState = States.IDLE;
		critterMesh.renderer.material.color = critter.color;
		StartCoroutine (NeedsDecayLoop ());
		TargetNewConsumable (Game.GetClosestFood (transform.position));
	}

	public void OnApplicationQuit() {
		Save ();
	}
	
	public void OnApplicationPause(bool paused) {
		if (paused) {
			Save ();
		}
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

	IEnumerator GARDENING_EnterState() {
		myCactus = CactusGarden.GetMyCactus (critter.Id);
		if(myCactus == null) currentState = States.IDLE;
		agent.SetDestination (myCactus.transform.position);
		while (Vector3.Distance(transform.position, myCactus.transform.position) > 2.0f) {
			yield return null;
		}

		while (true) {
			yield return new WaitForSeconds(Random.Range(0.8f, 1.0f));
			//TODO: play gardening anim
			myCactus.spot.CactusQuality += 0.1f;
			if(Random.value < 0.1f) {
				currentState = States.IDLE;
				break;
			}
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
			if(Random.value < 0.2f) {
				if(ConversationManager.FindSomeoneToTalkTo(critter)) {
					currentState = States.TALKING;
				}
			} else {
				WanderToTarget(new Vector3(Random.Range (-10,10), 0, Random.Range(-10,10)));
			}
		}

		if (Random.value < 0.01f) {
			AttemptToFulfillNeed();
		}

		if (Random.value < 0.001f) {
			currentState = States.GARDENING;
		}

	}
	#endregion

	#region Debug
	override protected void Always_AfterUpdate() {
		animator.SetFloat ("Speed", agent.velocity.magnitude);

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


	void Save() {
		critter.Save ();
	}
}
