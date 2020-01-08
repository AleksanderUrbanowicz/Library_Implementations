using BaseLibrary.StateMachine;
using UnityEngine;

namespace GeneralImplementations.StateMachine
{
    [CreateAssetMenu(fileName = "Decision_Employee_NextWaypoint", menuName = "States/Decisions/Characters/Next Waypoint Decision")]

    public class NextWaypointNavigationDecision : NavigationDecision
    {
        public float distance = -1;
        [Tooltip("If destination==null  destination = nearest waypoint, instead of next")]
        public bool findNearest = false; // if null destination
        public override bool Decide(StateControllerMBBase controller)
        {
            NavigationStateController _controller = controller as NavigationStateController;

            if (distance == -1)
            {
                distance = _controller.NavMeshAgent.stoppingDistance;

            }

            if (_controller.NavMeshAgent.destination == null)
            {
                if (findNearest)
                {
                    _controller.SetNearestWaypoint();

                }


                _controller.NavMeshAgent.SetDestination(_controller.NavPointList[_controller.nextNavPoint].position);
                _controller.NavMeshAgent.isStopped = false;
                return false;

            }
            if (_controller.NavMeshAgent.remainingDistance <= distance && !_controller.NavMeshAgent.pathPending)
            {
                _controller.nextNavPoint = (_controller.nextNavPoint + 1) % _controller.NavPointList.Count;
                _controller.NavMeshAgent.SetDestination(_controller.NavPointList[_controller.nextNavPoint].position);
                _controller.NavMeshAgent.isStopped = false;
                return true;
            }

            return false;
        }

        public override bool Decide(NavigationStateController controller)
        {
            throw new System.NotImplementedException();
        }
    }
}