using System;
using UnityEngine;

namespace GeneralImplementations.Data
{
    [Serializable]
    public class RaycastExecutorData :MonoBehaviour
    {
        public Vector3 lastPoint;
        public Vector3 lastMappedPoint;
        private Transform previewObjectTransform;
        public bool boolOutput;

        public RaycastHit raycastHitOutput;

        public Vector3 Point { get => raycastHitOutput.point;  }
        public Vector3 CollisionNormal { get => raycastHitOutput.normal;  }
        public RaycastHit RaycastHitOutput { get {
                return raycastHitOutput; }  }

        public Transform PreviewObjectTransform { get
            { 
                if(previewObjectTransform==null)
                {
                    previewObjectTransform = new GameObject(name).transform;
                }
                return previewObjectTransform; } set => previewObjectTransform = value; }
    }
}