using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;

public class LoadSubjects : MonoBehaviour {

    [SerializeField]
    private GameObject Subjects;
    public Transform prefabSubject;
    public DiagonalGrid grid;
    DB db;
    History history;

	// Use this for initialization
	void Start () {
        db = GameObject.FindGameObjectWithTag("Backend").GetComponent<DB>();
        history = GameObject.FindGameObjectWithTag("Backend").GetComponent<History>();
        addToHistory();
        loadSubject();
	}

    /// <summary>
    /// Adding to history Subject panel
    /// </summary>
    void addToHistory()
    {
        Package package = new Package(Subjects, Subjects.GetComponent<TweenPosition>());
        history.addPackage(package, "Subjects");
    }

    /// <summary>
    /// Load buttons of subjects
    /// </summary>
    void loadSubject()
    {
        List<Resultado> subjects = db.getMaterias();
        int i = 1;
        foreach(Resultado subject in subjects)
        {
            Transform button = PoolManager.Pools["Buttons"].Spawn(prefabSubject, transform);
            button.name = i + "_btn_subject";
            button.GetComponentInChildren<UILabel>().text = subject.nombre;
            button.GetComponentInChildren<UILabel>().transform.localScale = new Vector3(1, 1.5f, 1);
            LoadThemes script_loadthemes = button.GetComponent<LoadThemes>();
            script_loadthemes.idSubject = subject.id_resultado;
            script_loadthemes.directory = subject.directory;
            i++;
        }
        grid.Reposition();
    }
}
