using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TopicList", menuName = "Topic/List", order = 2)]

public class TopicList : ScriptableObject
{

    public string listName;
    public List<Topic> topics;

}
