using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class PenguinArea : MonoBehaviour
{
    public PenguinAgent penguinAgent;
    public GameObject penguinBaby;
    public TMP_Text cumulativeRewardText;
    public Fish fishPrefab;
    private List<GameObject> fishList;

    public void ResetArea()
    {
        RemoveAllFish();
        PlacePenguin();
        PlayBaby();
        SpawnFish(4, .5f);
    }
    public static Vector3 ChooseRandomPosition(Vector3 center, float minAngle, float maxAngle, float minRadius, float maxRadius)
    {
        float radius = minRadius;
        float angle = minAngle;

        if (maxRadius > minRadius)
        {
            radius = Random.Range(minRadius, maxRadius);
        }
        if (maxAngle > minAngle)
        {
            angle = Random.Range(minAngle, maxAngle);
        }
        return center + Quaternion.Euler(0f, angle, 0f) * Vector3.forward * radius;
    }
    private void PlacePenguin()
    {
        Rigidbody rigid = penguinAgent.GetComponent<Rigidbody>();
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;

        penguinAgent.transform.position = ChooseRandomPosition(transform.position, 0f, 360f, 0f, 9f) + Vector3.up * .5f;
        penguinAgent.transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360), 0);
    }
    private void PlayBaby()
    {
        Rigidbody rigid = penguinBaby.GetComponent<Rigidbody>();
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
        penguinBaby.transform.position = ChooseRandomPosition(transform.position, -45f, 45f, 4f, 9f) + Vector3.up * .5f;
        penguinBaby.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }

    private void SpawnFish(int count, float fishSpeed)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject fishObject = Instantiate(fishPrefab.gameObject);
            fishObject.transform.position = ChooseRandomPosition(transform.position, 100f, 260f, 2f, 13f) + Vector3.up * .5f;
            fishObject.transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

            fishObject.transform.SetParent(transform);
            fishList.Add(fishObject);
            fishObject.GetComponent<Fish>().fishSpeed = fishSpeed;
        }
    }

    private void RemoveAllFish()
    {
        if (fishList != null)
        {
            for (int i = 0; i < fishList.Count; i++)
            {
                if (fishList[i] != null)
                {
                    Destroy(fishList[i]);
                }
            }
            fishList = new List<GameObject>();
        }
    }
}
