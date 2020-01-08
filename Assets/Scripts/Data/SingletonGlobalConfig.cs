using BaseLibrary.Managers;
using BaseLibrary.StateMachine;
using UnityEngine;

namespace GeneralImplementations.Data
{
    [CreateAssetMenu(fileName = "Config_Global", menuName = "Config/Singleton Global Config")]
    public class SingletonGlobalConfig : ScriptableSingleton<SingletonGlobalConfig>
    {

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void BeforeSceneLoad() { CreateSingletonInstance(); }


        public State buildObjectStartState;
        public State remainInState;


    }
}