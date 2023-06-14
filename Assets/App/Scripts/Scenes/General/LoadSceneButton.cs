using System;
using App.Scripts.General.LoadScene;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.General
{
    public class LoadSceneButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private SceneEnum _scene;

        private void OnEnable()
        {
            _button.onClick.AddListener(LoadScene);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(LoadScene);
        }
        
        private void LoadScene()
        {
            SceneLoader.Instance.LoadScene(_scene);
        }
    }
}