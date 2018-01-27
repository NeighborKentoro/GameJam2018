using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    public float currentFrequency;
    private float prevFrequency;

    public RectTransform barGlow;
    public Image display_ten;
    public Image display_one;
    public Image display_hundredth;
    public Image display_thousandth;

    public Sprite[] digitalInts;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        //check if frequence has changed
        if (prevFrequency != currentFrequency) {
            //it has! do the stuff! 21.455
            //change the size of the frequency bar
            float newHeight = Mathf.Floor(currentFrequency) * 21.455f;
            barGlow.sizeDelta = new Vector2(barGlow.sizeDelta.x, newHeight);

            //change the sprites to match the float value
            display_ten.sprite = digitalInts[Mathf.FloorToInt(currentFrequency * 0.1f)];
            display_one.sprite = digitalInts[Mathf.FloorToInt(currentFrequency % 10f)];
            display_hundredth.sprite = digitalInts[Mathf.FloorToInt(currentFrequency * 10f % 10f)];
            display_thousandth.sprite = digitalInts[Mathf.FloorToInt(currentFrequency * 100f % 10f)];
        }
        prevFrequency = currentFrequency;
    }

    void OnEnable() {
        EventManager.SendFrequencyEvent += SetFrequency;
    }

    void OnDisable() {
        EventManager.SendFrequencyEvent -= SetFrequency;
    }

    public void SetFrequency(float frequency) {
        currentFrequency = frequency;
    }
}
