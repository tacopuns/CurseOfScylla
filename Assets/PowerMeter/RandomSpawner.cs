using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RandomSpawner : MonoBehaviour
{

[Header("Spawn Settings")]
    public GameObject objectToSpawn;
    public RectTransform parentObject;
    public List<RectTransform> spawnPoints;
    public float spawnInterval = 1f;
    public float visibleDuration = 1f;
    public float fadeDuration = 0.5f;
    public float coyoteTime = 0.5f;

    [Header("Power Meter")]
    public PowerMeter powerMeter;
    public GameObject powerMeterUI;

    private List<GameObject> activeObjects = new List<GameObject>();
    private GameObject caughtObject = null;
    private bool isPowerMeterActive = false;
    private Coroutine spawnLoopCoroutine;

    void Start()
    {
        spawnLoopCoroutine = StartCoroutine(SpawnLoop());
        powerMeterUI.SetActive(false);
        powerMeter.enabled = false;
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            if (!isPowerMeterActive)
            {
                SpawnNewObject();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnNewObject()
    {
        RectTransform point = spawnPoints[Random.Range(0, spawnPoints.Count)];
        GameObject obj = Instantiate(objectToSpawn, point.position, Quaternion.identity, parentObject);

        CanvasGroup cg = obj.GetComponent<CanvasGroup>();
        if (cg == null)
            cg = obj.AddComponent<CanvasGroup>();

        activeObjects.Add(obj);

        
        SpearTarget clickScript = obj.GetComponent<SpearTarget>();
        if (clickScript != null)
        {
            clickScript.powerMeterSpawner = this; 
        }

        
        StartCoroutine(FadeCycle(obj, cg, visibleDuration + fadeDuration * 2));

    }

    IEnumerator FadeCycle(GameObject obj, CanvasGroup cg, float totalLife)
    {
        yield return StartCoroutine(FadeCanvasGroup(cg, 0f, 1f, fadeDuration));
        float timer = 0f;
        while (timer < totalLife - fadeDuration)
        {
            if (isPowerMeterActive && caughtObject == obj)
            {
                
                yield return null;
            }
            else
            {
                timer += Time.deltaTime;
                yield return null;
            }
        }
        yield return StartCoroutine(FadeCanvasGroup(cg, 1f, 0f, fadeDuration));
        activeObjects.Remove(obj);
        Destroy(obj);
    }

    IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, elapsed / duration);
            yield return null;
        }
        cg.alpha = end;
    }

    
    public void OnObjectCaught(GameObject obj)
    {
        if (isPowerMeterActive)
            return; 

        caughtObject = obj;
        isPowerMeterActive = true;

        
        foreach (var o in activeObjects)
        {
            if (o != obj)
            {
                o.SetActive(false);
            }
        }

        powerMeterUI.SetActive(true);
        powerMeter.enabled = true;

        
        float limitedTime = visibleDuration + coyoteTime;
        powerMeter.StartMeter(EndPowerMeterPhase, limitedTime);
    }

    
    void EndPowerMeterPhase(bool success)
    {
        if (success && powerMeter.lastValue > 70)
        {
        
            Debug.Log("Speared!");
        }
        else
        {
            Debug.Log("It got away...");
        }

        
        foreach (var o in activeObjects)
        {
            o.SetActive(true);
        }

        powerMeterUI.SetActive(false);
        powerMeter.enabled = false;

        
        StartCoroutine(RemoveCaughtObjectAfterDelay(caughtObject, coyoteTime));

        caughtObject = null;
        isPowerMeterActive = false;
    }

    IEnumerator RemoveCaughtObjectAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (obj != null)
        {
            activeObjects.Remove(obj);
            Destroy(obj);
        }
    }
}