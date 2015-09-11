using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileSigUtility
{
    /// <summary>
    /// 对于任意文件，识别其中可能存在的其他文件类型
    /// 基于文件头尾签名识别
    /// 需要提供足够多的文件特征串
    /// 未完成
    /// </summary>
    public class Identifier
    {
        public Dictionary<byte[], string> sigDic = new Dictionary<byte[], string>();
        public Dictionary<string, FileSignature> sigRefDic = new Dictionary<string, FileSignature>();

        private KeyNode virRoot = new KeyNode();

        public Identifier()
        {
        }

        private void Init()
        {

        }

        private void ConstructSigTree()
        {
            KeyNode cur = virRoot;
            foreach (byte[] sig in sigDic.Keys)
            {
                for (int i = 0; i < sig.Length; i++)
                {
                    if (!cur.Next.ContainsKey(sig[i]))
                    {
                        cur.Next[sig[i]] = new KeyNode(sig[i]);
                    }
                    cur = cur.Next[sig[i]];
                }
                cur.result = sigDic[sig];
            }
        }

        private class KeyNode
        {
            private byte key;
            public string result;
            public Dictionary<byte, KeyNode> Next = new Dictionary<byte, KeyNode>();

            public KeyNode NextKey(byte c)
            {
                if (this.Next.ContainsKey(c))
                {
                    return Next[c];
                }
                else
                {
                    return null;
                }
            }

            public void InsertKey(byte c, string result = null)
            {
                if (this.Next != null && this.Next.ContainsKey(c))
                {
                    throw new Exception("Cannot create duplicate keywords.");
                }
                else
                {
                    if (this.Next == null)
                    {
                        this.Next = new Dictionary<byte, KeyNode>();
                    }
                    this.Next[c] = new KeyNode(c, result);
                }
            }

            public KeyNode(byte? key = null, string result = null)
            {
                if (key != null)
                {
                    this.key = (byte)key;
                }
                this.result = result;
            }
        }
    }
}
