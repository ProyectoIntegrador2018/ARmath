using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopicManager : MonoBehaviour {

    public List<MainTopic> topics;
    private MainTopic currentTopic = null;

    
   public void SetCurrentTopicList(int index){

        currentTopic = topics[index];

    }

}
