
using BaseLibrary.Managers;
using BaseLibrary.StateMachine;
using GeneralImplementations.Data;
using Managers;
using UnityEditor;
using UnityEngine;

namespace GeneralImplementations.Managers
{
    public class RaycastExecutor : UpdateExecutorBase, IRaycastExecutor
    {

        // public LayerMask layersToCheck;
        public Transform targetFrom;
        //private RaycastData raycastdata;
        public ScriptableEventListener currentPreviewObjectChangedListener;
        private RaycastHit raycastHitOutput;

        public void Init()
        {
            isExecuting = false;

            if (targetFrom == null)
            {
                targetFrom = GameObject.FindGameObjectWithTag(Raycastdata.targetTag).transform;
            }
            // layersToCheck = Raycastdata.defaultLayerToScan;
           // Debug.LogError("MonoBehaviourHookup.name: " + MonoBehaviourHookup.name);
        }

        public override void Awake()
        {
            base.Awake();
            Debug.LogError("RaycastExecutor.Awake()");
        }
        public void InitEventListeners()
        {
            //hitMissListeners = new BoolEventListener("BuildRaycastHit", transform, raycastdata.hitMissEvents.scriptableEventTrue, HandlePreviewAvailable, raycastdata.hitMissEvents.scriptableEventFalse, HandlePreviewUnavailable);
            currentPreviewObjectChangedListener = gameObject.AddComponent<ScriptableEventListener>();
            currentPreviewObjectChangedListener.Initialize(SingletonBuildManager.Instance.BuildObjectsHelper.currentchangedEvent, UpdateCurrentPreviewObject);
        }

        private void UpdateCurrentPreviewObject()
        {
            Debug.Log("UpdateCurrentPreviewObject");
        }

        public override void StartExecute()
        {
            base.StartExecute();
            Debug.Log("StartRaycasExecute");

        }

        public override void StopExecute()
        {
            base.StopExecute();
            Debug.Log("StopRaycasExecute");


        }

        public void StartExecute(LayerMask _layersToCheck)
        {
            Raycastdata.defaultLayerToScan = _layersToCheck;
            StartExecute();

        }
        public void SendEvent()
        {

            boolOutput = !boolOutput;
            if (boolOutput)
            {
                if (Raycastdata.stopAfterHit)
                {
                    (this as IUpdateExecutor).StopExecute();

                }
                Raycastdata.hitMissEvents.scriptableEventTrue.Raise();
            }
            else
            {
                Raycastdata.hitMissEvents.scriptableEventFalse.Raise();
            }
        }

        public override void Execute()
        {
            // Debug.Log("Execute Raycast");
            if (boolOutput != Physics.Raycast(targetFrom.position, targetFrom.forward, out MonoBehaviourHookup.raycastHitOutput, Raycastdata.raycastMaxDistance, Raycastdata.defaultLayerToScan))
            {
                Debug.Log("Execute Raycast, output changed: " + boolOutput);
                SendEvent();

            }



        }

        public RaycastHit RaycastHitOutput { get => raycastHitOutput; set => raycastHitOutput = value; }
        public RaycastData Raycastdata { get => SingletonBuildManager.Instance.raycastData; set => SingletonBuildManager.Instance.raycastData = value; }

        void OnDrawGizmos()
        {
            if (RaycastHitOutput.normal == default)
            {
                return;
            }
            Vector3 normal = RaycastHitOutput.normal;
            Vector3 point = RaycastHitOutput.point;
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(point, 0.04f);
            // Gizmos.DrawLine(targetFrom.position, targetFrom.position+ point);
            // Gizmos.DrawLine(targetFrom.position,  point);


            //Handles.Label(point+Vector3.up*0.7f, layersToCheck.value.ToString());
            Handles.Label(point + Vector3.up * 0.5f, RaycastHitOutput.collider != null ? RaycastHitOutput.collider.gameObject.name : "No hit");
            Gizmos.color = new Color(normal.x, normal.y, normal.z);


            Gizmos.DrawLine(point, point + normal);

            // Gizmos.color = Color.white;
            Gizmos.color = Color.white;

            Vector3 cornerAxisVector = targetFrom.position - Vector3.up * targetFrom.position.y + Vector3.forward + Vector3.right;
            Gizmos.DrawSphere(cornerAxisVector, 0.04f);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(cornerAxisVector, cornerAxisVector + Vector3.right * SingletonBuildManager.Instance.gizmosData.mainAxisLength);

            Gizmos.color = Color.green;
            Gizmos.DrawLine(cornerAxisVector, cornerAxisVector + Vector3.up * SingletonBuildManager.Instance.gizmosData.mainAxisLength);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(cornerAxisVector, cornerAxisVector + Vector3.forward * SingletonBuildManager.Instance.gizmosData.mainAxisLength);






        }

    }
}