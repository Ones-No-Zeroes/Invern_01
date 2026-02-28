using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class VisualController : Controller
{
    [Header("General Visual Controller Attributes")]
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private VisualEffectsManager visualEffectsManager;

    protected override void Update()
    {
        visualEffectsManager.SetValue(brightnessSlider.value);
        saveableData.brightnessAmount = brightnessSlider.value;
    }

    public override void ApplyLoadedValuesFromSavedData()
    {
        visualEffectsManager.SetValue(saveableData.brightnessAmount);
    }

}
