﻿using System.Security.Cryptography;
using System.Text;

namespace Shadowsocks.Encryption
{
    public struct EncryptorInfo
    {
        public int iv_size;
        public int key_size;
        public bool display;
        public int type;
        public int ctx_size;
        public string name;

        public EncryptorInfo(int key, int iv, bool display, int type, int ctx = 0, string name = "")
        {
            key_size = key;
            iv_size = iv;
            this.display = display;
            this.type = type;
            ctx_size = ctx;
            this.name = name;
        }
    }
    public abstract class EncryptorBase
        : IEncryptor
    {
        public const int MAX_INPUT_SIZE = 65536;

        protected EncryptorBase(string method, string password)
        {
            Method = method;
            Password = password;
        }

        protected string Method;
        protected string Password;

        protected byte[] GetPasswordHash()
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(Password);
            byte[] hash = MbedTLS.MD5(inputBytes);
            return hash;
        }

        public abstract void Encrypt(byte[] buf, int length, byte[] outbuf, out int outlength);

        public abstract void Decrypt(byte[] buf, int length, byte[] outbuf, out int outlength);
        public abstract void ResetEncrypt();
        public abstract void ResetDecrypt();

        public abstract void Dispose();
        public abstract byte[] getIV();
        public abstract byte[] getKey();
        public abstract EncryptorInfo getInfo();
    }
}
