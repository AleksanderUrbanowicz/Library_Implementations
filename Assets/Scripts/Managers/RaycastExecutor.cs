
using BaseLibrary.Managers;
using BaseLibrary.StateMachine;
using GeneralImplementations.Data;
using System;
using UnityEngine;

namespace GeneralImplementations.Managers
{
    public class RaycastExecutor : MonoBehaviour, IRaycastExecutor
    {
        public bool isRaycasting;
        private int counter = 0;
        //[SerializeField]
        //private RaycastExecutorData raycastExecutorData;
        public bool boolOutput;

        public Vector3 collisionNormal;

        public RaycastHit raycastHitOutput;
        public LayerMask layersToCheck;
        public Transform targetFrom;
        public RaycastData raycastdata;
        public BoolEventListener hitMissListeners;
        public void Init(RaycastData _raycastdata)
        {
            isRaycasting = false;
            raycastdata = _raycastdata;
            if (targetFrom == null)
            {
                targetFrom = GameObject.FindGameObjectWithTag(raycastdata.targetTag).transform;
            }
            layersToCheck = raycastdata.defaultLayerToScan;
            //RaycastExecutorData = _raycastExecutorData;

             //raycastdata.hitMissEvents = new BoolEventGroup(raycastdata.hitMissEvents.scriptableEventTrue, raycastdata.hitMissEvents.scriptableEventFalse);
            //InitEventListeners(_raycastdata);
        }
        /*
        public void InitEventListeners(RaycastData _raycastdata)
        {
            hitMissListeners = new BoolEventListener("Buildycast", transform, _raycastdata.hitMissEvents.scriptableEventTrue, HandleHit, _raycastdata.hitMissEvents.scriptableEventFalse, HandleMiss);

        }
        private void HandleHit()
        {
            Debug.LogError("HandleHit");
           // SingletonBuildManager.
        }
        private void HandleMiss()
        {
            Debug.LogError("HandleMiss");
        }
        */
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

            boolOutput = !boolOutput;
            if (boolOutput)
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
        public void StartExecute()
        {
            isRaycasting = true;
            Debug.LogError("StartRaycastExecute");
        }
        public void StopExecute()
        {
            isRaycasting = false;
            Debug.LogError("StopRaycastExecute");
        }
        public void Execute()
        {
           // Debug.Log("Execute");
            if (boolOutput != Physics.Raycast(targetFrom.position, targetFrom.forward, out raycastHitOutput, raycastdata.raycastMaxDistance, layersToCheck))
            {

                SendEvent();

            }



        }
       // public RaycastExecutorData GetExecutorData()
      //  {
         //   return RaycastExecutorData;
       // }
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
        public bool CheckPreConditions
        {
            get
            {
                return isRaycasting;
            }
        }
        public bool IsExecuting => IsExecuting;
       // public RaycastExecutorData RaycastExecutorData { get => raycastExecutorData; set => raycastExecutorData = value; }
       
    }
}