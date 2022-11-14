using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class launchButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public string onDefault;
    public string onClick;

    // Start is called before the first frame update
    public void OnPointerDown(PointerEventData eventData){
        gameObject.GetComponentInChildren<Text>().text = onClick;
    }
    
    public void OnPointerUp(PointerEventData eventData){
        gameObject.GetComponentInChildren<Text>().text = onDefault;
    }
}
