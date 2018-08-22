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
using System.Net;

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
                PostTOserver();
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

            Debug.Log("Email" + EM);
            Debug.Log("Fb" + FB);
            Debug.Log("PR" + PR);

            
            DirectoryInfo directoryInfo = new DirectoryInfo(Application.persistentDataPath + "/Initial");
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                file.Delete();
                Debug.Log("Files deleted");
                //PostTOserver();
                Succeed = false;

            }
            SceneLevel ScNo = new SceneLevel();
            int SceneNo = ScNo.GetSceneNumber();
            SceneManager.LoadScene(7);
            ScNo.SetSceneIndex(7);
        }
     }

    public void PostTOserver()
    {
        string imgPath = Application.persistentDataPath + "/Initial/Default.png";
        ftp ftpClient = new ftp(@"ftp://192.168.0.101/", "sling", "socialiot1212");
        Debug.Log("Image Path: "+imgPath);

        ftpClient.upload("Default.png", @imgPath);
        Debug.Log(ftpClient);
        /* Get the Size of a File */
        string fileSize = ftpClient.getFileSize("Default.png");
        Debug.Log(fileSize);
    }
}

class ftp
{
    private string host = null;
    private string user = null;
    private string pass = null;
    private FtpWebRequest ftpRequest = null;
    private FtpWebResponse ftpResponse = null;
    private Stream ftpStream = null;
    private int bufferSize = 2048;

    /* Construct Object */
    public ftp(string hostIP, string userName, string password) { host = hostIP; user = userName; pass = password; }

