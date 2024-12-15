using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterToAllElements : MonoBehaviour
{
    public Transform targetTeleporter; // 目标传送点
    public float delayTime = 2.0f; // 物体需要在传送点上停留的时间

    private Dictionary<GameObject, float> objectsInTeleporter = new Dictionary<GameObject, float>();

    private void Update()
    {
        List<GameObject> objectsToRemove = new List<GameObject>();
        foreach (var item in objectsInTeleporter)
        {
            objectsInTeleporter[item.Key] = item.Value + Time.deltaTime;
            if (item.Value >= delayTime)
            {
                // 达到延迟时间，执行传送
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
            objectsInTeleporter.Add(other.gameObject, 0f); // 重置计时器
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
            obj.transform.position = targetTeleporter.position; // 将物体传送到目标传送点的位置
        }
    }
}
