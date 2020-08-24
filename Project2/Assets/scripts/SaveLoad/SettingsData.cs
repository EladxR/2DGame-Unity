using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SettingsData 
{
    public float soundEffectVol;
    public bool soundEffectMute;
    

    public SettingsData(SettingsScript settings)
    {

        this.soundEffectVol = settings.soundEffectVol;
        this.soundEffectMute = settings.soundEffectMute;
    }
    
}
