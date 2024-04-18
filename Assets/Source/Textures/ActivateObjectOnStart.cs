using UnityEngine;

public class ActivateObjectOnStart : MonoBehaviour
{
    public GameObject objectToActivate;

    void Start()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
    }
}
