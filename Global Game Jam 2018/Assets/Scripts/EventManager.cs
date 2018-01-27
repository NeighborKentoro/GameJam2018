using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

	public delegate void SendFrequencyAction(float frequency);

	public static event SendFrequencyAction SendFrequencyEvent;


	public static void SendFrequency(float frequency) {
		SendFrequencyEvent(frequency);
	}
}
