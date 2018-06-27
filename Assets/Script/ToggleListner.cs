using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleListner : MonoBehaviour {


    Toggle fb_Toggle;

    private bool Nfb = false;

    void Start()
    {
        //Fetch the Toggle GameObject
        fb_Toggle = GetComponent<Toggle>();
        //Add listener for when the state of the Toggle changes, to take action
        fb_Toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(fb_Toggle);
        });

        //Initialise the Text to say the first state of the Toggle
        //m_Text.text = "First Value : " + m_Toggle.isOn;
    }

    //Output the new state of the Toggle into Text
    public void ToggleValueChanged(Toggle change)
    {
        //m_Text.text = "New Value : " + m_Toggle.isOn;
        Nfb = fb_Toggle.isOn;

        ImageMove nFB = new ImageMove();
        nFB.SetFacebook(Nfb);
    }
}
