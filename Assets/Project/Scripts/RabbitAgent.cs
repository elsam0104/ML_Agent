using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RabbitAgent : Agent, IWeaponable
{
    private Rigidbody rigidBody;

    public float moveSpeed = 1.5f;
    public float turnSpeed = 200f;

    [SerializeField]
    private Board board;
    [SerializeField]
    private GameObject gunPrefab;
    [SerializeField]
    private GameObject gun;
    private Vector3 startPos;
    private Animator animator;
    public void GetWeapon()
    {
        AddReward(5);
        //Controller.instance.DataSO.rabbitWin++;
        //if (Controller.instance.DataSO.rabbitWin == 10)
        //{
        //    gun.SetActive(true);
        //    Controller.instance.RabbitWin();
        //    return;
        //}

        Controller.instance.RabbitWin(); //너무 멍청해서 한 번 이기면 바로 끝
        EndEpisode();
    }

    public GameObject ReturnWeapon()
    {
        return gunPrefab;
    }

    public override void Initialize()
    {
        MaxStep = 5000;
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        startPos = transform.position;
    }
    public override void OnEpisodeBegin()
    {
        rigidBody.velocity = rigidBody.angularVelocity = Vector3.zero;
        transform.position = startPos;
        board.ResetBoard();
    }

    public override void CollectObservations(VectorSensor sensor)
    {

    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var action = actions.DiscreteActions;

        Vector3 dir = Vector3.zero;
        Vector3 rot = Vector3.zero;


        switch (action[0])
        {
            case 1: dir = transform.forward; break;
            case 2: dir = -transform.forward; break;
        }

        switch (action[1])
        {
            case 1: rot = -transform.up; break;
            case 2: rot = transform.up; break;
        }
        if (dir != Vector3.zero)
        {
            animator.SetBool("Run", true);
        }
        else
            animator.SetBool("Run", false);

        transform.Rotate(rot, Time.fixedDeltaTime * turnSpeed);
        rigidBody.AddForce(dir * moveSpeed, ForceMode.VelocityChange);

        AddReward(-1.5f / (float)MaxStep);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var action = actionsOut.DiscreteActions;
        actionsOut.Clear();

        if (Input.GetKey(KeyCode.W))
        {
            action[0] = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            action[0] = 2;
        }
        if (Input.GetKey(KeyCode.A))
        {
            action[1] = 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            action[1] = 2;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("WALL"))
        {
            AddReward(-0.1f);
        }
    }

}
