using System;
using UnityEngine;
namespace GeneralImplementations.Data
{
    [Serializable]
    public class ParameterBase
    {
        // [ConfigSelector(ParamsSetKey = StringDefines.AnyParameterSelectorKey)]
        public string id;

        //Initial value/ one-time increment.
        [Range(-100, 100)]
        public float value;
        public ParameterBase()
        {

        }
        public ParameterBase(string _id, float _val)
        {
            id = _id;
            value = _val;

        }

        public ParameterBase(ParameterBase parameterBase)
        {
            id = parameterBase.id;
            value = parameterBase.value;

        }
    }
}