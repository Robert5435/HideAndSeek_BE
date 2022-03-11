using System;
using System.IO;

namespace HideAndSeek.Util
{
    public class FileToBytes
    {
        public Byte[] ToBytes(string fileName)
        {
            byte[] fileContent = null;
            var fin = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fin);
            long byteLength = new FileInfo(fileName).Length;
            fileContent = binaryReader.ReadBytes((Int32)byteLength);
            fin.Close();
            fin.Dispose();
            binaryReader.Close();
            return fileContent;
        }

        public static bool ToFile(string fileName, byte[] encryptedFileContent,byte[]? fileContent = null)
        {
            try
            {
                BinaryWriter Writer = new BinaryWriter(File.OpenWrite(fileName));
                Writer.Write(encryptedFileContent);
                if (fileContent != null)
                {
                    Writer.Write(fileContent);
                }
                Writer.Flush();
                Writer.Close();
            }
            catch(Exception e)
            {
                throw new Exception("error", e);
                return false;
            }
            return true;

        }
    }
}
