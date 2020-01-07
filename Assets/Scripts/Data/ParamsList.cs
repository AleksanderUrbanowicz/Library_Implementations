using System;
using System.Collections.Generic;

namespace GeneralImplementations.Data
{
    [Serializable]
    public class ParamsList
    {
        public string id;
        // [ConfigSelectorAttribute(ParamsSetKey = StringDefines.AnyParameterSelectorKey)]
        public List<string> parameters;


        public ParamsList(string _id, List<string> _parameters)
        {

            id = _id;
            parameters = _parameters;

        }

    }
}
