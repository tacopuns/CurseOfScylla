using UnityEngine;
using System.Collections.Generic;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance;

    [Header("Static Cursors")]
    public Texture2D defaultCursor;
    public Vector2 hotspot = Vector2.zero;

    [Header("Animated Cursor Frames")]
    public Texture2D[] interactFrames;
    public Texture2D[] talkFrames;
    public Texture2D[] inspectFrames;
    public Texture2D[] teleportFrames;

    private Dictionary<string, Texture2D[]> animatedCursors;
    private Texture2D[] currentFrames;
    private int currentFrameIndex;
    private float frameTimer;
    public float frameRate = 0.5f;

    private bool isAnimating;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        animatedCursors = new Dictionary<string, Texture2D[]>
        {
            { "Interact", interactFrames },
            { "Talk", talkFrames },
            { "Inspect", inspectFrames },
            { "Teleport", teleportFrames }
        };

        SetDefault();
    }

    private void Update()
    {
        if (!isAnimating || currentFrames == null || currentFrames.Length == 0)
            return;

        frameTimer += Time.deltaTime;
        if (frameTimer >= frameRate)
        {
            frameTimer = 0f;
            currentFrameIndex = (currentFrameIndex + 1) % currentFrames.Length;
            Cursor.SetCursor(currentFrames[currentFrameIndex], hotspot, CursorMode.Auto);
        }
    }

    public void SetDefault()
    {
        isAnimating = false;
        Cursor.SetCursor(defaultCursor, hotspot, CursorMode.Auto);
    }

    public void SetAnimated(string key)
    {
        if (!animatedCursors.ContainsKey(key) || animatedCursors[key].Length == 0)
        {
            Debug.LogWarning($"Cursor animation key '{key}' not found or empty.");
            return;
        }

        currentFrames = animatedCursors[key];
        currentFrameIndex = 0;
        frameTimer = 0f;
        isAnimating = true;

        Cursor.SetCursor(currentFrames[0], hotspot, CursorMode.Auto);
    }

    // Optional helpers for specific types
    public void SetInteract() => SetAnimated("Interact");
    public void SetTalk() => SetAnimated("Talk");
    public void SetInspect() => SetAnimated("Inspect");
    public void SetTeleport() => SetAnimated("Teleport");
}