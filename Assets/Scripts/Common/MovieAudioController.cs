using UnityEngine;
using System.Collections;

public class MovieAudioController : MonoBehaviour
{
    [SerializeField]
    bool m_loop = true;

    [SerializeField]
    AudioSource m_audioSource;

    [SerializeField]
    MMT.MobileMovieTexture m_movieTexture;

    public UISprite iconPlay;

    void Awake()
    {
        if (m_audioSource != null)
        {
            m_audioSource.playOnAwake = false;
            m_audioSource.loop = false;
        }

        if (m_movieTexture != null)
        {
            m_movieTexture.PlayAutomatically = false;
            //m_movieTexture.loop = false;

            m_movieTexture.onFinished += OnFinished;

            //m_movieTexture.Play(); //Prewarm
            //m_movieTexture.pause = true;
        }

        NGUITools.SetActive(iconPlay.gameObject, true);
    }

    public void setVideo(string videoName)
    {
        m_movieTexture.Path = "Videos/" + videoName + ".ogg";
        AudioClip audio = Resources.Load<AudioClip>("Audio/" + videoName);
        m_audioSource.clip = audio;
        PreLoad();
    }

    void PreLoad()
    {
        if (m_movieTexture != null)
        {
            m_movieTexture.Play(); //Prewarm video
            m_movieTexture.Pause = true;
        }
    }

    void OnDestroy()
    {
        if (m_movieTexture != null)
        {
            m_movieTexture.onFinished -= OnFinished;
        }
    }

    public void Play()
    {
        if (m_audioSource != null)
        {
            m_audioSource.Play();
        }

        if (m_movieTexture != null)
        {
            m_movieTexture.Pause = false;
        }

        NGUITools.SetActive(iconPlay.gameObject, false);
    }

    public void Pause()
    {
        if (m_audioSource != null)
        {
            m_audioSource.Pause();
        }

        if (m_movieTexture != null)
        {
            m_movieTexture.Pause = true;
        }

        NGUITools.SetActive(iconPlay.gameObject, true);
    }

    public void Stop()
    {
        if (m_movieTexture != null)
        {
            m_movieTexture.Stop();
            m_movieTexture.Play(); //Prewarm video in case of need
            m_movieTexture.Pause = true;
        }

        if (m_audioSource != null)
            m_audioSource.Stop();

        NGUITools.SetActive(iconPlay.gameObject, true);
    }

    public bool isPlaying
    {
        get { return !m_movieTexture.Pause; }
    }

    void OnFinished(MMT.MobileMovieTexture sender)
    {
        if (m_loop)
        {
            if (m_audioSource != null)
            {
                m_audioSource.Stop();
                m_audioSource.Play();
            }

            if (m_movieTexture != null)
            {
                m_movieTexture.Play();
            }
        }
        else
        {
            NGUITools.SetActive(iconPlay.gameObject, true);
            m_movieTexture.Stop();
            m_movieTexture.Play();
            m_movieTexture.Pause = true;
            m_audioSource.Stop();
        }
    }
}
