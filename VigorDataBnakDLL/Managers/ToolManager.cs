using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SuperGcodeDLL.Managers
{
    public class ToolManager
    {
        public static ToolManager Instance { get; } = new ToolManager();

        /// <summary>
        /// Int16 轉換 Byte[]
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public byte[] ShortToBytes(short value)
        {
            byte[] src = new byte[2];
            src[1] = (byte)((value >> 8) & 0xFF);
            src[0] = (byte)(value & 0xFF);
            return src;
        }
        /// <summary>
        /// Int32 轉換 Byte[]
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public byte[] IntToBytes(int value)
        {
            byte[] src = new byte[4];
            src[0] = (byte)((value >> 24) & 0xFF);
            src[1] = (byte)((value >> 16) & 0xFF);
            src[2] = (byte)((value >> 8) & 0xFF);
            src[3] = (byte)(value & 0xFF);
            return src;
        }
        /// <summary>
        /// Int64 轉換 Byte[]
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public byte[] LongToBytes(long value)
        {
            byte[] src = new byte[8];
            src[0] = (byte)((value >> 56) & 0xFF);
            src[1] = (byte)((value >> 48) & 0xFF);
            src[2] = (byte)((value >> 40) & 0xFF);
            src[3] = (byte)((value >> 32) & 0xFF);
            src[4] = (byte)((value >> 24) & 0xFF);
            src[5] = (byte)((value >> 16) & 0xFF);
            src[6] = (byte)((value >> 8) & 0xFF);
            src[7] = (byte)(value & 0xFF);
            return src;
        }
        /// <summary>
        /// Byte[] 轉換 Int64
        /// </summary>
        /// <param name="src"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public int BytesToLong(byte[] src, int offset)
        {
            int value;
            value = (int)
                      ((src[offset + 7] & 0xFF)
                    | ((src[offset + 6] & 0xFF) << 8)
                    | ((src[offset + 5] & 0xFF) << 16)
                    | ((src[offset + 4] & 0xFF) << 24)
                    | ((src[offset + 3] & 0xFF) << 32)
                    | ((src[offset + 2] & 0xFF) << 40)
                    | ((src[offset + 1] & 0xFF) << 48)
                    | ((src[offset + 0] & 0xFF) << 56));
            return value;
        }
        /// <summary>
        /// Byte[] 轉換 Int32
        /// </summary>
        /// <param name="src"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public int BytesToInt(byte[] src, int offset)
        {
            int value;
            value = (int)
                      ((src[offset + 3] & 0xFF)
                    | ((src[offset + 2] & 0xFF) << 8)
                    | ((src[offset + 1] & 0xFF) << 16)
                    | ((src[offset + 0] & 0xFF) << 24));
            return value;
        }
        /// <summary>
        /// Byte[] 轉換 Int16
        /// </summary>
        /// <param name="src"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public short BytesToShort(byte[] src, int offset)
        {
            short value;
            value = (short)
                      ((src[offset + 1] & 0xFF)
                    | ((src[offset + 0] & 0xFF) << 8));
            return value;
        }
        /// <summary>
        /// Int 轉 Bool陣列
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public bool[] IntToBcd(int value)
        {
            var data = Convert.ToString(value, 2).Select(s => s.Equals('1')).ToList();
            data.Reverse();

            var c = 8 - data.Count;

            for (int i = 0; i < c; i++)
            {
                data.Add(false);
            }


            return data.ToArray();
        }

        public byte ConvertBoolArrayToByte(bool[] source)
        {
            byte result = 0;
            // This assumes the array never contains more than 8 elements!
            int index = 8 - source.Length;
            Array.Reverse(source);
            //int index = 0;
            // Loop through the array

            foreach (bool b in source)
            {
                // if the element is 'true' set the bit at that position
                if (b)
                    result |= (byte)(1 << (7 - index));

                index++;
            }



            return result;
        }

        public UInt16 SwapUInt16(UInt16 inValue)
        {
            return (UInt16)(((inValue & 0xff00) >> 8) |
                     ((inValue & 0x00ff) << 8));
        }

        public byte[] ConvertHexStringToByteArray(string hexString)
        {
            if (hexString.Length % 2 != 0)
            {
                throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", hexString));
            }

            byte[] data = new byte[hexString.Length / 2];
            for (int index = 0; index < data.Length; index++)
            {
                string byteValue = hexString.Substring(index * 2, 2);
                data[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }

            return data;
        }
    }
}