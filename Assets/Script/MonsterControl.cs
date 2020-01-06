using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControl : MonoBehaviour
{
    
    
    /// <summary>
    /// 修改名稱@@@@@@@@@@@@@@@@@@@@@@@@@@
    /// </summary>
    public int monsterNum;
    public GameObject monsterPrefab;
    public List<GameObject> monsterList; //monsterList
    private GameObject newMonster;


    //delegate void Test();
    //Test test;


    // Start is called before the first frame update
    void Start()
    {
        monsterNum = 3;
        StartCoroutine(AiInstantiate());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator AiInstantiate()
    {

        while (true)
        {
            if (monsterList.Count < monsterNum)
            {
                newMonster = Instantiate(monsterPrefab, new Vector3(Random.Range(-15, 15), 10f, Random.Range(-15, 15)), Quaternion.identity);
                monsterList.Add(newMonster);
                yield return new WaitForSeconds(2f);
            }
            else
            {
                yield return null;
            }
        }
        
    }


}
