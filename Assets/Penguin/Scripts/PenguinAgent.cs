using System.Collections;
using System.Collections.Generic;
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

        if (actions.DiscreteActions[1]==1f)
        {
            turnAmount = -1; //왼쪽
        }
        else if (actions.DiscreteActions[1]==2f)
        {
            turnAmount = 1; //오른쪽
        }
        rigid.MovePosition(transform.position + transform.forward*forwardAmount*moveSpeed*Time.fixedDeltaTime);
        transform.Rotate(transform.up*turnAmount*turnSpeed*Time.fixedDeltaTime);

    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        int forwardAction = 0;
        int turnAction = 0;

        if(Input.GetKey(KeyCode.W))
        {
            forwardAction = 1;
        }
        if(Input.GetKey(KeyCode.A)) 
        { 
            turnAction = 1; 
        }
        else if(Input.GetKey(KeyCode.D))
        {
            turnAction= 2;
        }

        actionsOut.DiscreteActions.Array[0] = forwardAction;
        actionsOut.DiscreteActions.Array[1] = turnAction;
    }
}
