﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Epoche;

namespace Blockchain
{
    public static class StringExt
    {
        public static string GetHash(this string data)
        {
            SHA256 sha256 = SHA256.Create();

            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] hashByte = sha256.ComputeHash(bytes);
            string hash = BitConverter.ToString(hashByte);

            string formattedHash = hash.Replace("-", "").ToLower();

            return formattedHash;
        }

        public static string GetHashKeccak(this string data)
        {
            return ByteArrayToString(Keccak256.ComputeHash(data));
        }

        private static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static List<Block> TryConvertToBlockList(this string data)
        {
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(data)))
            {
                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(List<Block>));

                List<Block> blocks = (List<Block>)deserializer.ReadObject(ms);
                return blocks;
            }
        }

        public static string ParseFileToBinary(this string path)
        {
            try
            {
                return Encoding.Default.GetString(File.ReadAllBytes(path));

            }
            catch
            {
                return "";
            }
        }

        public static void TryCreateFileFromBinary(this string binaryStr, string path)
        {
            File.WriteAllBytes(path, Encoding.Default.GetBytes(binaryStr));
        }

        public static void TryDeleteFile(this string path)
        {
            File.Delete(path);
        }
    }
}
