using BaseLibrary.Data;
using BaseLibrary.Managers;
using GeneralImplementations.Data;
using GeneralImplementations.Managers;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class UIManagerMonoBehaviourHookup : MonoBehaviourHookup
    {
        public Canvas UICanvas;
        private UISpawner spawner;

        public UISpawner Spawner { get => spawner; set => spawner = value; }

        public void InitSpawner(List<SpawnableUIData> _spawnableUIs)
        {
            Spawner = gameObject.GetComponent<UISpawner>() != null ? gameObject.GetComponent<UISpawner>() : gameObject.gameObject.AddComponent<UISpawner>();

            UICanvas = new GameObject("UICanvas",typeof( Canvas)).GetComponent<Canvas>();
            UICanvas.transform.parent = spawner.transform;
            InitCanvas();
           // Spawner = gameObject.AddComponent<UISpawner>();
            Spawner.Init( _spawnableUIs, UICanvas.transform);
        }

        public void InitCanvas()
        {
            CanvasData canvasData = SingletonUIManager.Instance.CanvasData;
            if(canvasData==null)
            {
                return;

            }
            UICanvas.worldCamera = GameObject.FindGameObjectWithTag(canvasData.UICameraTag).GetComponent<Camera>();
            UICanvas.renderMode = canvasData.renderMode;
            
        }
        /*
       public Dictionary<string,RectTransform> uICanvases;
       public Dictionary<string, GameObject> uIScreensPrefabs;
       public List<ISpawnableUI> registeredUIScreens;
       public List<ISpawnableUI> registeredUIScreensDictionary;
       
       public void RegisterScreenUI(IScreenUI screenUI)
       {
           if(uIScreensPrefabs==null)
           {
               uIScreensPrefabs = new Dictionary<string, GameObject>();

           }
           if(uIScreensPrefabs.ContainsKey(screenUI.GetID))
           {
               Debug.LogWarning("UIScreen with id: " + screenUI.GetID + " was already registered.");

           }
           else
           {
               uIScreensPrefabs.Add(screenUI.GetID, screenUI.GetPrefab);

               Debug.Log("RegisterScreenUI with id: " + screenUI.GetID + ", name: "+screenUI.GetPrefab.name);


           }

       }
      

        public void RegisterScreenUI(IScreenUI screenUI)
        {
            if (uIScreensPrefabs == null)
            {
                uIScreensPrefabs = new Dictionary<string, GameObject>();

            }
            if (uIScreensPrefabs.ContainsKey(screenUI.GetID))
            {
                Debug.LogWarning("UIScreen with id: " + screenUI.GetID + " was already registered.");

            }
            else
            {
                uIScreensPrefabs.Add(screenUI.GetID, screenUI.GetPrefab);

                Debug.Log("RegisterScreenUI with id: " + screenUI.GetID + ", name: " + screenUI.GetPrefab.name);


            }

        }
         */
        private void InstantiateScreen(string _id)
        {


        }
    }
}