using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class TheWorld : MonoBehaviour  {

    public SceneNode TheRoot;
    public GameObject RedLight;
    public GameObject BlueLight;
    public GameObject GreenLight;
    private PointLight redPL;
    private PointLight bluePL;
    private PointLight greenPL;

    public GameObject FinishWallMaze1;
    public GameObject FinishWallMaze2;

    public AudioSource bgm;
    public AudioSource escapeBGM;
    public AudioSource doorOpen;

    public FinishLine finishLine;

    bool escapeRequirements;
    bool escapeFlag;

    public GameObject finished;
    public GameObject menu;

    public GameObject instructions;
    public GameObject inputSettings;
    bool menuIsOpen = false;

    private void Start()
    {
        redPL = RedLight.GetComponent<PointLight>();
        bluePL = BlueLight.GetComponent<PointLight>();
        greenPL = GreenLight.GetComponent<PointLight>();
        escapeRequirements = false;
        escapeFlag = true;

    }
    private void Update()
    {
        Matrix4x4 i = Matrix4x4.identity;
        TheRoot.CompositeXform(ref i);
        if (Input.GetKeyDown(KeyCode.E))
        {
            TheRoot.GetComponent<Animator>().Play("ArmAnimation", -1, 0f);
        }

        if (escapeFlag)
        {
            checkRequirements();
        }

        if (escapeRequirements == true && escapeFlag == true)
        {
            doorOpen.Play();
            bgm.Stop();
            escapeBGM.Play();
            FinishWallMaze1.SetActive(false);
            FinishWallMaze2.SetActive(false);
            escapeFlag = false;
        }

        if (finishLine.GetFinished())
        {
            bgm.Stop();
            escapeBGM.Stop();
            finished.SetActive(true);
            TheRoot.GetComponent<FPSCameraController>().CursorStateNone();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menu.activeInHierarchy && !instructions.activeInHierarchy && !inputSettings.activeInHierarchy)
            {
                OpenMenu();
            }
            else if (instructions.activeInHierarchy)
            {
                CloseInstruction();
            } else if (inputSettings.activeInHierarchy)
            {
                CloseInput();
            } else if (menu.activeInHierarchy)
            {
                CloseMenu();
            }
        }
    }

    private void checkRequirements()
    {
        if (redPL.GetFar() == 10 && bluePL.GetFar() == 10 && greenPL.GetFar() == 10)
        {
            escapeRequirements = true;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("ScaryMazeGame");
    }

    public void OpenInstruction()
    {
        menu.SetActive(false);
        instructions.SetActive(true);
    }

    public void CloseInstruction()
    {
        instructions.SetActive(false);
        menu.SetActive(true);
    }

    public void OpenInput()
    {
        menu.SetActive(false);
        inputSettings.SetActive(true);
    }

    public void CloseInput()
    {
        inputSettings.SetActive(false);
        menu.SetActive(true);
    }

    public void CloseMenu()
    {
        Time.timeScale = 1;
        menu.SetActive(false);
        TheRoot.GetComponent<FPSCameraController>().SetMenuStatus(false);
    }

    public void OpenMenu()
    {
        menu.SetActive(true);
        TheRoot.GetComponent<FPSCameraController>().SetMenuStatus(true);
    }
}
