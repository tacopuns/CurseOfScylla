using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PowerMeter : MonoBehaviour
{
    public float increaseSpeed = 120f;
    public float decreaseSpeed = 70f;
    public Slider mainSlider;
    public float successThreshold = 70f;

    private bool isDecreasing = false;
    private bool isRunning = false;
    private bool stopRequested = false;
    private Action<bool> onCompleteCallback;

    private float timeoutDuration = 0f;
    private float timeoutTimer = 0f;
    private bool useTimeout = false;

    public float lastValue { get; private set; }

    void Update()
    {
        if (!isRunning) return;

        // Handle timeout
        if (useTimeout)
        {
            timeoutTimer += Time.deltaTime;
            if (timeoutTimer >= timeoutDuration)
            {
                TimeoutReached();
                return;
            }
        }

        if (!stopRequested)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StopMeter();
                return;
            }

            if (!isDecreasing)
            {
                MeterMoveRight();

                if (mainSlider.value >= 100)
                    isDecreasing = true;
            }
            else
            {
                MeterMoveLeft();

                if (mainSlider.value <= 0)
                    isDecreasing = false;
            }
        }
    }

    public void StartMeter(Action<bool> onComplete, float timeoutSeconds = 0f)
    {
        // Reset state
        mainSlider.value = 0;
        isDecreasing = false;
        isRunning = true;
        stopRequested = false;
        onCompleteCallback = onComplete;

        timeoutDuration = timeoutSeconds;
        timeoutTimer = 0f;
        useTimeout = timeoutSeconds > 0f;

        gameObject.SetActive(true);
    }

    public void StopMeter()
    {
        if (!isRunning || stopRequested) return;

        stopRequested = true;
        lastValue = mainSlider.value;
        isRunning = false;

        bool success = lastValue >= successThreshold;
        onCompleteCallback?.Invoke(success);

        gameObject.SetActive(false);
    }

    private void TimeoutReached()
    {
        // Timeout means player failed to stop in time
        stopRequested = true;
        isRunning = false;

        onCompleteCallback?.Invoke(false);

        gameObject.SetActive(false);
    }

    void MeterMoveRight()
    {
        mainSlider.value += increaseSpeed * Time.deltaTime;
        if (mainSlider.value > 100)
            mainSlider.value = 100;
    }

    void MeterMoveLeft()
    {
        mainSlider.value -= decreaseSpeed * Time.deltaTime;
        if (mainSlider.value < 0)
            mainSlider.value = 0;
    }
}
