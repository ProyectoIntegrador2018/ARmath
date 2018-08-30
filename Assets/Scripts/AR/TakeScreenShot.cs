using UnityEngine;
using System.Collections;
using System.IO;

public class TakeScreenShot : MonoBehaviour {

    public string filename;

    void OnClick()
    {
        string path = filename + "_" + Time.frameCount + ".png"; // +System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
        StartCoroutine(ScreenShot(path));
    }

#if (UNITY_ANDROID)

    IEnumerator ScreenShot(string path)
    {
        int num = 0;
        yield return new WaitForEndOfFrame();
        Application.CaptureScreenshot(path);
        path = Application.persistentDataPath + "/" + path;
        Debug.Log(path);
        while(!File.Exists(path) || num <= 5)
        {
            num++;
            yield return new WaitForSeconds(1);
        }
        bool didSave = EtceteraAndroid.saveImageToGallery(path, "Evidence");
        Debug.Log("It did save to gallery: " + didSave);
    }
#elif(UNITY_IPHONE)
    IEnumerator ScreenShot(string path)
    {
        int num = 0;
		yield return new WaitForEndOfFrame();
        Application.CaptureScreenshot(path);
        while(!File.Exists(path) || num <= 5)
        {
            num++;
            yield return new WaitForSeconds(1);
        }
				Prime31.EtceteraBinding.saveImageToPhotoAlbum(path);
    }
#endif
}
