using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CaptureScreenshot : MonoBehaviour
{
    private string Initial;
    private static bool Shot_taken = false;

    void Start()
    {
        //path for Initial folder
        Initial = Application.persistentDataPath + "/Initial";
        //Create folder
        if (Directory.Exists(Initial) == false)
        {
            Directory.CreateDirectory(Initial);
            Debug.Log("Folder Created" + Initial);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(2);
        }
    }

    public void TakeAShot()
    {
        StartCoroutine("CaptureIt");
        Debug.Log("Photo Click");
    }

    IEnumerator CaptureIt()
    {
        // Wait till the last possible moment before screen rendering to hide the UI
        yield return null;
        GameObject.Find("MyCanvas").GetComponent<Canvas>().enabled = false;
        //input field token
        UserID_Token username = new UserID_Token();
        string uid = username.GetuserName();

        string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        //string fileName = uid + timeStamp + ".png";
        string fileName = "Default.png";
        string Name = fileName;
        yield return new WaitForEndOfFrame();
        ScreenCapture.CaptureScreenshot("Initial/" + Name);
        GameObject.Find("MyCanvas").GetComponent<Canvas>().enabled = true;
            Shot_taken = true;
            if (Shot_taken == true)
            {
                SceneLevel ScNo = new SceneLevel();
                int SceneNo = ScNo.GetSceneNumber();
                SceneManager.LoadScene(5);
                SceneNo = 5;
            }


        }
}

/*
if (Shot_Taken == true)
       {
           string Origin_Path = System.IO.Path.Combine(Application.persistentDataPath, SaveFileName);
           // This is the path of my folder.
           string Path = "/storage/emulated/0/DCIM/SBT/" + SaveFileName;
           SceneManager.LoadScene(5);
           Debug.Log("Path is set");
           if (System.IO.File.Exists(Origin_Path))
           {
               Debug.Log("Path is exist");
               System.IO.File.Move(Origin_Path, Path);
               Shot_Taken = false;
           }
       }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureScreenshot : MonoBehaviour {

    protected const string MEDIA_STORE_IMAGE_MEDIA = "android.provider.MediaStore$Images$Media";
    protected static AndroidJavaObject m_Activity;

    protected static string SaveImageToGallery(Texture2D a_Texture, string a_Title, string a_Description)
    {
        using (AndroidJavaClass mediaClass = new AndroidJavaClass(Absolutepath))
        {
            using (AndroidJavaObject contentResolver = Activity.Call<AndroidJavaObject>("getContentResolver"))
            {
                AndroidJavaObject image = Texture2DToAndroidBitmap(a_Texture);
                return mediaClass.CallStatic<string>("insertImage", contentResolver, image, a_Title, a_Description);
            }
        }
    }

    protected static AndroidJavaObject Texture2DToAndroidBitmap(Texture2D a_Texture)
    {
        byte[] encodedTexture = a_Texture.EncodeToPNG();
        using (AndroidJavaClass bitmapFactory = new AndroidJavaClass("android.graphics.BitmapFactory"))
        {
            return bitmapFactory.CallStatic<AndroidJavaObject>("decodeByteArray", encodedTexture, 0, encodedTexture.Length);
        }
    }

    protected static AndroidJavaObject Activity
    {
        get
        {
            if (m_Activity == null)
            {
                AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                m_Activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            }
            return m_Activity;
        }
    }

	public void CaptureScreenShot()
    {
        StartCoroutine(CaptureScreenshotCoroutine(Screen.width, Screen.height));
    }

    private IEnumerator CaptureScreenshotCoroutine(int width, int height)
    {
      
        yield return new WaitForEndOfFrame();
        Texture2D tex = new Texture2D(width, height);
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();
        ScreenCapture.CaptureScreenshot(name);
        yield return tex;
        string path = SaveImageToGallery(tex,"Name","Description");
        Debug.Log("Picture has been saved at:\n" + path);
    }
}*/
