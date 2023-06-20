using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btn : MonoBehaviour
{
    [SerializeField]
    private Board board;
    private int id = -1;
    private bool isTouched = false;
    private Vector3 touchedVec = Vector3.zero;
    private Vector3 defaultVec = Vector3.zero;

    public int Id { get { return id; } set { id = value; } }

    private void Awake()
    {
        touchedVec = new Vector3(transform.localScale.x, transform.localScale.y - 0.1f, transform.localScale.z);
        defaultVec = transform.localScale;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isTouched)
        {
            board.ResetBtn(id);
            isTouched = false;
            transform.localScale = defaultVec;
            return;
        }
        isTouched = true;
        transform.localScale = touchedVec;
        board.SetBtn(id);
    }
}
