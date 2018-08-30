using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;

public class LoadExperiences : MonoBehaviour {

    public int idThemeType;
    public string directory;
    public Transform prefabExperience;
    public UILabel label;
    public UITexture texture;

    private DB db;
    private History history;
    public Transform grid;
    private GameObject Themes;
    private GameObject Experiences;
    private TweenPosition tweenThemes;
    private UITexture imageExperience;

    void Start()
    {
        db = GameObject.FindGameObjectWithTag("Backend").GetComponent<DB>(); //Reference to DB
        history = GameObject.FindGameObjectWithTag("Backend").GetComponent<History>(); //Reference to history
        Themes = history.getReference("Themes");
        Experiences = history.getReference("Experiences");
        tweenThemes = Themes.GetComponent<TweenPosition>();
    }

    void OnClick()
    {
        history.getReference("Navigation").GetComponentInChildren<BackButton>().experienceActivated = true;
        GetComponent<UIToggle>().optionCanBeNone = false;
        TweenPosition tweenExperiences = null;

        //If Panel is not activated
        if (!Experiences.activeSelf)
        {
            NGUITools.SetActive(Experiences, true); //Activating experiences panel
            tweenExperiences = Experiences.GetComponent<TweenPosition>();            
        }

        grid = Experiences.transform.Find("ExperienceButtons").GetChild(0).GetChild(0); //Locate Grid

        //Despawn previus buttons, if has any.
        foreach (Transform child in grid)
        {
            if (child.gameObject.activeSelf)
                PoolManager.Pools["Buttons"].Despawn(child);
        }

        //Loading image
        imageExperience = Experiences.transform.Find("ExpImage").GetComponentInChildren<UITexture>();
        Imagen imagen = db.getImagen(idThemeType);
        imageExperience.mainTexture = Resources.Load<Texture>("Images/" + directory + imagen.archivo);
        imageExperience.MakePixelPerfect();
        
        //Loading subtitle
        Experiences.transform.Find("ExpImage").GetComponentInChildren<UILabel>().text = db.getSubtitulo(idThemeType);

        //Loading buttons
        List<Resultado> experiencesList = db.getTemas(idThemeType);
        int i = 1;
        foreach (Resultado experience in experiencesList)
        {
            Transform button = PoolManager.Pools["Buttons"].Spawn(prefabExperience, grid);
            button.name = i + "_Experience";
            LoadAR script = button.GetComponent<LoadAR>();
            script.idExperience = experience.id_resultado;
            script.directory = directory;

            if (experience.nombre.Contains(".png"))
            {
                NGUITools.SetActive(button.GetComponentsInChildren<UILabel>(true)[0].gameObject, false);
                UITexture texture = button.GetComponentsInChildren<UITexture>(true)[0];
                if (!texture.gameObject.activeSelf) NGUITools.SetActive(texture.gameObject, true);

                texture.mainTexture = Resources.Load<Texture>("Images/" + directory + experience.nombre.Remove(experience.nombre.Length - 4));
                texture.MakePixelPerfect();
                texture.width = (int)(texture.width * experience.escala);
                texture.height = (int)(texture.height * experience.escala);
                texture.MarkAsChanged();
            }
            else
            {
                NGUITools.SetActive(button.GetComponentsInChildren<UITexture>(true)[0].gameObject, false);
                UILabel label = button.GetComponentsInChildren<UILabel>(true)[0];
                if (!label.gameObject.activeSelf) NGUITools.SetActive(label.gameObject, true);
                label.text = experience.nombre;
            }
            i++;
        }
        grid.GetComponent<UIGrid>().Reposition();

        //Play tweens
        if (tweenExperiences)
        {
            //Setting tween of themes
            tweenThemes.from = Vector3.zero;
            tweenThemes.to = Vector3.left * 200;

            Vector2 size = history.getScreenSize();
            Experiences.transform.localPosition = new Vector3(size.x / 2, 0, 0);
            Vector3 pos = Experiences.transform.localPosition;
            tweenExperiences.from = pos;
            tweenExperiences.to = new Vector3(pos.x - 400.0f, pos.y, pos.z);

            tweenThemes.PlayForward();
            tweenExperiences.PlayForward();
        }
    }
}
