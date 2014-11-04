using UnityEngine;
using System.Collections;

public class SoundtrackTrigger : Trigger {
	public enum Actions {
		PLAY,
		NEXT,
		STOP,
		RESET,
		QUIET,
		LOUD,
		ONE_SHOT,
		PLAY_SPECIFIC
	}

	public AudioClip clip;
	public Actions action = Actions.PLAY;
}
