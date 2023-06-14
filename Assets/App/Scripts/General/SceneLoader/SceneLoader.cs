using System;
using System.Linq;
using App.Scripts.General.Singleton;
using App.Scripts.Scenes.General;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.Scripts.General.LoadScene
{
    public class SceneLoader : MonoSingleton<SceneLoader>
    {
        public event Action OnSceneStartLoading;
        public event Action OnSceneLoaded;
        
        [SerializeField] private float _timeUntilLoadScene;
        [SerializeField] private SceneScriptableObject _sceneSO;
        [SerializeField] private LoadingPanel _loadingPanel;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += SceneLoadedEvent;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= SceneLoadedEvent;
        }

        private void SceneLoadedEvent(Scene arg0, LoadSceneMode arg1)
        {
            OnSceneLoaded?.Invoke();
        }

        public void LoadScene(SceneEnum sceneEnum)
        {
            OnSceneStartLoading?.Invoke();
            _loadingPanel.StartLoading(_timeUntilLoadScene);
            SceneManager.LoadScene(GetSceneNameByEnum(sceneEnum));
        }

        private string GetSceneNameByEnum(SceneEnum sceneEnum)
        {
            return _sceneSO.scenes.FirstOrDefault(scene => scene.sceneEnumEnum == sceneEnum)?.sceneName;
        }
    }
}