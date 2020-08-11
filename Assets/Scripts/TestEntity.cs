using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestEntity : MonoBehaviour {
    

    void Start() {
      
    }

    void Update() {
		  if (Input.GetKeyDown(KeyCode.Space)) GetComponent<EntityEventHandler>().Hit();
        
    }
}
