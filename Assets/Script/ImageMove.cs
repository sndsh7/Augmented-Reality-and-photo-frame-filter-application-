using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using PrintUtil;
using UnityEditor;
using LCPrinter;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ImageMove : MonoBehaviour {

    private static bool Succeed = false;
    private static bool FB = false;
    private static bool EM = false;
    private static bool PR = false;

    public string printerName = "";
    public int copies = 1;

    public void SetFacebook(bool fb)
    {
        FB = fb;
    }
    public void SetEmail(bool em)
    {
        EM = em;
    }
    public void SetPrint(bool pr){
        PR = pr;
    }

    public void Move()
    {
        //timestamp and user id token call by get method
        string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        UserID_Token username = new UserID_Token();
        string uid = username.GetuserName();

        //Toggle value listner
        Debug.Log("Email"+EM);
        Debug.Log("Fb"+FB);
        Debug.Log("PR"+PR);

        DirectoryInfo sourceInfo = new DirectoryInfo(Application.persistentDataPath+"/Initial");
        DirectoryInfo destInfo = new DirectoryInfo(Application.persistentDataPath + "/Success");
        if(!Directory.Exists(destInfo.FullName)){
            Directory.CreateDirectory(destInfo.FullName);
        }
        foreach(FileInfo fi in sourceInfo.GetFiles())
        {
            if (fi.Length != 0 && FB == true && EM == true)
            {
                fi.CopyTo(Path.Combine(destInfo.ToString(), uid + "___1___1___" + timeStamp + ".png"), true);
                Succeed = true;
            }
        
            else if (fi.Length != 0 && FB == true && EM == false)
            {
   
                fi.CopyTo(Path.Combine(destInfo.ToString(), uid + "___1___0___" + timeStamp + ".png"), true);
                Succeed = true;

            }
            else if (fi.Length != 0 && FB == false && EM == true)
            {

                fi.CopyTo(Path.Combine(destInfo.ToString(), uid + "___0___1___" + timeStamp + ".png"), true);
                Succeed = true;

            }
            else if (fi.Length != 0 && FB == false && EM == false)
            {
                fi.CopyTo(Path.Combine(destInfo.ToString(), uid + "___0___0___" + timeStamp + ".png"), true);
                Succeed = true;
            }
            else if(fi.Length != 0 && PR == true && FB == false && EM == false)
            {
                fi.CopyTo(Path.Combine(destInfo.ToString(), uid + "PrintOut" + timeStamp + ".png"), true);
                Succeed = true;
                Print.PrintTextureByPath(Application.persistentDataPath + "/Initial/*.png", copies, printerName);
                Debug.Log("Printing :" + Application.persistentDataPath + "/Initial/*.png");
            }
           
           
        }

        if(Succeed == true){
            FB = false;
            EM = false;
            PR = false;
            Succeed = false;

            Debug.Log("Email" + EM);
            Debug.Log("Fb" + FB);
            Debug.Log("PR" + PR);

            
            DirectoryInfo directoryInfo = new DirectoryInfo(Application.persistentDataPath + "/Initial");
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                file.Delete();
                Debug.Log("Files deleted");
                StartCoroutine(Upload());

            }
            SceneLevel ScNo = new SceneLevel();
            int SceneNo = ScNo.GetSceneNumber();
            SceneManager.LoadScene(7);
            ScNo.SetSceneIndex(7);
        }
     }

    IEnumerator Upload()
    {
        string imgPath = Application.persistentDataPath + "/Success/*.png";
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("MyImage", imgPath));
        Debug.Log(imgPath);

        UnityWebRequest www = UnityWebRequest.Post("http://www.", formData);
        yield return www.Send();

        if(www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload completed");
        }
    }
}
