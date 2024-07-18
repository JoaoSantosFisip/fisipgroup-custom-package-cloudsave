using UnityEngine;

namespace FisipGroup.CustomPackage.CloudSave
{
    /// <summary>
    /// Enables screen blocker on cloud save request.
    /// </summary>
    public class CloudSaveBlockScreenOnRequest : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;

        private void OnEnable()
        {
            CloudSaveManager.OnRequestStart.AddListener(EnableScreenBlocker);
            CloudSaveManager.OnRequestFinish.AddListener(DisableScreenBlocker);
        }
        private void OnDisable()
        {
            CloudSaveManager.OnRequestStart.RemoveListener(EnableScreenBlocker);
            CloudSaveManager.OnRequestFinish.RemoveListener(DisableScreenBlocker);
        }
        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.blocksRaycasts = false;
        }

        private void EnableScreenBlocker()
        {
            _canvasGroup.blocksRaycasts = true;
        }
        private void DisableScreenBlocker()
        {
            _canvasGroup.blocksRaycasts = false;
        }
    }
}
