using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightHack : MonoBehaviour {
    public float Near = 5.0f;
    public float Far = 10.0f;
    public float Angle = 45.0f; // Spotlight's angle
    public Color LightColor = Color.white;

    public bool ShowLightRanges = false;

    // Use this for initialization
    void Start () {

    }
    
    // Update is called once per frame
    void Update () {
        GetComponent<Renderer>().material.color = LightColor;

        UpdateLightDirectionAndAngle();
    }

    void UpdateLightDirectionAndAngle() {
        Vector3 lightDirection = transform.forward; // Spotlight direction
        Shader.SetGlobalVector("LightDirection", lightDirection);
        Shader.SetGlobalFloat("LightAngle", Angle);
    }

    public void LoadLightToShader()
    {
        Shader.SetGlobalVector("LightPosition", transform.localPosition);
        Shader.SetGlobalColor("LightColor", LightColor);
        Shader.SetGlobalFloat("LightNear", Near);
        Shader.SetGlobalFloat("LightFar", Far);

        UpdateLightDirectionAndAngle(); // Also load direction and angle
    }
}
