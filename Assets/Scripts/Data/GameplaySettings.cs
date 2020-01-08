using System;
using UnityEngine;

namespace GeneralImplementations.Data
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings")]

    [Serializable]
    public class GameplaySettings : ScriptableObject
    {
        public int i = 0;

        private void OnEnable()
        {
            i++;
        }
    }
}