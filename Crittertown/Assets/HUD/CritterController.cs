﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public class CritterController : SVBLM.Core.FSM  {

	#region Interface
	public enum States {
		IDLE,
		WANDER,
		EVENT
	}

	public void WanderToTarget(Vector3 target) {
		wanderProgress = 0f;
		wanderStart = transform.position;
		wanderTarget = target;
		currentState = States.WANDER;
	}

	public Critter critter = new Critter ();
	#endregion


	#region Implementation
	NavMeshAgent agent;


	Vector3 wanderTarget =  Vector3.zero;
	Vector3 wanderStart = Vector3.zero;
	float wanderProgress = 0.0f;


	void Start() {
		agent = GetComponent<NavMeshAgent> ();
		currentState = States.IDLE;
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

	void IDLE_Update() {
		if (Random.value < 0.005f) {
			WanderToTarget(new Vector3(Random.Range (-10,10), 0, Random.Range(-10,10)));
		}
	}
	#endregion

	#region Debug
	void Always_AfterUpdate() {
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
}