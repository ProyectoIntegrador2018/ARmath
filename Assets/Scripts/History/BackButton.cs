using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BackButton : MonoBehaviour {

    public string previusScene;
    public bool experienceActivated;
    public UILabel lblNavigation;
    private History history;

    void Start()
    {
        history = GameObject.FindGameObjectWithTag("Backend").GetComponent<History>();
    }

    void OnClick()
    {
        if (history.getLength() > 1)
        {
            if (experienceActivated)
            {
                //Deactive radio button
                UIToggle.GetActiveToggle(1).optionCanBeNone = true;
                UIToggle.GetActiveToggle(1).value = false;

                //Move Themes to right
                TweenPosition tweenThemes = history.getReference("Themes").GetComponent<TweenPosition>();
                tweenThemes.PlayReverse();
                //Move Experience to right
                TweenPosition tweenExperience = history.getReference("Experiences").GetComponent<TweenPosition>();
                EventDelegate.Add(tweenExperience.onFinished, DisableObject, true);
                tweenExperience.PlayReverse();

                experienceActivated = false;
            }
            else
                history.removePackageMove();

            if (history.getLength() == 1)
            {
                history.changeBackground(1);
                lblNavigation.text = "";
            }
        }
        else
			SceneManager.LoadScene(previusScene);
    }

    private void DisableObject()
    {
        NGUITools.SetActive(TweenPosition.current.gameObject, false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OnClick();
    }
}
