using System.Collections;
using System.Collections.Generic;
using KreliStudio;
using UnityEngine;
using UnityEngine.UI;

public class SubMenuLoader : MonoBehaviour {


    public GameObject carouselPanel;
    public GameObject buttonPrefab;
    public GameObject subMenuButtonPrefab;

    private List<TopicList> mainTopics;

    private void Start()
    {
       
        LoadMenu();
        carouselPanel.GetComponent<UICarouselPanel>().Init();
    }


    private void LoadMenu(){

        foreach(TopicList topicList in mainTopics){


            if (topicList == null){
                Debug.Log("I'm a null object");
            }

            GameObject clone = Instantiate(buttonPrefab, this.transform);
            clone.transform.parent = this.transform;

            clone.transform.Find("SubTopicImage").gameObject.GetComponent<Image>().sprite = topicList.imageSpriteSubTopic;


            Text cloneText = clone.GetComponentInChildren<Text>();

            cloneText.text = topicList.listName;
            LoadButtons(clone, topicList);

        }

    }


    private void LoadButtons(GameObject clone, TopicList topicList){

        GameObject container = clone.transform.Find("ButtonPanel").gameObject;
        container.GetComponent<DynamicGrid>().setNumberOfButtons(topicList.topics.Count);

        foreach (Topic topic in topicList.topics){
            GameObject buttonClone = Instantiate(subMenuButtonPrefab, this.transform);

            buttonClone.transform.parent = container.transform;

            Text cloneText = buttonClone.GetComponentInChildren<Text>();
            if (topic.hasButtonImage){
                GameObject buttonImage = buttonClone.transform.Find("ButtonImage").gameObject;
                buttonImage.SetActive(true);
                cloneText.enabled = !cloneText.enabled;
                buttonImage.GetComponent<Image>().sprite = topic.buttonImage;
            }else{
               
                cloneText.text = topic.topicName;

            }

            Actions buttonActions = new Actions();
            buttonActions.isUIScreen = true;
            buttonActions.uiScreenIndex = 0;
            GameObject screenToMove = GameObject.Find("Layout1");
            buttonActions.uiScreen = screenToMove.GetComponent<UIScreen>();
            buttonClone.GetComponent<UIButton>().actionsOnClick = buttonActions;

            buttonClone.GetComponent<TopicButtonSettings>().SetSelectedTopic(topic);
            buttonClone.GetComponent<TopicButtonSettings>().SetActions();
        }


    }

    public void SetMainTopics(List<TopicList> mainTopics){
        this.mainTopics = mainTopics;
    }
	
   
}
