using MosaiqueServeur.Controllers;
using MosaiqueServeur.Models;
using MosaiqueServeur.Packets.ServerPackets;
using Serveur.Controllers.Server;
using System;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace MosaiqueServeur.Views
{
    public partial class FrmRegValueEditString : Form
    {
        private readonly ClientMosaique _connectClient;

        private readonly RegValueData _value;

        private readonly string _keyPath;

        public FrmRegValueEditString(string keyPath, RegValueData value, ClientMosaique c)
        {
            _connectClient = c;
            _keyPath = keyPath;
            _value = value;

            //var serializer = new JavaScriptSerializer();

            //var result = serializer.DeserializeObject(value.Data);


            InitializeComponent();

            this.valueNameTxtBox.Text = RegValueHelper.GetName(value.Name);
            //if(value.Data == "\"\"")
            //{
            //    this.valueDataTxtBox.Text = "";
            //}
            //else
            //{
                this.valueDataTxtBox.Text = value.Data == null ? "" : value.Data.ToString();
            //}
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (_value.Data == null || valueDataTxtBox.Text != _value.Data.ToString())
            {
                string valueData = valueDataTxtBox.Text;
                new DoChangeRegistryValue(_keyPath, new RegValueData(_value.Name, _value.Kind, valueData)).Execute(_connectClient);
                this.Close();
            }
        }
    }
}
