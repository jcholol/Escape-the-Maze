using UnityEngine;

public class PointLight : MonoBehaviour {
    public float Near = 5.0f;
    public float Far = 10.0f;
    public Color LightColor = Color.white;
    private bool spotlightActive = true; // Initial state of the spotlight

    // Spotlight properties
    public bool IsSpotlight = false;
    public Vector3 SpotlightDirection = Vector3.forward;
    public float SpotlightAngle = 60.0f;

    void Update() {
        // Toggle spotlight on and off with the F key
        if (Input.GetKeyDown(KeyCode.F)) {
            spotlightActive = !spotlightActive;
            Shader.SetGlobalInt("_SpotlightActive3", spotlightActive ? 1 : 0); // Set the spotlight active state as an integer
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Entered colision with: " + collision.gameObject.name);

        if (collision.gameObject.name == "FrontOrg")
        {
            SetFar(10f);
            GetComponent<AudioSource>().Play();
        }
    }

    public void SetFar(float v)
    {
        Far = v;
    }

    public float GetFar()
    {
        return Far;
    }

    public void LoadLightToShader(int lightIndex)
    {
        string lightPositionVar = $"LightPosition{lightIndex}";
        string lightColorVar = $"LightColor{lightIndex}";
        string lightNearVar = $"LightNear{lightIndex}";
        string lightFarVar = $"LightFar{lightIndex}";

        Vector3 lightPosition = transform.localPosition;

        // Move spotlight back 3 units if it's the active spotlight at index 3
        if (IsSpotlight && lightIndex == 3 && spotlightActive) {
            lightPosition -= 3.0f * transform.forward; // Move back by 3 units in local forward direction
        }

        Shader.SetGlobalVector(lightPositionVar, lightPosition);
        Shader.SetGlobalColor(lightColorVar, LightColor);
        Shader.SetGlobalFloat(lightNearVar, Near);
        Shader.SetGlobalFloat(lightFarVar, Far);

        // Spotlight properties
        if (IsSpotlight && lightIndex == 3) {
            Shader.SetGlobalVector($"LightDirection{lightIndex}", transform.TransformDirection(SpotlightDirection.normalized));
            Shader.SetGlobalFloat($"LightSpotAngle{lightIndex}", SpotlightAngle);
            Shader.SetGlobalInt("_SpotlightActive3", spotlightActive ? 1 : 0); // Set the spotlight active state as an integer
        }
    }
}
