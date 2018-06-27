using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public class LoginScript : MonoBehaviour
{
    public GameObject username;
    public GameObject email;
    public GameObject contact;
    private string Username;
    private string Email;
    private string Contact;
    private string form;
    private bool EmailValid = false;

    void Start()
    {

    }
    public void SubmitButton()
    {
        bool UN = false;    //username
        bool EM = false;    //Email
        bool CO = false;    //Contact
       
        if(Username != ""){
            if (!System.IO.File.Exists(Application.persistentDataPath + "/UserInfo.txt"))
            {
                UN = true;
            }else{
                Debug.LogWarning("Username is taken");
            }
        }else{
            Debug.LogWarning("Username Field is empty");
        }
        if(Email != ""){
            if(!EmailValid){
                if(Email.Contains("@")){
                    if(Email.Contains(".")){
                        EM = true;
                    }else{
                        Debug.LogWarning("Email has no .");
                    }
                }else{
                    Debug.LogWarning("Email has no @");
                }
            }else{
                Debug.LogWarning("Emailvalidate problem");
            }
        }else{
            Debug.LogWarning("Email Field is empty");
        }
        if(Contact !=""){
            if(Contact.Length == 10){
                CO = true;
            }else{
                Debug.LogWarning("Mobile no is incorrect");
            }
        }else{
            Debug.LogWarning("Please enter mobile no");
        }
        if(UN == true && EM == true && CO == true){
            Debug.Log(Username+ ":" + Email+ ":" + Contact);
            form = (Username + Environment.NewLine + Email + Environment.NewLine + Contact);
            System.IO.File.WriteAllText(Application.persistentDataPath + Username +".txt", form);
            //System.IO.File.WriteAllText(@"/storage/emulated/0/Android/data/com.SBT.EasyAR/files" + Username + ".txt", form);
            username.GetComponent<InputField>().text = "";
            email.GetComponent<InputField>().text = "";
            contact.GetComponent<InputField>().text = "";  
            SceneManager.LoadScene(1);
        }
    }


	void Update()
	{
        Screen.orientation = ScreenOrientation.Portrait;
        if(Input.GetKeyDown(KeyCode.Tab)){
            if(username.GetComponent<InputField>().isFocused){
                email.GetComponent<InputField>().Select();
            }
            if(email.GetComponent<InputField>().isFocused){
                contact.GetComponent<InputField>().Select();
            }
        }
        if(Input.GetKeyDown(KeyCode.Return)){
            if(Username != "" && Email !="" && Contact !=""){
                SubmitButton();
            }else{
                Debug.Log("Please Fill Form");
            }
        }
        Username = username.GetComponent<InputField>().text;
        Email = email.GetComponent<InputField>().text;
        Contact = contact.GetComponent<InputField>().text;
	}

}


/*
{
    
    public new InputField name;
    public InputField email;
    public InputField mobile;
    public GameObject loginbtn;

    public static string username;
    public static string Email;
    public static string Mobile;

    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        if (username != null)
        {
            name.text = username;
        }
    }

    public void SaveUserDetails(string newName)
    {
        username = newName;
    }

    private void Update()
    {
        Button button = loginbtn.GetComponent<Button>();
    }


    public void validateLogin(string EasyAR)
    {

        SceneManager.LoadScene(EasyAR);

    }

}*/
