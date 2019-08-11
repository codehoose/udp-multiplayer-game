using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class SingletonBehaviour<T> : MonoBehaviour where T: MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance;
            }

            _instance = FindObjectOfType<T>();
            if (_instance == null)
            {
                var go = new GameObject(typeof(T) + "_Singleton");
                _instance = go.AddComponent<T>();
            }

            return _instance;
        }
    }
}
