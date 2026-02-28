using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VisualEffectsManager : MonoBehaviour, IValueAdjustable
{
    [Header("Visual Effects Manager Attributes")]
    [SerializeField] private Volume colorAdjustmentsBrightness;

    // Private fields
    private ColorAdjustments colAdjustment;

    /// <summary>
    /// Sets the value of the postExposure as to bring up or down the brightness
    /// </summary>
    /// <param name="volume"></param>
    public void SetValue(float brightnessAmount)
    {
        bool hasColAdjustment = colorAdjustmentsBrightness.profile.TryGet(out ColorAdjustments colAdjustment);
        if(hasColAdjustment)
        {
            colAdjustment.postExposure.value = brightnessAmount;
        }
        else
        {
            Debug.Log("Doesn't have Color Adjustment... Needed.");
        }

    }
}
