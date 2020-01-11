using BaseLibrary.StateMachine;
namespace GeneralImplementations.StateMachine
{
    public class NavigationDestinationDecision : NavigationDecision
    {
        private float distance = -1;

        public float Distance { get { return distance; } set { distance = value; } }

        public override bool Decide(NavigationStateController controller)
        {

            if (Distance == -1)
            {
                Distance = controller.NavMeshAgent.stoppingDistance;

            }

            if (controller.NavMeshAgent.remainingDistance <= Distance && !controller.NavMeshAgent.pathPending)
            {
                return true;
            }
            return false;
        }

        public override bool Decide(StateControllerMBBase controller)
        {
            return Decide(controller as NavigationStateController);
        }
    }
}