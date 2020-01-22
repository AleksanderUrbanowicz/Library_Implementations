using BaseLibrary.Managers;
using BaseLibrary.StateMachine;

using GeneralImplementations.Data;
using GeneralImplementations.Managers;
using UnityEngine;

namespace Managers
{
    public class BuildPreviewExecutor : UpdateExecutorBase, IPreviewExecutor
    {
        
        private PreviewRaycastHitInterpreter raycastHitInterpreter;
        public ScriptableEventListener currentPreviewObjectChangedListener;
        private RaycastExecutorData raycastExecutorData;


        public PreviewRaycastHitInterpreter RaycastHitInterpreter { get => raycastHitInterpreter; set => raycastHitInterpreter = value; }

        public override void Awake()
        {
            base.Awake();
            Debug.Log("BuildPreviewExecutor.Awake()");
            Init();
        }

        public void Start()
        {
            Debug.Log("BuildPreviewExecutor.Start()");
        }
       
        public PreviewData PreviewData { get => SingletonBuildManager.PreviewData; set => SingletonBuildManager.PreviewData = value; }

        public PreviewHelper PreviewHelper
        {
            get
            {
                return SingletonBuildManager.MonoBehaviourHookup.PreviewHelper;
            }

        }

        public RaycastExecutorData RaycastExecutorData
        {
            get
            {
                if (raycastExecutorData == null)
                {

                    raycastExecutorData = GetComponent<RaycastExecutorData>() != null ? GetComponent<RaycastExecutorData>() : gameObject.AddComponent<RaycastExecutorData>();
                }
                return raycastExecutorData;
            }
            set => raycastExecutorData = value;
        }
      
        public  void Init()
        {
            isExecuting = false;
          
            InitEventListeners();
           // RaycastHitInterpreter = gameObject.AddComponent<PreviewRaycastHitInterpreter>();

        }

        public void InitEventListeners()
        {
              hitMissListeners = new BoolEventListener("BuildRaycastHit", transform, SingletonBuildManager.RaycastData.hitMissEvents.scriptableEventTrue, HandleRaycastHit, SingletonBuildManager.RaycastData.hitMissEvents.scriptableEventFalse, HandleRaycastMiss);
            //currentPreviewObjectChangedListener = gameObject.AddComponent<ScriptableEventListener>();
           // currentPreviewObjectChangedListener.Initialize(SingletonBuildManager.BuildObjectsHelper.currentchangedEvent, UpdateCurrentPreviewObject);
        }

        private void HandleRaycastHit()
        {
            StartExecute();
        }

        private void HandleRaycastMiss()
        {
            StopExecute();
        }

        private void UpdateCurrentPreviewObject()
        {
            Debug.LogError("BuildPreviewExecutor.UpdateCurrentPreviewObject()");
            //PreviewHelper.UpdateCurrentPreview();
        }

        public override void Update()
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
        public override void Execute()
        {
            //  Debug.Log("PreviewExecute");
            
            if (RaycastHitInterpreter.CheckRaycastDelta())
            {
                RaycastHitInterpreter.MapPreviewToGrid();

                boolOutput = true;
                SendEvent();
                 // SetPreviewColor();
                //DisplayPreview(RaycastHitOutput.point, RaycastHitOutput.normal);

            }
            else
            {
                boolOutput = false;

            }
        }
 
        public void SendEvent()
        {

           
            if (boolOutput)
            {
                PreviewData.gridSnapEvent.Raise();
            }
          
        }
        public override void StartExecute()
        {
            base.StartExecute();
            Debug.Log("StartPreviewExecute");
           PreviewHelper.PreviewBuildObject.ToggleVisibility(true);
        }
        public override void StopExecute()
        {
            base.StopExecute();
            Debug.Log("StopPreviewExecute");

            PreviewHelper.PreviewBuildObject.ToggleVisibility(false);
        }



        void OnDrawGizmos()
        {
            //if (previewHelper.da.RaycastHitInterpreter.RaycastExecutorData.lastMappedPoint == default)
            //{
                return;
           // }
            if (true)
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.red;
            }
            Gizmos.DrawSphere(MonoBehaviourHookup.RaycastExecutorData.lastMappedPoint, 0.06f);
            // Gizmos.DrawLine(targetFrom.position, targetFrom.position+ point);
            // Gizmos.DrawLine(targetFrom.position,  point);
        }

        public void Init(ISpawnableBuildObject spawnable)
        {
            throw new System.NotImplementedException();
        }
    }
}