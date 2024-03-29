﻿using UnityEngine;
using System.Collections;
using System;

public class TimeManager : MonoBehaviour {
	private const string TIME_MANAGER_KEY = "LAST_QUIT_TIME";

	void Start() {
		StartCoroutine (TimeTick ());
	}

	IEnumerator TimeTick() {
		while(true) {
			yield return new WaitForSeconds (5.0f);
			OnTimeTick ();
		}
	}

	void OnTimeTick() {
		Debug.Log ("DONE TIME TICK :)");
		CactusGarden.Instance.spots.ForEach (x => x.TheCruelAndInevitableDecayOfTime ());
	}

	void CalculateSpan() {
		if (PlayerPrefs.HasKey (TIME_MANAGER_KEY)) {
			DateTime prevTime = Convert.ToDateTime(PlayerPrefs.GetString(TIME_MANAGER_KEY, DateTime.Now.ToString()));
			DateTime now = DateTime.Now;
			
			TimeSpan span = now - prevTime;
			
			UI.ToastDebug(Mathf.FloorToInt((float) span.TotalHours) + " hours since last login. (" + Mathf.FloorToInt((float) span.TotalMinutes) + " minutes)");
		} else {
			UI.ToastDebug("First Run!");
		}
	}

	void SaveLogout() {
		PlayerPrefs.SetString (TIME_MANAGER_KEY, DateTime.Now.ToString ());

	}

	void OnApplicationQuit () {
		SaveLogout ();
	}

	void OnApplicationPause(bool pauseStatus) {
		if (pauseStatus) {
			SaveLogout ();
		} else {
			CalculateSpan();
		}
	}
}
