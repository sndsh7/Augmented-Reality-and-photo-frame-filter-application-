using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UserID_Token : MonoBehaviour
{

    public GameObject userID;
    public GameObject submit;
    public Button SubmitBtn;

    private static string UserID;

    void Start()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(Application.persistentDataPath + "/Initial");
        foreach (FileInfo file in directoryInfo.GetFiles())
        {
            file.Delete();
            Debug.Log("Files deleted");
        }
    }

    void Update()
    {
        UserID = userID.GetComponent<InputField>().text;
        SubmitBtn = submit.GetComponent<Button>();
        if (UserID != "")
        {
            //SubmitBtn.onClick.AddListener(userValidation);
        }
    }

    public string GetuserName(){
        return UserID;
    }
    public void SetuserName(string userID){
        UserID = userID;
    }
}
        