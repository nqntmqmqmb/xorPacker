using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xorPacker
{
    class Program
    {
        public static string XORCipher(string data, string key)
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

        static void Main(string[] args)
        {
            int length = 25;
            StringBuilder str_build = new StringBuilder();
            Random random = new Random();

            char letter;

            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }

            Console.Write("PE to pack : ");
            string exe = Console.ReadLine();
            File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\" + "packed_exe.exe", Properties.Resources.xorStub);
   
            try
            {
                Byte[] bytes = File.ReadAllBytes(exe);
                Console.WriteLine("[*] PE (works with .NET) : " + exe);
                String file = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(XORCipher(Convert.ToBase64String(bytes), str_build.ToString())));
                String randomKey = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(XORCipher(str_build.ToString(), "randomkey")));

                using (FileStream filestream = new FileStream("packed_exe.exe", FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    using (BinaryWriter binaryWriter = new BinaryWriter(filestream))
                    {
                        filestream.Position = filestream.Length + 1;
                        binaryWriter.Write("***" + file + "|" + randomKey);
                        Console.WriteLine("[!] PE packed");
                    }
                }

            } 
            
            catch (System.IO.FileNotFoundException) {
                Console.WriteLine("File does not exist.");
            }
            
            Console.ReadLine();
        }
    }
}
