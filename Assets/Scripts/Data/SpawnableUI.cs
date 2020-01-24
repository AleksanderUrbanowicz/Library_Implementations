using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BaseLibrary.Data
{
    public class SpawnableUI : MonoBehaviour, ISpawnableUI
    {
        public SpawnableUIData spawnableUIData;

        public void Init(SpawnableUIData spawnableUIData)
        {
            this.spawnableUIData = spawnableUIData;
        }

        public GameObject GetPrefab => spawnableUIData.GetPrefab;

        public string GetID => spawnableUIData.GetID;

       
        public void ToggleVisibility(bool b)
        {
            gameObject.SetActive(b);
        }

      
    }
}