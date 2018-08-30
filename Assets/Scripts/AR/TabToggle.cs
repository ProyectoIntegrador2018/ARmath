using UnityEngine;
using System.Collections;

public class TabToggle : MonoBehaviour {
	
	public TweenPosition tweenTop;
    public TweenPosition tweenBottom;
    public MovieAudioController videoManager;
    bool showFlag = true;

    void Start()
    {
        StartCoroutine(initTweens());
    }

    IEnumerator initTweens()
    {
        yield return new WaitForSeconds(0.5f);
        tweenTop.from = tweenTop.transform.localPosition;
        tweenTop.to = new Vector3(tweenTop.from.x + 295, tweenTop.from.y, tweenTop.from.z);

        tweenBottom.from = tweenBottom.transform.localPosition;
        tweenBottom.to = new Vector3(tweenBottom.from.x + 295, tweenBottom.from.y, tweenBottom.from.z);
    }

	void OnClick(){
        if (showFlag)
        {
            if (videoManager.isPlaying)
                videoManager.Pause();
        }
        showFlag = !showFlag;
        tweenTop.Toggle();
        tweenBottom.Toggle();
	}
}
