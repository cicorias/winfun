using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RSAcheck
{
    class Program
    {
        static void Main(string[] args)
        {
            var rsa = new RSACng() { 
            };

            rsa.EncryptionPaddingMode = AsymmetricPaddingMode.Oaep;

            var tt =  rsa.Encrypt(Encoding.UTF8.GetBytes("foo"));

            var dd = Encoding.UTF8.GetString( rsa.Decrypt(tt));

            Console.WriteLine(dd);
        }
    }
}
