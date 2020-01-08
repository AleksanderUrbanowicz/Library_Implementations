

using BaseLibrary.StateMachine;
using UnityEngine;
namespace GeneralImplementations.StateMachine
{
    [CreateAssetMenu(fileName = "Action_Employee_GoToNextWaypoint", menuName = "States/Actions/Characters/Go To Next Waypoint Action")]

    public class GoToNextWaypointAction : NavigationAction
    {

        public override void Act(StateControllerMBBase controller)
        {

            NavigationStateController _controller = controller as NavigationStateController;

            if (_controller.NavPointList != null && _controller.NavPointList.Count > 0)
            {
                GoToTarget(_controller);


            }

        }

        public override void Act(NavigationStateController controller)
        {
            GoToTarget(controller);
        }

        private void GoToTarget(NavigationStateController controller)
        {

            controller.NavMeshAgent.destination = controller.NavPointList[controller.nextNavPoint].position;
            controller.NavMeshAgent.isStopped = false;

            if (controller.NavMeshAgent.remainingDistance <= controller.NavMeshAgent.stoppingDistance && !controller.NavMeshAgent.pathPending)
            {
                controller.nextNavPoint = (controller.nextNavPoint + 1) % controller.NavPointList.Count;
            }

        }
    }
}