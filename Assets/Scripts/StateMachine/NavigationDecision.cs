using BaseLibrary.StateMachine;
using GeneralImplementations.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GeneralImplementations.StateMachine
{
    public abstract class NavigationDecision : Decision
    {
        public abstract bool Decide(NavigationStateController controller);

    }
}