using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine.Playables;
using UnityEngine;
using Vuforia;

public class ModelManager : MonoBehaviour{


    public VideoPlayer videoPlayer;
    public List<GameObject> layouts;
    public bool shouldBeActive = false;

    private Topic currentTopic;
    private GameObject currentModel = null;
    private GameObject grid = null;



    public void LoadScreen(){
        GameObject.Find("ARCamera").GetComponent<VuforiaBehaviour>().enabled = true;
        RetrieveTopic();
        SetVideoPlayer();
        SetLayout();
        SetButtons();
    }

    private void RetrieveTopic(){

        currentTopic = GameObject.Find("TopicManager").GetComponent<TopicManager>().GetCurrentTopic();

    }

    private void SetVideoPlayer(){
        videoPlayer.Stop();
        videoPlayer.clip = currentTopic.videoClip;
    }

    private void SetLayout(){
        layouts[currentTopic.layoutType].SetActive(true);
    }

    private void SetButtons(){

        for (int i = 0; i < currentTopic.models.Count; i++){
            layouts[currentTopic.layoutType].GetComponent<LayoutManager>().SetButtonText(i, currentTopic.buttonText[i]);
        }

    }

    public void CloseScreen(){
        GameObject.Find("ARCamera").GetComponent<VuforiaBehaviour>().enabled = false;
        DestroyCurrentModel();
        DestroyGrid();
        videoPlayer.Stop();
        layouts[currentTopic.layoutType].SetActive(false);
        GameObject.Find("TopicManager").GetComponent<TopicManager>().SetCurrentTopic(null);
        this.GetComponent<StreamVideo>().ResetState();

    }


    private void DestroyCurrentModel(){

        if (currentModel != null)
        {
            Destroy(currentModel);
            currentModel = null;
        }

    }

    private void DestroyGrid(){
       
        if (grid!= null)
        {
            Destroy(grid);
            grid = null;
        }

    }


    private void SetGrid(int index){
        if (currentTopic.overridesGrid[index] == false && grid == null)
        {
            GameObject imageTarget = GameObject.Find("ImageTarget");
            GameObject newModel = Instantiate(currentTopic.grid, new Vector3(0, 0, 0), Quaternion.identity);
            newModel.transform.parent = imageTarget.transform;
            grid = newModel;

        }else if(currentTopic.overridesGrid[index]){
            DestroyGrid();
        }
    }

    public void PlayModel(int index){
    
        DestroyCurrentModel();

        SetGrid(index);
        GameObject imageTarget = GameObject.Find("ImageTarget");
        GameObject newModel = Instantiate(currentTopic.models[index], imageTarget.transform,true);
        newModel.transform.parent = imageTarget.transform;
        currentModel = newModel;
        PlayAnimation();

        if(shouldBeActive == false){
            SetInactive();
        }

    }


    public void SetInactive(){

        if(currentModel != null){
            var rendererComponents = currentModel.GetComponentsInChildren<Renderer>(true);
            var canvasComponents = currentModel.GetComponentsInChildren<Canvas>(true);

            // Disable rendering:
            foreach (var component in rendererComponents)
                component.enabled = false;
            // Disable canvas':
            foreach (var component in canvasComponents)
                component.enabled = false;
        }

        if(grid != null){
            var rendererComponents = grid.GetComponentsInChildren<Renderer>(true);
            var canvasComponents = grid.GetComponentsInChildren<Canvas>(true);

            // Disable rendering:
            foreach (var component in rendererComponents)
                component.enabled = false;
            // Disable canvas':
            foreach (var component in canvasComponents)
                component.enabled = false;

        }



    }



    private void PlayAnimation(){
        currentModel.GetComponent<PlayableDirector>().Play();
    }

  







}
