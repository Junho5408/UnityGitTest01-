using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_movement : MonoBehaviour
{

    public float movePower = 1f;

    Animator animator;
    Vector3 movement;
    int movementFlag = 0;   //0: Idle, 1:Left, 2:Right 
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();

        StartCoroutine("ChangeMovement");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if(movementFlag == 1)
        {
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(1,1,1); 
        }
        else if(movementFlag == 2)
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(-1,1,1);
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }
    IEnumerator ChangeMovement()
    {
        Debug.Log("Front Logic : "+Time.time);
        //Random Change Movement
        movementFlag = Random.Range(0,3);

        //Mapping Animation
        if(movementFlag == 0)
            animator.SetBool("isMoving",false);
        else
            animator.SetBool("isMoving",true);

        yield return new WaitForSeconds(3f);
        Debug.Log("Behind Logic : "+Time.time);
        StartCoroutine("ChangeMovement");

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Attach : " + other.gameObject.layer);
        //Change 
        if(other.gameObject.layer == 0 && movementFlag ==1)
            movementFlag = 2;
        else if(other.gameObject.layer == 0 && movementFlag ==2)
            movementFlag = 1;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Detach : " + other.gameObject.layer);
    }
}
