using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TopicData", menuName = "Topic/Item", order = 1)]

public class Topic : ScriptableObject {

    public string topicName;
    public int layoutType;
    public List<GameObject> models;

}
