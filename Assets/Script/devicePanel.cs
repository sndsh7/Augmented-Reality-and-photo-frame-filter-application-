using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class devicePanel : MonoBehaviour {



    void Update()
    {
       
    }

    public void MenuOnClick()
    {
        StartCoroutine("MenuCoroutin");
    }

    IEnumerator MenuCoroutin(){
        yield return null;
        GameObject.Find("DevicePanel").GetComponent<Canvas>().enabled = true;
        //yield return new WaitForEndOfFrame();

    }

    public void EnterOnClick()
    {
        StartCoroutine("EnterCoroutin");
    }
    IEnumerator EnterCoroutin()
    {
        yield return null;
        GameObject.Find("DevicePanel").GetComponent<Canvas>().enabled = false;

    }

    public void CancelOnClick(){
        StartCoroutine("CancelCoroutin");
    }
    IEnumerator CancelCoroutin()
    {
        yield return null;
        GameObject.Find("DevicePanel").GetComponent<Canvas>().enabled = false;

    }

}
