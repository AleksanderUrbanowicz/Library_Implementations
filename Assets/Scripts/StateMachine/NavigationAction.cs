using BaseLibrary.StateMachine;

namespace GeneralImplementations.StateMachine
{
    public abstract class NavigationAction : Action
    {
        public abstract void Act(NavigationStateController controller);
    }
}