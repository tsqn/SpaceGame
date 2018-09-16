using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Менеджер сцен.
/// </summary>
public class ScenesManager : MonoBehaviour
{
    /// <summary>
    /// Инстанс менеджера сцен.
    /// </summary>
    public static ScenesManager Instance;

    /// <summary>
    /// Название текущей сценны.
    /// </summary>
    private string _currentSceneName;

    /// <summary>
    /// Название следующей сцены.
    /// </summary>
    private string _nextSceneName;

    /// <summary>
    /// Таска загруки ресурсов.
    /// </summary>
    private AsyncOperation _resourceUnloadTask;

    /// <summary>
    /// Таска загрузки сцены.
    /// </summary>
    private AsyncOperation _sceneLoadTask;

    /// <summary>
    /// Состояние сцены.
    /// </summary>
    private SceneState _sceneState;

    /// <summary>
    /// Коллекция делегатов обновления.
    /// </summary>
    private UpdateDelegate[] _updateDelegates;

    /// <summary>
    /// Название стартовой сцены.
    /// </summary>
    public string DefaultSceneName;

    /// <summary>
    /// Флаг загрузки.
    /// </summary>
    [HideInInspector]
    public bool Loading;

    /// <summary>
    /// Экран загрузки.
    /// </summary>
    public Canvas LoadingScreen;

    /// <summary>
    /// Текст экрана загрузки.
    /// </summary>
    public Text LoadingText;

    /// <summary>
    /// Переключает сцену.
    /// </summary>
    /// <param name="nextSceneName">Название новой сцены.</param>
    public static void SwitchScene(string nextSceneName)
    {
        if (Instance == null)
            return;

        if (Instance._currentSceneName != nextSceneName)
            Instance._nextSceneName = nextSceneName;
    }

    private void Awake()
    {
        Loading = true;

        DontDestroyOnLoad(gameObject);

        _updateDelegates = new UpdateDelegate[(int) SceneState.Count];

        _updateDelegates[(int) SceneState.Reset] = UpdateSceneReset;
        _updateDelegates[(int) SceneState.Preload] = UpdateScenePreload;
        _updateDelegates[(int) SceneState.Load] = UpdateSceneLoad;
        _updateDelegates[(int) SceneState.Unload] = UpdateSceneUnload;
        _updateDelegates[(int) SceneState.PostLoad] = UpdateScenePostLoad;
        _updateDelegates[(int) SceneState.Ready] = UpdateSceneReady;
        _updateDelegates[(int) SceneState.Run] = UpdateSceneRun;


        if (!string.IsNullOrEmpty(DefaultSceneName))
            _nextSceneName = DefaultSceneName;
        else
            _nextSceneName = SceneManager.GetActiveScene().name;


        _sceneState = SceneState.Reset;
    }

    private void OnDestroy()
    {
        if (_updateDelegates == null)
            return;

        for (var i = 0; i < (int) SceneState.Count; i++)
            _updateDelegates[i] = null;
    }

    /// <summary>
    /// Показывает экран загрузки между сценами.
    /// </summary>
    private IEnumerator ShowLoadingScreen()
    {
        LoadingScreen.gameObject.SetActive(true);

        yield return new WaitForSeconds(2);

        LoadingScreen.gameObject.SetActive(false);
        Loading = false;
    }

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    private void Update()
    {
        _updateDelegates[(int) _sceneState].Invoke();
    }

    /// <summary>
    /// Обновление загрузки сцены.
    /// </summary>
    private void UpdateSceneLoad()
    {
        if (_sceneLoadTask.isDone)
            _sceneState = SceneState.Unload;
        {
            //update scene loading progress
        }
    }

    /// <summary>
    /// Действия сразу после загрузки.
    /// </summary>
    private void UpdateScenePostLoad()
    {
        _currentSceneName = _nextSceneName;
        _sceneState = SceneState.Ready;
    }

    /// <summary>
    /// Начинает загрузку сцены.
    /// </summary>
    private void UpdateScenePreload()
    {
        StartCoroutine(ShowLoadingScreen());
        _sceneLoadTask = SceneManager.LoadSceneAsync(_nextSceneName);
        _sceneState = SceneState.Load;
    }

    /// <summary>
    /// Действия сразу после загрузки.
    /// </summary>
    private void UpdateSceneReady()
    {
        GC.Collect();
        _sceneState = SceneState.Run;
    }

    /// <summary>
    /// Перезагрузка сцены.
    /// </summary>
    private void UpdateSceneReset()
    {
        Loading = true;
        GC.Collect();
        LoadingText.text = $"LOADING: {Utils.SplitByCamelCase(_nextSceneName).ToUpper()}";
        _sceneState = SceneState.Preload;
    }

    /// <summary>
    /// Ожиданиен изменения сцены.
    /// </summary>
    private void UpdateSceneRun()
    {
        if (_currentSceneName != _nextSceneName)
            _sceneState = SceneState.Reset;
    }

    /// <summary>
    /// Очистка неиспользуемых ресурсов.
    /// </summary>
    private void UpdateSceneUnload()
    {
        if (_resourceUnloadTask == null)
        {
            _resourceUnloadTask = Resources.UnloadUnusedAssets();
        }
        else
        {
            if (!_resourceUnloadTask.isDone)
                return;

            _resourceUnloadTask = null;
            _sceneState = SceneState.PostLoad;
        }
    }

    /// <summary>
    /// Состояние сцены.
    /// </summary>
    private enum SceneState
    {
        Reset, // Очистка сцены.
        Preload, // Начало загрузки сцены.
        Load, //  Загрузка сцены.
        Unload, // Выгрузка сцены.
        PostLoad, // Сцена загрузкилась.
        Ready, // Сцена готова к игре.
        Run, // Сцена исполняется.
        Count // Колличество состояний.
    }

    /// <summary>
    /// Делегат обновления.
    /// </summary>
    private delegate void UpdateDelegate();
}