// Ignore Spelling: Fisip

using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models;
using Unity.Services.Authentication;
using System.Threading.Tasks;

namespace FisipGroup.CustomPackage.CloudSave
{
    /// <summary>
    /// This script has the necessary methods to access and control the CloudSave player's Data.
    /// </summary>
    public static class CloudSaveManager
    {
        private static readonly int RetryWaitTime = 1; //In seconds
        private static readonly string ConnectionErrorMessage = "The request to the Cloud Save service failed - make sure you're connected to an internet connection and try again.";

        /// <summary>
        /// Clears all the player's data.
        /// </summary>
        /// <param name="callback"></param>
        public static async void ClearData(Action<bool> callback)
        {
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                Debug.LogError("CloudSaveManager.cs: Can't clear data because the player is not logged in.");

                callback?.Invoke(false);

                return;
            }

            try
            {
                await CloudSaveService.Instance.Data.Player.DeleteAllAsync();
        
                Debug.Log("<color=#12a182>CloudSaveManager.cs: User data cleared successfully.</color>");
        
                callback?.Invoke(true);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(ConnectionErrorMessage))
                {
                    Debug.LogWarning("CloudSaveManager.cs: Connection error deleting user data: " + ex.Message);

                    await Task.Delay(new TimeSpan(0, 0, RetryWaitTime)).ContinueWith(o => { ClearData(callback); });
                }
                else
                {
                    Debug.LogError("CloudSaveManager.cs: Error deleting user data: " + ex.Message);

                    callback?.Invoke(false);
                }
            }
        }
        /// <summary>
        /// Retrieves a specific value from the player's data.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="callback"></param>
        public static async void RetrieveSpecificData<T>(string key, Action<bool, T> callback)
        {
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                Debug.LogError("CloudSaveManager.cs: Can't retrieve specific data because the player is not logged in.");

                callback?.Invoke(false, default);

                return;
            }

            try
            {
                var data = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { key });

                if (data.TryGetValue(key, out var item))
                {
                    Debug.Log("<color=#12a182>CloudSaveManager.cs: Specific data retrieved successfully.</color>");

                    callback?.Invoke(true, item.Value.GetAs<T>());
                }
                else
                {
                    Debug.Log("<color=#12a182>CloudSaveManager.cs: There is no such key as " + key + "!</color>");

                    callback?.Invoke(false, default);
                }
            }
            catch (Exception ex)
            {
                // Connection error, trying again.
                if (ex.Message.Contains(ConnectionErrorMessage))
                {
                    Debug.LogWarning("CloudSaveManager.cs: Connection error retrieving specific data: " + ex.Message);

                    await Task.Delay(new TimeSpan(0, 0, RetryWaitTime)).ContinueWith(o => { RetrieveSpecificData(key, callback); });
                }
                else
                {
                    Debug.LogError("CloudSaveManager.cs: Error retrieving specific data: " + ex.Message);

                    callback?.Invoke(false, default);
                }
            }
        }
        /// <summary>
        /// Retrieves all the values from the player's data.
        /// </summary>
        /// <param name="callback"></param>
        public static async void RetrieveAllData(Action<bool, Dictionary<string, Item>> callback)
        {
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                Debug.LogError("CloudSaveManager.cs: Can't retrieve all of player's data because the player is not logged in.");

                callback?.Invoke(false, null);

                return;
            }

            try
            {
                var allData = await CloudSaveService.Instance.Data.Player.LoadAllAsync();
        
                Debug.Log("<color=#12a182>CloudSaveManager.cs: All data loaded successfully.</color>");
        
                callback?.Invoke(true, allData);
            }
            catch (Exception ex)
            {
                // Connection error, trying again.
                if (ex.Message.Contains(ConnectionErrorMessage))
                {
                    Debug.LogWarning("CloudSaveManager.cs: Connection error loading all data: " + ex.Message);

                    await Task.Delay(new TimeSpan(0, 0, RetryWaitTime)).ContinueWith(o => { RetrieveAllData(callback); });
                }
                else
                {
                    Debug.LogError("CloudSaveManager.cs: Error loading all data: " + ex.Message);

                    callback?.Invoke(false, null);
                }
            }
        }
        /// <summary>
        /// Sends specific value to player's cloud data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="callback"></param>
        public static async void SaveSpecificData<T>(string key, T data, Action<bool> callback)
        {
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                Debug.LogError("CloudSaveManager.cs: Can't save specific data because the player is not logged in.");

                callback?.Invoke(false);

                return;
            }

            try
            {
                await CloudSaveService.Instance.Data.Player.SaveAsync(new Dictionary<string, object> { { key, data } });
        
                Debug.Log("<color=#12a182>CloudSaveManager.cs: " + key + " data saved successfully.</color>");
        
                callback?.Invoke(true);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(ConnectionErrorMessage))
                {
                    Debug.LogWarning("CloudSaveManager.cs: Connection error saving data: " + ex.Message);

                    await Task.Delay(new TimeSpan(0, 0, RetryWaitTime)).ContinueWith(o => { SaveSpecificData(key, data, callback); });
                }
                else
                {
                    Debug.LogError("CloudSaveManager.cs: Error saving data: " + key + " - " + ex.Message);

                    callback?.Invoke(false);
                }
            }
        }
    }
}