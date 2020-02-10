using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControl : MonoBehaviour
{
    
    
    public int monsterNum;
    public GameObject monsterPrefab;
    public List<GameObject> monsterList;
    public Vector3 instantiatePos;
    public int DecideNum;

    private GameObject newMonster;

    

    // Start is called before the first frame update
    void Start()
    {
        monsterNum = 15;
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
                //完全隨機
                //newMonster = Instantiate(monsterPrefab, new Vector3(Random.Range(-15, 15), 10f, Random.Range(-15, 15)), Quaternion.identity);

                //只有上到下
                //instantiatePos = new Vector3(Random.Range(-15, 15), 5f, 15f); 
                //newMonster = Instantiate(monsterPrefab, instantiatePos, Quaternion.Euler(0, 180, 0));

                DecideNum = Random.Range(0, 3);
                instantiatePos = DecideInstantiatPos(DecideNum);
                newMonster = Instantiate(monsterPrefab, instantiatePos, Quaternion.Euler(0,180,0));
                monsterList.Add(newMonster);
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                yield return null;
            }
        }
        
    }

    private Vector3 DecideInstantiatPos(int Num)
    {
        Vector3 result;

        if (Num == 0)
        {
            result = new Vector3(Random.Range(-15, 15), 5f, 15f); //up
            return result;
        }
        else if (Num == 1)
        {
            result = new Vector3(15f, 5f, Random.Range(-15, 15)); //right
            return result;
        }
        else 
        {
            result = new Vector3(-15f, 5f, Random.Range(-15, 15)); //left
            return result;
        }

    }

}
