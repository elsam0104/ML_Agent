using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class FloorAgent : Agent
{
    [SerializeField]
    private GameObject ball;
    private Rigidbody ballRigid;
    public override void Initialize() // awake
    {
        ballRigid = ball.GetComponent<Rigidbody>();
    }
    public override void OnEpisodeBegin()
    {
        transform.rotation = new Quaternion(0f,0f,0f,0f);
        transform.Rotate(new Vector3(1, 0, 0), Random.Range(-10f, 10f));
        transform.Rotate(new Vector3(0, 0, 1), Random.Range(-10f, 10f));

        ballRigid.velocity = new Vector3(0f, 0f, 0f);
        ball.transform.localPosition = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f));
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.rotation.z); //1
        sensor.AddObservation(transform.rotation.x); //1
        sensor.AddObservation(ball.transform.position-transform.position); //3
        sensor.AddObservation(ballRigid.velocity); //3
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float zRotation = Mathf.Clamp(actions.ContinuousActions[0],-1f,1f);
        float xRotation = Mathf.Clamp(actions.ContinuousActions[1],-1f,1f);

        transform.Rotate(new Vector3(0, 0, zRotation));
        transform.Rotate(new Vector3(xRotation, 0, 0));

        if(DropBall()) //틀린행동
        {
            SetReward(-1f);
            EndEpisode();
        }
        else
        {
            SetReward(0.1f);
        }
    }

    private bool DropBall()
    {
        return ball.transform.position.y - transform.position.y<-2f 
            || Mathf.Abs(ball.transform.position.x - transform.position.x) > 2.5f 
            || Mathf.Abs(ball.transform.position.z - transform.position.z) > 2.5f;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
    }
}
