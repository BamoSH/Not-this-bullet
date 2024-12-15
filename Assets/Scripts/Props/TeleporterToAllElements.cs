using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterToAllElements : MonoBehaviour
{
    public Transform targetTeleporter; 
    public float delayTime = 2.0f; 

    private Dictionary<GameObject, float> objectsInTeleporter = new Dictionary<GameObject, float>();

    private void Update()
    {
        List<GameObject> objectsToRemove = new List<GameObject>();
        foreach (var item in objectsInTeleporter)
        {
            objectsInTeleporter[item.Key] = item.Value + Time.deltaTime;
            if (item.Value >= delayTime)
            {
                TeleportObject(item.Key);
                objectsToRemove.Add(item.Key);
            }
        }

        foreach (var obj in objectsToRemove)
        {
            objectsInTeleporter.Remove(obj);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!objectsInTeleporter.ContainsKey(other.gameObject))
        {
            objectsInTeleporter.Add(other.gameObject, 0f); 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (objectsInTeleporter.ContainsKey(other.gameObject))
        {
            objectsInTeleporter.Remove(other.gameObject);
        }
    }

    private void TeleportObject(GameObject obj)
    {
        if (targetTeleporter != null)
        {
            obj.transform.position = targetTeleporter.position; 
        }
    }
}
