using BaseLibrary.Data;
using BaseLibrary.Managers;
using UnityEngine;

namespace GeneralImplementations.Data
{
    [CreateAssetMenu(fileName = "Config_Gameplay", menuName = "Config/Singleton Gameplay Config")]
    public class GameplayConfig : ScriptableSingleton<GameplayConfig>
    {
        public TransformRuntimeCollection AllPluggableTransforms;

        public GameplaySettings gameplaySettings;
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void BeforeSceneLoad() { CreateSingletonInstance(); }

        public void Init()
        {
            Debug.Log(GetType().Name + " Init.");

        }
    }
}
