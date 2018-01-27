using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

	public delegate void SendFrequencyAction(float frequency);
	public static event SendFrequencyAction SendFrequencyEvent;

	public delegate void ExitLevelAction();
	public static event ExitLevelAction ExitLevelEvent;

	public static void SendFrequency(float frequency) {
		SendFrequencyEvent(frequency);
	}

	public static void ExitLevel() {
		ExitLevelEvent();
	}
}
