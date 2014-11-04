using UnityEngine;
using System.Collections;

public class SoundtrackDemo : TriggerObserver<SoundtrackTrigger> {
	void Start() {
		UI.Toast("Welcome to the soundtrack demo!");
		UI.Toast("Touch the orbs to begin");
	}

	override protected void TriggerWasActivated(Trigger trigger) {
		SoundtrackTrigger strigger = (SoundtrackTrigger) trigger;
		UI.ToastDebug("Soundtrack: " + strigger.action.ToString());

		switch(strigger.action) {
		case SoundtrackTrigger.Actions.PLAY:
			Soundtrack.Reset();
			break;

		case SoundtrackTrigger.Actions.NEXT:
			Soundtrack.Next();
			break;

		case SoundtrackTrigger.Actions.LOUD:
			Soundtrack.Loud();
			break;

		case SoundtrackTrigger.Actions.STOP:
			Soundtrack.Stop();
			break;

		case SoundtrackTrigger.Actions.QUIET:
			Soundtrack.Quiet();
			break;

		case SoundtrackTrigger.Actions.ONE_SHOT:
			Soundtrack.PlayOneShot(strigger.clip);
			break;

		case SoundtrackTrigger.Actions.PLAY_SPECIFIC:
			Soundtrack.PlaySpecific(strigger.clip);
			break;
		}
	}
}
