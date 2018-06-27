using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideme : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.Find("DevicePanel").GetComponent<Canvas>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
