using UnityEngine;

public class TextureMover : MonoBehaviour
{
    public Vector2 Offset = Vector2.zero;
    public Vector2 Scale = Vector2.one;
    public float Rotation = 0f; // Rotation in degrees

    private bool isAnimationActive = true; // Variable to control the animation

    void Start()
    {
        Matrix3x3 staticMatrix = Matrix3x3Helpers.CreateTRS(Vector2.zero, 0f, Scale);
        PassStaticMatrixToShader(staticMatrix);
    }

    void Update()
    {
        // Toggle animation on spacebar press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isAnimationActive = !isAnimationActive;
        }

        if (isAnimationActive && gameObject.tag == "Sky")
        {
            Offset += new Vector2(.01f * Time.deltaTime, .01f * Time.deltaTime);
        }

        Matrix3x3 trsMatrix = Matrix3x3Helpers.CreateTRS(Offset, Rotation, Scale);
        PassMatrixToShader(trsMatrix);
    }

    void PassMatrixToShader(Matrix3x3 matrix)
    {
        Material mat = GetComponent<Renderer>().material;

        mat.SetFloat("_Matrix00", matrix.m00);
        mat.SetFloat("_Matrix01", matrix.m01);
        mat.SetFloat("_Matrix02", matrix.m02);
        mat.SetFloat("_Matrix10", matrix.m10);
        mat.SetFloat("_Matrix11", matrix.m11);
        mat.SetFloat("_Matrix12", matrix.m12);
        mat.SetFloat("_Matrix20", matrix.m20);
        mat.SetFloat("_Matrix21", matrix.m21);
        mat.SetFloat("_Matrix22", matrix.m22);
    }

    void PassStaticMatrixToShader(Matrix3x3 matrix)
    {
        Material mat = GetComponent<Renderer>().material;

        mat.SetFloat("_StaticMatrix00", matrix.m00);
        mat.SetFloat("_StaticMatrix01", matrix.m01);
        mat.SetFloat("_StaticMatrix02", matrix.m02);
        mat.SetFloat("_StaticMatrix10", matrix.m10);
        mat.SetFloat("_StaticMatrix11", matrix.m11);
        mat.SetFloat("_StaticMatrix12", matrix.m12);
        mat.SetFloat("_StaticMatrix20", matrix.m20);
        mat.SetFloat("_StaticMatrix21", matrix.m21);
        mat.SetFloat("_StaticMatrix22", matrix.m22);
    }
}
