using UnityEngine;
using System.Collections;

public class NodePrimitive : MonoBehaviour {
    public Color MyColor = new Color(0.1f, 0.1f, 0.2f, 1.0f);
    public Vector3 Pivot;
    public Vector3 RotationAxis = Vector3.up;
    public bool isRotating = false;
    public float rotationSpeed = 45.0f;
    public Transform ShowPivotPosition;

    private float currentAngle = 0.0f; 
    private bool rotatingForward = true; 

    void Update() {
    }
    public void LoadShaderMatrix(ref Matrix4x4 nodeMatrix) {
        Matrix4x4 p = Matrix4x4.TRS(Pivot, Quaternion.identity, Vector3.one);
        Matrix4x4 invp = Matrix4x4.TRS(-Pivot, Quaternion.identity, Vector3.one);
        Matrix4x4 trs = Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale);
        Matrix4x4 m = nodeMatrix * p * trs * invp;
        GetComponent<Renderer>().material.SetMatrix("MyTRSMatrix", m);
        GetComponent<Renderer>().material.SetColor("MyColor", MyColor);
    }
}
