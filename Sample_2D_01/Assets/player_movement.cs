using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player_movement : MonoBehaviour
{

    public float movePower = 1f;
    public float jumpPower = 1f;

    Rigidbody2D rigid;
    Animator animator;

    Vector3 movement;
    bool isJumping = false;

    public GameObject player;
    Vector3 StartingPos;
    Quaternion StartingRotate;
    bool isStarted = false;
    static bool isEnded = false;
    static int stageLevel = 0;


    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponentInChildren<Animator>();

        StartingPos = GameObject.FindGameObjectWithTag("Start").transform.position;
        StartingRotate = GameObject.FindGameObjectWithTag("Start").transform.rotation;

        //if(stageLevel > 0)
            //StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && !animator.GetBool("isJumping"))
        {
            isJumping = true;
            animator.SetTrigger("doJumping");
            animator.SetBool("isJumping", true);
        }

    }

    //Physics engine Updates
    void FixedUpdate()
    {
        Move();
        Jump();
    }
    //---------------------------------------------------[Movement Function]
    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            animator.SetBool("isMoving", false);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(-1, 1, 1);
            animator.SetBool("isMoving", true);
        }

        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(1, 1, 1);
            animator.SetBool("isMoving", true);
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    void Jump()
    {
        if (!isJumping)
            return;

        //Prevent Velocity amplification.
        rigid.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
    }
       
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Attach : " + other.gameObject.layer);
        //Landing
        if(other.gameObject.layer == 0 &&rigid.velocity.y < 0)
        {
            animator.SetBool("isJumping", false);
        }
        //Get Coin
        if(other.gameObject.tag == "Coin")
        {
            //Get Money
            BlockStatus coin = other.gameObject.GetComponent<BlockStatus>();
            moneyManager.setMoney((int)coin.value);

            //Remove Object
            Destroy(other.gameObject, 0f);
        }
        if(other.gameObject.tag == "Finish"){
            Debug.Log("Finish START");
            EndGame();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Detach : " + other.gameObject.layer);
    }

    public static void EndGame()
    {
        //Stop Game
        //Time.timeScale = 0f;

        //Stage Set
        //stageLevel++;

        //if(stageLevel ==3)
        //   isEnded = true;
        //SceneManager.LoadScene("Stage01"); //Next Stage
        SceneManager.LoadSceneAsync(1,LoadSceneMode.Single);
    }
       
}
