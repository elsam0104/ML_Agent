using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public float fishSpeed = 0f;

    private float randomSpeed = 0;
    private float nextActionTime = -1f;
    private Vector3 targetPosition;

    private void FixedUpdate()
    {
        if (fishSpeed > 0f)
        {
            Swim();
        }
    }
    private void Swim()
    {
        if (Time.fixedTime >= nextActionTime)
        {
            randomSpeed = fishSpeed * Random.Range(.5f, 1.5f);

            targetPosition = PenguinArea.ChooseRandomPosition(transform.parent.position, 100f, 260f, 2f, 13f) + Vector3.up * .5f;
            transform.rotation = Quaternion.LookRotation(targetPosition - transform.position, Vector3.up);

            float timeToReach = Vector3.Distance(targetPosition, transform.position) / randomSpeed;// 거리 / 속도 = 시간
            nextActionTime = Time.deltaTime + timeToReach;
        }
        else
        {
            Vector3 moveVector = randomSpeed * transform.forward * Time.fixedDeltaTime;
            if (moveVector.sqrMagnitude <= Vector3.Distance(transform.position, targetPosition))
            {
                transform.position += moveVector;
            }
            else
            {
                transform.position = targetPosition;
                nextActionTime = Time.fixedTime;
            }
        }
    }
}
