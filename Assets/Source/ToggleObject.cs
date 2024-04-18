using UnityEngine;

public class ToggleObject : MonoBehaviour
{
    // Reference to the child object you want to toggle
    public GameObject childObject;

    void Update()
    {
        // Check if the 'F' key was pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Ensure the childObject is assigned
            if (childObject != null)
            {
                // Toggle the active state of the child object
                childObject.SetActive(!childObject.activeSelf);
            }
        }
    }
}
