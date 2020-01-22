using GeneralImplementations.Data;
using UnityEngine;

namespace BaseLibrary.Managers
{
    public interface IPreviewObject
    {
        GameObject GameObjectInstance { get; }
        BoxCollider PreviewCollider { get; }
        MeshRenderer PreviewRenderer { get; }
        void ToggleVisibility(bool b);

        //void Init(ISpawnableBuildObject spawnableBuildObject);
    }
}