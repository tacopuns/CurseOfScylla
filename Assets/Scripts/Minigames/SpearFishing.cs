using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpearFishing : MonoBehaviour
{
    public PowerMeter powerMeter;
    
    void Start()
    {

    }

    
    void Update()
    {

    }
    
    

    public void OnPointerClick(PointerEventData eventData) //click on a freak fish to activate power meter
    {
        if (powerMeter != null)
        {
            powerMeter.ActivateMeter();
        }
    }
}
