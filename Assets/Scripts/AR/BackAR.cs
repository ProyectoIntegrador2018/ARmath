using UnityEngine;
using System.Collections;
using Vuforia;

public class BackAR : MonoBehaviour {

    public GameObject ARPanel;
    public GameObject ARButtons;
    public GameObject Themes_Experiences;
    public GameObject Navigation;
    public ModelManager modelManager;
    public VuforiaBehaviour ARCamera;
    public VideoManager videoManager;

    void OnClick()
    {
        //Stop Video
        videoManager.Stop();

        //Reset Animation
        videoManager.resetAnimation();

        //Stop AR
        ARCamera.enabled = false;

        //Deletes models and hide text
        modelManager.Reset();

        //Reset buttons
        if (UIToggle.GetActiveToggle(2) != null)
        {
            UIToggle.GetActiveToggle(2).optionCanBeNone = true;
            UIToggle.GetActiveToggle(2).value = false;
        }
        //Reset the name of the models
        LoadModel[] scriptLoadModel = ARButtons.GetComponentsInChildren<LoadModel>();
        foreach (LoadModel script in scriptLoadModel)
            script.model_name = "";

        //Hide and show the panels
        NGUITools.SetActive(ARPanel, false);
        NGUITools.SetActive(Themes_Experiences, true);
        NGUITools.SetActive(Navigation, true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OnClick();
    }
}
