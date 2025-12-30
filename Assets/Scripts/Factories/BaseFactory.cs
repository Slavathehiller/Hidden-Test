using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Factories
{
    public abstract class BaseFactory 
    {
        protected readonly DiContainer _container;

        protected BaseFactory(DiContainer container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container)); 
        }

        protected T Create<T>(string prefabName)
        {
            var gameObject = Resources.Load<GameObject>(prefabName);

            var instance = GameObject.Instantiate(gameObject);
            _container.InjectGameObject(instance);

            var comp = instance.GetComponent<T>();
            if (comp == null) 
                Debug.LogError($"Wrong prefab, component {typeof(T).Name} not found.");
            return comp;
        }
    }
}
