using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpawn : MonoBehaviour {

    public Transform Sphere;
    int numOfspheres = 20;


    // Use this for initialization
    void Start()
    {

        //Instantiate sphere
        for (int i = 0; i < numOfspheres; i++)
        {
            Instantiate(Sphere,
           //Spawn range
           new Vector3(Random.Range(4.3f, -4.3f),
                       Random.Range(20, 1),
                       Random.Range(3.75f, -4.3f)),
                       Quaternion.identity);

          
        }
            
        

    }

    // Update is called once per frame
    void Update () {
		
	}
}
