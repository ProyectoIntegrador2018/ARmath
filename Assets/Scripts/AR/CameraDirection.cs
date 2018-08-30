using UnityEngine;
using System.Collections;
using Vuforia;

public class CameraDirection : MonoBehaviour {//, ITrackerEventHandler {

    DB db;

	// Use this for initialization
	void Start () {
        db = GameObject.FindGameObjectWithTag("Backend").GetComponent<DB>();
		VuforiaBehaviour qcar = GetComponent<VuforiaBehaviour>();
		qcar.RegisterVuforiaStartedCallback(OnInitialized);
        //qcar.RegisterTrackerEventHandler(this);
	}

    public void OnInitialized()
    {
        int camera = db.getCameraDevice();
        CameraDevice.Instance.Stop();
        if (camera == 1)
            CameraDevice.Instance.Init(CameraDevice.CameraDirection.CAMERA_BACK);
        else if(camera == 2)
            CameraDevice.Instance.Init(CameraDevice.CameraDirection.CAMERA_FRONT);
        CameraDevice.Instance.Start();

        bool focusModeSet = CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);

        if (!focusModeSet)
            Debug.LogError("Failed to set focus mode (unsupported mode).");
    }

    //public void OnTrackablesUpdated()
    //{
    //}
}
