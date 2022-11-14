using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerSpawnEvent : MonoBehaviour
{
    public string colliderName;
    public string tagName;
    public GameObject objectSpawning;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("calling function");
        if (other.name == colliderName) {
            if (other.tag == null || other.tag == tagName) {
                Instantiate(objectSpawning, transform.position, transform.rotation, transform);
            }
        }
    }
}
