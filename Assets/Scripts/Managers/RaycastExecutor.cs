
using UnityEngine;
using BaseLibrary.Managers;
using BaseLibrary.StateMachine;
using GeneralImplementations.Data;

namespace GeneralImplementations.Managers
{
    public class RaycastExecutor : MonoBehaviour, IUpdateExecutor
    {

        public bool isRaycasting;
        //public bool boolOutput;
        private int counter = 0;
        public RaycastExecutorData raycastExecutorData;
        //public Vector3 collisionNormal;
        public LayerMask layersToCheck;
        public Transform targetFrom;
        // public RaycastHit raycastHitOutput;
        public RaycastData raycastdata;
        public void Init(RaycastData _raycastdata)
        {
            isRaycasting = false;
            raycastdata = _raycastdata;
            if (targetFrom == null)
            {
                targetFrom = GameObject.FindGameObjectWithTag("MainCamera").transform;
            }


            raycastdata.hitMissEvents = new BoolEventGroup(raycastdata.hitMissEvents.scriptableEventTrue, raycastdata.hitMissEvents.scriptableEventFalse);

        }

        public void Update()
        {
            if (raycastdata == null)
            {
                Debug.Log("raycastdata==null");
                return;
            }
            if (!CheckPreConditions)
            {
                return;
            }


            if ((this as IUpdateExecutor).CheckUpdateConditions)
            {
                //Debug.Log("CheckUpdateConditions true");
                Execute();

            }

        }


        public void StartExecute(LayerMask _layersToCheck)
        {
            layersToCheck = _layersToCheck;
            StartExecute();

        }


        public void SendEvent()
        {

            raycastExecutorData.boolOutput = !raycastExecutorData.boolOutput;
            if (raycastExecutorData.boolOutput)
            {
                if (raycastdata.stopAfterHit)
                {
                    (this as IUpdateExecutor).StopExecute();

                }
                raycastdata.hitMissEvents.scriptableEventTrue.Raise();
            }
            else
            {
                raycastdata.hitMissEvents.scriptableEventFalse.Raise();
            }
        }

        #region Interface implementations

        public void StartExecute()
        {
            isRaycasting = true;
        }

        public void StopExecute()
        {
            isRaycasting = false;
        }

        public void Execute()
        {
            if (raycastExecutorData.boolOutput != Physics.Raycast(targetFrom.position, targetFrom.forward, out raycastExecutorData.raycastHitOutput, raycastdata.raycastMaxDistance, layersToCheck))
            {

                SendEvent();

            }



        }

        bool IUpdateExecutor.CheckUpdateConditions
        {
            get
            {
                if (raycastdata.raycastInterval == 0)
                {
                    return true;

                }

                counter++;
                if (counter > raycastdata.raycastInterval)
                {

                    counter = 0;
                    //  Debug.Log("Update");
                    return true;
                }
                //  Debug.Log("SkipUpdate");
                return false;
            }
        }
        /*
        bool IConditionalExecutor.CheckEventConditions
        {
            get
            {
                if (isRaycasting && boolOutput != Physics.Raycast(targetFrom.position, targetFrom.forward, out raycastHitOutput, raycastdata.raycastMaxDistance, layersToCheck))
                {

                    return true;

                }
                return false;
            }
        }
        */

        public bool CheckPreConditions
        {
            get
            {
                return isRaycasting;
            }
        }



        #endregion Interface implementations
    }
}