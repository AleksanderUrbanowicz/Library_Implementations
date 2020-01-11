using BaseLibrary.StateMachine;
using System.Collections;
using System.Collections.Generic;
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


        public void Update()
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

        public void StartExecute()
        {
            IsExecuting = true;
        }
        public void StopExecute()
        {
            IsExecuting = false;
        }

        public virtual void Execute()
        {
          
        }

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