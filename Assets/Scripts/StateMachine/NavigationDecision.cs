using BaseLibrary.StateMachine;
namespace GeneralImplementations.StateMachine
{
    public abstract class NavigationDecision : Decision
    {
        public abstract bool Decide(NavigationStateController controller);

    }
}