using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class MummyAgent : Agent
{
    [SerializeField]
    private Material goodWork;
    [SerializeField]
    private Material badWork;
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private MeshRenderer floor;

    private Rigidbody rigid;
    private Material defaultMat;

    private bool isEnd = false;
    private void SetRandomPos(GameObject obj)
    {
        float randomX = Random.Range(-3.15f, 4.4f);
        float randomZ = Random.Range(-3.15f, 3.25f);
        obj.transform.position = new Vector3(floor.transform.position.x + randomX, obj.transform.position.y, floor.transform.position.z + randomZ);
    }

    IEnumerator ChangeFloorColor(bool isGood)
    {
        isEnd = true;
        if (isGood)
            floor.material = goodWork;
        else
            floor.material = badWork;
        AddReward(isGood ? 1f : -1f);
        yield return new WaitForSeconds(2f);
        EndEpisode();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Target"))
        {
            rigid.velocity = Vector3.zero;
            StartCoroutine(ChangeFloorColor(true));
        }
        if (collision.collider.CompareTag("DeadZone"))
        {
            rigid.velocity = Vector3.zero;
            StartCoroutine(ChangeFloorColor(false));
        }
    }
    #region episode code
    public override void Initialize()
    {
        rigid = GetComponent<Rigidbody>();
        defaultMat = floor.material;
    }
    public override void OnEpisodeBegin()
    {
        rigid.velocity = Vector3.zero;
        floor.material = defaultMat;
        isEnd = false;
        SetRandomPos(target);
        SetRandomPos(gameObject);
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition.x);//1
        sensor.AddObservation(transform.localPosition.z);//1
        sensor.AddObservation(rigid.velocity);//3
        sensor.AddObservation(Vector3.Distance(target.transform.position,transform.position));//1
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        if (isEnd) return;
        float xPos = actions.ContinuousActions[0];
        float zPos = actions.ContinuousActions[1];
        Vector3 movePos = new Vector3(xPos, 0, zPos);
        rigid.MovePosition(transform.position + movePos * 5f * Time.fixedDeltaTime);

        AddReward(-0.001f);
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var ContinuousActionOut = actionsOut.ContinuousActions;
        ContinuousActionOut[0] = -Input.GetAxis("Horizontal");
        ContinuousActionOut[1] = -Input.GetAxis("Vertical");
    }
    #endregion
}
