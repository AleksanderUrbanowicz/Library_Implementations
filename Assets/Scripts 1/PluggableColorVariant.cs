using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "Pluggable_ColorVariant_", menuName = "UI/Scriptable Enums/ColorVariant")]

    public class PluggableColorVariant : ScriptableObject, IPluggableUI
    {



        public void Plug(IPlugginableUI plugginable)
        {
            throw new NotImplementedException();
        }
    }

}