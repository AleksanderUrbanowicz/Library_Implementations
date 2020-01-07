using UnityEngine;

namespace Managers
{
    public class Spawner : ISpawner
    {


        public Spawner()
        {



        }
       
        public GameObject CreateInstance(Transform parent, Vector3 position, Quaternion rotation, ISpawnable _spawnable)
        {

            GameObject instance = GameObject.Instantiate(_spawnable.GetPrefab, position, rotation, parent);
            instance.name = _spawnable.GetID;
            return instance;
        }

    }
}