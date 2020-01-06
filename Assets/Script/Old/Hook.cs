using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Hook : MonoBehaviour
{
    public LineRenderer hookObj;                //钩子起始点 附加有LineRenderer组件
    public float hookSpeed = 5f;               //钩子发射速度
    public float totalDistance = 10f;            //钩子最大长度
    public bool isOut = false;                  //钩子发射或者收回标记
    public Transform hookTransform;             //钩子物体（即小球）

    private Collider[] colCollection;           //发射中碰撞到的物体
    private float oriZvalue;
    private bool isStop = true;                 //用来判断钩子是否停止
    private float tempZvalue;

    private Vector3 oriPosValue;
    public Vector3 lockedPosition;

    // Use this for initialization
    void Start()
    {
        oriPosValue = hookTransform.localPosition;

        oriZvalue = hookTransform.localPosition.z;
        tempZvalue = oriZvalue;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.forward);
        CheckLength();                  //检测是否超过钩子长度

        //Debug.Log("oriZvalue : "+oriZvalue);

        if (Input.GetKeyDown(KeyCode.H) && hookTransform.childCount == 0)
        {
            isOut = true;
        }
        //发射钩子
        if (isOut)
        {
            hookTransform.Translate(0, 0, hookSpeed * Time.deltaTime);
            hookObj.SetPosition(1, hookTransform.localPosition);
            checkCollider();                //检测发射过程中碰到的碰撞体
        }
        //收回钩子
        else
        {
            if (hookTransform.localPosition.z > oriZvalue)
            {
               // Debug.Log("Come in back...");
                checkCollider();                //检测发射过程中碰到的碰撞体
                //hookTransform.Translate(0, 0, -hookSpeed * Time.deltaTime);
                hookTransform.localPosition = Vector3.MoveTowards(hookTransform.localPosition , oriPosValue , hookSpeed * Time.deltaTime);
                hookObj.SetPosition(1, hookTransform.localPosition);
            }
            
        }

        //Debug.Log("hookTransform.localPosition.z : " + hookTransform.localPosition.z);

        //Debug.Log("hookTransform.childCount : "+hookTransform.childCount);

        //判断钩子是否为停止状态
        if (hookTransform.childCount > 0)
        {
            //isStop = Mathf.Abs(hookTransform.localPosition.z - tempZvalue) <= 0 ? true : false;
        }
        else
            isStop = false;
        //将敌人够到身前后，放开钩子
        if (Input.GetKeyDown(KeyCode.G) && hookTransform.childCount != 0)
        {
            hookTransform.GetChild(0).GetComponent<PositionConstraint>().constraintActive = false; //@@@@@@@@@@@@@@@@@@@@@@改 for PositionConstraint
            RealeaseChild();
        }

    }

    private void LateUpdate()
    {
        tempZvalue = hookTransform.localPosition.z;
    }
    void CheckLength()
    {
        if (Vector3.Distance(hookTransform.position, hookObj.transform.position) > totalDistance)
        {
            //Debug.Log("isOut False 1");

            isOut = false;
        }
    }


    void checkCollider()
    {
        //对钩子进行球形检测，返回所有碰到或者在球范围内的碰撞体数组     
        //注意将人称控制器及其子物体的Layer修改为不是Default的一个层，否则钩子会检测到自身的碰撞体
        colCollection = Physics.OverlapSphere(hookTransform.position, 0.2f, 1 << LayerMask.NameToLayer("Default"));
        if (colCollection.Length > 0)
        {
            foreach (Collider item in colCollection)
            {
                //将敌人的tag设置为“Enemy”
                if (item.gameObject.tag.Equals("Enemy") && hookTransform.childCount < 1)
                {
                    item.transform.SetParent(hookTransform);
                    lockedPosition = item.transform.localPosition;
                    Debug.Log(lockedPosition);
                }
            }

           // Debug.Log("isOut False 2");
            isOut = false;
        }
    }

    public void RealeaseChild()
    {
        if (hookTransform.childCount > 0)
        {
            for (int i = 0; i < hookTransform.childCount; i++)
            {
                hookTransform.GetChild(i).transform.SetParent(null);
            }
        }
    }

}
