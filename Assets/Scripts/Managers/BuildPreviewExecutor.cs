using BaseLibrary.Managers;
using BaseLibrary.StateMachine;

using GeneralImplementations.Data;
using GeneralImplementations.Managers;
using UnityEngine;

namespace Managers
{
    public class BuildPreviewExecutor : UpdateExecutorBase, IPreviewExecutor
    {
        private PreviewHelper previewHelper;
        public ScriptableEventListener currentPreviewObjectChangedListener;

        public BuildPreviewExecutor()
        {
            Debug.LogError("BuildPreviewExecutor()");
        }

        public override void Awake()
        {
            base.Awake();
            Debug.LogError("BuildPreviewExecutor.Awake()");
        }

        public void Start()
        {
            Debug.LogError("BuildPreviewExecutor.Start()");
        }
       
        public PreviewData PreviewData { get => SingletonBuildManager.Instance.previewData; set => SingletonBuildManager.Instance.previewData = value; }

        public PreviewHelper PreviewHelper
        {
            get
            {
                if (previewHelper == null)
                {
                    Debug.LogError("BuildPreviewExecutor.PreviewHelper== null");
                    previewHelper = gameObject.AddComponent<PreviewHelper>();

                }
                return previewHelper;
            }
        }

        public void Init(ISpawnableBuildObject _spawnableBuildObject)
        {
            Debug.LogError("BuildPreviewExecutor.Init");
            if (_spawnableBuildObject == null)
            {
                Debug.LogError("BuildPreviewExecutor.Init(ISpawnableBuildObject==null)");

                _spawnableBuildObject = SingletonBuildManager.Instance.BuildObjectsHelper.CurrentBuildObject;

            }
            IsExecuting = false;
            if (previewHelper == null)
            {
                Debug.LogError("BuildPreviewExecutor.Init(previewHelper==null)");
                previewHelper = gameObject.AddComponent<PreviewHelper>();
               // previewHelper.
            }
           // Debug.LogError("MonoBehaviourHookup.name: "+ MonoBehaviourHookup.name);
            InitEventListeners();



        }

        public void InitEventListeners()
        {
              hitMissListeners = new BoolEventListener("BuildRaycastHit", transform, SingletonBuildManager.Instance.raycastData.hitMissEvents.scriptableEventTrue, HandleRaycastHit, SingletonBuildManager.Instance.raycastData.hitMissEvents.scriptableEventFalse, HandleRaycastMiss);
            currentPreviewObjectChangedListener = gameObject.AddComponent<ScriptableEventListener>();
            currentPreviewObjectChangedListener.Initialize(SingletonBuildManager.Instance.BuildObjectsHelper.currentchangedEvent, UpdateCurrentPreviewObject);
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
           // PreviewHelper.UpdateCurrentPreview();
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

            if (MonoBehaviourHookup.RaycastHitInterpreter.CheckRaycastDelta())
            {
                MonoBehaviourHookup.RaycastHitInterpreter.MapPreviewToGrid();

                boolOutput = true;
                SendEvent();
                //  SetPreviewColor();
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
            //InitPreview(SingletonBuildManager.Instance.BuildObjectsHelper.CurrentBuildObject);
            //PreviewObject.ChangePreviewObject(SingletonBuildManager.Instance.BuildObjectsHelper.CurrentBuildObject);
            PreviewHelper.previewBuildObject.ToggleVisibility(true);
        }
        public override void StopExecute()
        {
            base.StopExecute();
            Debug.Log("StopPreviewExecute");

            PreviewHelper.previewBuildObject.ToggleVisibility(false);
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
            Gizmos.DrawSphere(MonoBehaviourHookup.RaycastHitInterpreter.RaycastExecutorData.lastMappedPoint, 0.06f);
            // Gizmos.DrawLine(targetFrom.position, targetFrom.position+ point);
            // Gizmos.DrawLine(targetFrom.position,  point);
        }


    }
}