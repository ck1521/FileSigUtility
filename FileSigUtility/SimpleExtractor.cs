using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileSigUtility
{
    /// <summary>
    /// 给定特定文件格式的头尾（或其一）
    /// 提取数据块中包含此文件格式的内容
    /// </summary>
    public class SimpleExtractor
    {
        byte[] header;
        byte[] trailer;

        public SimpleExtractor(byte[] header = null, byte[] trailer = null)
        {
            this.header = header;
            this.trailer = trailer;
        }

        public byte[] ExtractContent(byte[] raw, int offset = 0)
        {
            int headerIndex = 0;
            if (header != null)
            {
                headerIndex = Utility.GetIndexOfPattern(header, raw, offset);
                if (headerIndex < 0)
                {
                    return null;
                }
            }

            int tailIndex = raw.Length - 1;
            if (trailer != null)
            {
                tailIndex = Utility.GetIndexOfPattern(trailer, raw, offset);
                if (tailIndex < 0)
                {
                    return null;
                }
            }

            return raw.Skip(headerIndex).Take(tailIndex - headerIndex).ToArray();
        }
    }
}
