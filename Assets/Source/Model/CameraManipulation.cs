using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class CameraManipulation : MonoBehaviour {

    public Transform LookAtPosition = null;
    protected float CameraDistance = 15f;
    protected Vector3 LocalRotation;

    public Slider SliderX;
    public Slider SliderY;
    public Slider SliderZ;

    public TextMeshProUGUI TextX;
    public TextMeshProUGUI TextY;
    public TextMeshProUGUI TextZ;

    void Start() {
        SliderX.minValue = -30f;
        SliderX.maxValue = 30f;
        SliderY.minValue = -30f;
        SliderY.maxValue = 30f;
        SliderZ.minValue = -30f;
        SliderZ.maxValue = 30f;

        if (LookAtPosition != null) {
            SliderX.value = Mathf.Clamp(LookAtPosition.position.x, -30f, 30f);
            SliderY.value = Mathf.Clamp(LookAtPosition.position.y, -30f, 30f);
            SliderZ.value = Mathf.Clamp(LookAtPosition.position.z, -30f, 30f);

            UpdateTextValues();
        }

        //I didnt use parenting because the sliders dont work with panning, it was one or another, so I changed to the other approach.
        SliderX.onValueChanged.AddListener(delegate { UpdateLookAtPosition(); });
        SliderY.onValueChanged.AddListener(delegate { UpdateLookAtPosition(); });
        SliderZ.onValueChanged.AddListener(delegate { UpdateLookAtPosition(); });
    }

    void UpdateLookAtPosition() {
        if (LookAtPosition != null) {
            LookAtPosition.position = new Vector3(SliderX.value, SliderY.value, SliderZ.value);
            UpdateTextValues();
        }
    }
    //My text values wouldnt allow "X:" + SliderX.value.ToString("F2"); am i missing some braincells?
    void UpdateTextValues() {
        if (TextX != null && TextY != null && TextZ != null) {
            TextX.text = SliderX.value.ToString("F2"); 
            TextY.text = SliderY.value.ToString("F2");
            TextZ.text = SliderZ.value.ToString("F2");
        }
    }


    void Update () {
        //Bunch of math to avoid using fromtorotation here
        Vector3 currentDirection = LookAtPosition.localPosition - transform.localPosition;
        CameraDistance = currentDirection.magnitude;

        Vector3 V = currentDirection.normalized;
        Vector3 W = Vector3.Cross(-V, Vector3.up);
        Vector3 U = Vector3.Cross(W, -V);

        float angle = Mathf.Acos(Vector3.Dot(Vector3.up, U.normalized)) * Mathf.Rad2Deg;
        Vector3 axis = Vector3.Cross(Vector3.up, U).normalized;
        Quaternion rotation = Quaternion.AngleAxis(angle, axis);
        transform.localRotation = rotation;

        float alignAngle = Mathf.Acos(Vector3.Dot(transform.forward, V)) * Mathf.Rad2Deg;
        Vector3 alignAxis = Vector3.Cross(transform.forward, V).normalized;

        Quaternion alignU = Quaternion.AngleAxis(alignAngle, alignAxis);

        transform.localRotation = alignU * transform.localRotation;
        //Zooming, easiest of the three
        float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetKey(KeyCode.LeftAlt) && mouseScroll != 0f)
        {
            CameraDistance -= mouseScroll * 2f * (CameraDistance * 0.3f);
            CameraDistance = Mathf.Max(CameraDistance, 1f); //I could have used clamping here, but i wanted to see if I could do it manually
            //I guess a ternary operator or if statement would be most manual but whatever.
            transform.localPosition = LookAtPosition.localPosition - V * CameraDistance;
        }
        //Panning, move both camera and the look at position, took 2 tries to get right
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(1))
        {
            Camera camera = GetComponent<Camera>();
            Vector3 offset = camera.transform.right * -Input.GetAxis("Mouse X") + camera.transform.up * -Input.GetAxis("Mouse Y");
            LookAtPosition.position += offset * 3f;
            transform.position += offset * 3f;
        }
        //Uhh, this probably works, tumbling is kinda tricky to work in tandem with the sliders. I reused some code from other previous projects from this class.
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(0))
        {
            float deltaX = Input.GetAxis("Mouse X") * 4f;
            float deltaY = -Input.GetAxis("Mouse Y") * 4f;
            Vector3 angles = transform.eulerAngles;
            //This is like the wrapping from ealier projects
            angles.x = angles.x > 180 ? angles.x - 360 : angles.x; 
            //This should be allowed, this makes sure it doesnt flip
            float newX = Mathf.Clamp(angles.x + deltaY, -88f, 88f);
            //Had some issues with it resetting on click, this should fix it
            deltaY = newX - angles.x;
            Quaternion deltaRotation = Quaternion.Euler(deltaY, deltaX, 0);
            Quaternion targetRotation = transform.rotation * deltaRotation;

            Vector3 positionOffset = targetRotation * new Vector3(0, 0, -CameraDistance);

            transform.position = LookAtPosition.position + positionOffset;
            //Lookrotation simulation
            Vector3 forward = (LookAtPosition.position - transform.position).normalized;
            Vector3 right = Vector3.Cross(Vector3.up, forward).normalized;
            Vector3 up = Vector3.Cross(forward, right);
            Matrix4x4 m = new Matrix4x4();
            m.SetColumn(0, right);
            m.SetColumn(1, up);
            m.SetColumn(2, forward);
            m.SetColumn(3, new Vector4(0, 0, 0, 1));
            Quaternion manualRotation = m.rotation;

            transform.rotation = manualRotation;
        }

    }

}
