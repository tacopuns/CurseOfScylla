using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SpearTarget : MonoBehaviour, IPointerClickHandler
{
    [HideInInspector]
    public RandomSpawner powerMeterSpawner;

    public void OnPointerClick(PointerEventData eventData)
    {
        
        
        powerMeterSpawner?.OnObjectCaught(gameObject);


    }
}