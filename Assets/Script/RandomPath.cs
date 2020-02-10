using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPath : MonoBehaviour
{
    private Vector3 randomPos;
    
    // Start is called before the first frame update
    void Start()
    {
        int decideNum = GameObject.Find("GameManager").GetComponent<MonsterControl>().DecideNum;
        if (decideNum == 0) //up
        {
            randomPos = new Vector3(GameObject.Find("GameManager").GetComponent<MonsterControl>().instantiatePos.x, 0f, -50);
            transform.position = randomPos;
        }
        else if (decideNum == 1) //right
        {
            randomPos = new Vector3(-50, 0f, GameObject.Find("GameManager").GetComponent<MonsterControl>().instantiatePos.z);
            transform.position = randomPos;
        }
        else //left
        {
            randomPos = new Vector3(50, 0f, GameObject.Find("GameManager").GetComponent<MonsterControl>().instantiatePos.z);
            transform.position = randomPos;
        }

        //完全隨機
        //randomPos = new Vector3(Random.Range(-20, 20), 0f, Random.Range(-20, 20));
        //transform.position = randomPos;
        //InvokeRepeating("RandomDestination", 5f, 5f);
    }


    private void RandomDestination()
    {

        randomPos = new Vector3(Random.Range(-20,20), 0f, Random.Range(-20,20));
        transform.position = randomPos;

    }
}
