using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomSpawner : MonoBehaviour
{
    public List<RectTransform> spawnPoints;
    public GameObject objectToSpawn;
    public RectTransform parentObject;

     private List<GameObject> spawnedObjects = new List<GameObject>();

    public void SpawnObjects()
    {
        foreach (RectTransform point in spawnPoints)
        {
            // ✅ Instantiate using world position so the position is correct
            GameObject obj = Instantiate(objectToSpawn, point.position, Quaternion.identity, parentObject);

            // ✅ Ensure UI layout is preserved (important for RectTransform)
            RectTransform objRect = obj.GetComponent<RectTransform>();
            objRect.position = point.position;
            objRect.rotation = point.rotation;
            objRect.localScale = point.localScale;

            spawnedObjects.Add(obj);
        }
    }

    public void DestroyAllSpawnedObjects()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            Destroy(obj);
        }

        spawnedObjects.Clear();
    }
}
