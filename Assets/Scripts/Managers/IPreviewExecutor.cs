using BaseLibrary.Managers;
using GeneralImplementations.Data;
using UnityEngine;

namespace GeneralImplementations.Managers
{
    public interface IPreviewExecutor : IUpdateExecutor
    {
        void Init( ISpawnableBuildObject spawnable);

        void SetPreview(ISpawnableBuildObject spawnable);

        void DisplayPreview(Vector3 _point, Vector3 _normal);

        bool CheckRaycastDelta(Vector3 _point, Vector3 _normal);
    }
}