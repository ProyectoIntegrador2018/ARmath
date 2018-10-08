using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ModelEventHandler : MonoBehaviour, ITrackableEventHandler
{


    protected TrackableBehaviour mTrackableBehaviour;
    private GameObject mainScreen;

    void Start()
    {
        mainScreen = GameObject.Find("MainScreenManager");
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);

    }

    void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }


    public void OnTrackableStateChanged(
      TrackableBehaviour.Status previousStatus,
      TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            mainScreen.GetComponent<ModelManager>().shouldBeActive = true;
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            mainScreen.GetComponent<ModelManager>().shouldBeActive = true;
        }
        else
        {
            mainScreen.GetComponent<ModelManager>().shouldBeActive = false;
        }
    }


}
