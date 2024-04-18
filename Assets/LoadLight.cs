using System.Collections.Generic;
using UnityEngine;

public class LoadLight : MonoBehaviour {
    public List<PointLight> Lights; // List of lights

    void Update()
    {
        for (int i = 0; i < Lights.Count; i++)
        {
            // Load each light in the list to the shader
            Lights[i].LoadLightToShader(i + 1);
        }
    }
}
