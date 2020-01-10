using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookGun : MonoBehaviour
{
    
    public GameObject hookObj;
    public LineRenderer hookRope;
    Player player;
    MoveControl moveControl;

    float hookSpeed = 25f;
    float ropeMaxDistance = 15f;

    bool isHookGoOut = false;
    bool isShooting = false;

    Vector3 hookDirection;

    int targetMask;

    private Collider[] colCollection;           //发射中碰撞到的物体

    public event EventHandler ShootStatus;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        moveControl = GetComponentInParent<MoveControl>();
    }

    // Start is called before the first frame update
    void Start()
    {
        targetMask = LayerMask.GetMask("Floor");
    }

    // Update is called once per frame
    void Update()
    {
        CheckHookStatus();
        ShootHook();
        CheckCollider();

    }

    #region ShootHook

    void ShootHook()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) )
        {
            CheckShooting();
        }
    }


    void CheckShooting()
    {
        if (!isShooting && hookObj.transform.childCount == 0 ) //@@@@@@@@@@@@@@@@@@@@修改過
        {
            DoShoot();
        }
    }

    void DoShoot() {
        SetShootingStatusTrue();
        isHookGoOut = true;
        GetMouseClickPosition();
        StartCoroutine("Shooting");
    }

    void GetMouseClickPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000f, targetMask))
        {
            Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red, 2);
            Debug.Log("HitPoint:" + hit.point);
            FindHookDirection(hit.point);
        }
    }

    
    IEnumerator Shooting()
    {
        while (isShooting)
        {
            MoveHookToPosition();
            player.gameObject.transform.LookAt (hookDirection + transform.position);//###
            //Debug.Log("AAA:"+ GetComponentInParent<GameObject>());
            yield return null;
        }
    }

    void FindHookDirection(Vector3 clickPoint)
    {
        Vector3 clickPointWithoutParent = clickPoint - transform.parent.position;
        hookDirection = new Vector3(clickPointWithoutParent.x, 0.5f, clickPointWithoutParent.z);
        Debug.Log("Direction:" + hookDirection);
        
    }

    

    void MoveHookToPosition()
    {
        if (isHookGoOut)
        {
            HookTranslateOut();
        }
        else
        {
            HookTranslateBack();
        }
    }


    void HookTranslateOut()
    {
        hookObj.transform.Translate(hookDirection / hookDirection.magnitude * Time.deltaTime * hookSpeed, Space.World);
        hookRope.SetPosition(1, hookObj.transform.localPosition);
    }

    void HookTranslateBack()
    {
        hookObj.transform.position = Vector3.MoveTowards(hookObj.transform.position, hookRope.transform.position, hookSpeed * Time.deltaTime);
        hookRope.SetPosition(1, hookObj.transform.localPosition);
    }

    void CancelHookShooting() {
        isShooting = false;
        hookObj.transform.position = hookRope.transform.position;
        hookRope.SetPosition(1, hookObj.transform.localPosition);
    }

    #endregion

    #region CheckHookStatus
    void CheckHookStatus()
    {
        CheckHookMaxLength();
        CheckHookIsBack();

    }

    void CheckHookMaxLength()
    {
        if (Vector3.Distance(hookObj.transform.position, hookRope.transform.position) > ropeMaxDistance)
        {
            isHookGoOut = false;
        }
    }

    void CheckHookIsBack()
    {

        if (Vector3.Distance(hookObj.transform.position, hookRope.transform.position) < 0.1)
        {
            SetShootingStatusFalse();
        }
    }

    public void RealeaseChild()  //R聰專用
    {
        if (hookObj.transform.childCount > 0)
        {
            for (int i = 0; i < hookObj.transform.childCount; i++)
            {
                hookObj.transform.GetChild(i).transform.SetParent(null);
            }
        }
    }

    #endregion

    #region CheckHookCollider
    void CheckCollider()
    {
        //对钩子进行球形检测，返回所有碰到或者在球范围内的碰撞体数组     
        //注意将人称控制器及其子物体的Layer修改为不是Default的一个层，否则钩子会检测到自身的碰撞体
        colCollection = Physics.OverlapSphere(hookObj.transform.position, 0.5f, 1 << LayerMask.NameToLayer("Monster"));
        if (colCollection.Length > 0)
        {
            Debug.Log("colCollection.Length" + colCollection.Length);
            foreach (Collider item in colCollection)
            {
               // Debug.Log("item:" + item);
                if (isShooting && item.gameObject.tag.Equals("Monster") && hookObj.transform.childCount < 1) //@@@@@@@@@@@@@@@@@@@@修改過
                {
                    item.transform.SetParent(hookObj.transform);
                    isHookGoOut = false;
                }
                else if (item.gameObject.tag.Equals("Monster") && hookObj.transform.childCount == 1) //@@@@@@@@@@@@@@@@@@@@修改過
                {
                    Debug.Log("times 1");
                    isHookGoOut = false;
                    //HookTranslateBack();
                    //CancelHookShooting();
                    //player.SetRidingTarget(item.gameObject);
                }
                Debug.Log("times 2");
            }
            isHookGoOut = false;
        }
    }
    #endregion

    #region boolStatus
    //isShotting
    void SetShootingStatusTrue()
    {
        isShooting = true;
        moveControl.SetAttackingStatusTrue();
    }

    void SetShootingStatusFalse()
    {
        isShooting = false;
        moveControl.SetAttackingStatusFalse();
    }
    #endregion

}
