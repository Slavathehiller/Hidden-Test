using UnityEngine;

namespace Assets.Scripts.Factories.Interfaces
{
    public interface IUIAssetFactory
    {
        public T CreateAsset<T>(string prefabName, GameObject parent) where T : MonoBehaviour;
    }
}
