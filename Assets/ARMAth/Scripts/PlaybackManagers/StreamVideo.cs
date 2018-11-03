﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour
{

    public RawImage rawImage;
    public VideoPlayer videoPlayer;
    public GameObject image;

    private bool firstPlayback = true;

    private void Update()
    {
        HasPlaybackFinished();
    }

    private void HasPlaybackFinished(){

        long playerCurrentFrame = videoPlayer.frame;
        long playerFrameCount = Convert.ToInt64(videoPlayer.frameCount);

        if (playerCurrentFrame < playerFrameCount)
        {
            print("VIDEO IS PLAYING");
        }
        else
        {
            videoPlayer.Pause();
            image.SetActive(true);
            firstPlayback = true;
            rawImage.color = new Color(0, 0, 0, 1);
            image.SetActive(true);

        }


    }

    public void ResetState(){
        firstPlayback = true;
        rawImage.texture = null;
        rawImage.color = new Color(0, 0, 0,1);
        image.SetActive(true);
    }


    private void PlayState(){
        firstPlayback = false;
        rawImage.color = new Color(1, 1, 1,1);
        image.SetActive(false);
    }


    IEnumerator StartVideoPlayback(){

        videoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        while (!videoPlayer.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }
        rawImage.texture = videoPlayer.texture;
        PlayState();
        videoPlayer.Play();
    }

   public void ToggleVideoState()
    {
        if(!videoPlayer.isPlaying){
            StartCoroutine(StartVideoPlayback());

        }else{
            videoPlayer.Pause();
            image.SetActive(true);
        }

    }
}