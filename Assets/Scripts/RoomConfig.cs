using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoomConfig : MonoBehaviour
{
    public float newOrthographicSize;

    [SerializeField] public CinemachineVirtualCamera vMainCam;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            vMainCam.m_Lens.OrthographicSize = newOrthographicSize;
            Debug.Log("Current room's orthorgrpahic size:" + vMainCam.m_Lens.OrthographicSize);
        }
    }
}