    /* Download File */
    public void download(string remoteFile, string localFile)
    {
        try
        {
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + remoteFile);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            /* Specify the Type of FTP Request */
            ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
            /* Establish Return Communication with the FTP Server */
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            /* Get the FTP Server's Response Stream */
            ftpStream = ftpResponse.GetResponseStream();
            /* Open a File Stream to Write the Downloaded File */
            FileStream localFileStream = new FileStream(localFile, FileMode.Create);
            /* Buffer for the Downloaded Data */
            byte[] byteBuffer = new byte[bufferSize];
            int bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize);
            /* Download the File by Writing the Buffered Data Until the Transfer is Complete */
            try
            {
                while (bytesRead > 0)
                {
                    localFileStream.Write(byteBuffer, 0, bytesRead);
                    bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize);
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            /* Resource Cleanup */
            localFileStream.Close();
            ftpStream.Close();
            ftpResponse.Close();
            ftpRequest = null;
        }
        catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        return;
    }

    /* Upload File */
    public void upload(string remoteFile, string localFile)
    {
        try
        {
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + remoteFile);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            /* Specify the Type of FTP Request */
            ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
            /* Establish Return Communication with the FTP Server */
            ftpStream = ftpRequest.GetRequestStream();
            /* Open a File Stream to Read the File for Upload */
            FileStream localFileStream = new FileStream(localFile, FileMode.Open);
            /* Buffer for the Downloaded Data */
            byte[] byteBuffer = new byte[bufferSize];
            int bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);
            /* Upload the File by Sending the Buffered Data Until the Transfer is Complete */
            try
            {
                while (bytesSent != 0)
                {
                    ftpStream.Write(byteBuffer, 0, bytesSent);
                    bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            /* Resource Cleanup */
            localFileStream.Close();
            ftpStream.Close();
            ftpRequest = null;
        }
        catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        return;
    }

    /* Delete File */
    public void delete(string deleteFile)
    {
        try
        {
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + deleteFile);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            /* Specify the Type of FTP Request */
            ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;
            /* Establish Return Communication with the FTP Server */
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            /* Resource Cleanup */
            ftpResponse.Close();
            ftpRequest = null;
        }
        catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        return;
    }

    /* Rename File */
    public void rename(string currentFileNameAndPath, string newFileName)
    {
        try
        {
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + currentFileNameAndPath);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            /* Specify the Type of FTP Request */
            ftpRequest.Method = WebRequestMethods.Ftp.Rename;
            /* Rename the File */
            ftpRequest.RenameTo = newFileName;
            /* Establish Return Communication with the FTP Server */
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            /* Resource Cleanup */
            ftpResponse.Close();
            ftpRequest = null;
        }
        catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        return;
    }

    /* Create a New Directory on the FTP Server */
    public void createDirectory(string newDirectory)
    {
        try
        {
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + newDirectory);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            /* Specify the Type of FTP Request */
            ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
            /* Establish Return Communication with the FTP Server */
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            /* Resource Cleanup */
            ftpResponse.Close();
            ftpRequest = null;
        }
        catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        return;
    }

    /* Get the Date/Time a File was Created */
    public string getFileCreatedDateTime(string fileName)
    {
        try
        {
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + fileName);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            /* Specify the Type of FTP Request */
            ftpRequest.Method = WebRequestMethods.Ftp.GetDateTimestamp;
            /* Establish Return Communication with the FTP Server */
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            /* Establish Return Communication with the FTP Server */
            ftpStream = ftpResponse.GetResponseStream();
            /* Get the FTP Server's Response Stream */
            StreamReader ftpReader = new StreamReader(ftpStream);
            /* Store the Raw Response */
            string fileInfo = null;
            /* Read the Full Response Stream */
            try { fileInfo = ftpReader.ReadToEnd(); }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            /* Resource Cleanup */
            ftpReader.Close();
            ftpStream.Close();
            ftpResponse.Close();
            ftpRequest = null;
            /* Return File Created Date Time */
            return fileInfo;
        }
        catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        /* Return an Empty string Array if an Exception Occurs */
        return "";
    }

    /* Get the Size of a File */
    public string getFileSize(string fileName)
    {
        try
        {
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + fileName);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            /* Specify the Type of FTP Request */
            ftpRequest.Method = WebRequestMethods.Ftp.GetFileSize;
            /* Establish Return Communication with the FTP Server */
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            /* Establish Return Communication with the FTP Server */
            ftpStream = ftpResponse.GetResponseStream();
            /* Get the FTP Server's Response Stream */
            StreamReader ftpReader = new StreamReader(ftpStream);
            /* Store the Raw Response */
            string fileInfo = null;
            /* Read the Full Response Stream */
            try { while (ftpReader.Peek() != -1) { fileInfo = ftpReader.ReadToEnd(); } }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            /* Resource Cleanup */
            ftpReader.Close();
            ftpStream.Close();
            ftpResponse.Close();
            ftpRequest = null;
            /* Return File Size */
            return fileInfo;
        }
        catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        /* Return an Empty string Array if an Exception Occurs */
        return "";
    }

    /* List Directory Contents File/Folder Name Only */
    public string[] directoryListSimple(string directory)
    {
        try
        {
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + directory);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            /* Specify the Type of FTP Request */
            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
            /* Establish Return Communication with the FTP Server */
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            /* Establish Return Communication with the FTP Server */
            ftpStream = ftpResponse.GetResponseStream();
            /* Get the FTP Server's Response Stream */
            StreamReader ftpReader = new StreamReader(ftpStream);
            /* Store the Raw Response */
            string directoryRaw = null;
            /* Read Each Line of the Response and Append a Pipe to Each Line for Easy Parsing */
            try { while (ftpReader.Peek() != -1) { directoryRaw += ftpReader.ReadLine() + "|"; } }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            /* Resource Cleanup */
            ftpReader.Close();
            ftpStream.Close();
            ftpResponse.Close();
            ftpRequest = null;
            /* Return the Directory Listing as a string Array by Parsing 'directoryRaw' with the Delimiter you Append (I use | in This Example) */
            try { string[] directoryList = directoryRaw.Split("|".ToCharArray()); return directoryList; }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }
        catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        /* Return an Empty string Array if an Exception Occurs */
        return new string[] { "" };
    }

    /* List Directory Contents in Detail (Name, Size, Created, etc.) */
    public string[] directoryListDetailed(string directory)
    {
        try
        {
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + directory);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            /* Specify the Type of FTP Request */
            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            /* Establish Return Communication with the FTP Server */
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            /* Establish Return Communication with the FTP Server */
            ftpStream = ftpResponse.GetResponseStream();
            /* Get the FTP Server's Response Stream */
            StreamReader ftpReader = new StreamReader(ftpStream);
            /* Store the Raw Response */
            string directoryRaw = null;
            /* Read Each Line of the Response and Append a Pipe to Each Line for Easy Parsing */
            try { while (ftpReader.Peek() != -1) { directoryRaw += ftpReader.ReadLine() + "|"; } }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            /* Resource Cleanup */
            ftpReader.Close();
            ftpStream.Close();
            ftpResponse.Close();
            ftpRequest = null;
            /* Return the Directory Listing as a string Array by Parsing 'directoryRaw' with the Delimiter you Append (I use | in This Example) */
            try { string[] directoryList = directoryRaw.Split("|".ToCharArray()); return directoryList; }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }
        catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        /* Return an Empty string Array if an Exception Occurs */
        return new string[] { "" };
    }
}
