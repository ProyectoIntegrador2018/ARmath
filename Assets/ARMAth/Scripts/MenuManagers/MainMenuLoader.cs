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
                /*
               
                Actions buttonActions = new Actions();
                buttonActions.isUIScreen = true;

                buttonActions.uiScreenIndex = 0;
                buttonActions.uiScreen = screenToMove.GetComponent<UIScreen>();
                */

                GameObject clone = Instantiate(buttonPrefab, this.transform);
                clone.transform.parent = this.transform;

                //clone.GetComponent<UIButton>().actionsOnClick = buttonActions;

                RectTransform rectTranform = clone.GetComponent<RectTransform>();
                Text cloneText = clone.GetComponentInChildren<Text>();
                cloneText.text = topicMananger.GetComponent<TopicManager>().topics[i].topicName;

             }


        }

    }
}
