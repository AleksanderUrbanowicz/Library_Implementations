using BaseLibrary.Data;
using BaseLibrary.Managers;
using BaseLibrary.UI;
using Managers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GeneralImplementations.Managers
{
    [CreateAssetMenu(fileName = "Manager_UI", menuName = "Managers/ Singleton UI Manager")]

    public class SingletonUIManager : ScriptableSingleton<SingletonUIManager, UIManagerMonoBehaviourHookup>
    {

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void BeforeSceneLoad() { CreateSingletonInstance(); }

        [Header("region ThemedUI")]
        #region ThemedUI
        public PluggableUIData currentPluggableUI;
        public PluggableUIData overridePluggableUI;
        public List<PluggableUIData> pluggableUIs;
        public List<ScriptableObject> ipluggableUIs;

        public PluggableUIData PluggableUIData
        {
            get
            {
                if (overridePluggableUI == null)
                {

                    return currentPluggableUI;
                }
                else
                {
                    return overridePluggableUI;
                }
            }
            set { }
        }

        [Header("Color defines")]
        public List<string> colors;

        [Header("Sprite defines")]
        public List<string> sprites;

        private void InitData()
        {
            // spawnableDatas = Resources.LoadAll<ScriptableObject>("").Where(x => x is SpawnableUIData).ToList();
            // ipluggableUIs = Resources.LoadAll<ScriptableObject>("").Where(x => x is IPluggableUI).ToList();


        }
        #endregion ThemedUI

        [Header("region SpawnableUI")]
        #region SpawnableUI
        public bool autoRegisterSpawnableUIs;
        public new SpawnableUIData spawnableUIData;
        public CanvasData canvasData;
        public List<SpawnableUIData> spawnableUIDatas;

        protected override SpawnableUIData SpawnableUIData { get => spawnableUIData; }
        public List<SpawnableUIData> SpawnableUIDatas
        {
            get
            {
                if (spawnableUIDatas == null || spawnableUIDatas.Count == 0)
                {
                    if (autoRegisterSpawnableUIs)
                    {
                        spawnableUIDatas = Resources.LoadAll<SpawnableUIData>("").ToList();
                    }
                    else if (spawnableUIDatas == null)
                    {
                        spawnableUIDatas = new List<SpawnableUIData>();
                    }


                }
                return spawnableUIDatas;
            }
            set => spawnableUIDatas = value;
        }

        #endregion SpawnableUI

        private UIManagerMonoBehaviourHookup monoBehaviourHookup;
        public static UIManagerMonoBehaviourHookup MonoBehaviourHookup
        {
            get
            {
                if (Instance.monoBehaviourHookup == null)
                {
                    Instance.monoBehaviourHookup = _MonoBehaviour.GetComponent<UIManagerMonoBehaviourHookup>();
                }
                return Instance.monoBehaviourHookup;
            }
            set => Instance.monoBehaviourHookup = value;
        }

        public CanvasData CanvasData { get {
                if (canvasData==null)
                {
                    canvasData = Resources.Load<CanvasData>("");
                }
                return canvasData; } set => canvasData = value; }

        public override void Start()
        {
         
            InitMonoBehaviours();
            

        }
        public override void Update()
        {
            GetDebugInput();
        }

        public void GetDebugInput()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                SpawnableUIData.isVisible = !SpawnableUIData.isVisible;
                ToggleUI(SpawnableUIData, SpawnableUIData.isVisible);
            }
            /*
           else if (Input.GetKeyDown(KeyCode.U))
            {
                IsManagerActive = false;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                SingletonUIManager.Instance.ToggleUI(spawnableUIData, true);
            }
            else if (Input.GetKeyDown(KeyCode.H))
            {
                SingletonUIManager.Instance.ToggleUI(spawnableUIData, false);
            }
            */
        }

        public void InitMonoBehaviours()
        {
            //MonoBehaviourHookup.Spawner  = monoBehaviourHookup.GetComponent<UISpawner>() != null ? monoBehaviourHookup.GetComponent<UISpawner>() : monoBehaviourHookup.gameObject.AddComponent<UISpawner>();
            MonoBehaviourHookup.InitSpawner(SpawnableUIDatas);
            //MonoBehaviourHookup.Spawner.Init(spawnableUIDatas);
            // MonoBehaviourHookup.InitSpawner(spawnableUIs);

        }

        private void OnEnable()
        {

            InitData();

        }

        public void RegisterUI(SpawnableUIData _spawnableUIData)
        {

            MonoBehaviourHookup.Spawner.RegisterUI(_spawnableUIData);
        }

        public void ToggleUI(SpawnableUIData _spawnableUIData, bool b)
        {
            string id = _spawnableUIData.GetID;
            if (!MonoBehaviourHookup.Spawner.IsUIRegistered(id))
            {
                MonoBehaviourHookup.Spawner.RegisterUI(_spawnableUIData);
              

            }
            MonoBehaviourHookup.Spawner.ToggleVisibility(id,b);        }

    }
}