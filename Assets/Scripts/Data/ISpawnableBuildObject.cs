using BaseLibrary.Managers;
using UnityEngine;

namespace GeneralImplementations.Data
{
    public interface ISpawnableBuildObject : ISpawnable
    {
        //  ObjectOrientation  GetObjectOrientation { get; }
        Vector3 GetOrientation { get; }
        Vector3 GetGridSize { get; }

        Vector3 GetActualSize { get; }

        Vector3 GetOffset { get; }

        LayerMask GetObstacleLayerMask { get; }

        LayerMask GetBuildLayerMask { get; }
        ScriptableObject GetScriptableObject { get; }
    }
}