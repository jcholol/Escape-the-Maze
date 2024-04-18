using UnityEngine;

public class MazeCull2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Camera camera = GetComponent<Camera>();
        if (camera != null)
        {
            int skyLayer = 1 << LayerMask.NameToLayer("MazeTopDown");
            int floorLayer = 1 << LayerMask.NameToLayer("Floor");
            int combinedMask = skyLayer | floorLayer;
            camera.cullingMask &= ~combinedMask;
        }
    }
}
