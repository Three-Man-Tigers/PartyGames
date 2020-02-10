using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class AiMove : MonoBehaviour
{
    private GameObject aiTarget;
    MonsterControl monsterControl;

    private void Awake()
    {
        monsterControl = GameObject.Find("GameManager").GetComponent<MonsterControl>();
    }

    // Start is called before the first frame update
    void Start()
    {       
        //將 AiTarget 移出子物件
<<<<<<< HEAD
        aiTarget = transform.FindChild("AiTarget").gameObject;
        transform.FindChild("AiTarget").SetParent(null);
     }
=======
        aiTarget = transform.Find("AiTarget").gameObject;
        transform.Find("AiTarget").SetParent(null);

        isCaught = false;     
    }
>>>>>>> 222c7fec4d530d6be5ddb7bbc592899ddaec2a18

    // Update is called once per frame
    void Update()
    {
        MonsterDestroy();          
    }
    
    private void MonsterDestroy()
    {
        if (gameObject.transform.position.y < -50)
        {
            Destroy(gameObject);
            Destroy(aiTarget);
            monsterControl.monsterList.Remove(gameObject); // 暫時寫在Monster身上 須移至系統架構???@@@@@@@@@@@@@@@@@@@@@@@@@@
        }
    }

    
}
