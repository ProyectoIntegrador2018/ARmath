using UnityEngine;
using System.Collections;

public class ToggleCredits : MonoBehaviour {

    public GameObject[] objectsToggle;

    public void TurnOn()
    {
        foreach(GameObject go in objectsToggle)
        {
            NGUITools.SetActive(go, true);

            if (go.GetComponent<TweenScale>())
                go.GetComponent<TweenScale>().PlayForward();
        }
    }

    public void TurnOff()
    {
        foreach(GameObject go in objectsToggle)
        {
            if (go.GetComponent<TweenScale>())
            {
                TweenScale tween = go.GetComponent<TweenScale>();
                EventDelegate del = new EventDelegate(this, "Visible");
                del.parameters[0] = new EventDelegate.Parameter(go, "gameObject");
                EventDelegate.Add(tween.onFinished, del, true);
                tween.PlayReverse();
            }
            else
                NGUITools.SetActive(go, false);
        }
    }

    void Visible(GameObject go)
    {
        NGUITools.SetActive(go, !go.activeSelf);
    }
}
