﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(20 * Time.deltaTime * Vector3.forward);
        //transform.position = Vector3.Lerp(transform.position, new Vector3(0, 0, 30), Time.deltaTime);
    }


}
