﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestEntity : MonoBehaviour {
    

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space)) GetComponent<EntityEventHandler>().Hit();
        
    }
}
