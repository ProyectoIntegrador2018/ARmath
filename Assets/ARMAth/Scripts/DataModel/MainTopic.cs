using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MainTopic", menuName = "Topic/MainTopic", order = 3)]

public class MainTopic : ScriptableObject
{

    public string topicName;
    public List<TopicList> mainTopics;

}