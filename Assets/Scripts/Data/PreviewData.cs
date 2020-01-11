using BaseLibrary.StateMachine;
using System;
using UnityEngine;

namespace GeneralImplementations.Data
{
    [Serializable]
    [CreateAssetMenu(fileName = "PreviewData_", menuName = "ScriptableSystems/Helpers/Preview Data Asset")]
    public class PreviewData : ScriptableObject
    {
        public Color availableColor = new Color(0, 1.0f, 0, 0.2f);
        public Color unavailableColor = new Color(1.0f, 0, 0, 0.2f);
        public Material previewMaterial;

        [Tooltip("Events to notify when preview object is available<->inavailable to build")]
        public BoolEventGroup buildAvailableEvents = new BoolEventGroup();
        [Tooltip("Number of Updates to skip  per one executed")]
        //public int displayInterval;
        public float offset = 1.0f;
        public float gridSize = 1.0f;
        public float previewSnapFactor = 1.0f;
    }
}