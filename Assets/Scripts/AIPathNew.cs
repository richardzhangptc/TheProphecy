using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;

namespace Pathfinding {
	using Pathfinding.RVO;
	using Pathfinding.Util;

    public class AIPathNew : MonoBehaviour
    {
        private Seeker seeker;
        public Transform target; //constantly update target position and seeker paths, check for collisions and modify waypoints appropriately
        [SerializeField] private float recalculationTime = 2f; //in seconds, how long till you recalculate the path


        public Path path; //the calculated path
        public float waypointDistanceThreshold = 1f; //distance from waypoint needed to cross in order to switch to the next waypoint


        private int currentWaypoint = 0; //the waypoint we are currently moving toward
        public Vector3 currentWaypointPosition;
        public Vector3 dirToWaypoint;

        public bool inCombat = false;
        public bool flee = false;

        public event Action OnWaypointReached;
        
        void Start ()
        {
            seeker = GetComponent<Seeker>();
            InvokeRepeating("UpdatePath", UnityEngine.Random.Range(0f,1f), recalculationTime);
        }


        public void Disengage()
        {
            target = null;
        }

        private void UpdatePath() //invoked repeatedly in start()
        {
            if (target == null)
            {
                return;
            }
            

            Transform currentTarget = target;
            float distanceToTarget = Vector3.Distance(target.position, transform.position);
            
            if (flee == true)
            {
                FleePath fleePath = FleePath.Construct(transform.position, target.position, 10000);
                fleePath.aimStrength = 1;
                seeker.StartPath(fleePath, OnPathComplete);
            }
            else
            {
                seeker.StartPath (transform.position, currentTarget.position, OnPathComplete);
            }
        }

        private void Update()
        {
           
            if(path == null)
            {
                return; //no path to move to yet
            }

            if(currentWaypoint >= path.vectorPath.Count)
            {
                return; //end of path reached
            }

            currentWaypointPosition = (Vector2)path.vectorPath[currentWaypoint];
            

            if(Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < waypointDistanceThreshold)
            {
                if (path.vectorPath[currentWaypoint] == path.vectorPath.Last())
                {
                    OnWaypointReached?.Invoke();
                    path = null;
                    dirToWaypoint = Vector3.zero;
                }
                currentWaypoint++;
                return; 
            }

            dirToWaypoint = (Vector2)(currentWaypointPosition - transform.position).normalized;
        }

        
        private void OnPathComplete (Path p) 
        {
            if(p.error == false)
            {
                path = p;
                currentWaypoint = 0; 
            }
        }


        private void OnDestroy()
        {
            CancelInvoke("UpdatePath");
        }
    }

}