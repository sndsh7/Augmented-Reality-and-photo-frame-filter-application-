using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DropDownMenu : MonoBehaviour {

    public string DeviceID;

    //Create a List of new Dropdown options
    List<string> m_DropOptions = new List<string> { "", "Device_ID","Add Frame", "Delete Frame","FaceBook","Email","Print" };
    //This is the Dropdown
    Dropdown m_Dropdown;

    public void Dropdown_IndexChanged(int index){
        if(index == 1){
            
            GameObject.Find("DevicePanel").GetComponent<Canvas>().enabled = true;
        }
    }

	// Use this for initialization
	void Start () {
        //Fetch the Dropdown GameObject the script is attached to
        m_Dropdown = GetComponent<Dropdown>();

        //Clear the old options of the Dropdown menu
        m_Dropdown.ClearOptions();
        //Add the options created in the List above
        m_Dropdown.AddOptions(m_DropOptions);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
