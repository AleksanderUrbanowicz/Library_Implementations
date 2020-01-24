using BaseLibrary.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseLibrary.Data
{

    [CreateAssetMenu(fileName = "SpawnableUIData_", menuName = "UI/Spawnable UI Data")]

    public class SpawnableUIData : ScriptableObject, ISpawnable
    {
        public string id;
        public GameObject objectPrefab;
        public GameObject GetPrefab => objectPrefab;

        public string GetID => id;

        public bool isVisible;
       
    }
}