using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadScene : MonoBehaviour {

    public string sceneName;
    public GameObject loading;

    void OnClick()
    {
        if (loading != null)
        {
            NGUITools.SetActive(loading, true);
			SceneManager.LoadSceneAsync(sceneName);
        }
        else
			SceneManager.LoadScene(sceneName);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
