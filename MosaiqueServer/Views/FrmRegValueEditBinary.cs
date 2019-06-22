using Microsoft.Win32;
using MosaiqueServeur.Controllers;
using MosaiqueServeur.Models;
using MosaiqueServeur.Packets.ServerPackets;
using Serveur.Controllers.Server;
using System;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace MosaiqueServeur.Views
{
    public partial class FrmRegValueEditBinary : Form
    {
        
        private readonly ClientMosaique _connectClient;

        private readonly RegValueData _value;

        private readonly string _keyPath;

        #region Constant

        private const string INVALID_BINARY_ERROR = "The binary value was invalid and could not be converted correctly.";

        #endregion

        public FrmRegValueEditBinary(string keyPath, RegValueData value, ClientMosaique c)
        {
            string[] convertToStrArr = null;
            byte[] byteRewrited = null;

            _connectClient = c;
            _keyPath = keyPath;
            _value = value;

            InitializeComponent();

            this.valueNameTxtBox.Text = RegValueHelper.GetName(value.Name);

            if (value.Data == null)
            {
                hexEditor.HexTable = new byte[] { };
            }
            else
            {
                if (value.Kind == RegistryValueKind.Binary)
                {
                    
                    var serializer = new JavaScriptSerializer();
                    var result = serializer.DeserializeObject(value.Data);
                    object[] convertToObjectArray = (Object[])result;
                    byteRewrited = new byte[convertToObjectArray.Length];
                    int i = 0;
                    foreach (object obj in convertToObjectArray)
                    {
                        byteRewrited[i] = byte.Parse(obj.ToString());
                        i++;
                    }
                }
                if (value.Kind == RegistryValueKind.MultiString)
                {
                    var serializer = new JavaScriptSerializer();
                    var result = serializer.DeserializeObject(value.Data);
                    var objVal = (Object[])result;
                    convertToStrArr = new string[objVal.Length];
                    int i = 0;

                    foreach (object obj in objVal)
                    {
                        convertToStrArr[i] = obj.ToString();
                        i++;
                    }
                }

                switch (value.Kind)
                {
                    case RegistryValueKind.Binary:
                        hexEditor.HexTable = (byte[])byteRewrited;
                        break;
                    case RegistryValueKind.DWord:
                        try
                        {
                            var serializer = new JavaScriptSerializer();
                            var result = serializer.DeserializeObject(value.Data);
                            hexEditor.HexTable = ByteConverter.GetBytes((uint)(int)result);
                        }catch{ }                        
                        break;
                    case RegistryValueKind.QWord:
                        hexEditor.HexTable = ByteConverter.GetBytes((ulong)(long.Parse(value.Data.ToString())));
                        break;
                    case RegistryValueKind.MultiString:
                        hexEditor.HexTable = ByteConverter.GetBytes((string[])convertToStrArr);
                        break;
                    case RegistryValueKind.String:
                    case RegistryValueKind.ExpandString:
                        hexEditor.HexTable = ByteConverter.GetBytes(value.Data);
                        break;
                }
            }
        }

        private object GetData()
        {
            byte[] bytes = hexEditor.HexTable;
            if (bytes != null)
            {
                try
                {
                    switch (_value.Kind)
                    {
                        case RegistryValueKind.Binary:
                            return bytes;
                        case RegistryValueKind.DWord:
                            return (int)ByteConverter.ToUInt32(bytes);
                        case RegistryValueKind.QWord:
                            return (long)ByteConverter.ToUInt64(bytes);
                        case RegistryValueKind.MultiString:
                            return ByteConverter.ToStringArray(bytes);
                        case RegistryValueKind.String:
                        case RegistryValueKind.ExpandString:
                            return ByteConverter.ToString(bytes);
                    }
                }
                catch
                {
                    ShowWarning(INVALID_BINARY_ERROR, "Warning");
                }
            }
            return null;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            object valueData = GetData();

            if(_value.Kind == RegistryValueKind.String || _value.Kind == RegistryValueKind.ExpandString)
            {
                if (valueData != null)
                    new DoChangeRegistryValue(_keyPath, new RegValueData(_value.Name, _value.Kind, valueData.ToString())).Execute(_connectClient);
                else
                    DialogResult = DialogResult.None;
            }
            else
            {
                if (valueData != null)
                    new DoChangeRegistryValue(_keyPath, new RegValueData(_value.Name, _value.Kind, new JavaScriptSerializer().Serialize(valueData))).Execute(_connectClient);
                else
                    DialogResult = DialogResult.None;
            }

            this.Close();
        }

        private void ShowWarning(string msg, string caption)
        {
            MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
