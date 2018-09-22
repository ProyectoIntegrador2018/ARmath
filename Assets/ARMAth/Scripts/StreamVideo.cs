using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour
{

    public RawImage rawImage;
    public VideoPlayer videoPlayer;


    IEnumerator StartVideoPlayback(){

        videoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        while (!videoPlayer.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();


    }

    public void EndStreaming(){
        videoPlayer.Stop();
    }
   

   public void ToggleVideoState()
    {

        if(!videoPlayer.isPlaying){
            StartCoroutine(StartVideoPlayback());

        }else{
            videoPlayer.Pause();
        }
     
      
    }
}