using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=vK6DlWkG4po
public class DoorController : MonoBehaviour 
{
    bool open = false;

	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		if(Input.GetKeyDown("o"))
        {
            if(open)
                this.transform.Translate(0, -3, 0);
            
            else
                this.transform.Translate(0, 3, 0);

            open = !open;
        }
	}
}
