using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    List<Btn> btns = new List<Btn>(9);
    [SerializeField]
    List<MeshRenderer> screens = new List<MeshRenderer>(9);
    [SerializeField]
    private Material wrongMaterial;
    [SerializeField]
    private Material rightMaterial;
    [SerializeField]
    private Material defaultMaterial;
    [SerializeField]
    private GameObject targetGameObject;
    [Header("Agent")]
    public bool isAgent = false;
    [SerializeField]
    private Agent agent;
    private IWeaponable target;
    private List<bool> answer = new List<bool>(9);
    private List<bool> right = new List<bool>(9);
    private void Start()
    {
        target = targetGameObject.GetComponent<IWeaponable>();
        ResetBoard();
    }
    public void ResetBoard()
    {
        for (int i = 0; i < btns.Count; i++)
        {
            int tmp = Random.Range(0, 2);
            if (tmp == 0)
                screens[i].tag = "BAD_ITEM";
            else
                screens[i].tag = "GOOD_ITEM";
            screens[i].sharedMaterial = (tmp == 0) ? defaultMaterial : rightMaterial;
            btns[i].Id = i;
            btns[i].GetComponent<MeshRenderer>().sharedMaterial = defaultMaterial;
        }
        for (int i = 0; i < 9; i++)
        {
            if (screens[i].sharedMaterial == rightMaterial)//¸ÂÀ½
            {
                answer.Add(true);
                right.Add(false);
            }
            else
            {
                right.Add(true);
                answer.Add(false);
            }
        }
    }
    private void CheckRight(int idx)
    {
        //if (right[idx]) return;
        bool tmp = screens[idx].sharedMaterial == btns[idx].GetComponent<MeshRenderer>().sharedMaterial;
        right[idx] = tmp;
        if (tmp && isAgent)
            agent.AddReward(0.3f);
        else if (!tmp && isAgent)
            agent.AddReward(-0.3f);
        if (right.TrueForAll((x) => x))
        {
            Instantiate(target.ReturnWeapon(), btns[5].transform.position, Quaternion.identity);
            for (int i = 0; i < 9; i++)
            {
                btns[i].enabled = false;
            }
            this.enabled = false;
        }

    }
    public void SetBtn(int id)
    {
        if (screens[id].sharedMaterial == rightMaterial)
        {
            btns[id].GetComponent<MeshRenderer>().sharedMaterial = rightMaterial;
            CheckRight(id);
        }
        else
        {
            btns[id].GetComponent<MeshRenderer>().sharedMaterial = wrongMaterial;
            CheckRight(id);
        }
    }
    public void ResetBtn(int id)
    {
        btns[id].GetComponent<MeshRenderer>().sharedMaterial = defaultMaterial;
        CheckRight(id);
    }
}
