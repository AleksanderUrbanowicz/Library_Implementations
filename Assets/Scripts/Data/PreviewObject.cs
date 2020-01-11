using BaseLibrary.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeneralImplementations.Data
{
    [Serializable]
    public class PreviewObject : MonoBehaviour
    {
        private Data.ISpawnableBuildObject spawnable;
        private PreviewData previewData;
        private BoxCollider previewCollider;
        private MeshRenderer previewRenderer;
        public float userRotationF;
        public PreviewObject(ISpawnableBuildObject spawnableBuildObject, PreviewData _previewData)
        {
            this.spawnable = spawnableBuildObject;
            previewData = _previewData;
        }
        public void SetPreviewObject(ISpawnableBuildObject _spawnableBuildObject, PreviewData _previewData)
        {
            Debug.Log("SetPreviewObject");
            spawnable = _spawnableBuildObject;
            previewData = _previewData;
            //(spawnableBuildObject as ISpawnable).CreateInstance(transform, Vector3.zero, Quaternion.identity, SpawnableBuildObject);
            //AddPreviewCollider();
              AddPreviewMesh();

        }


        private void AddPreviewMesh()
        {
            Vector3 gridSize = spawnable.GetGridSize;
            Vector3 actualSize = spawnable.GetActualSize;
            Vector3 orientation = spawnable.GetOrientation;
            Vector3 offset = spawnable.GetOffset;
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);


            cube.transform.SetParent(transform);

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
        public MeshRenderer PreviewRenderer { get => previewRenderer; set => previewRenderer = value; }
        public BoxCollider PreviewCollider { get => previewCollider; set => previewCollider = value; }
        public ISpawnableBuildObject SpawnableBuildObject { get => spawnable; set => spawnable = value; }
    }
}