using BaseLibrary.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BaseLibrary.Managers
{
    public class UISpawner : MonoBehaviour, IUISpawner
    {
        private Dictionary<string, GameObject> registeredUIs;
        public Transform uiParent;


       
        public void Init( List<SpawnableUIData> _spawnables, Transform parent=null)
        {
            if(parent==null)
            {
                uiParent = new GameObject("UIParent", typeof(Canvas)).transform;

            }
            else
            {
                uiParent = parent;

            }
            foreach(SpawnableUIData ui in _spawnables)
            {

                RegisterUI(ui);
            }
            
        }

        public void RegisterUI(SpawnableUIData _spawnableUI)
        {
            Debug.Log("RegisterUI with id: " + _spawnableUI.GetID+ ", name: " + _spawnableUI.GetPrefab.name + ".");

            if (registeredUIs == null)
            {
                registeredUIs = new Dictionary<string, GameObject>();

            }
            if (registeredUIs.Count>0 && registeredUIs.ContainsKey(_spawnableUI.GetID))
            {
                Debug.Log("ISpawnableUI with id: " + _spawnableUI.GetID + " was already registered.");

            }
            else
            {
                registeredUIs.Add(_spawnableUI.id, InstantiateUI(_spawnableUI));

                Debug.Log("Register ISpawnableUI with id: " + _spawnableUI.GetID + ", name: " + _spawnableUI.GetPrefab.name+"registered: "+ registeredUIs.Count);

            }

        }

        public GameObject GetUIGameObject(string _id)
        {
            if (IsUIRegistered(_id))
            {

                return registeredUIs[_id];
            }

            return null;


        }

        public bool IsUIRegistered(string _id)
        {
            if (registeredUIs.Keys.Contains(_id))
            {

                return true;
            }

            return false ;


        }

        public GameObject InstantiateUI(SpawnableUIData _spawnableUI)
        {
            Debug.Log("InstantiateUI with id: " + _spawnableUI.GetID +".");

            GameObject instance = Object.Instantiate(_spawnableUI.GetPrefab, Vector3.zero, Quaternion.identity, uiParent);
            instance.name = _spawnableUI.GetID;
            instance.transform.localPosition = Vector3.zero;
            instance.SetActive(false);
            return instance;


        }

     
        public void ToggleVisibility(string _id, bool b)
        {
            GameObject go= GetUIGameObject(_id);
            if (go != null)
            {

                go.SetActive(b);
            }
            else
            {
                Debug.LogError("UISpawner.ToggleVisibility(" + _id + "," + b + ") no GameObject with given id.");
            }
        }

    }
}