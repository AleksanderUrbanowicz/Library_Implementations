using BaseLibrary.Managers;
using BaseLibrary.StateMachine;

using GeneralImplementations.Data;
using GeneralImplementations.Managers;
using System;
using UnityEngine;

namespace Managers
{
    public class BuildPreviewExecutor : MonoBehaviour, IPreviewExecutor
    {
        private int counter = 0;
        private RaycastExecutorData raycastExecutorData;
        private PreviewData previewData;
        private Spawner previewSpawner;
        public BoolEventListener previewAvailableListeners;
        private PreviewObject previewObject;
        private bool isPreviewAvailable;

        public PreviewObject PreviewObject { get { 
                
                return previewObject; } }
        public Transform PreviewTransform { get { return PreviewObject.transform; } }
        public GameObject PreviewGameObject { get { return PreviewObject.gameObject; }  }
        public Vector3 PreviewPosition { get => PreviewObject.transform.position; set => PreviewTransform.position = value; }
        //public Quaternion PreviewRotation { get => PreviewObject.transform.rotation; set => PreviewTransform.rotation = value; }
        public Spawner PreviewSpawner { get => previewSpawner = previewSpawner == null ? new Spawner() : previewSpawner; set => previewSpawner = value; }

        public void Init(PreviewData _previewData, ISpawnableBuildObject _spawnableBuildObject)
        {
            IsExecuting = false;
            previewData = _previewData;
            //RaycastExecutorData = _raycastExecutorData;
            
            InitEventListeners(previewData);
            //previewSpawner = new Spawner();
            SetPreview(_spawnableBuildObject);
        }

        public void InitEventListeners(PreviewData _previewData)
        {
            previewAvailableListeners = new BoolEventListener("BuildPreviewAvailable", transform, previewData.buildAvailableEvents.scriptableEventTrue, HandlePreviewAvailable, previewData.buildAvailableEvents.scriptableEventFalse, HandlePreviewUnavailable);

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
            previewObject = PreviewSpawner.CreateInstance(transform, Vector3.zero, Quaternion.identity, _spawnable).AddComponent<PreviewObject>();
            PreviewObject.SetPreviewObject(_spawnable, previewData);

            TooglePreviewGameObject(false);
        }

        public void SetPreviewColor(bool b)
        {
            PreviewObject.PreviewRenderer.material.color = b ? previewData.availableColor : previewData.unavailableColor;
        }

        public void Update()
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

        public bool CheckUpdateConditions
        {
            get
            {
                if (previewData.displayInterval == 0)
                {
                    return true;

                }

                counter++;
                if (counter > previewData.displayInterval)
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

        public bool IsPreviewAvailable { get { 
                if(isPreviewAvailable!= CheckAvailability())
                {
                    isPreviewAvailable = !isPreviewAvailable;
                    SendEvent();
                }
                return isPreviewAvailable; } set => isPreviewAvailable = value; }

        public bool IsExecuting { get; private set; }
        public RaycastExecutorData RaycastExecutorData { get => raycastExecutorData; set => raycastExecutorData = value; }

        public void Execute()
        {
            Debug.LogError("PreviewExecute");
           // SingletonBuildManager.
            if (CheckRaycastDelta(RaycastExecutorData.raycastHitOutput.point, RaycastExecutorData.collisionNormal))
            {
                DisplayPreview(RaycastExecutorData.raycastHitOutput.point, RaycastExecutorData.collisionNormal);

            }
        }

        public void DisplayPreview(Vector3 _point, Vector3 _normal)
        {
            Debug.LogError("DisplayPreview");
            MapPreviewToGrid(_point, _normal);
            
        }

        public bool CheckRaycastDelta(Vector3 _point, Vector3 _normal)
        {
            Debug.LogError("CheckRaycastDelta");

            if (RaycastExecutorData.raycastHitOutput.point == _point || PreviewObject == null)
            {
                return false;
            }


            float distance = Vector3.Distance(PreviewPosition, _point);
            if (distance > previewData.previewSnapFactor * previewData.gridSize)
            {

                return true;

            }
            return false;
        }

        public void MapPreviewToGrid(Vector3 _point, Vector3 _normal, Vector3 orientationVector = new Vector3())
        {
            Debug.LogError("MapPreviewToGrid");
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
            CurrentPosition -= Vector3.one * previewData.offset;
            CurrentPosition /= previewData.gridSize;
            CurrentPosition = new Vector3(Mathf.Round(CurrentPosition.x), Mathf.Round(CurrentPosition.y), Mathf.Round(CurrentPosition.z));
            CurrentPosition *= previewData.gridSize;
            CurrentPosition += Vector3.one * previewData.offset;

            PreviewTransform.position = CurrentPosition;

            PreviewTransform.rotation = Rotation;
        }

        public bool CheckAvailability()
        {

            // collisionCenterDebug = PreviewTransform.position + PreviewObject.PreviewCollider.center;
            Vector3 halfEx = PreviewObject.PreviewCollider.bounds.extents *0.9f;
            Collider[] hitColliders = Physics.OverlapBox(PreviewTransform.position + PreviewObject.PreviewCollider.center, halfEx, PreviewObject.PreviewCollider.transform.rotation, PreviewObject.SpawnableBuildObject.GetObstacleLayerMask);
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
            IsExecuting = b;
            PreviewObject.gameObject.SetActive(b);
        }

        public void StartExecute()
        {
            Debug.LogError("StartPreviewExecute");
            TooglePreviewGameObject(true);
        }

        public void StopExecute()
        {
            Debug.LogError("StopPreviewExecute");

            TooglePreviewGameObject(false);
        }

        public void SendEvent()
        {

            if (isPreviewAvailable)
            {

                previewData.buildAvailableEvents.scriptableEventTrue.Raise();
            }
            else
            {
                previewData.buildAvailableEvents.scriptableEventFalse.Raise();
            }
        }

       
    }
}