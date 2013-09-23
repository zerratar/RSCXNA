﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace RSC2DLib.Data
//{
//    public class DataEncryption
//    {

//        public DataEncryption(sbyte[] arg0)
//        {
//            data = arg0;
//            offset = 0;
//        }

//        public void addByte(int arg0)
//        {
//            data[offset++] = (byte)arg0;
//        }

//        public void addInt(int arg0)
//        {
//            data[offset++] = (byte)(arg0 >> 24);
//            data[offset++] = (byte)(arg0 >> 16);
//            data[offset++] = (byte)(arg0 >> 8);
//            data[offset++] = (byte)arg0;
//        }

//        public void addString(String arg0)
//        {

//            //arg0.getBytes(0, arg0.Length, data, offset);

//            var bytes = Encoding.UTF8.GetBytes(arg0);

//            Array.Copy(bytes, 0, data, offset, bytes.Length);

//            offset += bytes.Length;

//            data[offset++] = 10;
//        }

//        public int getByte()
//        {
//            return data[offset++] & 0xff;
//        }

//        public int getShort()
//        {
//            offset += 2;
//            return ((data[offset - 2] & 0xff) << 8) + (data[offset - 1] & 0xff);
//        }

//        public int getInt()
//        {
//            offset += 4;
//            return ((data[offset - 4] & 0xff) << 24) + ((data[offset - 3] & 0xff) << 16) + ((data[offset - 2] & 0xff) << 8) + (data[offset - 1] & 0xff);
//        }

//        public void getBytes(sbyte[] arg0, int arg1, int arg2)
//        {
//            for (int i = arg1; i < arg1 + arg2; i++)
//                arg0[i] = data[offset++];

//        }

//        public sbyte[] data;
//        public int offset;
//    }
//}
