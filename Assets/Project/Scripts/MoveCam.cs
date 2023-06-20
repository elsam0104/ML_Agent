using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveCam : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed = 500f;
    [SerializeField]
    private Transform target;

    private float xRotateMove = 0f;
    private float yRotateMove = 0f;
    private float yRotate = 0f;
    private float xRotate = 0f;

    //안씀
    //private void LateUpdate()
    //{
    //    //xRotateMove = -Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed;
    //    yRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;

    //    yRotate = transform.eulerAngles.y + yRotateMove;
    //    //xRotate = transform.eulerAngles.x + xRotateMove; 
    //    //xRotate = xRotate + xRotateMove;


    //    //xRotate = Mathf.Clamp(xRotate, -90, 90); // 위, 아래 고정d
        
    //    //target.eulerAngles = new Vector3(/*xRotate*/0, yRotate, 0);
    //}
}
