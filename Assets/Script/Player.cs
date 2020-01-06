using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Move
    public float moveSpeed = 10f;
    public float rotateSpeed = 10f;
    Vector3 moveDirection;
    float flySpeed = 30f;

    bool isMovable = true;
    bool isFlying = false;
    bool isRiding = false;
    bool isShooting = false;

    float hieght = 0.5f;

    Vector3 RideDirection;

    GameObject hookGunObj;
    GameObject hookObj;


    void Start()
    {
        hookGunObj = transform.GetChild(0).gameObject;
        hookObj = hookGunObj.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        CheckMove();
        CheckHookShooting();
        //Debug.Log("isRiding:" + isRiding);
        //Debug.Log("isShooting:" + isShooting);
        //CheckRiding();
    }

    #region Move
    //移動控制
    void CheckMove()
    {
        if (isMovable)
        {
            MoveControl();
            FaceDirection();
        }
    }


    void MoveControl()
    {

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            transform.Translate(moveDirection * Time.deltaTime * moveSpeed, Space.World);
        }
    }


    void FaceDirection()
    {
        if (!isShooting && !GetComponent<CharacterThrow>().isThrowing)  // 暫改@@@@@@@@@@@@@@@@@@@@@@@@@@@
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * rotateSpeed);
        }
        //else
        //{
        //    transform.LookAt(hookObj.transform);
        //}
    }


    void CheckHookShooting()
    {
        if (Vector3.Distance(hookObj.transform.position, transform.position) > 0.6) //@@@@@@@@@@@@@@@@@@@@修改過 根據localposition位置 原為0.1 hook在原點
        {
            isShooting = true;
        }
        else
        {
            isShooting = false;
        }
    }

    void MoveableDisable()
    {
        isMovable = false;
    }

    void MoveableEnable()
    {
        isMovable = true;
    }

    #endregion



    /*
    #region Flying
    private void OnCollisionEnter(Collision collision)
    {
        if (isFlying && collision.gameObject.CompareTag("Monster"))
        {
            isShooting = false;
            isFlying = false;
            StopCoroutine("FlyToRidingPosition");
            RideOnMonster(collision.gameObject);
        }
    }

    public void SetRidingTarget(GameObject monster)
    {
        StartCoroutine("FlyToRidingPosition", monster);
    }

    void RideOnMonster(GameObject monster)
    {
        SetRidingPosition(monster);
        SetMonsterToParent(monster);
        isRiding = true;
        HookGunDisable();
        MoveableDisable();
    }

    void SetRidingPosition(GameObject monster)
    {
        Vector3 ridingPosition = monster.transform.GetChild(0).transform.position;
        transform.position = new Vector3(ridingPosition.x,
            ridingPosition.y + hieght,
            ridingPosition.z);
        transform.rotation = monster.transform.rotation;
    }



    IEnumerator FlyToRidingPosition(GameObject monster)
    {
        isFlying = true;

        RideDirection = new Vector3(monster.transform.position.x,
          monster.transform.position.y,
          monster.transform.position.z);

        //Debug.Log("RideDirection_First:" + RideDirection);
        while (transform.position != RideDirection)
        {

            transform.position = Vector3.MoveTowards(transform.position, RideDirection, Time.deltaTime * flySpeed);
            //transform.Translate(RideDirection * Time.deltaTime * flySpeed,Space.World);
            //Debug.Log("RideDirection:" + RideDirection);
            yield return null;
        }
    }


    #endregion

    */
    #region HookGun
    void HookGunDisable()
    {
        hookGunObj.SetActive(false);
    }

    void HookGunEnable()
    {
        hookGunObj.SetActive(true);
    }

    #endregion

    /*
    #region OnRide
    void CheckRiding()
    {
        if (isRiding)
            ExitMonster();
    }

    void ExitMonster()
    {
        if (Input.GetKey("z"))
        {
            isRiding = false;
            HookGunEnable();
            MoveableEnable();
            SetMonsterLeaveParent();
        }
    }
    #endregion

    #region GetMonster
    void SetMonsterToParent(GameObject monster)
    {
        transform.SetParent(monster.transform);
    }

    void SetMonsterLeaveParent()
    {
        transform.SetParent(null);
    }
    #endregion

    */
}




