using UnityEngine;
using System.Collections;

public class LoadModel : MonoBehaviour {

    public string model_name;
    public string model_static_name;
    public string text;
    public bool megafiers;
    public ModelManager mark;

    void OnClick()
    {
        GetComponent<UIToggle>().optionCanBeNone = false;
        if (string.IsNullOrEmpty(model_static_name))
            mark.setModel(model_name, "", text, megafiers);
        else
            mark.setModel(model_name, model_static_name, "", megafiers);
    }

    public void EnableButton()
    {
        if (!string.IsNullOrEmpty(model_name))
        {
            GetComponent<UIButton>().isEnabled = true;
        }
    }

    public void DisableButton()
    {
        if (string.IsNullOrEmpty(model_name))
        {
            GetComponent<UIButton>().isEnabled = false;
        }
    }
}
