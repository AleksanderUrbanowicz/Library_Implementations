using BaseLibrary.Managers;
using GeneralImplementations.Data;
using UnityEngine;

namespace GeneralImplementations.Managers
{
    public interface IRaycastExecutor : IUpdateExecutor
    {
        void Init(RaycastData raycastData);

        RaycastHit GetRaycastHit();

        Vector3 GetPoint();
    }
}