
﻿
using BaseLibrary.StateMachine;

using System;
using UnityEngine;
namespace GeneralImplementations.Data
{
    [Serializable]
    [CreateAssetMenu(fileName = "RaycastData_", menuName = "ScriptableSystems/Helpers/Raycast Data Asset")]

    public class RaycastData : ScriptableObject
    {
        //public string layerString;
        public LayerMask defaultLayerToScan;
        public string targetTag;

        public float raycastMaxDistance;
        [Tooltip("Events to notify when hit<->miss")]
        public BoolEventGroup hitMissEvents;
        [Tooltip("Number of Updates to skip  per one executed")]
        public int raycastInterval;
        [Tooltip("Stop executing after first succesfull hit")]
        public bool stopAfterHit;
    }
}