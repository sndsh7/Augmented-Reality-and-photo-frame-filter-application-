using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLevel : MonoBehaviour
{
    public static int SceneNumber;
    private static bool forAR = false;
    private static bool forPhotoframe = false;

    public int GetSceneNumber(){
        return SceneNumber;
    } 
    public void SetSceneIndex(int SceneIndex){
        SceneNumber = SceneIndex;
    }

    void Start()
    {
        if (SceneNumber == 0)
        {
            StartCoroutine("Scene1");
            Debug.Log("Index 0");
        }
        if(SceneNumber == 7){
            StartCoroutine("ToNewUser");
        }
    }
    IEnumerator Scene1()
    {
        yield return new WaitForSeconds(2);
        SceneNumber = 1;
        SceneManager.LoadScene(1);
        Debug.Log("Scene 1");
    }
    IEnumerator ToNewUser()
    {
        yield return new WaitForSeconds(2);
        SceneNumber = 2;
        SceneManager.LoadScene(2);

    }

    public void ToUserID()
    {
        UserID_Token userid = new UserID_Token();
        string newUserID = userid.GetuserName();

        if(forAR == true && newUserID != "")
        {
            Debug.Log(forAR);
            SceneManager.LoadScene(3);

        }
        if (forPhotoframe == true && newUserID != "") 
        {
            Debug.Log(forPhotoframe);
            SceneManager.LoadScene(4);
        }

    }


    public void ToARCamera()
    {
        Debug.Log("forAR true");
        SceneManager.LoadScene(2);
        SceneNumber = 3;
        Debug.Log("Scene 3");
        forAR = true;
        Debug.Log(forAR);


    }
    public void ToPhotoFrameCamera()
    {
        Debug.Log("forPhotoFrame true");
        SceneManager.LoadScene(2);
        SceneNumber = 4;
        Debug.Log("Scene 4");
        forPhotoframe = true;
        Debug.Log(forPhotoframe);
    }

    public void ToPreview(){

        SceneManager.LoadScene(5);
        SceneNumber = 5;


    }
    public void ToAdminHome(){
        
        SceneManager.LoadScene(1);
        SceneNumber = 1;
        forAR = false;
        forPhotoframe = false;

    }
    public void ToShareScreen(){
        
        SceneManager.LoadScene(6);
        SceneNumber = 6;
       
        
    }
    public void ToHome()
    {
        if(forAR == true){
            ToARCamera();
        }else if (forPhotoframe == true){
            ToPhotoFrameCamera();
        }
    }

}