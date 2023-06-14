using System;
using App.Scripts.General.LoadScene;
using UnityEngine;

namespace App.Scripts.Scenes.General
{
    public class LoadSceneButtonByKey : MonoBehaviour
    {
        [SerializeField] private KeyCode _key;
        [SerializeField] private SceneEnum _scene;

        private void Update()
        {
            if (Input.GetKeyDown(_key))
            {
                LoadScene();
            }
        }

        private void LoadScene()
        {
            SceneLoader.Instance.LoadScene(_scene);
        }
    }
}