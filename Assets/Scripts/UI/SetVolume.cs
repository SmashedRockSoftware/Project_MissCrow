using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour {
    [SerializeField] Slider slider;

    public void SetLevel(float sliderValue) {
        AudioListener.volume = sliderValue;
        PlayerPrefs.SetFloat("Volume", sliderValue);
    }

    private void Start() {
        //if(slider is null) slider = gameObject.GetComponent<Slider>();
        float volume = PlayerPrefs.GetFloat("Volume", 0.4f);
        SetLevel(volume);
        if (!(slider is null)) slider.value = volume;
    }
}
