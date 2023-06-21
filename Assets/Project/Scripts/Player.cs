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

    //private GameObject flower;
    private GameObject gun;
    private Animator animator;

    public float turnSpeed = 4.0f;
    public float moveSpeed = 4.0f;


    private int dead = 0;
    private int walk = 0;
    private int attatchGun = 0;
    private void Awake()
    {
        //flower = transform.GetChild(3).gameObject;
        gun = transform.GetChild(3).gameObject;
        animator = GetComponent<Animator>();
        dead = Animator.StringToHash("Dead");
        walk = Animator.StringToHash("Walk");
        attatchGun = Animator.StringToHash("AttatchGun");
    }
    public void GetWeapon()
    {
        Controller.instance.DataSO.playerWin++;
        if (Controller.instance.DataSO.playerWin == 10)
        {
            gun.SetActive(true);
            animator.SetTrigger(attatchGun);
            Controller.instance.PlayerWin();
            return;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    void Update()
    {
        float yRotateSize = Input.GetAxis("Mouse X") * turnSpeed;
        float yRotate = transform.eulerAngles.y + yRotateSize;

        transform.eulerAngles = new Vector3(0, yRotate, 0);

        Vector3 move =
            transform.forward * Input.GetAxis("Vertical") +
            transform.right * Input.GetAxis("Horizontal");
        if (move != Vector3.zero)
        {
            animator.SetBool(walk, true);
        }
        else
        {
            animator.SetBool(walk, false);
        }
        transform.position += move * moveSpeed * Time.deltaTime;
    }


    public GameObject ReturnWeapon()
    {
        return gunPrefab;
    }
}
