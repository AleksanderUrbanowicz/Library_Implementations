﻿using BaseLibrary.Managers;
using BaseLibrary.StateMachine;

using GeneralImplementations.Data;
using GeneralImplementations.Managers;
using System;
using UnityEngine;

namespace Managers
{
    public class BuildPreviewExecutor : UpdateExecutorBase, IPreviewExecutor
    {
        private int counter = 0;

        private Spawner previewSpawner;
        public BoolEventListener previewAvailableListeners;
        private PreviewObject previewObject;
        private bool isPreviewAvailable;
        public Vector3 lastPoint;
        public BuildManagerMonoBehaviourHookup monoBehaviourHookup;

        public PreviewObject PreviewObject
        {
            get
            {

                return previewObject;
            }
        }
        public Spawner PreviewSpawner { get => previewSpawner = previewSpawner == null ? new Spawner() : previewSpawner; set => previewSpawner = value; }
        public RaycastHit RaycastHitOutput { get => MonoBehaviourHookup.RaycastHitOutput; set => MonoBehaviourHookup.RaycastHitOutput = value; }
        public BuildManagerMonoBehaviourHookup MonoBehaviourHookup
        {
            get
            {
                monoBehaviourHookup = monoBehaviourHookup == null ? monoBehaviourHookup = GetComponent<BuildManagerMonoBehaviourHookup>() : monoBehaviourHookup;
                return monoBehaviourHookup;
            }
            set => monoBehaviourHookup = value;
        }
        public void Init(ISpawnableBuildObject _spawnableBuildObject)
        {
            IsExecuting = false;

            InitEventListeners(SingletonBuildManager.Instance.previewData);
            //previewSpawner = new Spawner();
            SetPreview(_spawnableBuildObject);
        }

        public void InitEventListeners(PreviewData _previewData)
        {
            previewAvailableListeners = new BoolEventListener("BuildPreviewAvailable", transform, previewObject.previewData.buildAvailableEvents.scriptableEventTrue, HandlePreviewAvailable, previewObject.previewData.buildAvailableEvents.scriptableEventFalse, HandlePreviewUnavailable);

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
            PreviewObject.SetPreviewObject(_spawnable, previewObject.previewData);

            TooglePreviewGameObject(false);
        }

        public void SetPreviewColor(bool b)
        {
            PreviewObject.PreviewRenderer.material.color = b ? previewObject.previewData.availableColor : previewObject.previewData.unavailableColor;
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
                if (previewObject.previewData.displayInterval == 0)
                {
                    return true;

                }

                counter++;
                if (counter > previewObject.previewData.displayInterval)
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

        public bool IsExecuting { get; private set; }
        //  public RaycastExecutorData RaycastExecutorData { get => raycastExecutorData; set => raycastExecutorData = value; }

        public void Execute()
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
            if (distance > previewObject.previewData.previewSnapFactor * previewObject.previewData.gridSize)
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
            CurrentPosition -= Vector3.one * previewObject.previewData.offset;
            CurrentPosition /= previewObject.previewData.gridSize;
            CurrentPosition = new Vector3(Mathf.Round(CurrentPosition.x), Mathf.Round(CurrentPosition.y), Mathf.Round(CurrentPosition.z));
            CurrentPosition *= previewObject.previewData.gridSize;
            CurrentPosition += Vector3.one * previewObject.previewData.offset;

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

                previewObject.previewData.buildAvailableEvents.scriptableEventTrue.Raise();
            }
            else
            {
                previewObject.previewData.buildAvailableEvents.scriptableEventFalse.Raise();
            }
        }


    }
}