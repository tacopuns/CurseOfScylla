using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance;

    [Header("Cursor Textures")]
    public Texture2D defaultCursor;
    public Texture2D interactCursor;
    public Texture2D talkCursor;
    public Texture2D inspectCursor;
    public Texture2D teleportCursor;

    public Vector2 hotspot = Vector2.zero;

    private Texture2D currentCursor;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        SetCursor(defaultCursor);
    }

    public void SetCursor(Texture2D cursor)
    {
        if (currentCursor == cursor) return; // avoid redundant calls
        Cursor.SetCursor(cursor, hotspot, CursorMode.Auto);
        currentCursor = cursor;
    }

    public void SetDefault() => SetCursor(defaultCursor);
    public void SetInteract() => SetCursor(interactCursor);
    public void SetTalk() => SetCursor(talkCursor);
    public void SetInspect() => SetCursor(inspectCursor);
    public void SetTeleport() => SetCursor(teleportCursor);

}
