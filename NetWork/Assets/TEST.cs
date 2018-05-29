using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour {
  
    private float speed = 100;
 
    private void Start()
    {
      
       
        this.GetComponent<Rigidbody2D>().velocity = Vector2 .up  * speed;
    }


}
