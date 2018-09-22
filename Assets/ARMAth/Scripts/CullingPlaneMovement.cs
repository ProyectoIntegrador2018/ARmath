using UnityEngine;
using System.Collections;

public class CullingPlaneMovement : MonoBehaviour
{
    public GameObject cullingPlane;

    private float objectPosition = 0.0F;

    void Update()
    {
        objectPosition = cullingPlane.transform.position.x;

        if (objectPosition <= 0.7){
            cullingPlane.transform.Translate(Vector3.back * Time.deltaTime * 0.05F);
        }
    }


    public void ResetPosition(){
        cullingPlane.transform.position = new Vector3(0, cullingPlane.transform.position.y, cullingPlane.transform.position.z);
    }
}