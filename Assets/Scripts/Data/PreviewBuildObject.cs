using BaseLibrary.Managers;
using Data;
using Managers;
using System;
using UnityEngine;

namespace GeneralImplementations.Data
{
    [Serializable]
    public class PreviewBuildObject : MonoBehaviour, IPreviewObject
    {
        public BuildObjectData buildObjectData;
        private BoxCollider previewCollider;
        private MeshRenderer previewRenderer;

        public void Init(ISpawnableBuildObject _spawnableBuildObject)
        {

            AddPreviewComponents(buildObjectData);

        }

        private void AddPreviewComponents(BuildObjectData buildObjectData = null)
        {

            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);


            cube.transform.SetParent(GameObjectInstance.transform);

            cube.transform.localScale = new Vector3(buildObjectData.gridSize.x, buildObjectData.gridSize.y, buildObjectData.gridSize.z);
            if (buildObjectData.orientationVector == Vector3.forward)
            {
                cube.transform.localPosition = new Vector3(0, 0, -(buildObjectData.gridSize.z / 2.0f) + buildObjectData.actualSize.z) + buildObjectData.offset;


            }
            else if (buildObjectData.orientationVector == Vector3.up)
            {

                cube.transform.localPosition = new Vector3(0, buildObjectData.gridSize.y / 2.0f, 0) + buildObjectData.offset;

            }
            previewRenderer = cube.gameObject.GetComponent<MeshRenderer>();
            previewRenderer.material = SingletonBuildManager.Instance.previewData.previewMaterial;
            previewCollider = cube.GetComponent<BoxCollider>();
            previewCollider.isTrigger = true;

        }
        public void SetPreviewColor(bool? _b = null)
        {
            bool b = _b == null ? CheckAvailability() : (bool)_b;

            PreviewRenderer.material.color = b ? SingletonBuildManager.Instance.previewData.availableColor : SingletonBuildManager.Instance.previewData.unavailableColor;
        }

        public void ToggleVisibility(bool b)
        {
            PreviewRenderer.enabled = b;
        }

        public bool CheckAvailability()
        {

            // collisionCenterDebug = PreviewTransform.position + PreviewObject.PreviewCollider.center;
            Vector3 halfEx = PreviewCollider.bounds.extents * 0.9f;
            Collider[] hitColliders = Physics.OverlapBox(transform.position + PreviewCollider.center, halfEx, PreviewCollider.transform.rotation, SpawnableBuildObject.BuildObjectData.obstacleLayers);
            int i = 0;


            while (i < hitColliders.Length)
            {
                Collider hitCollider = hitColliders[i];

                if (hitCollider.gameObject != gameObject && hitCollider.gameObject.layer != SpawnableBuildObject.BuildObjectData.layersToBuildOn)
                {
                    //Debug.Log("Unavailable: " + hitCollider.gameObject.name);
                    return false;
                }

                i++;
            }

            return true;

        }
        public MeshRenderer PreviewRenderer { get => previewRenderer; set => previewRenderer = value; }
        public BoxCollider PreviewCollider { get => previewCollider; set => previewCollider = value; }
        public ISpawnableBuildObject SpawnableBuildObject { get => buildObjectData; set => buildObjectData = value as BuildObjectData; }

        public GameObject GameObjectInstance => gameObject;

    }
}