using System.Collections;
using System.Collections.Generic;
using KreliStudio;
using UnityEngine.Events;
using UnityEngine;

public class TopicButtonSettings : MonoBehaviour {

    private Topic selectedTopic;

    public void SetActions()
    {

        Actions action = this.GetComponent<UIButton>().actionsOnClick;
        action.isEvent = true;
        action.unityEvent.AddListener(StoreTopic);


    }

    void StoreTopic(){
        GameObject.Find("TopicManager").GetComponent<TopicManager>().SetCurrentTopic(selectedTopic);
    }

    public void SetSelectedTopic(Topic topic){
        selectedTopic = topic;
    }

}
