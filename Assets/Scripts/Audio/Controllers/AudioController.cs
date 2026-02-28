using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : Controller
{


    [Header("General Audio Controller Attributes")]
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;

    [Header("SFX Attributes")]
    [SerializeField] PlayerSFXManager playerSFXManager;

    [Header("Music Attributes")]
    [SerializeField] MusicManager musicManager;

    protected override void Update()
    {  
        playerSFXManager.SetValue(sfxVolumeSlider.value);
        musicManager.SetValue(musicVolumeSlider.value);
        
        // Assignment of values in the volSettingsPackage
        saveableData.sfxVolumeAmount = sfxVolumeSlider.value;
        saveableData.musicVolumeAmount = musicVolumeSlider.value;
    }

    public override void ApplyLoadedValuesFromSavedData()
    {
        playerSFXManager.SetValue(saveableData.sfxVolumeAmount);
        musicManager.SetValue(saveableData.musicVolumeAmount);

        sfxVolumeSlider.value = saveableData.sfxVolumeAmount;
        musicVolumeSlider.value = saveableData.musicVolumeAmount;
    }
}


