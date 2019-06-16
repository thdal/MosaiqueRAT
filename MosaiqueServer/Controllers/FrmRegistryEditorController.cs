using Microsoft.Win32;
using MosaiqueServeur.Models;
using MosaiqueServeur.Packets.ClientPackets;
using Serveur.Controllers.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace MosaiqueServeur.Controllers
{
    public static class FrmRegistryEditorController
    {
        public static void loadRegistryKey(GetRegistryKeysResponse packet, ClientMosaique client)
        {
            try
            {
                // Make sure that the client is in the correct state to handle the packet appropriately.
                if (client != null && client.value.frmRe != null && !client.value.frmRe.IsDisposed || !client.value.frmRe.Disposing)
                {
                    if (!packet.isError)
                    {
                        client.value.frmRe.AddKeys(packet.rootKey, packet.matches);
                    }
                    else
                    {
                        client.value.frmRe.ShowErrorMessage(packet.errorMsg);
                        //If root keys failed to load then close the form
                        if (packet.rootKey == null)
                        {
                            //Invoke a closing of the form
                            client.value.frmRe.PerformClose();
                        }
                    }
                }
            }
            catch { }
        }

        public static void createRegistryKey(GetCreateRegistryKeyResponse packet, ClientMosaique client)
        {
            try
            {
                // Make sure that the client is in the correct state to handle the packet appropriately.
                if (client != null && client.value.frmRe != null && !client.value.frmRe.IsDisposed || !client.value.frmRe.Disposing)
                {
                    if (!packet.IsError)
                    {
                        client.value.frmRe.CreateNewKey(packet.ParentPath, packet.Match);
                    }
                    else
                    {
                        client.value.frmRe.ShowErrorMessage(packet.ErrorMsg);
                    }
                }
            }
            catch { }
        }

        public static void renameRegistryKey(GetRenameRegistryKeyResponse packet, ClientMosaique client)
        {
            try
            {
                // Make sure that the client is in the correct state to handle the packet appropriately.
                if (client != null && client.value.frmRe != null && !client.value.frmRe.IsDisposed || !client.value.frmRe.Disposing)
                {
                    if (!packet.IsError)
                    {
                        client.value.frmRe.RenameKey(packet.ParentPath, packet.OldKeyName, packet.NewKeyName);
                    }
                    else
                    {
                        client.value.frmRe.ShowErrorMessage(packet.ErrorMsg);
                    }
                }
            }
            catch { }
        }

        public static void deleteRegistryKey(GetDeleteRegistryKeyResponse packet, ClientMosaique client)
        {
            try
            {
                // Make sure that the client is in the correct state to handle the packet appropriately.
                if (client != null && client.value.frmRe != null && !client.value.frmRe.IsDisposed || !client.value.frmRe.Disposing)
                {
                    if (!packet.IsError)
                    {
                        client.value.frmRe.RemoveKey(packet.ParentPath, packet.KeyName);
                    }
                    else
                    {
                        client.value.frmRe.ShowErrorMessage(packet.ErrorMsg);
                    }
                }
            }
            catch { }
        }

        public static void createRegistryValue(GetCreateRegistryValueResponse packet, ClientMosaique client)
        {
            try
            {
                // Make sure that the client is in the correct state to handle the packet appropriately.
                if (client != null && client.value.frmRe != null && !client.value.frmRe.IsDisposed || !client.value.frmRe.Disposing)
                {
                    if (!packet.IsError)
                    {
                        if(packet.Value.Kind == RegistryValueKind.String)
                        {
                            var serializer = new JavaScriptSerializer();
                            var result = serializer.DeserializeObject(packet.Value.Data);
                            packet.Value.Data = result.ToString();
                        }
                        client.value.frmRe.CreateValue(packet.KeyPath, packet.Value);
                    }
                    else
                    {
                        client.value.frmRe.ShowErrorMessage(packet.ErrorMsg);
                    }
                }
            }
            catch { }
        }

        public static void renameRegistryValue(GetRenameRegistryValueResponse packet, ClientMosaique client)
        {
            try
            {
                // Make sure that the client is in the correct state to handle the packet appropriately.
                if (client != null && client.value.frmRe != null && !client.value.frmRe.IsDisposed || !client.value.frmRe.Disposing)
                {
                    if (!packet.IsError)
                    {
                        client.value.frmRe.RenameValue(packet.KeyPath, packet.OldValueName, packet.NewValueName);
                    }
                    else
                    {
                        client.value.frmRe.ShowErrorMessage(packet.ErrorMsg);
                    }
                }
            }
            catch { }
        }

        public static void changeRegistryValue(GetChangeRegistryValueResponse packet, ClientMosaique client)
        {
            try
            {
                // Make sure that the client is in the correct state to handle the packet appropriately.
                if (client != null && client.value.frmRe != null && !client.value.frmRe.IsDisposed || !client.value.frmRe.Disposing)
                {
                    if (!packet.IsError)
                    {
                        var serializer = new JavaScriptSerializer();
                        var result = serializer.DeserializeObject(packet.Value.Data);
                        packet.Value.Data = result.ToString();
                        client.value.frmRe.ChangeValue(packet.KeyPath, packet.Value);
                    }
                    else
                    {
                        client.value.frmRe.ShowErrorMessage(packet.ErrorMsg);
                    }
                }
            }
            catch { }
        }

        public static void deleteRegistryValueResponse(GetDeleteRegistryValueResponse packet, ClientMosaique client)
        {
            try
            {
                // Make sure that the client is in the correct state to handle the packet appropriately.
                if (client != null && client.value.frmRe != null && !client.value.frmRe.IsDisposed || !client.value.frmRe.Disposing)
                {
                    if (!packet.IsError)
                    {
                        client.value.frmRe.DeleteValue(packet.KeyPath, packet.ValueName);
                    }
                    else
                    {
                        client.value.frmRe.ShowErrorMessage(packet.ErrorMsg);
                    }
                }
            }
            catch { }
        }
    }


    public class RegistryValueLstItem : ListViewItem
    {
        private string _type { get; set; }
        private string _data { get; set; }

        public string RegName
        {
            get { return this.Name; }
            set
            {
                this.Name = value;
                this.Text = RegValueHelper.GetName(value);
            }
        }
        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;

                if (this.SubItems.Count < 2)
                    this.SubItems.Add(_type);
                else
                    this.SubItems[1].Text = _type;

                this.ImageIndex = GetRegistryValueImgIndex(_type);
            }
        }

        public string Data
        {
            get { return _data; }
            set
            {
                _data = value;

                if (this.SubItems.Count < 3)
                    this.SubItems.Add(_data);
                else
                    this.SubItems[2].Text = _data;
            }
        }

        public RegistryValueLstItem(RegValueData value) :
            base()
        {
            RegName = value.Name;
            Type = value.Kind.RegistryTypeToString();
            Data = value.Kind.RegistryTypeToString(value.Data);            
        }

        private int GetRegistryValueImgIndex(string type)
        {
            switch (type)
            {
                case "REG_MULTI_SZ":
                case "REG_SZ":
                case "REG_EXPAND_SZ":
                    return 0;
                case "REG_BINARY":
                case "REG_DWORD":
                case "REG_QWORD":
                default:
                    return 1;
            }
        }
    }

    public class RegValueHelper
    {
        private static string DEFAULT_REG_VALUE = "(Default)";

        public static bool IsDefaultValue(string valueName)
        {
            return String.IsNullOrEmpty(valueName);
        }

        public static string GetName(string valueName)
        {
            return IsDefaultValue(valueName) ? DEFAULT_REG_VALUE : valueName;
        }
    }

    public static class RegistryKeyExtensions
    {
        public static string RegistryTypeToString(this RegistryValueKind valueKind, string valueData)
        {
            //Forced to convert object in byte before used it case registry value is Binary
            byte[] byteRewrited = null;
            string[] strArr = new string[] { };
            var serializer = new JavaScriptSerializer();

            if (valueKind == RegistryValueKind.Binary)
            {
                var result = serializer.DeserializeObject(valueData);
                object[] convertToObjectArray = (Object[])result;
                byteRewrited = new byte[convertToObjectArray.Length];
                int i = 0;
                foreach (object obj in convertToObjectArray)
                {
                    byteRewrited[i] = byte.Parse(obj.ToString());
                    i++;
                }
            }

            if (valueKind == RegistryValueKind.MultiString)
            {
                string[] rep = valueData.Split(',');
                if (rep.Length > 1)
                {
                    var t = valueData;
                    var u = t.Replace("\\\"", "\"");
                    string[] w = u.Split(new[] { "\",\"" }, StringSplitOptions.None);
                    w[0] = w[0].Substring(2, w[0].Length - 2);
                    w[w.Length - 1] = w[w.Length - 1].Substring(0, w[w.Length - 1].Length - 2);
                    strArr = w;
                }
                else
                {
                    if (valueData != "[]")
                    {
                        int i = 0;
                        foreach (string s in rep)
                        {
                            if (rep.Length == 1)
                            {
                                if (i == 0)
                                {
                                    var ss = s.Substring(2, s.Length - 2);
                                    ss = ss.Substring(0, ss.Length - 2);
                                    rep[i] = ss;
                                }
                            }
                            i++;
                        }
                        strArr = rep;
                    }
                    else
                    {
                        strArr = new string[] { "" };
                    }
                }
            }


            if (valueData == null)
                return "(value not set)";

            switch (valueKind)
            {
                case RegistryValueKind.Binary:
                    return ((byte[])byteRewrited).Length > 0 ? BitConverter.ToString((byte[])byteRewrited).Replace("-", " ").ToLower() : "(zero-length binary value)";
                case RegistryValueKind.MultiString:
                    return string.Join(" ", (string[])strArr);
                case RegistryValueKind.DWord:   //Convert with hexadecimal before int
                    try
                    {
                        return String.Format("0x{0} ({1})", ((uint)(int.Parse(valueData.ToString()))).ToString("x8"), ((uint)(int.Parse(valueData.ToString()))).ToString());
                    }
                    catch
                    {
                        return "Invalid Dword";
                    }
                case RegistryValueKind.QWord:
                    try
                    {
                        return String.Format("0x{0} ({1})", ((ulong)(long.Parse(valueData.ToString()))).ToString("x8"), ((ulong)(long.Parse(valueData.ToString()))).ToString());
                    }
                    catch
                    {
                        return "Invalid Qword";
                    }
                case RegistryValueKind.String:
                case RegistryValueKind.ExpandString:
                    return valueData;
                case RegistryValueKind.Unknown:
                default:
                    return string.Empty;
            }
        }

        public static string RegistryTypeToString(this RegistryValueKind valueKind)
        {
            switch (valueKind)
            {
                case RegistryValueKind.Binary:
                    return "REG_BINARY";
                case RegistryValueKind.MultiString:
                    return "REG_MULTI_SZ";
                case RegistryValueKind.DWord:
                    return "REG_DWORD";
                case RegistryValueKind.QWord:
                    return "REG_QWORD";
                case RegistryValueKind.String:
                    return "REG_SZ";
                case RegistryValueKind.ExpandString:
                    return "REG_EXPAND_SZ";
                case RegistryValueKind.Unknown:
                    return "(Unknown)";
                default:
                    return "REG_NONE";
            }
        }
    }


}
