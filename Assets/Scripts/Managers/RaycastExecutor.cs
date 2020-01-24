
using BaseLibrary.Managers;
using BaseLibrary.StateMachine;
using Data;
using GeneralImplementations.Data;
using Managers;
using UnityEditor;
using UnityEngine;

namespace GeneralImplementations.Managers
{
    public class RaycastExecutor : UpdateExecutorBase, IRaycastExecutor
    {

        public Transform targetFrom;
        public ScriptableEventListener currentPreviewObjectChangedListener;

        public RaycastData Raycastdata { get => SingletonBuildManager.RaycastData; set => SingletonBuildManager.RaycastData = value; }
        public RaycastExecutorData _RaycastExecutorData { get => SingletonBuildManager.MonoBehaviourHookup.RaycastExecutorData; set => SingletonBuildManager.MonoBehaviourHookup.RaycastExecutorData = value; }

        public  void Init()
        {
            isExecuting = false;

            if (targetFrom == null)
            {
                targetFrom = GameObject.FindGameObjectWithTag(Raycastdata.targetTag).transform;
            }
            
        }

        public override void Awake()
        {
            base.Awake();
            //Debug.Log("RaycastExecutor.Awake()");
            Init();
            InitEventListeners();
        }
        public void InitEventListeners()
        {
            //hitMissListeners = new BoolEventListener("BuildRaycastHit", transform, raycastdata.hitMissEvents.scriptableEventTrue, HandlePreviewAvailable, raycastdata.hitMissEvents.scriptableEventFalse, HandlePreviewUnavailable);
            currentPreviewObjectChangedListener = new GameObject(SingletonBuildManager.BuildObjectsHelper.currentchangedEvent.name +"_Listener").AddComponent<ScriptableEventListener>();
            currentPreviewObjectChangedListener.transform.parent = MonoBehaviourHookup.EventsListenersParent;
            currentPreviewObjectChangedListener.Initialize(SingletonBuildManager.BuildObjectsHelper.currentchangedEvent, UpdateCurrentPreviewObject);
        }

        private void UpdateCurrentPreviewObject()
        {
           // Debug.Log("UpdateCurrentPreviewObject");
        }

        public override void StartExecute()
        {
            base.StartExecute();
            Debug.Log("StartRaycasExecute");

        }

        public override void StopExecute()
        {
            base.StopExecute();
            boolOutput = false;
            Raycastdata.hitMissEvents.scriptableEventFalse.Raise();
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
            if (boolOutput != Physics.Raycast(targetFrom.position, targetFrom.forward, out _RaycastExecutorData.raycastHitOutput, Raycastdata.raycastMaxDistance, Raycastdata.defaultLayerToScan))
            {
                Debug.Log("Execute Raycast, output changed: " + boolOutput);
                SendEvent();

            }

           // Debug.Log(_RaycastExecutorData.RaycastHitOutput.point);

        }


      

        void OnDrawGizmos()
        {
          
            Vector3 normal = _RaycastExecutorData.RaycastHitOutput.normal;
            Vector3 point = _RaycastExecutorData.RaycastHitOutput.point;
           // Gizmos.color = Color.magenta;
           // Gizmos.DrawSphere(point, 0.04f);
            // Gizmos.DrawLine(targetFrom.position, targetFrom.position+ point);
            // Gizmos.DrawLine(targetFrom.position,  point);


            //Handles.Label(point+Vector3.up*0.7f, layersToCheck.value.ToString());
            Handles.Label(point + Vector3.up * 0.5f, _RaycastExecutorData.RaycastHitOutput.collider != null ? _RaycastExecutorData.RaycastHitOutput.collider.gameObject.name : "No hit");
           // Gizmos.color = new Color(normal.x, normal.y, normal.z);

            Handles.color = new Color(normal.x, normal.y, normal.z);
            Handles.DrawAAPolyLine(5, point, point + normal*2f);

          //  Gizmos.DrawLine(point, point + normal);

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