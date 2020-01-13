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


        public RaycastHit RaycastHitOutput { get => MonoBehaviourHookup.RaycastHitOutput; }
        public PreviewData PreviewData { get => SingletonBuildManager.Instance.previewData; set => SingletonBuildManager.Instance.previewData = value; }

        public PreviewHelper PreviewHelper
        {
            get
            {
                if (previewHelper == null)
                {
                    previewHelper = new PreviewHelper(SingletonBuildManager.Instance.BuildObjectsHelper.CurrentBuildObject);

                }
                return previewHelper;
            }
        }

        public void Init(ISpawnableBuildObject _spawnableBuildObject)
        {
            if (_spawnableBuildObject == null)
            {
                _spawnableBuildObject = SingletonBuildManager.Instance.BuildObjectsHelper.CurrentBuildObject;

            }
            IsExecuting = false;
            previewHelper = new PreviewHelper(_spawnableBuildObject);
            InitEventListeners();



        }

        public void InitEventListeners()
        {
            //  hitMissListeners = new BoolEventListener("BuildRaycastHit", transform, SingletonBuildManager.Instance.raycastData.hitMissEvents.scriptableEventTrue, HandleRaycastHit, SingletonBuildManager.Instance.raycastData.hitMissEvents.scriptableEventFalse, HandleRaycastMiss);
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
            PreviewHelper.UpdateCurrentPreview();
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

            if (CheckRaycastDelta(RaycastHitOutput.point, RaycastHitOutput.normal))
            {
                PreviewHelper.MapPreviewToGrid(RaycastHitOutput.point, RaycastHitOutput.normal);


                //  SetPreviewColor();
                //DisplayPreview(RaycastHitOutput.point, RaycastHitOutput.normal);

            }
        }
        public bool CheckRaycastDelta(Vector3 _point, Vector3 _normal)
        {
            // Debug.LogError("CheckRaycastDelta");

            if (PreviewHelper.lastPoint == _point)
            {
                return false;
            }


            float distance = Vector3.Distance(PreviewHelper.previewBuildObject.transform.position, _point);
            if (distance > PreviewData.previewSnapFactor * PreviewData.gridSize)
            {
                PreviewHelper.lastPoint = _point;
                return true;

            }
            return false;
        }

        public override void StartExecute()
        {
            base.StartExecute();
            Debug.LogError("StartPreviewExecute");
            //InitPreview(SingletonBuildManager.Instance.BuildObjectsHelper.CurrentBuildObject);
            //PreviewObject.ChangePreviewObject(SingletonBuildManager.Instance.BuildObjectsHelper.CurrentBuildObject);
            PreviewHelper.previewBuildObject.ToggleVisibility(true);
        }
        public override void StopExecute()
        {
            base.StopExecute();
            Debug.LogError("StopPreviewExecute");

            PreviewHelper.previewBuildObject.ToggleVisibility(false);
        }



        void OnDrawGizmos()
        {
            if (PreviewHelper.lastMappedPoint == default)
            {
                return;
            }
            if (true)
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.red;
            }
            Gizmos.DrawSphere(PreviewHelper.lastMappedPoint, 0.06f);
            // Gizmos.DrawLine(targetFrom.position, targetFrom.position+ point);
            // Gizmos.DrawLine(targetFrom.position,  point);
        }


    }
}