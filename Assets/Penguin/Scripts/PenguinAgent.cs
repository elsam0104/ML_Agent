using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class PenguinAgent : Agent
{
    public float moveSpeed = 5f;
    public float turnSpeed = 180f;
    public GameObject heartPrefab;
    public GameObject fishPrefab;

    private PenguinArea penguinArea;
    private Rigidbody rigid;
    private GameObject baby;

    private bool isFull; //물고기 물고 있는지
    public override void Initialize()
    {
        penguinArea=GetComponentInParent<PenguinArea>();
        rigid = GetComponent<Rigidbody>();
        baby = penguinArea.penguinBaby;
    }
    public override void OnEpisodeBegin()
    {
        isFull = false;
        penguinArea.ResetArea();
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(isFull);//1
        sensor.AddObservation(Vector3.Distance(baby.transform.position, transform.position));//1
        sensor.AddObservation((baby.transform.position-transform.position).normalized);//3
        sensor.AddObservation(transform.forward);//3
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float forwardAmount = 0f;
        forwardAmount = actions.DiscreteActions[0];
        float turnAmount = 0f;

        if (actions.DiscreteActions[1] == 1f)
        {
            turnAmount = -1;
        }
        else if (actions.DiscreteActions[1] == 2f)
        {
            turnAmount = 1f;
        }


        rigid.MovePosition(transform.position + transform.forward * forwardAmount * moveSpeed * Time.fixedDeltaTime);
        transform.Rotate(transform.up * turnAmount * turnSpeed * Time.fixedDeltaTime);

        if (MaxStep > 0) AddReward(-1f / MaxStep);

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        int forwardAction = 0;
        int turnAction = 0;

        if (Input.GetKey(KeyCode.W))
        {
            forwardAction = 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            turnAction = 1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            turnAction = 2;
        }


        actionsOut.DiscreteActions.Array[0] = forwardAction;
        actionsOut.DiscreteActions.Array[1] = turnAction;

    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Fish"))
        {
            EatFish(collision.gameObject);
        }
        else if(collision.collider.CompareTag("BabyPenguin"))
        {
            RegurgitateFish();
        }
    }

    private void EatFish(GameObject fish)
    {
        if (isFull) return;
        isFull = true;
        penguinArea.RemoveFish(fish);
        AddReward(1);
    }
    private void RegurgitateFish()
    {
        if(!isFull) return;
        isFull = false;

        GameObject fish = Instantiate(fishPrefab.gameObject);
        fish.transform.parent = transform.parent;
        fish.transform.position = baby.transform.position;
        Destroy(fish, 4f);
        GameObject heart = Instantiate(heartPrefab.gameObject);
        heart.transform.parent = transform.parent;
        heart.transform.position = baby.transform.position+Vector3.up;
        Destroy(heart, 4f);

        AddReward(1);
        if(penguinArea.FishRemaining<=0)
            EndEpisode();
    }
}
