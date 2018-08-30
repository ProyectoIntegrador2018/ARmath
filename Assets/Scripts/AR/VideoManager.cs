using UnityEngine;
using System.Collections;

public class VideoManager : MonoBehaviour
{

    //Public variables
    public MovieAudioController movieController;

    //Dinamic Textures
    public UITexture texture1;
    public UITexture texture2;
    public UITexture texture3;
    public UISprite arrow;
    public UILabel noteLabel;
    public float time1;
    public float time2;

    //Temp variables
    private bool animationPlaying;
    private Color startColor1;
    private Color startColor2;
    private TweenPosition posTween1;
    private TweenPosition posTween2;
    private TweenAlpha alphaTween1;
    private TweenAlpha alphaTween2;
    private bool hasAnimation;
    private float timer;
    private Vector3 tempPos1;
    private Vector3 tempPos2;

    void OnEnable()
    {
        hasAnimation = false;
        animationPlaying = false;
        startColor1 = texture1.color;
        startColor2 = texture2.color;
        posTween1 = texture1.GetComponent<TweenPosition>();
        posTween2 = texture2.GetComponent<TweenPosition>();
        alphaTween1 = texture1.GetComponent<TweenAlpha>();
        alphaTween2 = texture2.GetComponent<TweenAlpha>();
    }

    void OnClick()
    {
        if (movieController.isPlaying)
            Pause();
        else
            Play();
    }

    public void setVideo(string directory, string filename)
    {
        movieController.setVideo(directory + filename);
    }

    public void setAnimation(string directory, string text1, string text2, string text3, float scale, float time1, float time2, bool note)
    {
        //Setting variables
        timer = 0;
        hasAnimation = true;
        StopAllCoroutines();

        //Set texture 1
        if (text1.Contains(".png"))
        {
            this.texture1.mainTexture = Resources.Load<Texture>("Images/" + directory + text1.Remove(text1.Length - 4));
            if (directory.Contains("matematicas1"))
            {
                this.texture1.MakePixelPerfect();
                this.texture1.width = (int)(scale * this.texture1.width);
                this.texture1.height = (int)(scale * this.texture1.height);
            }
            else if (directory.Contains("matematicas2"))
            {
                if (note)
                {
                    arrow.flip = UIBasicSprite.Flip.Nothing;
                    arrow.transform.localPosition = new Vector3(97, -33, 0);
                    arrow.transform.localEulerAngles = new Vector3(0, 0, -90);
                }
                else
                {
                    arrow.flip = UIBasicSprite.Flip.Nothing;
                    arrow.transform.localPosition = new Vector3(-33, 97, 0);
                    arrow.transform.localEulerAngles = new Vector3(0, 0, -160);
                }
            }else
            {
                this.texture1.mainTexture = Resources.Load<Texture>("Images/" + directory + text1.Remove(text1.Length - 4));
            }
        }

        //Set texture 2
        if (text2.Contains(".png"))
        {
            this.texture2.mainTexture = Resources.Load<Texture>("Images/" + directory + text2.Remove(text2.Length - 4));
            if (directory.Contains("matematicas1"))
            {
                this.texture2.MakePixelPerfect();
                this.texture2.width = (int)(scale * this.texture2.width);
                this.texture2.height = (int)(scale * this.texture2.height);
            }
            else if (directory.Contains("matematicas2"))
            {
                if (note)
                {
                    arrow.flip = UIBasicSprite.Flip.Nothing;
                    arrow.transform.localPosition = new Vector3(97, -33, 0);
                    arrow.transform.localEulerAngles = new Vector3(0, 0, -90);
                }
                else
                {
                    arrow.flip = UIBasicSprite.Flip.Nothing;
                    arrow.transform.localPosition = new Vector3(-33, 97, 0);
                    arrow.transform.localEulerAngles = new Vector3(0, 0, -160);
                }
            }
        }

        //Set texture 3
        if (text3.Contains(".png"))
        {
            this.texture3.mainTexture = Resources.Load<Texture>("Images/" + directory + text3.Remove(text3.Length - 4));
        }

        //Set note
        if (note && posTween2 != null)
            EventDelegate.Add(posTween2.onFinished, enableNote, true);

        //Set Timers
        this.time1 = time1;
        this.time2 = time2;
    }

    public void resetAnimation()
    {
        if (hasAnimation)
        {
            hasAnimation = false;
            timer = 0;
            StopAllCoroutines();

            //Text 1
            texture1.color = startColor1;
            if (posTween1 != null)
                posTween1.ResetToBeginning();
            if (alphaTween1 != null)
                alphaTween1.ResetToBeginning();

            //Text 2
            texture2.color = startColor2;
            if (posTween2 != null)
                posTween2.ResetToBeginning();
            if (alphaTween2 != null)
                alphaTween2.ResetToBeginning();

            //Note
            if (noteLabel)
            {
                noteLabel.GetComponent<TweenAlpha>().enabled = false;
                noteLabel.color = new Color(1, 1, 1, 0);
                NGUITools.SetActive(noteLabel.gameObject, false);
            }
        }
    }

    public void Play()
    {
        movieController.Play();

        if (hasAnimation)
        {
            animationPlaying = true;

            //Text 1
            if (posTween1 != null)
                posTween1.from = texture1.transform.localPosition;

            //Text 2
            if (posTween2 != null)
                posTween2.from = texture2.transform.localPosition;

            StartCoroutine("startTimer", 1.0f); //Init timer
        }
    }

    public void Pause()
    {
        movieController.Pause();
        if (hasAnimation)
        {
            animationPlaying = false;
            StopAllCoroutines();

            if (posTween1 != null)
                posTween1.enabled = false;
            if (alphaTween1 != null)
                alphaTween1.enabled = false;

            if (posTween2 != null)
                posTween2.enabled = false;
            if (alphaTween2 != null)
                alphaTween2.enabled = false;
        }
    }

    public void Stop()
    {
        movieController.Stop();
        if (hasAnimation)
        {
            animationPlaying = false;
            StopAllCoroutines();

            if (posTween1 != null)
                posTween1.enabled = false;
            if (alphaTween1 != null)
                alphaTween1.enabled = false;

            if (posTween2 != null)
                posTween2.enabled = false;
            if (alphaTween2 != null)
                alphaTween2.enabled = false;
        }
    }

    void enableNote()
    {
        NGUITools.SetActive(noteLabel.gameObject, true);
        noteLabel.gameObject.GetComponent<TweenAlpha>().PlayForward();
    }

    IEnumerator startTimer(float waitTime)
    {
        while (true)
        {
            if (timer >= time1 && animationPlaying)
            {
                if (posTween1 != null)
                    posTween1.PlayForward();
                if (alphaTween1 != null)
                    alphaTween1.PlayForward();
            }

            if (timer >= time2 && animationPlaying)
            {
                if (posTween2 != null)
                    posTween2.PlayForward();
                if (alphaTween2 != null)
                    alphaTween2.PlayForward();
            }

            timer++;
            yield return new WaitForSeconds(waitTime);
        }
    }
}
