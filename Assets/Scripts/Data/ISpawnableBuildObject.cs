using BaseLibrary.Managers;
using Data;

namespace GeneralImplementations.Data
{
    public interface ISpawnableBuildObject : ISpawnable
    {

        BuildObjectData BuildObjectData { get; }
        // ScriptableObject GetScriptableObject { get; }
    }
}