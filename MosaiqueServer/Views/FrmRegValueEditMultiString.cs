using MosaiqueServeur.Models;
using MosaiqueServeur.Packets.ServerPackets;
using Serveur.Controllers.Server;
using System;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace MosaiqueServeur.Views
{
    public partial class FrmRegValueEditMultiString : Form
    {
        private readonly ClientMosaique _connectClient;

        private readonly RegValueData _value;

        private readonly string _keyPath;

        public FrmRegValueEditMultiString(string keyPath, RegValueData value, ClientMosaique c)
        {
            _connectClient = c;
            _keyPath = keyPath;
            _value = value;

            InitializeComponent();

            var serializer = new JavaScriptSerializer();
            var result = serializer.DeserializeObject(value.Data);
            
            object[] convertToObjectArray = (Object[])result;
            string[] strArr = new string[convertToObjectArray.Length];
            int i = 0;
            foreach (object obj in convertToObjectArray)
            {
                strArr[i] = obj.ToString();
                i++;
            }

            this.valueNameTxtBox.Text = value.Name;
            this.valueDataTxtBox.Text = String.Join("\r\n", ((string[])strArr));
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            string[] valueData = valueDataTxtBox.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            var res = new JavaScriptSerializer().Serialize(valueData);
            new DoChangeRegistryValue(_keyPath, new RegValueData(_value.Name, _value.Kind, res)).Execute(_connectClient);
        }
    }
}
