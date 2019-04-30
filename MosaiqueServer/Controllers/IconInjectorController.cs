using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace MosaiqueServeur.Controllers
{
    /// <summary>
    /// IconInjector&#12463;&#12521;&#12473;&#12398;&#23450;&#32681;
    /// </summary>
    public static class IconInjectorController
    {



        [DllImport("kernel32.dll", SetLastError = true)]
        //static extern bool UpdateResource(IntPtr hUpdate, string lpType, string lpName, ushort wLanguage, IntPtr lpData, uint cbData);
        static extern int UpdateResource(IntPtr hUpdate, uint lpType, uint lpName, ushort wLanguage, byte[] lpData, uint cbData);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr BeginUpdateResource(string pFileName,
            [MarshalAs(UnmanagedType.Bool)]bool bDeleteExistingResources);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool EndUpdateResource(IntPtr hUpdate, bool fDiscard);

        ///// <summary>
        ///// アプリケーションのメイン エントリ ポイントです。
        ///// </summary>
        //[STAThread]
        //static void Main(string[] args)
        //{

        //    // アイコンの追加

        //    // TODO: 環境に合わせてパラメータを変更してください。
        //    InjectIcon("c:\\\\\\\\hoge\\\\\\\\hoge.exe", "c:\\\\\\\\hoge\\\\\\\\icon1.ico", 1000, 1000);
        //}
        public static void InjectIcon(string execFileName, string iconFileName)
        {
            InjectIcon(execFileName, iconFileName, 1, 1);
        }
        static void InjectIcon(string execFileName, string iconFileName, uint iconGroupID, uint iconBaseID)
        {
            const uint RT_ICON = 3;
            const uint RT_GROUP_ICON = 14;

            // アイコンファイルの読み込み
            IconFile iconFile = new IconFile();
            iconFile.Load(iconFileName);

            // リソースの更新開始
            IntPtr hUpdate = BeginUpdateResource(execFileName, false);
            Debug.Assert(hUpdate != IntPtr.Zero);

            // RT_GROUP_ICON 書き込み
            byte[] data = iconFile.CreateIconGroupData(iconBaseID);
            UpdateResource(hUpdate, RT_GROUP_ICON, iconGroupID, 0, data, (uint)data.Length);

            // RT_ICON書き込み
            for (int i = 0; i < iconFile.GetImageCount(); i++)
            {
                byte[] image = iconFile.GetImageData(i);
                UpdateResource(hUpdate, RT_ICON, (uint)(iconBaseID + i), 0, image, (uint)image.Length);
            }

            // リソースの更新終了
            EndUpdateResource(hUpdate, false);

        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ICONDIR
    {
        public ushort idReserved;
        public ushort idType;
        public ushort idCount;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ICONDIRENTRY
    {
        public byte bWidth;
        public byte bHeight;
        public byte bColorCount;
        public byte bReserved;
        public ushort wPlanes;
        public ushort wBitCount;
        public uint dwBytesInRes;
        public uint dwImageOffset;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct BITMAPINFOHEADER
    {
        public uint biSize;
        public int biWidth;
        public int biHeight;
        public ushort biPlanes;
        public ushort biBitCount;
        public uint biCompression;
        public uint biSizeImage;
        public int biXPelsPerMeter;
        public int biYPelsPerMeter;
        public uint biClrUsed;
        public uint biClrImportant;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct GRPICONDIRENTRY
    {
        public byte bWidth;
        public byte bHeight;
        public byte bColorCount;
        public byte bReserved;
        public ushort wPlanes;
        public ushort wBitCount;
        public uint dwBytesInRes;
        public ushort nID;
    }


    /// <summary>
    /// IconFile の概要の説明です。
    /// </summary>
    public class IconFile
    {
        ICONDIR _iconDir = new ICONDIR();
        ArrayList _iconEntry = new ArrayList();
        ArrayList _iconImage = new ArrayList();

        public IconFile()
        {
        }

        // アイコンの持つイメージの数を取得する
        public int GetImageCount()
        {
            return _iconDir.idCount;
        }

        // index番目のイメージデータを取得する
        public byte[] GetImageData(int index)
        {
            Debug.Assert(0 <= index && index < GetImageCount());
            return (byte[])_iconImage[index];
        }

        // アイコンファイルをロードする
        public unsafe void Load(string fileName)
        {
            FileStream fs = null;
            BinaryReader br = null;
            byte[] buffer = null;

            try
            {
                // アイコンファイルのオープン
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                br = new BinaryReader(fs);

                // ICONDIR部分の読み込み
                buffer = br.ReadBytes(sizeof(ICONDIR));
                fixed (ICONDIR* ptr = &_iconDir)
                {
                    Marshal.Copy(buffer, 0, (IntPtr)ptr, sizeof(ICONDIR));
                }

                // ICONDIRの内容確認
                Debug.Assert(_iconDir.idReserved == 0);
                Debug.Assert(_iconDir.idType == 1);
                Debug.Assert(_iconDir.idCount > 0);


                // ICONDIRENTRYの読み込み
                for (int i = 0; i < _iconDir.idCount; i++)
                {
                    ICONDIRENTRY entry = new ICONDIRENTRY();
                    buffer = br.ReadBytes(sizeof(ICONDIRENTRY));
                    ICONDIRENTRY* ptr = &entry;
                    {
                        Marshal.Copy(buffer, 0, (IntPtr)ptr, sizeof(ICONDIRENTRY));
                    }

                    _iconEntry.Add(entry);
                }

                // イメージデータの読み込み
                for (int i = 0; i < _iconDir.idCount; i++)
                {
                    fs.Position = ((ICONDIRENTRY)_iconEntry[i]).dwImageOffset;
                    byte[] img = br.ReadBytes((int)((ICONDIRENTRY)_iconEntry[i]).dwBytesInRes);
                    _iconImage.Add(img);
                }

                byte[] b = (byte[])_iconImage[0];

            }
            catch (Exception ex)
            {
                // Debug.Assert(false);
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (br != null)
                {
                    br.Close();
                }
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        // アイコングループデータのサイズ取得
        unsafe int SizeOfIconGroupData()
        {
            return sizeof(ICONDIR) + sizeof(GRPICONDIRENTRY) * GetImageCount();
        }

        // アイコングループデータの作成（nBaseIDはRT_ICONの基底インデックス番号）
        public unsafe byte[] CreateIconGroupData(uint nBaseID)
        {
            // アイコングループデータの領域確保
            byte[] data = new byte[SizeOfIconGroupData()];

            // アイコングループディレクトリを書き込む
            fixed (ICONDIR* ptr = &_iconDir)
            {
                Marshal.Copy((IntPtr)ptr, data, 0, sizeof(ICONDIR));
            }

            int offset = sizeof(ICONDIR);

            for (int i = 0; i < GetImageCount(); i++)
            {
                GRPICONDIRENTRY grpEntry = new GRPICONDIRENTRY();
                BITMAPINFOHEADER bitmapheader = new BITMAPINFOHEADER();

                // イメージデータからBITMAPINFOHEADER取得
                BITMAPINFOHEADER* ptr = &bitmapheader;
                {
                    Marshal.Copy(GetImageData(i), 0, (IntPtr)ptr, sizeof(BITMAPINFOHEADER));
                }

                // アイコングループエントリ作成
                grpEntry.bWidth = ((ICONDIRENTRY)_iconEntry[i]).bWidth;
                grpEntry.bHeight = ((ICONDIRENTRY)_iconEntry[i]).bHeight;
                grpEntry.bColorCount = ((ICONDIRENTRY)_iconEntry[i]).bColorCount;
                grpEntry.bReserved = ((ICONDIRENTRY)_iconEntry[i]).bReserved;
                grpEntry.wPlanes = bitmapheader.biPlanes;
                grpEntry.wBitCount = bitmapheader.biBitCount;
                grpEntry.dwBytesInRes = ((ICONDIRENTRY)_iconEntry[i]).dwBytesInRes;
                grpEntry.nID = (ushort)(nBaseID + i);

                // アイコングループエントリを書き込む
                GRPICONDIRENTRY* ptr2 = &grpEntry;
                {
                    Marshal.Copy((IntPtr)ptr2, data, offset, Marshal.SizeOf(grpEntry));
                }

                offset += sizeof(GRPICONDIRENTRY);
            }

            return data;
        }

    }
}

