using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

//https://www.youtube.com/watch?v=s67AYDD3j1E
namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class BasicAI : MonoBehaviour
    {
       private NavMeshAgent agent;
       private ThirdPersonCharacter character;
     

        public enum State
        {
            PATROL,
            CHASE
        }
        public State state;
        private bool alive;

        //variables for patrolling
        public GameObject[] waypoints;
        private int waypointInd = 0;
        public float patrolSpeed = 0.5f;


        //variables for chasing
        public float chaseSpeed = 1f;
        private GameObject target;
        
        // Use this for initialization
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

            agent.updatePosition = true;
            agent.updateRotation = false;

            state = BasicAI.State.PATROL;

            alive = true;

           


        }

        void Update()
        {
            StartCoroutine("FSM");
        }
        IEnumerator FSM()
        {
            while(alive)
            {
                switch(state)
                {
                    case State.PATROL:
                        Patrol();
                        break;

                    case State.CHASE:
                        Chase();
                        break;
                }
                yield return null;

            }
        }

        void Patrol()
        {
            agent.speed = patrolSpeed;
            if(Vector3.Distance(this.transform.position,waypoints[waypointInd].transform.position)>=2)
            {
                agent.SetDestination(waypoints[waypointInd].transform.position);
                character.Move(agent.desiredVelocity, false, false);
            }
            else if(Vector3.Distance(this.transform.position,waypoints[waypointInd].transform.position)<=2)
            {
                waypointInd += 1;

                if(waypointInd>=waypoints.Length)
                {
                    waypointInd = 0;
                }
            }
            else
            {
                character.Move(Vector3.zero, false, false);
            }
        }

        void Chase()
        {
            agent.speed = chaseSpeed;
            agent.SetDestination(target.transform.position);
            character.Move(agent.desiredVelocity, false, false);
        }

        void OnTriggerEnter(Collider col)
        {
           
            if(col.tag == "Player")
            {
                state = BasicAI.State.CHASE;
                target = col.gameObject;

            }
            //else
            //{
            //    state = BasicAI.State.PATROL;
            //}
        }

        void OnTriggerExit(Collider col)
        {

            if (col.tag == "Player")
            {
                target = null;
                state = BasicAI.State.PATROL;
            }
        }
    }

}

