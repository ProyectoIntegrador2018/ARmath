using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TopicManager : MonoBehaviour {

    private Topic currentTopic;

    public List<MainTopic> topics;

    private void Start()
    {
        GameObject.Find("ARCamera").GetComponent<VuforiaBehaviour>().enabled = false;
    }

    public void SetCurrentTopic(Topic topic){
        currentTopic = topic;
    }

    public Topic GetCurrentTopic(){
        return currentTopic;
    }

}
