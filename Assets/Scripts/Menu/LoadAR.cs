using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vuforia;

public class LoadAR : MonoBehaviour
{

    public int idExperience;
    public string directory;
    private History history;
    private DB db;
    private GameObject ARPanel;
    private VideoManager videoManager;
    private UISprite iconPlay;

    void Start()
    {
        history = GameObject.FindGameObjectWithTag("Backend").GetComponent<History>();
        db = GameObject.FindGameObjectWithTag("Backend").GetComponent<DB>();
    }

    void OnClick()
    {
        //Load DB info
        Tema tema = db.getTemaDetail(idExperience);

        //Choose wich AR Panel to use
        if (directory.Contains("matematicas1"))
            ARPanel = history.getReference("AR_1");
        else if (directory.Contains("matematicas2"))
            ARPanel = history.getReference("AR_2");
        else if (directory.Contains("matematicas3"))
        {
            if (tema.note)
                ARPanel = history.getReference("AR_3");
            else
                ARPanel = history.getReference("AR_4");
        }

        if (tema != null)
        {
            //Hide Experiences and Navigation panels
            hideMenu();

            //Show AR GUI
            NGUITools.SetActive(ARPanel, true);

            //Get Component
            videoManager = ARPanel.transform.Find("BottomPanel").Find("VideoContainer").GetComponentInChildren<VideoManager>();

            //Set IconPlay
            iconPlay = ARPanel.transform.Find("BottomPanel").Find("VideoContainer").GetComponentInChildren<UISprite>();
            GameObject.FindGameObjectWithTag("VideoManager").GetComponent<MovieAudioController>().iconPlay = iconPlay;

            //Setting video
            videoManager.setVideo(directory, tema.video);

            //Setting animation
            videoManager.setAnimation(directory, tema.texto1, tema.texto2, tema.texto3, tema.escala, tema.tiempo1, tema.tiempo2, tema.note);

            //Setting buttons
            setButtons(tema.modelos);

            //Activating AR
			Camera.main.transform.parent.GetComponent<VuforiaBehaviour>().enabled = true;
        }
        else
            Debug.LogWarning("No theme detail result");
    }

    void hideMenu()
    {
        GameObject Themes_Experiences = history.getReference("Themes-Experiences");
        NGUITools.SetActive(Themes_Experiences, false);
        GameObject Navigation = history.getReference("Navigation");
        NGUITools.SetActive(Navigation, false);
    }

    void setButtons(List<Modelo> listModels)
    {
        Transform Buttons = ARPanel.transform.Find("BottomPanel").Find("Buttons");
        foreach (Modelo model in listModels)
        {
            GameObject button = Buttons.Find("rd_button" + model.num_btn).gameObject;
            LoadModel script = button.GetComponent<LoadModel>();
            script.model_name = model.archivo; //setting the model
            script.text = model.texto; //setting text that is showed
            script.megafiers = model.megafiers; //setting if the model is using megafier
            if (!string.IsNullOrEmpty(model.estatico))
                script.model_static_name = model.estatico; //setting static text
            if (model.btn_text.Contains(".png"))
                button.transform.Find("lbl_sprite").GetComponent<UISprite>().spriteName = model.btn_text.Remove((model.btn_text.Length - 4), 4);
            else
                button.GetComponentInChildren<UILabel>().text = model.btn_text; //setting the label text
            script.EnableButton(); //Enable button in case it was disabled
        }

        //Disable the buttons that are not set
        foreach (Transform button in Buttons)
            button.GetComponent<LoadModel>().DisableButton();
    }
}
