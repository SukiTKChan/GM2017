using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private Animator enemyAnim;
   

    // Use this for initialization
    void Start()
    {
        enemyAnim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyAnim == null)
        {
            return;
        }



        //var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        //var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        //transform.Rotate(0, x, 0);
        //transform.Translate(0, 0, z);


        if (Input.GetKey(KeyCode.Return))
        {
            enemyAnim.SetBool("Dying", true);

        }
        if (Input.GetKey(KeyCode.J))
        {
            enemyAnim.SetBool("Jump",true) ;
        }
        else
        {
            enemyAnim.SetBool("Jump", false);
        }
       
    }

    private void move(float x, float y)
    {
        enemyAnim.SetFloat("VelX", x);
        enemyAnim.SetFloat("VelY", y);

    }

   
}
