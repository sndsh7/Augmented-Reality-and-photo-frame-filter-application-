using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyAR;

public class ExtendedTracker : MonoBehaviour {

    public GameObject Target;
    public GameObject ContainerTarget;
    public float timeActiveOnTargetLose = .5f;

    bool detected = false;
    float timer = 0;

    private void Update()
    {
        if (Target.activeSelf == true)
        {
            detected = true;
            timer = 0f;
        }
        else
        {
            detected = false;
        }
        if (Target.activeSelf == false && timer < timeActiveOnTargetLose){

            ContainerTarget.transform.SetParent(this.transform);
            timer += Time.deltaTime;

        }else if(Target.activeSelf == false){
            ContainerTarget.transform.SetParent(Target.transform);
        }
    }

}
