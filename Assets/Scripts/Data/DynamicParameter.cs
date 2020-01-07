using System;
using UnityEngine;

namespace Data
{


    [Serializable]
    public class DynamicParameter : ParameterBase
    {
        [Range(0, 1)]
        public float changeRate;
        public DynamicParameter()
        {


        }
        public DynamicParameter(string _id, float _val) : this()
        {
            id = _id;
            value = _val;


        }
        public DynamicParameter(string _id, float _val, float _valChange) : this(_id, _val)
        {

            changeRate = _valChange;

        }

        public DynamicParameter(DynamicParameter dynamicParameter) : this(dynamicParameter.id, dynamicParameter.value, dynamicParameter.changeRate)
        {



        }

        public ParameterBase ToNewParameterBase()
        {
            ParameterBase parameterBase = new ParameterBase(id, value);
            return parameterBase;

        }

        public ParameterBase AsParameterBase()
        {
            return this as ParameterBase;

        }
    }
}