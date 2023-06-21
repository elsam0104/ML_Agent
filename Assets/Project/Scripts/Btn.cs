using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btn : MonoBehaviour
{
    [SerializeField]
    private Board board;
    private int id = -1;
    private bool isTouched = false;

    public int Id { get { return id; } set { id = value; } }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTouched)
        {
            board.ResetBtn(id);
            isTouched = false;
            return;
        }
        isTouched = true;
        board.SetBtn(id);
    }
}
