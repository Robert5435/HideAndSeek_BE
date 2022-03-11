using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace HideAndSeek.Util
{
    public class EncryptBytes
    {
        public static Byte[] encryptDecrypt(Byte[] inputArray, string key)
        {
            Byte[] outputArray = new Byte[64];
            int dataLen = inputArray.Length;
            int keyLen = key.Length;
            for (int i = 0; i< dataLen; i++)
            {
                outputArray[i] = (byte)(inputArray[i] ^ key[i % keyLen]);
            }
            return outputArray;
        }
    }
}
