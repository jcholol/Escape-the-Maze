using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SceneNodeControl : MonoBehaviour
{
}
/*
    public TMP_Dropdown TheMenu = null;
    public SceneNode TheRoot = null;
    public XfromControl XformControl = null;
    public GameObject AxisFramePrefab; 
    private GameObject currentAxisFrame; 
    private LineRenderer viewDirectionLine;
    public GameObject SmallViewCameraPrefab; 
    private GameObject currentSmallViewCamera;
    public SceneNode petalNode1;
    const string kChildSpace = " ";
    List<TMP_Dropdown.OptionData> mSelectMenuOptions = null;
    List<Transform> mSelectedTransform = new List<Transform>();

void Start()
{
    mSelectMenuOptions = new List<TMP_Dropdown.OptionData>();
    mSelectMenuOptions.Add(new TMP_Dropdown.OptionData(TheRoot.transform.name));
    mSelectedTransform.Add(TheRoot.transform);
    GetChildrenNames("", TheRoot);
    TheMenu.AddOptions(mSelectMenuOptions);
    TheMenu.onValueChanged.AddListener(SelectionChange);
    XformControl.SetSelectedObject(TheRoot.transform);
    int petalNode1Index = mSelectedTransform.IndexOf(petalNode1.transform);
    TheMenu.value = petalNode1Index; 
    SelectionChange(petalNode1Index); 
    SetupSmallViewCamera(); //This was cutting off the rest of the start function only in build mode, idk why, in edit mode it worked fine, I put it at the end
    //Just to be sure hopefully no unseen bugs happen.

}

//Not mine, yours
    void GetChildrenNames(string blanks, SceneNode node) {
        string space = blanks + kChildSpace;
        List<SceneNode> children = node.ChildrenList;
        for (int i = children.Count - 1; i >= 0; i--) {
            SceneNode cn = children[i];
            NodePrimitive primitiveComponent = cn.GetComponent<NodePrimitive>();
            TMP_Dropdown.OptionData d = new TMP_Dropdown.OptionData(space + cn.transform.name);
            mSelectMenuOptions.Add(d);
            mSelectedTransform.Add(cn.transform);
            GetChildrenNames(space, cn);
        }
    }

    //Probably should have made a function for lookrotation instead of copy paste so often, anyway handles selection changes
    void SelectionChange(int index)
    {
        XformControl.SetSelectedObject(mSelectedTransform[index]);
        Transform selectedTransform = mSelectedTransform[index];
        SceneNode selectedNode = selectedTransform.GetComponent<SceneNode>();
        if (selectedNode != null)
        {
            Matrix4x4 combinedMatrix = selectedNode.mCombinedParentXform;
            Vector3 position = combinedMatrix.MultiplyPoint(Vector3.zero);
            Vector3 scale = new Vector3(
                combinedMatrix.GetColumn(0).magnitude,
                combinedMatrix.GetColumn(1).magnitude,
                combinedMatrix.GetColumn(2).magnitude
            );
            Vector3 forward = combinedMatrix.GetColumn(2).normalized;
            Vector3 up = combinedMatrix.GetColumn(1).normalized;
            Vector3 right = Vector3.Cross(up, forward).normalized;
            up = Vector3.Cross(forward, right).normalized;
            Matrix4x4 rotationMatrix = new Matrix4x4();
            rotationMatrix.SetColumn(0, right);
            rotationMatrix.SetColumn(1, up);
            rotationMatrix.SetColumn(2, forward);
            rotationMatrix.SetColumn(3, new Vector4(0, 0, 0, 1)); 
            Quaternion rotation = rotationMatrix.rotation;
            InstantiateOrMoveAxisFrame(position, rotation, scale);
        }
    }
*/
    /*Uhh this was working in edit mode but not build mode, 😨 i think i fixed it, but i am a bit spooked*/
/*    
    private void SetupSmallViewCamera()
    {
         if (currentSmallViewCamera == null)
            {
                currentSmallViewCamera = Instantiate(SmallViewCameraPrefab);
                viewDirectionLine = currentSmallViewCamera.AddComponent<LineRenderer>();
                viewDirectionLine.startWidth = .5f;
                viewDirectionLine.endWidth = .5f;
                viewDirectionLine.material = new Material(Shader.Find("Unlit/Color"));
                viewDirectionLine.material.color = Color.magenta;
            }   
    }
    //From mp5 apparently I had two methods doing the exact same thing lol, so i removed one.
    private void InstantiateOrMoveAxisFrame(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        if (AxisFramePrefab != null)
        {
            if (currentAxisFrame == null)
            {
                currentAxisFrame = Instantiate(AxisFramePrefab);
            }

            currentAxisFrame.transform.localPosition = position;
            currentAxisFrame.transform.localRotation = rotation;
            currentAxisFrame.transform.localScale = 3f*scale;
        }
    }
    //Camera is set up here too, it just copies the position and rotation of axisframe
    private void Update()
    {
        if (mSelectedTransform.Count > 0 && AxisFramePrefab != null)
        {
            Transform selectedTransform = mSelectedTransform[TheMenu.value];
            SceneNode selectedNode = selectedTransform.GetComponent<SceneNode>();

            if (selectedNode != null)
            {
                Matrix4x4 combinedMatrix = selectedNode.mCombinedParentXform;

                Vector3 position = combinedMatrix.MultiplyPoint(Vector3.zero);
                Vector3 scale = new Vector3(
                    combinedMatrix.GetColumn(0).magnitude,
                    combinedMatrix.GetColumn(1).magnitude,
                    combinedMatrix.GetColumn(2).magnitude
                );
                //Lookrotation math.
                Vector3 forward = combinedMatrix.GetColumn(2).normalized;
                Vector3 up = combinedMatrix.GetColumn(1).normalized;
                Vector3 right = Vector3.Cross(up, forward).normalized;
                up = Vector3.Cross(forward, right).normalized;
                Matrix4x4 rotMatrix = new Matrix4x4();
                rotMatrix.SetColumn(0, right);
                rotMatrix.SetColumn(1, up);
                rotMatrix.SetColumn(2, forward);
                rotMatrix.SetColumn(3, new Vector4(0, 0, 0, 1)); 
                Quaternion rotation = rotMatrix.rotation;
                InstantiateOrMoveAxisFrame(position, rotation, scale);
                currentSmallViewCamera.transform.position = position;
                currentSmallViewCamera.transform.rotation = rotation;
                currentSmallViewCamera.GetComponent<Camera>().cullingMask &= ~(1 << 6); //Hide the axis frame here, wasn't working on instantiation, so i just call this every frame, kinda bad but whatever.
                UpdateViewDirectionLine(combinedMatrix);
            }
        }
    }
        //Line is a bit fat, hope that is ok
        private void UpdateViewDirectionLine(Matrix4x4 combinedMatrix)
        {
            Vector3 position = combinedMatrix.MultiplyPoint(Vector3.zero);
            Vector3 direction = combinedMatrix.GetColumn(2);

            Vector3 endPoint = position + direction * 500; 

            Vector3[] linePoints = new Vector3[2];
            linePoints[0] = position; 
            linePoints[1] = endPoint;

            viewDirectionLine.SetPositions(linePoints);  //I think this is allowed, otherwise, this function straightforward
        }

}
*/
