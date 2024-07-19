using FisipGroup.CustomPackage.Tools.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace FisipGroup.CustomPackage.CloudSave
{
    public class CloudSaveSetupLoadingIcon : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject _loadingIconGO;

        private void Awake()
        {
            var info = HelperCustomPackage.GetInfoFile<CloudSaveSettingsScriptableObject>("CloudSave") as CloudSaveSettingsScriptableObject;

            _loadingIconGO.SetActive(info.enableLoadingIcon);

            if (info.enableLoadingIcon)
            {
                var image = _loadingIconGO.GetComponent<Image>();
                image.sprite = info.loadingIconSprite;
                image.color = info.loadingIconColor;

                var animator = _loadingIconGO.GetComponent<Animator>();
                animator.SetTrigger(info.loadingIconAnimationType.ToString());
                animator.speed = info.loadingIconAnimationSpeed;
            }
        }
    }
}