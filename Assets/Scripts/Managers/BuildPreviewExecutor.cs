using BaseLibrary.Managers;
using BaseLibrary.StateMachine;

using GeneralImplementations.Data;
using GeneralImplementations.Managers;
using UnityEngine;

namespace Managers
{
    public class BuildPreviewExecutor : UpdateExecutorBase, IPreviewExecutor
    {

        private Spawner previewSpawner;
        
        private PreviewObject previewObject;
        private bool isPreviewAvailable;
        public Vector3 lastPoint;



        public Spawner PreviewSpawner { get => previewSpawner = previewSpawner == null ? new Spawner() : previewSpawner; set => previewSpawner = value; }
        public RaycastHit RaycastHitOutput { get => MonoBehaviourHookup.RaycastHitOutput; set => MonoBehaviourHookup.RaycastHitOutput = value; }
        public PreviewObject PreviewObject { get => previewObject; set => previewObject = value; }
        public PreviewData PreviewData { get => SingletonBuildManager.Instance.previewData; set => SingletonBuildManager.Instance.previewData = value; }
        public void Init(ISpawnableBuildObject _spawnableBuildObject)
        {
            IsExecuting = false;

            InitEventListeners();
            //previewSpawner = new Spawner();
            SetPreview(_spawnableBuildObject);
        }

        public void InitEventListeners( )
        {
            hitMissListeners = new BoolEventListener("BuildPreviewAvailable", transform, PreviewData.buildAvailableEvents.scriptableEventTrue, HandlePreviewAvailable, PreviewData.buildAvailableEvents.scriptableEventFalse, HandlePreviewUnavailable);

        }

        private void HandlePreviewUnavailable()
        {
            IsPreviewAvailable = false;
            SetPreviewColor(false);

        }

        private void HandlePreviewAvailable()
        {
            IsPreviewAvailable = true;
            SetPreviewColor(true);
        }

        public void SetPreview(ISpawnableBuildObject _spawnable)
        {
            // PreviewObject.SpawnableBuildObject = _spawnable;
            if (PreviewObject != null)
            {
                Destroy(PreviewObject.gameObject);
            }
            // PreviewObject = new PreviewObject(_spawnableBuildObject);
            //PreviewObject = new PreviewObject(_spawnable);
            PreviewObject = PreviewSpawner.CreateInstance(transform, Vector3.zero, Quaternion.identity, _spawnable).AddComponent<PreviewObject>();
            PreviewObject.SetPreviewObject(_spawnable);

            TooglePreviewGameObject(false);
        }

        public void SetPreviewColor(bool b)
        {
            PreviewObject.PreviewRenderer.material.color = b ? PreviewData.availableColor : PreviewData.unavailableColor;
        }

        public override void Update()
        {

            if (!CheckPreConditions)
            {
                return;
            }
            if (PreviewObject.SpawnableBuildObject == null)
            {
                Debug.Log("spawnable==null");
                return;
            }

            if ((this as IUpdateExecutor).CheckUpdateConditions)
            {

                Execute();

            }

        }




        public bool IsPreviewAvailable
        {
            get
            {
                if (isPreviewAvailable != CheckAvailability())
                {
                    isPreviewAvailable = !isPreviewAvailable;
                    SendEvent();
                }
                return isPreviewAvailable;
            }
            set => isPreviewAvailable = value;
        }


        public new void Execute()
        {
            //  Debug.Log("PreviewExecute");

            if (CheckRaycastDelta(RaycastHitOutput.point, RaycastHitOutput.normal))
            {
                DisplayPreview(RaycastHitOutput.point, RaycastHitOutput.normal);

            }
        }

        public void DisplayPreview(Vector3 _point, Vector3 _normal)
        {
            // Debug.LogError("DisplayPreview");
            MapPreviewToGrid(_point, _normal);

        }

        public bool CheckRaycastDelta(Vector3 _point, Vector3 _normal)
        {
            // Debug.LogError("CheckRaycastDelta");

            if (lastPoint == _point || PreviewObject == null)
            {
                return false;
            }


            float distance = Vector3.Distance(PreviewObject.transform.position, _point);
            if (distance > PreviewData.previewSnapFactor * PreviewData.gridSize)
            {
                lastPoint = _point;
                return true;

            }
            return false;
        }

        public void MapPreviewToGrid(Vector3 _point, Vector3 _normal, Vector3 orientationVector = new Vector3())
        {
            //  Debug.LogError("MapPreviewToGrid");
            Quaternion Rotation;
            Vector3 CurrentPosition;
            // collisionNormal = _normal;
            if (orientationVector == default)
            {
                orientationVector.y = 1.0f;

            }
            Rotation = Quaternion.FromToRotation(orientationVector, _normal);
            Rotation *= Quaternion.Euler(orientationVector * PreviewObject.userRotationF);

            CurrentPosition = _point;
            CurrentPosition -= Vector3.one * PreviewData.offset;
            CurrentPosition /= PreviewData.gridSize;
            CurrentPosition = new Vector3(Mathf.Round(CurrentPosition.x), Mathf.Round(CurrentPosition.y), Mathf.Round(CurrentPosition.z));
            CurrentPosition *= PreviewData.gridSize;
            CurrentPosition += Vector3.one * PreviewData.offset;

            PreviewObject.transform.position = CurrentPosition;

            PreviewObject.transform.rotation = Rotation;
        }

        public bool CheckAvailability()
        {

            // collisionCenterDebug = PreviewTransform.position + PreviewObject.PreviewCollider.center;
            Vector3 halfEx = PreviewObject.PreviewCollider.bounds.extents * 0.9f;
            Collider[] hitColliders = Physics.OverlapBox(PreviewObject.transform.position + PreviewObject.PreviewCollider.center, halfEx, PreviewObject.PreviewCollider.transform.rotation, PreviewObject.SpawnableBuildObject.GetObstacleLayerMask);
            int i = 0;


            while (i < hitColliders.Length)
            {
                Collider hitCollider = hitColliders[i];

                if (hitCollider.gameObject != gameObject && hitCollider.gameObject.layer != PreviewObject.SpawnableBuildObject.GetBuildLayerMask)
                {

                    return false;
                }

                i++;
            }

            return true;

        }




        public void TooglePreviewGameObject(bool b)
        {
            // IsExecuting = b;
            PreviewObject.gameObject.SetActive(b);
        }

        public override void StartExecute()
        {
            base.StartExecute();
            Debug.LogError("StartPreviewExecute");
            TooglePreviewGameObject(true);
        }

        public override void StopExecute()
        {
            base.StopExecute();
            Debug.LogError("StopPreviewExecute");

            TooglePreviewGameObject(false);
        }

        public void SendEvent()
        {

            if (isPreviewAvailable)
            {

                PreviewData.buildAvailableEvents.scriptableEventTrue.Raise();
            }
            else
            {
                PreviewData.buildAvailableEvents.scriptableEventFalse.Raise();
            }
        }


    }
}