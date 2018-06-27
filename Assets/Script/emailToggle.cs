using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class emailToggle : MonoBehaviour {

    Toggle em_Toggle;

    private bool Nem = false;

    void Start()
    {
        //Fetch the Toggle GameObject
        em_Toggle = GetComponent<Toggle>();
        //Add listener for when the state of the Toggle changes, to take action
        em_Toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(em_Toggle);
        });

        //Initialise the Text to say the first state of the Toggle
        //m_Text.text = "First Value : " + m_Toggle.isOn;
    }

    //Output the new state of the Toggle into Text
    public void ToggleValueChanged(Toggle change)
    {
        //m_Text.text = "New Value : " + m_Toggle.isOn;
        Nem = em_Toggle.isOn;

        ImageMove nEM = new ImageMove();
        nEM.SetEmail(Nem);
    }
}
