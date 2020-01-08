using BaseLibrary.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeneralImplementations.StateMachine
{
    public abstract class NavigationAction : Action
    {
        public abstract void Act(NavigationStateController controller);
    }
}