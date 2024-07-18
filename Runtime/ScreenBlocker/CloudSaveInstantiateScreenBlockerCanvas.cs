using UnityEngine;

namespace FisipGroup.CustomPackage.CloudSave
{
    /// <summary>
    /// Loads screen blocker canvas on start of app.
    /// </summary>
    public static class CloudSaveInstantiateScreenBlockerCanvas
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            var screenBlockerRef = Resources.Load("CustomPackage/CloudSave/Canvas - ScreenBlocker");
            var screenBlockerObj = GameObject.Instantiate(screenBlockerRef);
            screenBlockerObj.name = screenBlockerRef.name;

            GameObject.DontDestroyOnLoad(screenBlockerObj);
        }
    }
}