using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControl : MonoBehaviour
{
    //Move
    public float moveSpeed = 10f;
    public float rotateSpeed = 10f;
    Vector3 moveDirection;

    public bool isControlable;
    public bool isMovable = true;
    public bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        InitStatus();
    }

    // Update is called once per frame
    void Update()
    {
        CheckMove();
    }

    #region InitStatus
    void InitStatus()
    {
        if (CompareTag("Player"))
        {
            ControlableEnable();
        }
        else if (CompareTag("Monster"))
        {
            ControlableDisable();
        }
    }
    #endregion

    #region Move
    //移動控制
    void CheckMove()
    {
        if (isControlable && isMovable)
        {
            MoveInput();
            FaceDirection();
        }
    }

    void MoveInput()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            transform.Translate(moveDirection * Time.deltaTime * moveSpeed, Space.World);
        }
    }


    void FaceDirection()
    {
        if (!isAttacking)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * rotateSpeed);
        }
    }

    #endregion

    #region boolStatus

    //isMovable
    public void MoveableDisable()
    {
        isMovable = false;
    }

    public void MoveableEnable()
    {
        isMovable = true;
    }

    //isAttacking
    public void SetAttackingStatusFalse()
    {
        isAttacking = false;
    }

    public void SetAttackingStatusTrue()
    {
        isAttacking = true;
    }

    //isControlable
    public void ControlableDisable()
    {
        isControlable = false;
    }

    public void ControlableEnable()
    {
        isControlable = true;
    }
   
    #endregion
}
