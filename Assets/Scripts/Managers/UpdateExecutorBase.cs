using BaseLibrary.StateMachine;
using Managers;
using UnityEngine;

namespace BaseLibrary.Managers
{
    public abstract class UpdateExecutorBase : MonoBehaviour, IUpdateExecutor
    {

        public bool isExecuting;
        private int counter = 0;
        public int interval;
        public bool boolOutput;
        public BoolEventListener hitMissListeners;
        public BuildManagerMonoBehaviourHookup monoBehaviourHookup;
        public BuildManagerMonoBehaviourHookup MonoBehaviourHookup
        {
            get
            {
                monoBehaviourHookup = monoBehaviourHookup == null ? monoBehaviourHookup = GetComponent<BuildManagerMonoBehaviourHookup>() : monoBehaviourHookup;
                return monoBehaviourHookup;
            }
            set => monoBehaviourHookup = value;
        }

        public bool IsExecuting
        {
            get
            {
                return isExecuting;
            }
            set
            {
                isExecuting = value;
            }
        }



        public virtual void Update()
        {

            if (!CheckPreConditions)
            {
                return;
            }


            if ((this as IUpdateExecutor).CheckUpdateConditions)
            {

                Execute();

            }

        }

        public virtual void StartExecute()
        {
            IsExecuting = true;
        }
        public virtual void StopExecute()
        {
            IsExecuting = false;
        }

        public abstract void Execute();


        public bool CheckUpdateConditions
        {
            get
            {
                if (interval == 0)
                {
                    return true;

                }

                counter++;
                if (counter > interval)
                {

                    counter = 0;
                    //  Debug.Log("Update");
                    return true;
                }
                //  Debug.Log("SkipUpdate");
                return false;
            }
        }

        public bool CheckPreConditions
        {
            get
            {
                return IsExecuting;
            }
        }



    }
}