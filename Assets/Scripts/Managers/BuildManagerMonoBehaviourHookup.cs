using BaseLibrary.Managers;
using GeneralImplementations.Managers;
using UnityEngine;

namespace Managers
{
    public class BuildManagerMonoBehaviourHookup : MonoBehaviourHookup
    {
        private RaycastExecutor buildSystemRaycast;
        private BuildPreviewExecutor buildPreviewExecutor;

        public RaycastHit raycastHitOutput;

        public RaycastHit RaycastHitOutput
        {
            get { return raycastHitOutput; }
            set
            {
                Debug.Log("RaycastHitOutput.Set: " + value.point);
                raycastHitOutput = value;
            }
        }

        public RaycastExecutor BuildSystemRaycast { get => buildSystemRaycast; set => buildSystemRaycast = value; }
        public BuildPreviewExecutor BuildPreviewExecutor { get => buildPreviewExecutor; set => buildPreviewExecutor = value; }
        /*  public PreviewBuildObject PreviewObject { get { 

                   return BuildPreviewExecutor.PreviewObject; } set => BuildPreviewExecutor.PreviewObject = value; }

       */
        private void OnGUI()
        {
            if (SingletonBuildManager.Instance.IsManagerActive)
            {
                GUI.color = Color.green;

            }
            else
            {
                GUI.color = Color.red;
            }

            GUI.Toggle(new Rect(Vector2.one, Vector2.one * 150), SingletonBuildManager.Instance.IsManagerActive, "BuildManager");

            if (BuildPreviewExecutor.IsExecuting)
            {
                GUI.color = Color.green;

            }
            else
            {
                GUI.color = Color.red;
            }

            GUI.Toggle(new Rect(Vector2.up * 60, Vector2.one * 150), BuildPreviewExecutor.IsExecuting, "buildPreviewExecutor");

            if (BuildSystemRaycast.IsExecuting)
            {
                GUI.color = Color.green;

            }
            else
            {
                GUI.color = Color.red;
            }
            GUI.Toggle(new Rect(Vector2.up * 120, Vector2.one * 150), BuildSystemRaycast.IsExecuting, "buildSystemRaycast");

            if (GUI.Button(new Rect(Vector2.up * 220, Vector2.one * 90), "Next"))
            {
                //Debug.LogError("Before Next: " + SingletonBuildManager.Instance.buildObjectsHelper.CurrentBuildObjectIndex);

                SingletonBuildManager.Instance.BuildObjectsHelper.CurrentBuildObjectIndex++;
                //Debug.LogError("Next: " + SingletonBuildManager.Instance.buildObjectsHelper.CurrentBuildObjectIndex);
            }

        }
    }
}