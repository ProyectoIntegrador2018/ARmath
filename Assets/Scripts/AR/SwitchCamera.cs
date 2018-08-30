using UnityEngine;
using System.Collections;
using Vuforia;

public class SwitchCamera : MonoBehaviour {

    int cameraNum;
    DB db;

    void Start()
    {
        db = GameObject.FindGameObjectWithTag("Backend").GetComponent<DB>();
    }

    void OnClick()
    {
        cameraNum = db.getCameraDevice();
        //Change camera
        CameraDevice.Instance.Stop();
        if (cameraNum == 1) //is back camera
        {
            db.setCameraDevice(2);
            CameraDevice.Instance.Init(CameraDevice.CameraDirection.CAMERA_FRONT); //change to front
        }
        else if (cameraNum == 2) //is front camera
        {
            db.setCameraDevice(1);
            CameraDevice.Instance.Init(CameraDevice.CameraDirection.CAMERA_BACK); //change to back
        }
        CameraDevice.Instance.Start();
    }
}
