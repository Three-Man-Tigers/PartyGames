using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CharacterThrow : MonoBehaviour
{
    //丟出力道 及 旋轉速度
    public float throwForce;
    public float throwRotateSpeed;
    private float oriThrowForce = 20f;
    private float oriThrowRotateSpeed = 100f;//初始值額外做
    public bool isThrowing;

    GameObject hookGunObj;
    GameObject hookObj;
    HookGun hookGun;

    private void Awake()
    {
        hookGunObj = transform.GetChild(0).gameObject;
        hookObj = hookGunObj.transform.GetChild(0).gameObject;
        hookGun = hookGunObj.GetComponent<HookGun>();
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
        ThrowControl();
    }

    //旋轉物件 及丟出
    private void ThrowControl()
    {
        if (hookObj.transform.childCount != 0 && Input.GetKeyDown(KeyCode.R))
        {
            isThrowing = true;
            InvokeRepeating("ThrowParameterControl", 0.5f, 0.5f);
        }

        if (hookObj.transform.childCount != 0 && Input.GetKey(KeyCode.R))
        {
            transform.Rotate(Vector3.up * throwRotateSpeed * Time.deltaTime);
            if (throwRotateSpeed >= 1000f)
            {
                CancelInvoke();
            }
        }

        if (hookObj.transform.childCount != 0 && Input.GetKeyUp(KeyCode.R))
        {
            hookObj.transform.GetChild(0).GetComponent<PositionConstraint>().constraintActive = false;  //for PositionConstraint @@@@@@
            hookObj.transform.GetChild(0).GetComponent<Rigidbody>().AddForce(throwForce * this.transform.right, ForceMode.Impulse);
            hookGun.RealeaseChild();
            CancelInvoke();
            throwForce = oriThrowForce;
            throwRotateSpeed = oriThrowRotateSpeed;
            isThrowing = false;
        }

    }

    private void ThrowParameterControl()
    {
        throwForce += 10f;
        throwRotateSpeed += 50f;
    }
}
