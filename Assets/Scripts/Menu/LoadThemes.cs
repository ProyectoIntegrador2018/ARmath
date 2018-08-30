using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;

public class LoadThemes : MonoBehaviour {

    public int idSubject;
    public string directory;
    
    [SerializeField]
    private Transform prefabTheme;

    private UILabel navLabel;
    private UIGrid grid;
    private DB db;
    private History history;

	// Use this for initialization
	void Start () {
        db = GameObject.FindGameObjectWithTag("Backend").GetComponent<DB>();
        history = GameObject.FindGameObjectWithTag("Backend").GetComponent<History>();
	}

    void OnClick()
    {
        //Get the subjects of the button
        List<Resultado> typeSubjects = db.getTemasTipo(idSubject);

        if (typeSubjects.Count > 0)
        {
            //Change Background
            history.changeBackground(0);

            //Setting text on the navigation label
            navLabel = history.getReference("Navigation").GetComponentInChildren<UILabel>();
            navLabel.text = transform.GetComponentInChildren<UILabel>().text;

            //Activating Themes Menu
            GameObject Themes = history.getReference("Themes-Experiences");
            NGUITools.SetActive(Themes, true);
            grid = Themes.transform.Find("Themes").Find("ScrollView").Find("grid").GetComponentInChildren<UIGrid>();

            //Despawn all childs on grid, if has any
            foreach (Transform child in grid.transform)
            {
                if (child.gameObject.activeSelf)
                    PoolManager.Pools["Buttons"].Despawn(child);
            }

            //Spawn all the new subjects
            int i = 1;
            foreach (Resultado subject in typeSubjects)
            {
                Transform button = PoolManager.Pools["Buttons"].Spawn(prefabTheme, grid.transform);
                button.name = i + "_btn_theme";
                LoadExperiences script_experiences = button.GetComponent<LoadExperiences>();
                script_experiences.idThemeType = subject.id_resultado;
                script_experiences.directory = directory + "/" + subject.directory + "/";

                //Setting text of button
                string textButton = subject.nombre;
                if (textButton.Contains(".png"))
                {
                    script_experiences.texture.mainTexture = Resources.Load<Texture>("Images/" + directory + "/" + textButton.Remove(textButton.Length - 4));
                    script_experiences.texture.MakePixelPerfect();
                    NGUITools.SetActive(script_experiences.texture.gameObject, true);
                    NGUITools.SetActive(script_experiences.label.gameObject, false);
                }
                else
                {
                    script_experiences.label.text = textButton;
                    NGUITools.SetActive(script_experiences.label.gameObject, true);
                    NGUITools.SetActive(script_experiences.texture.gameObject, false);
                }
                i++;
            }
            grid.Reposition();
            grid.transform.parent.GetComponent<UIScrollView>().ResetPosition();

            List<TweenPosition> tweens = new List<TweenPosition>();
            tweens.Add(Themes.GetComponent<TweenPosition>());
            Package package = new Package(Themes, tweens);
            history.addPackageMove(package, "Themes-Experiences");
        }
        else
        {
            Debug.LogWarning("No results");
        }
    }
}
