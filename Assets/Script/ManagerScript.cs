///**************
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// Copyright (c)2017 SAASHA INTERACTIVE. All rights reserved.
// You can purchase our next version of print html format using unity engine from unity asset store.
// 
///**************


using UnityEngine;
using System.Diagnostics;
using System;
using System.IO;

//----------------------------------------------------------------------------------------------
//This class use for to save file path into defined derectory. 
//Try to change textFilesavePath string variable for your cutom path.
//You can call this class into other script also.

public class FilePathSet
{
    public static string textFileSavePath = Application.streamingAssetsPath;

    public static string SaveFolderPath()
    {

        {
#if UNITY_EDITOR
            return textFileSavePath + "\\StoreTextFile\\" + "PrintThisTextFile" + ".txt";           //Here you can set you own path just dont forget to add ".txt" at the end of your statement.
#else
            return textFileSavePath + "\\StoreTextFile\\" + "PrintThisTextFile" + ".txt";
#endif
        }

    }
}


//---------------------------------------------------------------------------------------------

public class ManagerScript : MonoBehaviour {

    string storeTextData = "Hi printer!!! Print me into color mode.";                             //String for store text unto notepad file. You can use this to store your custom text.

    //===========================
    //This variable used for camera print
    public int resWidth;
    public int resHeight;
    public Camera cam;
    //===========================

    private void Start(){
        File.WriteAllText(FilePathSet.SaveFolderPath(), storeTextData);     //Storing text file into path folder. Also you can write your custom text into text document.
    }

    private void Update(){
        //if (Input.GetMouseButtonDown(0))
        //{
        //    PrinterFuntionNotepad();                                           //Called printer function for testing (Notepad print)
        //}
        //if (Input.GetMouseButtonDown(1))    
        //{
        //    PrintCanvas();                                                     //Called printer function for testing (Canvas print)
        //}
        //if (Input.GetMouseButtonDown(2))
        //{
        //    PrintFromCamera();                                                  //Called printer function for testing (Camera render print)
        //}
    }
    
    //*******************************************************************************
    //This function has ability print your text file using windows command prompt.
    //Make changes into path directory to relocate your defined path.

    //You can call this function wherever you want. By using  --PrinterFuntion();--
    // This prints notepad data only.
    void PrinterFuntionNotepad()
    {
        try
        {
            Process myProcess = new Process();
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            myProcess.StartInfo.CreateNoWindow = true;
            myProcess.StartInfo.UseShellExecute = false;
            myProcess.StartInfo.FileName = "C:\\Windows\\system32\\cmd.exe";            //here you define your custom path
            string path = "NOTEPAD /P " + FilePathSet.SaveFolderPath();
            myProcess.StartInfo.Arguments = "/c" + path;
            myProcess.EnableRaisingEvents = true;
            myProcess.Start();
            //myProcess.WaitForExit();
            int ExitCode = myProcess.ExitCode;
            print(ExitCode);
        }
        catch (Exception e)
        {            
        }
    }
    //********************************************************************************

    // This function Print whole screen canvas from the screen.
    public void PrintCanvas()
    {
        string printPath = Application.persistentDataPath + "Initial/*.png";
        ScreenCapture.CaptureScreenshot(printPath);
        
        try
        {
            Process myProcess = new Process();
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            myProcess.StartInfo.CreateNoWindow = true;
            myProcess.StartInfo.UseShellExecute = false;
            myProcess.StartInfo.FileName = "Canvas";
            string path = Application.persistentDataPath + "Initial/" + "*.png";      //here you define your custom path
            print(path);
            myProcess.StartInfo.Arguments = "/c" + path;
            myProcess.EnableRaisingEvents = true;
            myProcess.Start();            
            int ExitCode = myProcess.ExitCode;
            print(ExitCode);
            print("This prints from selected camera");
        }
        catch (Exception e)
        {
            
        }
    }
    //*******************************************************************************

    //This prints render from selected camera
    void PrintFromCamera()
    {        
        RenderTexture renderTex = new RenderTexture(resWidth, resHeight, 24);            
        cam.targetTexture = renderTex;
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        cam.Render();
        RenderTexture.active = renderTex;
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        cam.targetTexture = null;
        RenderTexture.active = null;                                    //  added to avoid errors and garbage
        Destroy(renderTex);                                             //  same
        byte[] bytes = screenShot.EncodeToPNG();
        string printPath = @Application.dataPath + "\\SelectedCanvas.png";
        string filename = printPath;
        File.WriteAllBytes(filename, bytes);
        print("This prints from selected camera");

        try
        {
            Process myProcess = new Process();
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            myProcess.StartInfo.CreateNoWindow = true;
            myProcess.StartInfo.UseShellExecute = false;
            myProcess.StartInfo.FileName = "C:\\Windows\\system32\\cmd.exe";
            string path = "mspaint.exe /p " + "Assets\\SelectedCanvas.png";        //here you define your custom path
            print(path);
            myProcess.StartInfo.Arguments = "/c" + path;
            myProcess.EnableRaisingEvents = true;
            myProcess.Start();
            int ExitCode = myProcess.ExitCode;
            print(ExitCode);
            print("This prints from selected camera");
        }
        catch (Exception e)
        {

        }
    }
    
}
