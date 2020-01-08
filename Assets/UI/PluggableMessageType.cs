
using BaseLibrary.UI;
using UnityEngine;
namespace GeneralImplementations.UI
{
    [CreateAssetMenu(fileName = "PluggableMessageType_", menuName = "UI/Scriptable Enums/MessageType")]

    public class PluggableMessageType : ScriptableObject, IPluggableUI
    {

        string YES_NO_NEUTRAL_DEFAULT_INFO;

        public void Plug(IPlugginableUI plugginable)
        {
            throw new System.NotImplementedException();
        }
    }
}
