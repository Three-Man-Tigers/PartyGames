using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPath : MonoBehaviour
{
    private Vector3 randomPos;
    
    // Start is called before the first frame update
    void Start()
    {
        randomPos = new Vector3(Random.Range(-20, 20), 0f, Random.Range(-20, 20));
        transform.position = randomPos;
        InvokeRepeating("RandomDestination", 5f, 5f);
    }   

    
    private void RandomDestination()
    {

        randomPos = new Vector3(Random.Range(-20,20), 0f, Random.Range(-20,20));
        transform.position = randomPos;

    }
}
