using BaseLibrary.Managers;
using GeneralImplementations.Data;
using GeneralImplementations.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class BuildManagerMonoBehaviourHookup : MonoBehaviourHookup
    {
        public RaycastExecutor buildSystemRaycast;
        public BuildPreviewExecutor buildPreviewExecutor;
        public PreviewObject previewObject;
        public RaycastHit raycastHitOutput;

        public RaycastHit RaycastHitOutput
        {
            get { return raycastHitOutput; }
            set
            {
                Debug.Log("RaycastHitOutput.Set: "+value.point);
                raycastHitOutput = value;
            }
        }

        private void OnGUI()
        {
            if(SingletonBuildManager.Instance.IsManagerActive)
            {
                GUI.color = Color.green;

            }
            else
            {
                GUI.color = Color.red;
            }

            GUI.Toggle(new Rect(Vector2.one , Vector2.one * 150), SingletonBuildManager.Instance.IsManagerActive, "BuildManager");

            if (buildPreviewExecutor.IsExecuting)
            {
                GUI.color = Color.green;

            }
            else
            {
                GUI.color = Color.red;
            }

            GUI.Toggle(new Rect(Vector2.up * 60, Vector2.one * 150), buildPreviewExecutor.IsExecuting, "buildPreviewExecutor");

            if (buildSystemRaycast.IsExecuting)
            {
                GUI.color = Color.green;

            }
            else
            {
                GUI.color = Color.red;
            }
            GUI.Toggle(new Rect(Vector2.up* 120, Vector2.one * 150), buildSystemRaycast.IsExecuting, "buildSystemRaycast");

            if (GUI.Button(new Rect(Vector2.up * 220, Vector2.one * 90), "Next"))
            {
                Debug.LogError("Before Next: " + SingletonBuildManager.Instance.buildObjectsHelper.CurrentBuildObjectIndex);

                SingletonBuildManager.Instance.buildObjectsHelper.CurrentBuildObjectIndex++;
                Debug.LogError("Next: " + SingletonBuildManager.Instance.buildObjectsHelper.CurrentBuildObjectIndex);
            }

        }
    }
}