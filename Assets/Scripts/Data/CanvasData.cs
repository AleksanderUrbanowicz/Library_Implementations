using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseLibrary.Data
{
    [CreateAssetMenu(fileName = "CanvasData_", menuName = "UI/Canvas Data")]

    public class CanvasData : ScriptableObject
    {
       public RenderMode renderMode;
       public bool pixelPerfect;
        public string UICameraTag;
    }
}