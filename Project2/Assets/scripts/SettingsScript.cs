using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    public float soundEffectVol = 1f;
    public bool soundEffectMute = false;
    public Slider soundEffectSlider;
    public Toggle soundEffectToggle;
    // Start is called before the first frame update
    void Start()
    {
        SettingsData data = SaveSystem.LoadSettings();
        if (data != null)
        {
            soundEffectVol = data.soundEffectVol;
            soundEffectMute = data.soundEffectMute;
        }
        soundEffectSlider.value = soundEffectVol;
        soundEffectToggle.isOn = soundEffectMute;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Back()
    {
        SaveSystem.SaveSettings(this);
        SceneManager.LoadScene("Main");
    }

    public void SoundEffectChange(float val)
    {
        if (!soundEffectMute)
        {
            soundEffectVol = val;
        }

    }
    public void SoundEffectMute(bool isMute)
    {
        soundEffectMute = isMute;
        if (isMute)
        {
            soundEffectSlider.enabled = false;
            soundEffectSlider.value = 0;
        }
        else
        {
            soundEffectSlider.enabled = true;
            soundEffectSlider.value = soundEffectVol;
        }
    }

    internal void Save()
    {
        SaveSystem.SaveSettings(this);
    }
}
