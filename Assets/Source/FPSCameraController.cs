using UnityEngine;

public class FPSCameraController : MonoBehaviour
{
    public float speed = 5.0f;
    public float sensitivity = 2.0f;
    public GameObject bobbingObject; // The GameObject that will bob
    public float bobbingAmount = 0.05f; // Amount of bobbing
    public float bobbingSpeed = 10.0f; // Speed of bobbing

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private bool menuIsOn = true;
    private CharacterController characterController;
    private float originalY; // Original Y position of the bobbing object
    private float timer = 0.0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("FPSCameraController script requires a CharacterController component.");
            return;
        }

        if (bobbingObject == null)
        {
            Debug.LogError("No GameObject set to bob.");
            return;
        }

        originalY = bobbingObject.transform.localPosition.y;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        if (menuIsOn)
        {
            CursorStateNone();
            Time.timeScale = 0;
            return;
        } else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }


        // Camera control
        yaw += sensitivity * Input.GetAxis("Mouse X");
        pitch -= sensitivity * Input.GetAxis("Mouse Y");
        pitch = Mathf.Clamp(pitch, -90f, 90f);
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        // Movement
        float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        Vector3 move = transform.right * x + transform.forward * z;
        move.y = 0;

        characterController.Move(move);

        // Bobbing effect
        if (Mathf.Abs(z) > 0) // Check if moving forward or backward
        {
            timer += Time.deltaTime * bobbingSpeed;
            float bobbingY = originalY + Mathf.Sin(timer) * bobbingAmount;
            Vector3 bobbingPos = bobbingObject.transform.localPosition;
            bobbingPos.y = bobbingY;
            bobbingObject.transform.localPosition = bobbingPos;
        }
        else
        {
            timer = 0;
            Vector3 bobbingPos = bobbingObject.transform.localPosition;
            bobbingPos.y = originalY;
            bobbingObject.transform.localPosition = bobbingPos;
        }
    }

    public void CursorStateNone()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void SetMenuStatus(bool v)
    {
        menuIsOn = v;
    }
}
