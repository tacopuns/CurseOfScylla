using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    public CursorType cursorType = CursorType.Interact;

    private void OnMouseEnter()
    {
        switch (cursorType)
        {
            case CursorType.Interact:
                CursorManager.Instance.SetInteract();
                break;
            case CursorType.Talk:
                CursorManager.Instance.SetTalk();
                break;
            case CursorType.Inspect:
                CursorManager.Instance.SetInspect();
                break;
            case CursorType.Teleport:
                CursorManager.Instance.SetTeleport();
                break;
            default:
                CursorManager.Instance.SetDefault();
                break;
                
        }
    }

    private void OnMouseExit()
    {
        CursorManager.Instance.SetDefault();
    }
}
