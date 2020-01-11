
using BaseLibrary.Managers;
using GeneralImplementations.Data;
using Managers;
using UnityEngine;

namespace GeneralImplementations.Managers
{
    public class RaycastExecutor : UpdateExecutorBase, IRaycastExecutor
    {

        public LayerMask layersToCheck;
        public Transform targetFrom;
        public RaycastData raycastdata;
        //public BoolEventListener hitMissListeners;
        public void Init(RaycastData _raycastdata)
        {
            isExecuting = false;
            raycastdata = _raycastdata;
            if (targetFrom == null)
            {
                targetFrom = GameObject.FindGameObjectWithTag(raycastdata.targetTag).transform;
            }
            layersToCheck = raycastdata.defaultLayerToScan;

        }

        public RaycastHit GetRaycastHit()
        {
            return RaycastHitOutput;
        }

        public Vector3 GetPoint()
        {
            return RaycastHitOutput.point;
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

        public new void Execute()
        {
            // Debug.Log("Execute");
            if (boolOutput != Physics.Raycast(targetFrom.position, targetFrom.forward, out MonoBehaviourHookup.raycastHitOutput, raycastdata.raycastMaxDistance, layersToCheck))
            {

                SendEvent();

            }



        }

        public RaycastHit RaycastHitOutput { get => MonoBehaviourHookup.RaycastHitOutput; set => MonoBehaviourHookup.RaycastHitOutput = value; }



        void OnDrawGizmos()
        {
            /*
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(SingletonBuildManager.Instance.BuildPreviewExecutor.PreviewObject.transform.position, 0.04f);
            Gizmos.color = Color.grey;
            Gizmos.DrawSphere(RaycastHitOutput.point, 0.02f);

            Gizmos.DrawLine(RaycastHitOutput.point, RaycastHitOutput.point + RaycastHitOutput.normal);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(SingletonBuildManager.Instance.gizmosData.cornerAxisVector, SingletonBuildManager.Instance.gizmosData.cornerAxisVector + Vector3.right * SingletonBuildManager.Instance.gizmosData.mainAxisLength);

            bool b = MonoBehaviourHookup.BuildPreviewExecutor.PreviewObject.transform.position != null;

            if (b)
            {

                Gizmos.DrawLine(MonoBehaviourHookup.BuildPreviewExecutor.PreviewObject.transform.position, MonoBehaviourHookup.BuildPreviewExecutor.PreviewObject.transform.position + Vector3.right * 1.5f);



                Gizmos.color = Color.green;
                Gizmos.DrawLine(SingletonBuildManager.Instance.gizmosData.cornerAxisVector, SingletonBuildManager.Instance.gizmosData.cornerAxisVector + Vector3.up * SingletonBuildManager.Instance.gizmosData.mainAxisLength);


                Gizmos.DrawLine(MonoBehaviourHookup.BuildPreviewExecutor.PreviewObject.transform.position, MonoBehaviourHookup.BuildPreviewExecutor.PreviewObject.transform.position + Vector3.up * 1.5f);

                Gizmos.color = Color.blue;



                Gizmos.DrawLine(MonoBehaviourHookup.BuildPreviewExecutor.PreviewObject.transform.position, MonoBehaviourHookup.BuildPreviewExecutor.PreviewObject.transform.position + Vector3.forward * 1.5f);
            }
            Gizmos.DrawLine(SingletonBuildManager.Instance.gizmosData.cornerAxisVector, SingletonBuildManager.Instance.gizmosData.cornerAxisVector + Vector3.forward * SingletonBuildManager.Instance.gizmosData.mainAxisLength);


    */

        }

    }
}