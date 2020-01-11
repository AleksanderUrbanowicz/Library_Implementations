using BaseLibrary.Managers;
using Data;
using GeneralImplementations.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeneralImplementations.Managers
{
    public interface IPreviewExecutor : IUpdateExecutor
    {
        void Init(PreviewData previewData, ISpawnableBuildObject spawnable);

        void SetPreview(ISpawnableBuildObject spawnable);

        void DisplayPreview(Vector3 _point, Vector3 _normal);

        bool CheckRaycastDelta(Vector3 _point, Vector3 _normal);
    }
}