using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    //丟出力道 及 旋轉速度
    public float throwForce;
    public float throwRotateSpeed;
    private float oriThrowForce = 50f;
    private float oriThrowRotateSpeed = 100f;//初始值額外做
    public bool isThrowing;

    GameObject hookGunObj;
    GameObject hookObj;
    HookGun hookGun;
    MoveControl moveControl;

    //抓取後設定
    public bool isCaught;
    bool catchSet;
    private ConstraintSource constraintSource;



    private void Awake()
    {
        hookGunObj = transform.GetChild(0).gameObject;
        hookObj = hookGunObj.transform.GetChild(0).gameObject;
        hookGun = hookGunObj.GetComponent<HookGun>();
        moveControl =gameObject.GetComponent<MoveControl>();

        //抓取後設定
        constraintSource.sourceTransform = hookObj.transform;
        constraintSource.weight = 1;
        //GetComponent<PositionConstraint>().SetSource(0, constraintSource);
        isCaught = false;
        catchSet = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        isThrowing = false;


        throwForce = oriThrowForce;
        throwRotateSpeed = oriThrowRotateSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        MonsterIsCaught();
        ThrowControl();
    }

    //抓取後設定
    private void MonsterIsCaught()
    {
        if (hookObj.transform.childCount !=0)
            isCaught = true;
        else
            isCaught = false;


        if (isCaught && !catchSet)
        {
            hookObj.transform.GetChild(0).GetComponent<Pathfinding.AIPath>().enabled = false;
            hookObj.transform.GetChild(0).GetComponent<PositionConstraint>().SetSource(0, constraintSource);
            hookObj.transform.GetChild(0).GetComponent<PositionConstraint>().constraintActive = true;
            hookObj.transform.GetChild(0).GetComponent<PositionConstraint>().translationOffset = new Vector3(0, -1f, 6); //固定相對座標點 Monster與Player
            hookObj.transform.GetChild(0).transform.localRotation = Quaternion.identity;
            hookObj.transform.GetChild(0).GetComponent<Rigidbody>().freezeRotation = true;
            catchSet = true;
        }
        else
        {
            //hookObj.transform.GetChild(0).GetComponent<Pathfinding.AIPath>().enabled = true;
            //hookObj.transform.GetChild(0).GetComponent<Rigidbody>().freezeRotation = false;
        }
    }

    //旋轉物件 及丟出
    private void ThrowControl()
    {
        if (hookObj.transform.childCount != 0 && Input.GetKeyDown(KeyCode.R) && Vector3.Distance(hookObj.transform.position, hookGun.hookRope.transform.position) < 0.1)
        {
            isThrowing = true;
            moveControl.SetAttackingStatusTrue();
            InvokeRepeating("ThrowParameterControl", 0.5f, 0.5f);
        }

        if (isThrowing && hookObj.transform.childCount != 0 && Input.GetKey(KeyCode.R))
        {
            transform.Rotate(Vector3.up * throwRotateSpeed * Time.deltaTime);
            if (throwRotateSpeed >= 1000f)
            {
                CancelInvoke();
            }
        }

        if (isThrowing && hookObj.transform.childCount != 0 && Input.GetKeyUp(KeyCode.R))
        {
            hookObj.transform.GetChild(0).GetComponent<PositionConstraint>().constraintActive = false;  //for PositionConstraint @@@@@@

            hookObj.transform.GetChild(0).GetComponent<Pathfinding.AIPath>().enabled = true;//抓取後設定
            hookObj.transform.GetChild(0).GetComponent<Rigidbody>().freezeRotation = false;//抓取後設定

            //hookObj.transform.GetChild(0).gameObject.transform.Translate(throwForce * transform.right * Time.deltaTime,Space.World);
            hookObj.transform.GetChild(0).GetComponent<Rigidbody>().AddForce(throwForce * hookObj.transform.GetChild(0).right , ForceMode.VelocityChange);
            hookGun.RealeaseChild();
            CancelInvoke();
            throwForce = oriThrowForce;
            throwRotateSpeed = oriThrowRotateSpeed;
            isThrowing = false;
            moveControl.SetAttackingStatusFalse();

            catchSet = false;
        }

    }

    private void ThrowParameterControl()
    {
        throwForce += 10f;
        throwRotateSpeed += 50f;
    }
}




