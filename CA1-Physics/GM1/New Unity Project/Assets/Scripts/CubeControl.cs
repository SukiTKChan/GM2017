using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://unity3d.com/learn/tutorials/temas/multiplayer-networking/shooting-single-player 

public class CubeControl : MonoBehaviour
{
    public float speed = 5.0f;

    public GameObject Bomb;
    public Transform bombSpawn;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 100.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireBomb();
        }


    }
    
    private void FireBomb()
    {
        var bomb = (GameObject)Instantiate(
            Bomb, 
            bombSpawn.position,
            bombSpawn.rotation);

        
        // Add velocity to the bullet
        bomb.GetComponent<SpherePhysicsCode>().velocity = bomb.transform.forward * 15;

        // Destroy the bullet after 2 seconds
        //Destroy(bomb, 5.0f);

    }
}
