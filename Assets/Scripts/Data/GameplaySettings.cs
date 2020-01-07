using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
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