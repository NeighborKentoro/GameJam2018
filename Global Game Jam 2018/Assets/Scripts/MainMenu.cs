using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour {

	private AudioSource selectSound;
	public AudioMixer mixer;
	public AudioMixerSnapshot[] snapshots;
	public float[] weights;

	// Use this for initialization
	void Start () {
		selectSound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnStartGame() {
		selectSound.Play();
		mixer.TransitionToSnapshots(snapshots, weights, 3);
		StartCoroutine(StartGame());
	}

	public void OnQuitGame() {
		Application.Quit();
	}

	IEnumerator StartGame() {
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(1);
	}
}
