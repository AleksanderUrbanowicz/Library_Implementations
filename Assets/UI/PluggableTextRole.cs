using BaseLibrary.UI;
using UnityEngine;
namespace GeneralImplementations.UI
{
    [CreateAssetMenu(fileName = "TextRole", menuName = "UI/Scriptable Enums/TextRole")]


    public class PluggableTextRole : ScriptableObject, IPluggableUI
    {
        public void Plug(IPlugginableUI plugginable)
        {
            throw new System.NotImplementedException();
        }
    }
}
