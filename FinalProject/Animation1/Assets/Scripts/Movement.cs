using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using UnityEngine;
using UnityEngine.Networking;

//https://www.youtube.com/watch?v=YgaLKrSApWM 
public class Movement : NetworkBehaviour
{
    

    private Animator anim;
    

    public GameObject bulletPrefab;
    //public Transform bulletSpawn;
    public Transform GunBulletSpawn;



    //private Transform rightHand;

    //https://www.youtube.com/watch?v=Xx21y9eJq1U&list=TL8IybT0YmsuFnTmZAuwzo4pUAlV6Mhfuu
    //static int idleState = Animator.StringToHash("Base Layer.Idle");
    //static int locoState = Animator.StringToHash("Base Layer.Locomotion");


    // Use this for initialization
    void Start ()
    {
        
        anim = GetComponent<Animator>();
        
        if(isLocalPlayer)
        {
            Camera.main.transform.position = this.transform.position - this.transform.forward * 7 + this.transform.up * 5;
            Camera.main.transform.LookAt(this.transform.position);
            Camera.main.transform.parent = this.transform;
        }
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (anim == null)
        {
            return;
        }

        if (!isLocalPlayer)
        {
            return;
        }

        

        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        move(x, y);


        if(Input.GetKeyDown(KeyCode.Space))
        {

            CmdFire();
            //GetComponent<Animator>().SetBool("Shoot",true);
        
        }
        
        if (Input.GetKey(KeyCode.H))
        { 
            GetComponent<Animator>().SetBool("Wave", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Wave", false);
        }



    }

    //https://unity3d.com/learn/tutorials/topics/multiplayer-networking/adding-multiplayer-shooting 
    [Command]
    void CmdFire()
    {
        
        //create bullet 
        var bullet = (GameObject)Instantiate(
            bulletPrefab, 
            anim.GetBoneTransform(HumanBodyBones.LeftHand).position,//get the enemy's right hand 
            GunBulletSpawn.rotation);

        
        
        //add velocity to bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

        // Spawn the bullet on the Clients
        NetworkServer.Spawn(bullet);

        //destroy bullet 
        //Destroy(bullet, 2.0f);
    }

    private void move(float x, float z)
    {
        anim.SetFloat("VelX", x * 100 * Time.deltaTime);
        anim.SetFloat("VelY", z * 100 * Time.deltaTime);
    }

  
}
