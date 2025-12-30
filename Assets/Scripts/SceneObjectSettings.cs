using UnityEngine;

public enum DisplayType
{
    Text = 0,
    Picture = 1,
}

[CreateAssetMenu(fileName = "SceneObjectSettings", menuName = "SceneObjectSettings")]
public class SceneObjectSettings : ScriptableObject
{
    [System.Serializable]
    public struct Entry
    {
        public string PrefabName;
        public string UISpriteName;
        public string Description;
        public int SearchOrder;
        public bool IncludeToScene;
    }

    public DisplayType DisplayInUI;
    public int Timer = 120;
    public bool UseTimer;
    public int ObjectsLimit = 3;
    [Header("Список объектов")]
    public Entry[] SceneObjects;
}
