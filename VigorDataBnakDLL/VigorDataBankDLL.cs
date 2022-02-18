using SuperGcodeDLL.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VigorDataBnakDLL
{
    public class VigorDataBankDLL
    {
        public VigorDataBankDLL() { }


        public string ReadHeader(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            int hexIn;
            String hex;
            string data = "";
            int count = 0;
            for (int i = 0; (hexIn = fs.ReadByte()) != -1; i++)
            {
                ++count;
                //34
                if (count > 34)
                {
                    hex = string.Format("Address : {0} , Value : {1:X2}", count, hexIn);
                    //Console.WriteLine(hex);
                }
                else
                {
                    data += string.Format("{0:X2}", hexIn) + "";
                }
            }
            Console.WriteLine(data);
            fs.Close();

            return data;
        }
        public bool WriteHex(string path , List<byte> data)
        {
            try
            {
                //FileStream fs = new FileStream("C:\\Users\\Chadigo\\Downloads\\BigData.DB", FileMode.Open);
                FileStream fs = new FileStream(path, FileMode.OpenOrCreate);

                //string header = "0C417265614461746142616E6B01000000094461746142616E6B310000000064000000";
                byte[] length = ToolManager.Instance.IntToBytes(data.Count/2);
                
                string lenByte = 
                    length[3].ToString("X2") + 
                    length[2].ToString("X2") + 
                    length[1].ToString("X2") + 
                    length[0].ToString("X2");

                string header = "0C417265614461746142616E6B01000000094461746142616E6B3100000000"+ lenByte ;
                var header_byte = ToolManager.Instance.ConvertHexStringToByteArray(header).ToList();

                List<byte> result = header_byte.Concat(data).ToList();

                fs.Write(result.ToArray(), 0, result.Count);
                fs.Close();
                return true;
            }
            catch (Exception ex) { return false; }
        }
    }
}
