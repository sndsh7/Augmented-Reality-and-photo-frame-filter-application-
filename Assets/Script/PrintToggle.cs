using UnityEngine;
using UnityEngine.UI;

public class PrintToggle : MonoBehaviour {
    
    Toggle pr_Toggle;

    private bool Npr = false;

    void Start()
    {
        //Fetch the Toggle GameObject
        pr_Toggle = GetComponent<Toggle>();
        //Add listener for when the state of the Toggle changes, to take action
        pr_Toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(pr_Toggle);
        });

        //Initialise the Text to say the first state of the Toggle
        //m_Text.text = "First Value : " + m_Toggle.isOn;
    }

    //Output the new state of the Toggle into Text
    public void ToggleValueChanged(Toggle change)
    {
        //m_Text.text = "New Value : " + m_Toggle.isOn;
        Npr = pr_Toggle.isOn;

        ImageMove nPR = new ImageMove();
        nPR.SetPrint(Npr);
    }
}
