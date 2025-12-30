using Assets.Scripts.Factories.Interfaces;
using Assets.Scripts.Interfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class PlayRoutine : MonoBehaviour
{
    [Inject]
    private ISceneAssetFactory _assetFactory;

    [Inject]
    private IUIAssetFactory _UIassetFactory;


    [SerializeField]
    private SceneObjectSettings _objectSettings;

    [SerializeField]
    private GameObject _currentObjectUIPlaceholder;

    [SerializeField]
    private GameObject _victoryWindow;

    [SerializeField]
    private GameObject _loseWindow;

    [SerializeField]
    private Timer _timer;

    private UIObject _currentObjectUI;

    private List<SceneObject> _sceneObjects = new();
    private int _currentObjectIndex = 0;

    void Start()
    {
        _currentObjectUI = _UIassetFactory.CreateAsset<UIObject>("CurrentObjectPrefab", _currentObjectUIPlaceholder);
        _currentObjectUI.SetDisplayType(_objectSettings.DisplayInUI);
        var objectsInScene = _objectSettings.SceneObjects.Where(x => x.IncludeToScene).OrderBy(x => x.SearchOrder).Take(_objectSettings.ObjectsLimit).ToList();
        foreach (var sceneObject in objectsInScene)
            _sceneObjects.Add(CreateSceneObject(sceneObject));
        StartNewGame();
        if (_objectSettings.UseTimer)
        {
            _timer.OnTimeout += TimeOut;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
        }
    }
    private void StartNewGame()
    {
        foreach (var sceneObject in _sceneObjects)
            sceneObject.Reset();
        _currentObjectUI.gameObject.SetActive(true);
        _currentObjectIndex = 0;
        ChangeCurrentObject();
        if (_objectSettings.UseTimer) 
            _timer.SetTimer(_objectSettings.Timer);

    }

    private void SwitchToNextObject()
    {
        _currentObjectIndex++;
        ChangeCurrentObject();
    }
    private void ChangeCurrentObject()
    {
        var currentObject = _sceneObjects[_currentObjectIndex];
        currentObject.Activate();
        _currentObjectUI.Init(currentObject.UISprite, currentObject.Description);
    }

    private SceneObject CreateSceneObject(SceneObjectSettings.Entry sceneObjectSettings)
    {
        var obj = _assetFactory.CreateAsset<SceneObject>($"SceneObjects/{sceneObjectSettings.PrefabName}");
        var UISprite = Resources.Load<Sprite>($"UIObjects/{sceneObjectSettings.UISpriteName}");
        obj.Init(UISprite, sceneObjectSettings.Description);
        obj.OnClick += OnObjectClick;
        return obj;
    }

    private void OnObjectClick(SceneObject clickedObject)
    {
        clickedObject.Deactivate();
        if (_currentObjectIndex == _sceneObjects.Count - 1)
            WinRoutine();
        else
            SwitchToNextObject();
    }

    private void WinRoutine()
    {
        _victoryWindow.gameObject.SetActive(true);
        _currentObjectUI.gameObject.SetActive(false);
    }

    public void OnRestartClick()
    {
        _victoryWindow.gameObject.SetActive(false);
        _loseWindow.gameObject.SetActive(false);
        StartNewGame();
    }

    public void OnExitClick()
    {
        SceneManager.LoadScene(Scenes.MAIN_MENU);
    }

    public void TimeOut()
    {
        LoseRoutine();
    }

    public void LoseRoutine()
    {
        _loseWindow.gameObject.SetActive(true);
        _currentObjectUI.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _timer.OnTimeout -= TimeOut;
        foreach (var sceneObject in _sceneObjects)
            sceneObject.OnClick -= OnObjectClick;
    }



}
