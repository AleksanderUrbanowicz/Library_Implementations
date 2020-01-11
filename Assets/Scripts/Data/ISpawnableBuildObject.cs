using BaseLibrary.Managers;
using UnityEngine;

namespace Data
{
    public interface ISpawnableBuildObject : ISpawnable
    {
        //  ObjectOrientation  GetObjectOrientation { get; }
        Vector3 GetOrientation { get; }
        Vector3 GetGridSize { get; }

        Vector3 GetActualSize { get; }

        Vector3 GetOffset { get; }
    }
}