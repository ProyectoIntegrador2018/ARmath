using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KreliStudio
{
    public class MainMenuLoader : MonoBehaviour
    {

        public GameObject carouselPanel;
        public GameObject buttonPrefab;

        // Use this for initialization
        void Start()
        {
            LoadMenu();
            carouselPanel.GetComponent<UICarouselPanel>().Init();           
        }

        private void LoadMenu()
        {
            GameObject topicMananger = GameObject.Find("TopicManager");
            int numberOfElements = topicMananger.GetComponent<TopicManager>().topics.Count;

            for (int i = 0; i < numberOfElements; i++)
            {
                //Screen to move to
               GameObject screenToMove = GameObject.Find("UIScreensGroup").GetComponent<ScreenLoader>().getGameScreenAt(i);

                //Actions setup
                Actions buttonActions = new Actions();
                buttonActions.isUIScreen = true;
                buttonActions.uiScreenIndex = 0;
                buttonActions.uiScreen = screenToMove.GetComponent<UIScreen>();


                //Instance of button
                GameObject clone = Instantiate(buttonPrefab, this.transform);
                clone.transform.parent = this.transform;



                //Setting up button configuration
                clone.GetComponentInChildren<UIButton>().actionsOnClick = buttonActions;
                clone.transform.Find("ButtonImage").gameObject.GetComponent<Image>().sprite = topicMananger.GetComponent<TopicManager>().topics[i].imageSprite;

                Text cloneText = clone.GetComponentInChildren<Text>();
                cloneText.text = topicMananger.GetComponent<TopicManager>().topics[i].topicName;

             }


        }


    }
}
