using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class imageTransformRect : MonoBehaviour {
    

	void Start () {
        


	}
	
	// Update is called once per frame
	void Update () {
        if(Input.deviceOrientation == DeviceOrientation.LandscapeLeft){
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        if (Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        if (Input.deviceOrientation == DeviceOrientation.Portrait)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
	}
}
