using BaseLibrary.Managers;
using BaseLibrary.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GeneralImplementations.Managers
{
    [CreateAssetMenu(fileName = "Manager_UI", menuName = "Managers/ Singleton UI Manager")]

    public class SingletonUIManager : ScriptableSingleton<SingletonUIManager>
    {

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void BeforeSceneLoad() { CreateSingletonInstance(); }

        public PluggableUIData currentPluggableUI;
        public PluggableUIData overridePluggableUI;
        public List<PluggableUIData> pluggableUIs;
        public List<ScriptableObject> ipluggableUIs;

        [Header("Color defines")]
        public List<string> colors;

        [Header("Sprite defines")]
        public List<string> sprites;
        private void OnEnable()
        {

            InitData();

        }
        private void InitData()
        {

            ipluggableUIs = Resources.LoadAll<ScriptableObject>("").Where(x => x is IPluggableUI).ToList();


        }

        public PluggableUIData PluggableUIData
        {
            get
            {
                if (overridePluggableUI == null)
                {

                    return currentPluggableUI;
                }
                else
                {
                    return overridePluggableUI;
                }
            }
            set { }
        }
    }
}