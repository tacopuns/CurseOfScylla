using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpearFishing : Clickable
{
    [SerializeField] private GameObject minigameCanvas;
    public override void OnClickableClicked()
    {
        minigameCanvas.SetActive(true);
    }
}
