using UnityEngine;

namespace FisipGroup.CustomPackage.CloudSave
{
    /// <summary>
    /// Enables screen blocker on cloud save request.
    /// </summary>
    public class CloudSaveShowUIOnRequest : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;

        private void OnEnable()
        {
            CloudSaveManager.OnRequestStart.AddListener(ShowUI);
            CloudSaveManager.OnRequestFinish.AddListener(HideUI);
        }
        private void OnDisable()
        {
            CloudSaveManager.OnRequestStart.RemoveListener(ShowUI);
            CloudSaveManager.OnRequestFinish.RemoveListener(HideUI);
        }
        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();

            HideUI();
        }

        private void ShowUI()
        {
            _canvasGroup.alpha = 1.0f;
            _canvasGroup.blocksRaycasts = true;
        }
        private void HideUI()
        {
            _canvasGroup.alpha = 0.0f;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}
