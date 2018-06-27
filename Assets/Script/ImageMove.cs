using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ImageMove : MonoBehaviour {

    private static bool Succeed = false;
    private static bool FB;
    private static bool EM;

    public void SetFacebook(bool fb)
    {
        FB = fb;
    }
    public void SetEmail(bool em)
    {
        EM = em;
    }

    public void Move()
    {
        //timestamp and user id token call by get method
        string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        UserID_Token username = new UserID_Token();
        string uid = username.GetuserName();

        //Toggle value listner
        Debug.Log(EM);
        Debug.Log(FB);

        DirectoryInfo sourceInfo = new DirectoryInfo(Application.persistentDataPath+"/Initial");
        DirectoryInfo destInfo = new DirectoryInfo(Application.persistentDataPath + "/Success");
        if(!Directory.Exists(destInfo.FullName)){
            Directory.CreateDirectory(destInfo.FullName);
        }
        foreach(FileInfo fi in sourceInfo.GetFiles())
        {
            if (fi.Length != 0 && FB == true && EM == true )
                fi.CopyTo(Path.Combine(destInfo.ToString(), uid + "___1___1___" + timeStamp + ".png"), true);
                Succeed = true;
        
            if (fi.Length != 0 && FB == true && EM == false)
            {
   
                fi.CopyTo(Path.Combine(destInfo.ToString(), uid + "___1___0___" + timeStamp + ".png"), true);
                Succeed = true;

            }
            if (fi.Length != 0 && FB == false && EM == true)
            {

                fi.CopyTo(Path.Combine(destInfo.ToString(), uid + "___0___1___" + timeStamp + ".png"), true);
                Succeed = true;

            }
            if (fi.Length != 0 && FB == false && EM == false)
            {
                fi.CopyTo(Path.Combine(destInfo.ToString(), uid + "___0___0___" + timeStamp + ".png"), true);
                Succeed = true;
            }
           
           
        }
        if(Succeed == true){
            FB = false;
            EM = false;
            
            DirectoryInfo directoryInfo = new DirectoryInfo(Application.persistentDataPath + "/Initial");
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                file.Delete();
                Debug.Log("Files deleted");

            }
            SceneLevel ScNo = new SceneLevel();
            int SceneNo = ScNo.GetSceneNumber();
            SceneManager.LoadScene(7);
            ScNo.SetSceneIndex(7);
        }
     }
 }
