using System;
using System.Collections;
using UnityEditor.U2D;
using UnityEngine;

public class SkyboxShaderColorTransitor : MonoBehaviour {
    [SerializeField] private Material skyBoxMaterial;

    [SerializeField] private Vector3 upVector;
    [SerializeField] private float intensity = 1;
    [SerializeField] private float pitch;
    [SerializeField] private float yaw;
    [SerializeField] private Color color1;
    [SerializeField] private Color color2;
    [SerializeField] private float exponent = 1;

    private float speed = 1f;
    public LeanTweenType exponentTweenType;
    public bool pingPongExponent;
    [SerializeField] private float maxExponent = 20f;
    [SerializeField] private float minExponent= 0f;
    [SerializeField] private float exponentTime = 3f;

    private bool tweenFired;

    void Start () {
        skyBoxMaterial = new Material (Shader.Find ("Skybox/Gradient Skybox"));
        RenderSettings.skybox = skyBoxMaterial;
    }

    void Update ()
    {
        if (pingPongExponent && !tweenFired)
        {
            LeanTween.value(gameObject, updateExponentCallback, exponent, maxExponent, exponentTime)
                .setEase(exponentTweenType).setOnComplete(() => pingPongExponent = false);
            tweenFired = true;
        }

        if (exponent == maxExponent)
        {
            LeanTween.value(gameObject, updateExponentCallback, exponent, minExponent, exponentTime).setEase(exponentTweenType);
        }
        SetExponent ();
        SetIntensity ();
        SetColors ();
        SetUpVector ();
    }

    private void updateExponentCallback(float val)
    {
        exponent = val;
    }

    private void SetIntensity () {
        if (skyBoxMaterial.HasProperty ("_Intensity"))
            skyBoxMaterial.SetFloat ("_Intensity", intensity);
    }

    private void SetExponent () {
        if (skyBoxMaterial.HasProperty ("_Exponent"))
            skyBoxMaterial.SetFloat ("_Exponent", exponent);
    }

    private void SetColors () {
        if (skyBoxMaterial.HasProperty ("_Color1"))
            skyBoxMaterial.SetColor ("_Color1", color1);
        if (skyBoxMaterial.HasProperty ("_Color2"))
            skyBoxMaterial.SetColor ("_Color2", color2);
    }

    private void SetUpVector () {

        upVector = CalculateUpVector ();
        if (skyBoxMaterial.HasProperty ("_UpVector")) 
            skyBoxMaterial.SetVector ("_UpVector", upVector);
        
    }

    private Vector3 CalculateUpVector () {
        var rp = pitch * Mathf.Deg2Rad;
        var ry = yaw * Mathf.Deg2Rad;

        var upVector = new Vector4 (
            Mathf.Sin (rp) * Mathf.Sin (ry),
            Mathf.Cos (rp),
            Mathf.Sin (rp) * Mathf.Cos (ry),
            0.0f
        );
        return upVector;
    }
}