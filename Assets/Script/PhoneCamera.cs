using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneCamera : MonoBehaviour {

    private bool CamAvailable;
    private WebCamTexture backCam;
    private Texture defaultBackground;

    public RawImage backgraound;
    public AspectRatioFitter fit;

	private void Start()
	{
        defaultBackground = backgraound.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            Debug.Log("No Camera device found");
            CamAvailable = false;
            return;
        }
            for (int i = 0; i < devices.Length; i++ ){
                if(!devices[i].isFrontFacing){
                backCam = new WebCamTexture(devices[i].name,Screen.width,Screen.height);
                }
            }
        if(backCam == null){
            Debug.Log("Unable to find back camera");
            return;
     
        }
        backCam.Play();
        backgraound.texture = backCam;
        CamAvailable = true;
	}

	private void Update()
	{
        Screen.orientation = ScreenOrientation.Portrait;
        if(!CamAvailable)
            return;

        float ratio = (float)backCam.width / (float)backCam.height;
        fit.aspectRatio = ratio;

        float scaleY = backCam.videoVerticallyMirrored ? -1f : 1f;
        backgraound.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orient = -backCam.videoRotationAngle;
        backgraound.rectTransform.localEulerAngles = new Vector3(0, 0, orient);

	}
}
