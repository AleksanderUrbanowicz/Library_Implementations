using System;
using UnityEngine;

namespace GeneralImplementations.Data
{
    [Serializable]
    public class RaycastExecutorData
    {

        public bool boolOutput;

        public Vector3 collisionNormal;

        public RaycastHit raycastHitOutput;

        public RaycastExecutorData()
        {
            this.raycastHitOutput = new RaycastHit();
        }
    }
}