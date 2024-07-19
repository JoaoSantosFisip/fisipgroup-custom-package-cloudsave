using UnityEngine;

namespace FisipGroup.CustomPackage.CloudSave
{
    public class CloudSaveSettingsScriptableObject : ScriptableObject
    {
        public bool enableScreenBlocker = true;

        public bool enableLoadingIcon = true;
        public Sprite loadingIconSprite = null;
        public Color loadingIconColor = Color.white;
        public CloudSaveSettingsIconAnimationType loadingIconAnimationType = CloudSaveSettingsIconAnimationType.Rotating;
        public float loadingIconAnimationSpeed = 1.0f;
    }
}