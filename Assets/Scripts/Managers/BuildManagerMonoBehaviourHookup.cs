using BaseLibrary.Managers;
using Data;
using GeneralImplementations.Data;
using GeneralImplementations.Managers;
using UnityEngine;

namespace Managers
{
    public class BuildManagerMonoBehaviourHookup : MonoBehaviourHookup
    {
        private RaycastExecutor buildSystemRaycast;
        private BuildPreviewExecutor buildPreviewExecutor;
        private RaycastExecutorData raycastExecutorData;
        private PreviewHelper previewHelper;
        private Transform eventsListenersParent;
        public RaycastExecutor BuildSystemRaycast { get => buildSystemRaycast; set => buildSystemRaycast = value; }
        public BuildPreviewExecutor BuildPreviewExecutor { get => buildPreviewExecutor; set => buildPreviewExecutor = value; }
        public RaycastExecutorData RaycastExecutorData { get { 
                if(raycastExecutorData==null)
                {

                    raycastExecutorData  =   GetComponent<RaycastExecutorData>() != null ?  GetComponent<RaycastExecutorData>(): gameObject.AddComponent<RaycastExecutorData>();
                }
                return raycastExecutorData; } set => raycastExecutorData = value; }
        public PreviewHelper PreviewHelper { get 
            { 
                if(previewHelper==null)
                {

                    //previewHelper = gameObject.AddComponent<PreviewHelper>();
                }
                return previewHelper; } set => previewHelper = value; }

        public Transform EventsListenersParent { get => eventsListenersParent; set => eventsListenersParent = value; }

        

        private void OnGUI()
        {
           
            GUI.color = SingletonBuildManager.IsManagerActive ? Color.green : Color.red;

            GUI.Toggle(new Rect(Vector2.one, Vector2.one * 150), SingletonBuildManager.IsManagerActive, this.name);

            GUI.color = BuildPreviewExecutor.IsExecuting ? Color.green : Color.red;

            GUI.Toggle(new Rect(Vector2.up * 60, Vector2.one * 150), BuildPreviewExecutor.IsExecuting, "buildPreviewExecutor");

            GUI.color = BuildSystemRaycast.IsExecuting ? Color.green : Color.red;
           
            GUI.Toggle(new Rect(Vector2.up * 120, Vector2.one * 150), BuildSystemRaycast.IsExecuting, "buildSystemRaycast");

           // if (GUI.Button(new Rect(Vector2.up * 220, Vector2.one * 90), "Next"))
           // {
               // Debug.LogError("Before Next: " + SingletonBuildManager.Instance.buildObjectsHelper.CurrentBuildObjectIndex);

               // SingletonBuildManager.BuildObjectsHelper.CurrentBuildObjectIndex++;
                //Debug.LogError("Next: " + SingletonBuildManager.Instance.buildObjectsHelper.CurrentBuildObjectIndex);
           // }

        }
    }
}