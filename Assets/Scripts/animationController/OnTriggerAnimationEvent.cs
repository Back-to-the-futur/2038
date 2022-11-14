using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerAnimationEvent : MonoBehaviour
{

    public string colliderName;
    public GameObject[] AnimationControllerArray = null;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        //Debug.Log(colliderName);
        if (other.name == colliderName) {
            for (int i = 0; i < AnimationControllerArray.Length; i++) {
                AnimationController[] controllerArray = AnimationControllerArray[i].GetComponents<AnimationController>();
                for (int j = 0; j < controllerArray.Length; j++) {
                    controllerArray[j].startAnimation();
                }
            }
        }
    }
}
