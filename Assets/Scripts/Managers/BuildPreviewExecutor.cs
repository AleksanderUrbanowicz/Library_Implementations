using BaseLibrary.Managers;
using BaseLibrary.StateMachine;
using Data;
using GeneralImplementations.Data;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeneralImplementations.Managers
{
    public class BuildPreviewExecutor : MonoBehaviour, IPreviewExecutor
    {
        public bool isPreviewActive;
        private int counter = 0;
        public float userRotationF;

        private GameObject previewGameObject;
        private Transform previewTransform;
        private Vector3 collisionNormal;
        private Vector3 currentPosition = Vector3.zero;
        private Quaternion rotation;
        private BoxCollider previewCollider;
        private MeshRenderer previewRenderer;

        private ISpawnableBuildObject spawnable;
        private PreviewData previewData;
        private Spawner previewSpawner;
        private RaycastExecutorData raycastExecutorData;
        private RaycastHit raycastHit;

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
                return isPreviewActive;
            }
        }

        public Transform PreviewTransform { get { return PreviewGameObject.transform; }  }

        public GameObject PreviewGameObject { get { return previewGameObject; } set => previewGameObject = value; }

        public void Init(PreviewData _previewData, ISpawnableBuildObject _spawnableBuildObject)
        {
            isPreviewActive = false;
            previewData = _previewData;
            previewData.buildAvailableEvents = new BoolEventGroup(previewData.buildAvailableEvents.scriptableEventTrue, previewData.buildAvailableEvents.scriptableEventFalse);
            spawnable = _spawnableBuildObject;
            previewSpawner = new Spawner();
            SetPreview(_spawnableBuildObject);
        }

        public void SetPreview(ISpawnableBuildObject _spawnable)
        {
            spawnable = _spawnable;
            if (PreviewGameObject != null)
            {
                Destroy(PreviewGameObject);


            }
            PreviewGameObject = previewSpawner.CreateInstance(transform, Vector3.zero, Quaternion.identity, spawnable);
            //AddPreviewCollider();
            AddPreviewMesh();

            TooglePreviewGameObject(false);
        }

        public void Update()
        {
            if (spawnable == null)
            {
                Debug.Log("spawnable==null");
                return;
            }
            if (!CheckPreConditions)
            {
                return;
            }


            if ((this as IUpdateExecutor).CheckUpdateConditions)
            {
                
                Execute();

            }

        }

        public void Execute()
        {
            if (CheckRaycastDelta(raycastExecutorData.raycastHitOutput.point, raycastExecutorData.collisionNormal))
            {
                DisplayPreview(raycastExecutorData.raycastHitOutput.point, raycastExecutorData.collisionNormal);

            }
        }

        public void DisplayPreview(Vector3 _point, Vector3 _normal)
        {

          
                MapPreviewToGrid(_point, _normal);
            //CheckAvailability();
            


            
        }

        public bool CheckRaycastDelta(Vector3 _point, Vector3 _normal)
        {
            if (raycastHit.point == _point || PreviewGameObject == null)
            {
                return false;
            }


            float distance = Vector3.Distance(currentPosition, _point);
            if (distance > previewData.previewSnapFactor * previewData.gridSize)
            {

                return true;
                //CheckObject();
            }
            return false;
        }

        public void MapPreviewToGrid(Vector3 _point, Vector3 _normal, Vector3 orientationVector =  new Vector3())
        {
            collisionNormal = _normal;
            if(orientationVector==default(Vector3))
            {
                orientationVector.y = 1.0f;

            }
            rotation = Quaternion.FromToRotation(orientationVector, collisionNormal);
            rotation *= Quaternion.Euler(orientationVector * userRotationF);

            currentPosition = _point;
            currentPosition -= Vector3.one * previewData.offset;
            currentPosition /= previewData.gridSize;
            currentPosition = new Vector3(Mathf.Round(currentPosition.x), Mathf.Round(currentPosition.y), Mathf.Round(currentPosition.z));
            currentPosition *= previewData.gridSize;
            currentPosition += Vector3.one * previewData.offset;

            PreviewTransform.position = currentPosition;

            PreviewTransform.rotation = rotation;
        }



        private void AddPreviewCollider()
        {

            previewCollider = PreviewGameObject.AddComponent<BoxCollider>();
            Vector3 gridSize = spawnable.GetGridSize;
            Vector3 actualSize = spawnable.GetActualSize;
            Vector3 orientation = spawnable.GetOrientation;
            previewCollider.isTrigger = true;
            if (orientation == Vector3.forward)
            {
                previewCollider.center = new Vector3(0, 0, -(gridSize.z / 2.0f) + actualSize.z);


            }
            else if (orientation == Vector3.up)
            {

                previewCollider.center = new Vector3(0, gridSize.y / 2.0f, 0);

            }
            previewCollider.size = gridSize;
            

        }

        private void AddPreviewMesh()
        {
            Vector3 gridSize = spawnable.GetGridSize;
            Vector3 actualSize = spawnable.GetActualSize;
            Vector3 orientation = spawnable.GetOrientation;
            Vector3 offset = spawnable.GetOffset;
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);


            cube.transform.SetParent(PreviewTransform);

            cube.transform.localScale = new Vector3(gridSize.x, gridSize.y, gridSize.z);
            if (orientation == Vector3.forward)
            {
                cube.transform.localPosition = new Vector3(0, 0, -(gridSize.z / 2.0f) + actualSize.z);


            }
            else if (orientation == Vector3.up)
            {

                cube.transform.localPosition = new Vector3(0, actualSize.y / 2.0f, 0) + offset;

            }
            previewRenderer = cube.gameObject.GetComponent<MeshRenderer>();
            previewRenderer.material = previewData.previewMaterial;
            previewCollider = cube.GetComponent<BoxCollider>();
            previewCollider.isTrigger = true;
            
        }

        public void TooglePreviewGameObject(bool b)
        {
            isPreviewActive = b;
            previewGameObject.SetActive(b);
        }

        public void StartExecute()
        {
            TooglePreviewGameObject(true);
        }

        public void StopExecute()
        {
            TooglePreviewGameObject(false);
        }

    

   
    }
}