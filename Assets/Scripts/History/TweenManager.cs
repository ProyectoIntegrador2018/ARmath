using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TweenManager : MonoBehaviour
{

    public List<TweenPosition> moveLeft = null;
    public List<TweenPosition> moveRight = null;

    public void playTweens()
    {
        if (moveLeft != null)
        {
            if (moveLeft.Count > 0)
            {
                for (int i = 0; i < moveLeft.Count; i++)
                {
                    //moveLeft[i].duration = 0.3f;
                    moveLeft[i].from = moveLeft[i].transform.localPosition;
                    moveLeft[i].to = moveLeft[i].transform.localPosition - Vector3.right * 2000;// position[i];
                    moveLeft[i].ResetToBeginning();
                    moveLeft[i].PlayForward();
                }
            }
        }
        if (moveRight != null)
        {
            if (moveRight.Count > 0)
            {
                for (int i = 0; i < moveRight.Count; i++)
                {
                    //moveRight[i].duration = 0.3f;
                    moveRight[i].from = moveRight[i].transform.localPosition;
                    moveRight[i].to = moveRight[i].transform.localPosition + Vector3.right * 2000;// position[i];
                    moveRight[i].ResetToBeginning();
                    moveRight[i].PlayForward();
                }
            }
        }
    }
}
