using FisipGroup.CustomPackage.Tools.Helpers;
using UnityEngine;

namespace FisipGroup.CustomPackage.CloudSave
{
    /// <summary>
    /// Loads screen blocker and loading icon canvases on start of app.
    /// </summary>
    public static class CloudSaveInstantiateUI
    {
        private static readonly string ScreenBlockerPrefabName = "CustomPackage_CloudSave_Canvas-ScreenBlocker";
        private static readonly string LoadingIconPrefabName = "CustomPackage_CloudSave_Canvas-LoadingIcon";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            var info = HelperCustomPackage.GetInfoFile<CloudSaveSettingsScriptableObject>("CloudSave") as CloudSaveSettingsScriptableObject;

            // Screen Blocker
            if (info.enableScreenBlocker)
            {
                var screenBlockerRef = Resources.Load($"CustomPackage/CloudSave/{ScreenBlockerPrefabName}");
                var screenBlockerObj = GameObject.Instantiate(screenBlockerRef);
                screenBlockerObj.name = screenBlockerRef.name;

                GameObject.DontDestroyOnLoad(screenBlockerObj);
            }
            // Loading Icon
            if (info.enableLoadingIcon)
            {
                var loadingIconRef = Resources.Load($"CustomPackage/CloudSave/{LoadingIconPrefabName}");
                var loadingIconObj = GameObject.Instantiate(loadingIconRef);
                loadingIconObj.name = loadingIconRef.name;

                GameObject.DontDestroyOnLoad(loadingIconObj);
            }
        }
    }
}