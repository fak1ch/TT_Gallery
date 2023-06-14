using UnityEngine;

namespace App.Scripts.Scenes.View
{
    [CreateAssetMenu(fileName = "ViewSceneConfig", menuName = "App/ViewSceneConfig")]
    public class ViewSceneConfigScriptableObject : ScriptableObject
    {
        public Sprite SpriteFromGallery;
    }
}