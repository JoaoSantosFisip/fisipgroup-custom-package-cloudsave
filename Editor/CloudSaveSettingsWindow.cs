using FisipGroup.CustomPackage.Tools.EditorTool;
using FisipGroup.CustomPackage.Tools.Helpers;
using UnityEditor;
using UnityEngine;

namespace FisipGroup.CustomPackage.CloudSave
{
    /// <summary>
    /// Editor window where the player selects the cloud save settings.
    /// </summary>
    public class CloudSaveSettingsWindow : EditorWindow
    {
        private static CloudSaveSettingsScriptableObject Info;

        private static readonly string PackageName = "CloudSave";

        [MenuItem("FisipGroup/CloudSave")]
        private static void ShowWindow()
        {
            HelperCustomPackage.CreateResourcesFolders(PackageName);

            Info = HelperCustomPackage.GetInfoFile<CloudSaveSettingsScriptableObject>(PackageName) as CloudSaveSettingsScriptableObject;

            GetWindow<CloudSaveSettingsWindow>("FisipGroup CloudSave");
        }
        private void OnGUI()
        {
            // ----- App Settings
            GUILayout.Label("CloudSave Settings", EditorWindowStyles.TitleStyle);

            // ----- Screenblocker
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("Enable ScreenBlocker", EditorWindowStyles.SectionStyle);
            Info.enableScreenBlocker = GUILayout.Toggle(Info.enableScreenBlocker, "");
            GUILayout.Space(10);
            GUILayout.EndHorizontal();
            // -----

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("Enable LoadingIcon", EditorWindowStyles.SectionStyle);
            Info.enableLoadingIcon = GUILayout.Toggle(Info.enableLoadingIcon, "");
            GUILayout.Space(10);
            GUILayout.EndHorizontal();
            
            if (Info.enableLoadingIcon)
            {
                // Sprite
                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                GUILayout.Label("Icon Image", EditorWindowStyles.SectionStyle);
                Info.loadingIconSprite = (Sprite)EditorGUILayout.ObjectField(Info.loadingIconSprite, typeof(Sprite), true);
                GUILayout.Space(10);
                GUILayout.EndHorizontal();
            
                // Color
                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                GUILayout.Label("Icon Color", EditorWindowStyles.SectionStyle);
                Info.loadingIconColor = EditorGUILayout.ColorField(Info.loadingIconColor);
                GUILayout.Space(10);
                GUILayout.EndHorizontal();
            
                // Animation Type
                GUILayout.BeginHorizontal();
                GUILayout.Space(30);
                GUILayout.Label("Icon Animation Type", EditorWindowStyles.SectionStyle);
                Info.loadingIconAnimationType = (CloudSaveSettingsIconAnimationType)EditorGUILayout.EnumPopup(string.Empty, Info.loadingIconAnimationType);
                GUILayout.Space(10);
                GUILayout.EndHorizontal();
            
                if(Info.loadingIconAnimationType != CloudSaveSettingsIconAnimationType.None)
                {
                    // Anim speed
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(60);
                    GUILayout.Label("Icon Animation Speed", EditorWindowStyles.SectionStyle);
                    Info.loadingIconAnimationSpeed = EditorGUILayout.FloatField(Info.loadingIconAnimationSpeed, EditorWindowStyles.InputTextStyle);
                    GUILayout.Space(10);
                    GUILayout.EndHorizontal();
                }
            }

            GUILayout.Space(10);
            GUILayout.Label("Tools", EditorWindowStyles.TitleStyle);
            if (GUILayout.Button("Save"))
            {
                var newInfoFile = (CloudSaveSettingsScriptableObject)CreateInstance(typeof(CloudSaveSettingsScriptableObject));
                newInfoFile.enableScreenBlocker = Info.enableScreenBlocker;
                newInfoFile.enableLoadingIcon = Info.enableLoadingIcon;
                newInfoFile.loadingIconSprite = Info.loadingIconSprite;
                newInfoFile.loadingIconColor = Info.loadingIconColor;
                newInfoFile.loadingIconAnimationType = Info.loadingIconAnimationType;
                newInfoFile.loadingIconAnimationSpeed = Info.loadingIconAnimationSpeed;

                Info = HelperCustomPackage.SaveFileChanges(newInfoFile, PackageName) as CloudSaveSettingsScriptableObject;
            }
        }
    }
}