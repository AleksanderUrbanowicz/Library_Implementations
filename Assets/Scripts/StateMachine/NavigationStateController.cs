using BaseLibrary.StateMachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace GeneralImplementations.StateMachine
{
    public class NavigationStateController : StateControllerMBBase
    {
        private NavMeshAgent navMeshAgent;
        private List<Transform> navPointList;
        public int nextNavPoint;

        public Transform Target { get; set; }

        public List<Transform> NavPointList
        {
            get
            {
                if (navPointList == null)
                {
                    // wayPointList = ScriptableSystemManager.Instance.patrolWaypoints;
                }
                return navPointList;
            }
            set
            {
                navPointList = value;
            }
        }
        public NavMeshAgent NavMeshAgent
        {
            get
            {
                if (navMeshAgent == null)
                {
                    navMeshAgent = GetComponent<NavMeshAgent>() != null ? GetComponent<NavMeshAgent>() : gameObject.AddComponent<NavMeshAgent>();

                }
                return navMeshAgent;
            }

        }

        public void SetNearestWaypoint()
        {
            float minDist = float.MaxValue;
            int index = -1;
            for (int i = 0; i < NavPointList.Count; i++)
            {
                Vector3 pos = NavPointList[i].position;
                float temp = Vector3.SqrMagnitude(pos - transform.position);
                if (temp < minDist)
                {

                    minDist = temp;
                    index = i;
                }

            }
            if (index >= 0)
            {

                nextNavPoint = index;
            }
        }
    }
}