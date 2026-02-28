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
    public void SetValue(float volume)
    {
        bool hasColAdjustment = colorAdjustmentsBrightness.profile.TryGet(out ColorAdjustments colAdjustment);
        if(hasColAdjustment)
        {
            colAdjustment.postExposure.value = volume;
        }
        else
        {
            Debug.Log("Doesn't have Color Adjustment... Needed.");
        }

    }
}
