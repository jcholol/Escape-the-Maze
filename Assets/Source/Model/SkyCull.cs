using UnityEngine;

public class SkyCull : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Camera camera = GetComponent<Camera>();
        if (camera != null)
        {
            int skyLayer = 1 << LayerMask.NameToLayer("Sky");
            int floorLayer = 1 << LayerMask.NameToLayer("Floor");
            int combinedMask = skyLayer | floorLayer;
            camera.cullingMask &= ~combinedMask;
        }
    }
}
