using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopicManager : MonoBehaviour {
    
    public List<MainTopic> topics;
    private Topic currentTopic = null;


    public void SetTopic(Topic topic){
        currentTopic = topic;
        
    }


    public Topic GetCurrentTopic(){
        return currentTopic;
    }

}
