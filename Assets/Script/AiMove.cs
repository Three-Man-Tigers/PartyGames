using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class AiMove : MonoBehaviour
{
    public bool isCaught;
    private GameObject aiTarget;
    private ConstraintSource constraintSource;
    Pathfinding.AIPath aiPathStatus;
    PositionConstraint positionConstraint;
    Rigidbody rigidbody;
    MonsterControl monsterControl;
    GameObject HookObj;

    private void Awake()
    {
        aiPathStatus = gameObject.GetComponent<Pathfinding.AIPath>();
        positionConstraint = gameObject.GetComponent<PositionConstraint>();
        rigidbody = gameObject.GetComponent<Rigidbody>();
        monsterControl = GameObject.Find("Player").GetComponent<MonsterControl>();
        HookObj = GameObject.Find("Hook").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        //設定 ConstrainSource 為 Player 底下之hook
        constraintSource.sourceTransform = GameObject.Find("Player").transform.GetChild(0).GetChild(0);
        constraintSource.weight = 1;
        GetComponent<PositionConstraint>().SetSource(0,constraintSource);
        
        //將 AiTarget 移出子物件
        aiTarget = transform.FindChild("AiTarget").gameObject;
        transform.FindChild("AiTarget").SetParent(null);

        isCaught = false;     
    }

    // Update is called once per frame
    void Update()
    {
        MonsterIsCaught();

        MonsterDestroy();
           
    }

    private void MonsterIsCaught()
    {
        if (gameObject.transform.IsChildOf(HookObj.transform))
            isCaught = true;
        else
            isCaught = false;


        if (isCaught == true)
        {
            aiPathStatus.enabled = false;
            positionConstraint.constraintActive = true;
            positionConstraint.translationOffset = new Vector3(0, 0.5f, 6); //固定相對座標點 Monster與Player
            transform.localRotation = Quaternion.identity; 
            rigidbody.freezeRotation = true;
        }
        else
        {
            //GetComponent<PositionConstraint>().constraintActive = false; 
            aiPathStatus.enabled = true;
            rigidbody.freezeRotation = false;
        }
    }

    private void MonsterDestroy()
    {
        if (gameObject.transform.position.y < -50)
        {
            Destroy(gameObject);
            Destroy(aiTarget);
            monsterControl.monsterList.Remove(gameObject); // 暫時寫在Player身上 須改@@@@@@@@@@@@@@@@@@@@@@@@@@
        }
    }

    
}
