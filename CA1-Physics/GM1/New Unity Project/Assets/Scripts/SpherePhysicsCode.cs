using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpherePhysicsCode : MonoBehaviour
{

    private bool collisionHasOccurred = false;
    private int collisionTimer;
    float CoR = 0.5f; //Coefficient of Restitution 
    float radius;
	
	public float Mass = 1.0f;
    
    public Vector3 acceleration, velocity;

    planeScript plane;
  
  
	// Use this for initialization
	void Start ()
    {
        plane = FindObjectOfType<planeScript>();
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(collisionHasOccurred)
        {
            collisionTimer--;
            if(collisionTimer<0)
            {
                collisionHasOccurred = false;
            }
        }

        acceleration = 9.8f * Vector3.down;

        velocity += acceleration * Time.deltaTime;

        transform.position += velocity * Time.deltaTime;

        //if (transform.position.y < 0.5f)
        //{
        //    transform.position -= velocity * Time.deltaTime;

        //    velocity = -CoR * velocity;
        //}

        Vector3 n = plane.transform.up;

        Vector3 d = transform.position - plane.transform.position;

        float distance = parallelComponent(d, n).magnitude;
        radius = transform.localScale.x / 2;

        if (distance < radius)
        {
            transform.position -= parallelComponent(velocity * Time.deltaTime, n);
            velocity = -CoR * parallelComponent(velocity, n) + perpendicularComponent(velocity, n);

        }



    }

   void OnTriggerEnter(Collider col)
    {   //step 1 detect if there is collision

        if (!collisionHasOccurred)
        {
            SpherePhysicsCode otherSphere = col.gameObject.GetComponent<SpherePhysicsCode>();
            if (otherSphere)
                processCollisionWith(otherSphere);
        }

 

        

    }

    private void processCollisionWith(SpherePhysicsCode otherSphere)
    {
        Vector3 v;
        Vector3 n;
        float m1 = Mass;
        float m2 = otherSphere.Mass;

     
            //collide = true;

            //step 2 cal the collision normal 
            v = transform.position - otherSphere.transform.position;

            //normalised v
            n = v.normalized;

            //step 4 cal conservation of monmentum
            Vector3 u1 = parallelComponent(velocity, n);
            Vector3 thisPerpendicular = perpendicularComponent(velocity, n);
            Vector3 u2 = otherSphere.parallelComponent(otherSphere.velocity, n);
            Vector3 otherPerpendicular = perpendicularComponent(otherSphere.velocity, n);

            Vector3 v1 = u1 * ((m1 - m2) / (m1 + m2)) + u2 * ((2 * m2) / (m1 + m2));
            Vector3 v2 = u2 * ((m2 - m1) / (m1 + m2)) + u1 * ((2 * m1) / (m1 + m2));

        // cal CoR
        v1 *= CoR;
        v2 *= CoR;



        transform.position -= u1 * Time.deltaTime;

        velocity = thisPerpendicular + v1;
        otherSphere.UpdatewithNewVelocityAfterCollision(this,otherSphere.transform.position -u2*Time.deltaTime ,otherPerpendicular + v2);


        
    }

    private void UpdatewithNewVelocityAfterCollision(SpherePhysicsCode spherePhysicsCode,Vector3 newPosition, Vector3 newVelocity)
    {
        
        transform.position = newPosition;
        velocity = newVelocity;
        collisionHasOccurred = true;
        collisionTimer = 3;
        
        


    }

    //step 3 de-composed both velocities
    /// <summary>
    /// Returns the parallel component of vector v parallell to the unit vector n
    /// </summary>
    /// <param name="v">The vector to be de-composed</param>
    /// <param name="n">The vector parallel to the return value</param>
    /// <returns></returns>
    Vector3 parallelComponent(Vector3 v, Vector3 n)
    {
        return Vector3.Dot(v, n.normalized) * n.normalized; 

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="v"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    Vector3 perpendicularComponent(Vector3 v, Vector3 n)
    {
        return v - parallelComponent(v, n);

    }
 
}
