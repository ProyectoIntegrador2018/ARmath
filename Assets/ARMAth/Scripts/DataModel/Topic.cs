using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;


[CreateAssetMenu(fileName = "TopicData", menuName = "Topic/Item", order = 1)]

public class Topic : ScriptableObject {

    public string topicName;
    public int layoutType;
    public VideoClip videoClip;
    public GameObject grid;
    public List<GameObject> models;
    public List<bool> overridesGrid;
    public List<string> buttonText;
    public Sprite buttonImage;
    public bool hasButtonImage = false;


}
