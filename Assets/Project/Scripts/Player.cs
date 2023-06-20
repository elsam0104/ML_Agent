using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface IWeaponable
{
    void GetWeapon();
    GameObject ReturnWeapon();
}
public class Player : MonoBehaviour, IWeaponable
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private GameObject gunPrefab;

    private GameObject flower;
    private GameObject gun;
    private Animator animator;

    public float turnSpeed = 4.0f;    
    public float moveSpeed = 4.0f; 
    private void Awake()
    {
        flower = transform.GetChild(3).gameObject;
        gun = transform.GetChild(2).gameObject;
        animator = GetComponent<Animator>();

    }
    public void GetWeapon()
    {
        Controller.instance.DataSO.playerWin++;
        if (Controller.instance.DataSO.playerWin == 10)
        {
            gun.SetActive(true);
            Controller.instance.PlayerWin();
            return;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
    }


    void Update()
    {
        float yRotateSize = Input.GetAxis("Mouse X") * turnSpeed;
        float yRotate = transform.eulerAngles.y + yRotateSize;

        transform.eulerAngles = new Vector3(0, yRotate, 0);

        Vector3 move =
            transform.forward * Input.GetAxis("Vertical") +
            transform.right * Input.GetAxis("Horizontal");

        transform.position += move * moveSpeed * Time.deltaTime;
    }


    public GameObject ReturnWeapon()
    {
        return gunPrefab;
    }
}
