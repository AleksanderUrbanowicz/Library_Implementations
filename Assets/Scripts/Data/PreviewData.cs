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

        public ScriptableEvent gridSnapEvent;
        
        public float offset;
        public float gridSize;
        public float previewSnapFactor;
        [Tooltip("Threshold gridSize to ignore grid")]
        public float gridSizeEpsilon;

        public bool SnapToGrid
        {
            get { return gridSizeEpsilon < gridSize; }
        }
    }
}