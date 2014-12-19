using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class ConversationManager : SVBLM.Core.Singleton<ConversationManager> {

	//Stores actively conversing pairs of critters: Key is the speaker.
	public Dictionary<Critter, Critter> activeConversations = new Dictionary<Critter, Critter>();


	public static bool StartConversation(Critter initiator, Critter target) {
		if (IsCritterBusy (initiator) || IsCritterBusy (target)) return false;
		Instance.activeConversations.Add (initiator, target);
		Instance.StartCoroutine(Instance.TalkItOut(initiator, target));
		return true;
	}

	public static bool IsCritterBusy(Critter critterToCheck) {
		bool busy = false;

		foreach(KeyValuePair<Critter, Critter> kv in Instance.activeConversations) {
			if(kv.Key.Id == critterToCheck.Id || kv.Value.Id == critterToCheck.Id) busy = true;
		}

		return busy;
	}


	public IEnumerator TalkItOut(Critter critter, Critter other) {
		while(Vector3.Distance(critter.GetController().transform.position, other.GetController().transform.position) > 2.5f) {
			critter.GetController().agent.SetDestination(other.GetController().transform.position);
			yield return null;
		}

		critter.GetController ().currentState = CritterController.States.TALKING;
		other.GetController ().currentState = CritterController.States.TALKING;

		critter.GetController().agent.SetDestination(critter.GetController().transform.position);
		other.GetController().agent.SetDestination(other.GetController().transform.position);
		critter.GetController().transform.LookAt(other.GetController().transform.position);
		other.GetController().transform.LookAt(critter.GetController().transform.position);

		critter.GetController().activeConversationAlert = Alert.Attach<ConversationAlert> (critter.GetController().transform);

		bool conversationShouldContinue = true;
		bool partnerIsSpeaking = false;

		while (conversationShouldContinue) {
			yield return new WaitForSeconds(3.0f);
			partnerIsSpeaking = !partnerIsSpeaking;

			if(Random.value < 0.5f) {
				Alert.Attach<CharmUpAlert>((partnerIsSpeaking ? critter.GetController().transform : other.GetController().transform));
				if(partnerIsSpeaking) {
					other.AddCharmPoints(10);
				} else {
					critter.AddCharmPoints(10);
				}
			}

			if(partnerIsSpeaking) {
				if(critter.GetController().activeConversationAlert != null) critter.GetController().activeConversationAlert.Hide ();
				other.GetController().activeConversationAlert = Alert.Attach<ConversationAlert> (other.GetController().transform);
				other.GetController().animator.SetTrigger("Conversation");
			} else {
				if(other.GetController().activeConversationAlert != null) other.GetController().activeConversationAlert.Hide ();
				critter.GetController().activeConversationAlert = Alert.Attach<ConversationAlert> (critter.GetController ().transform);
				critter.GetController().animator.SetTrigger("Conversation");
			}

			if(Random.value <  0.1f) conversationShouldContinue = false;
		}

		other.GetController().activeConversationAlert.Hide ();
		critter.GetController().activeConversationAlert.Hide ();

		critter.GetController ().currentState = CritterController.States.IDLE;
		other.GetController ().currentState = CritterController.States.IDLE;

		activeConversations.Remove (critter);
	}

	public static bool FindSomeoneToTalkTo(Critter critter) {
		//logic based on relationships
		List<Critter> otherCritters = new List<Critter>();

		foreach(Critter c in Critter.All()) {
			otherCritters.Add(c);
		}

		if (otherCritters.Count < 1) {
			UI.ToastDebug("Not enough Critters to start conversation");
			return false;
		}

		otherCritters.Remove(critter);

		Critter otherCritter = otherCritters[Mathf.FloorToInt(Random.value * otherCritters.Count)];

		if(IsCritterBusy(critter)) {
			UI.Toast(critter.Name + " tried to start a conversation with " + otherCritter.Name + " but they were busy");
			return false;
		}

		return StartConversation (critter, otherCritter);
	}
}
