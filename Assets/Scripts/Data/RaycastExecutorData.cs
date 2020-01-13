using System;
using UnityEngine;

namespace GeneralImplementations.Data
{
    [Serializable]
    public class RaycastExecutorData :MonoBehaviour
    {
        public Vector3 lastPoint;
        public Vector3 lastMappedPoint;
        public Transform previewObjectTransform;
        public bool boolOutput;
        
        private RaycastHit raycastHitOutput;

        public Vector3 Point { get => raycastHitOutput.point;  }
        public Vector3 CollisionNormal { get => raycastHitOutput.normal;  }
        public RaycastHit RaycastHitOutput { get {
                return raycastHitOutput; } set => raycastHitOutput = value; }

       
    }
}