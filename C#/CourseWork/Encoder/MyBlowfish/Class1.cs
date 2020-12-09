using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace MyBlowfish {
    class Class1 {

        static public void ReadBytes(string path, out byte[] bytes) {
            using (FileStream file = new FileStream(path, FileMode.Open)) {
                bytes = new byte[file.Length];
                for (int i = 0; i < file.Length; i++) {
                    bytes[i] = (byte)file.ReadByte();
                }
            }
        }

        static void WriteBytes(ref byte[] res, string path) {
            using (FileStream file = new FileStream(@"Output/res." + path.Substring(path.LastIndexOf('.')-1), FileMode.Create)) {
                for (int i = 0; i < res.Length; i++) {
                    file.WriteByte(res[i]);
                }
            }
        }

        static void Main(string[] args) {
            // = { "orig.enc", "0123456789", "d" };
            foreach (string s in args) {
                Console.WriteLine(s);
            }
            string path = args[0];
            byte[] bytes;

            ReadBytes(path, out bytes);
            
            Console.WriteLine("Bytes read done");
            MyBlowfish blowfish = new MyBlowfish(args[1]);
            byte[] res;
            switch (args[2]) {
                case "e":
                    res = blowfish.Encrypt(bytes);
                    break;
                case "d":
                    res = blowfish.Dencrypt(bytes);
                    break;
                default:
                    res = new byte[0];
                    break;
            }

            WriteBytes(ref res, path);
            
            Console.WriteLine("Write done");
            
        }   
    }
}
