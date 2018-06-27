using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetakeBtn : MonoBehaviour {

    private bool deleteflag = false;

	// Use this for initialization
	void Update () {

        if(Input.GetKeyDown(KeyCode.Escape)){
            retakePhoto();
        }
        
	}
    public void retakePhoto(){
        
        DirectoryInfo directoryInfo = new DirectoryInfo(Application.persistentDataPath+"/Initial");
        foreach (FileInfo file in directoryInfo.GetFiles())
        {
            file.Delete();
            Debug.Log("Files deleted");
            deleteflag = true;

            if (deleteflag == true)
            {
                SceneLevel ScNo = new SceneLevel();
                int SceneNo = ScNo.GetSceneNumber();
                SceneManager.LoadScene(3);
                SceneNo = 3;
            }

        }
        //foreach(DirectoryInfo dir in directoryInfo.GetDirectories()){
        //     directoryInfo.Delete();
        //}

    }
}
