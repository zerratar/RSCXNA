//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.IO;
//using System.Net;

//namespace RSC2DLib.Data
//{
//    public class DataOperations
//    {
//        public static MemoryStream openInputStream(String arg0)
//        {
//            Stream obj;
//            if (codeBase == null)
//            {
//                obj = File.OpenRead(arg0);
//            }
//            else
//            {
//                Uri url = new Uri(codeBase, arg0);

//                var req = HttpWebRequest.Create(url.ToString());

//                obj = req.GetResponse().GetResponseStream();
//            }

//            MemoryStream memory = new MemoryStream();
//            int j = 0;
//            sbyte[] buffer = new sbyte[2048];
//            while ((j = obj.Read(buffer, 0, buffer.Length)) != 0)
//            {
//                memory.Write(buffer, 0, j);
//            }


//            return memory;
//        }

//        public static void readFully(String s, sbyte[] abyte0, int i)
//        {
//            //Stream inputstream = openInputStream(s);
//            //var datainputstream = new MemoryStream(inputstream);
//            //try
//            //{
//            //    datainputstream.Read(abyte0, 0, i);
//            //}
//            //catch { }
//            //datainputstream.Close();
//        }

//        public static int getByte(byte byte0)
//        {
//            return byte0 & 0xff;
//        }

//        public static int getShort(sbyte[] abyte0, int i)
//        {
//            //return BitConverter.ToInt16(abyte0, i);
//            return ((abyte0[i] & 0xff) << 8) + (abyte0[i + 1] & 0xff);
//        }

//        public static int getInt(sbyte[] abyte0, int i)
//        {
//            //return BitConverter.ToInt32(abyte0, i);
//            return ((abyte0[i] & 0xff) << 24) + ((abyte0[i + 1] & 0xff) << 16) + ((abyte0[i + 2] & 0xff) << 8) + (abyte0[i + 3] & 0xff);
//        }

//        public static long getLong(sbyte[] abyte0, int i)
//        {
//            //return BitConverter.ToInt64(abyte0, i);
//            return (((long)getInt(abyte0, i) & 0xffffffffL) << 32) + ((long)getInt(abyte0, i + 4) & 0xffffffffL);
//        }

//        public static int getShort2(sbyte[] abyte0, int i)
//        {
//            int j = getByte(abyte0[i]) * 256 + getByte(abyte0[i + 1]);
//            if (j > 32767)
//                j -= 0x10000;
//            return j;
//        }

//        public static int getBits(sbyte[] bytes, int off, int len)
//        {
//            int bitOff = off >> 3;
//            int bitMod = 8 - (off & 7);
//            int k = 0;
//            for (; len > bitMod; bitMod = 8)
//            {
//                k += (bytes[bitOff++] & bitMask[bitMod]) << len - bitMod;
//                len -= bitMod;
//            }

//            if (len == bitMod)
//                k += bytes[bitOff] & bitMask[bitMod];
//            else
//                k += bytes[bitOff] >> bitMod - len & bitMask[len];
//            return k;
//        }

//        public static String formatString(String arg0, int arg1)
//        {
//            String s = "";
//            for (int i = 0; i < arg1; i++)
//                if (i >= arg0.Length)
//                {
//                    s = s + " ";
//                }
//                else
//                {
//                    char c = arg0[i];
//                    if (c >= 'a' && c <= 'z')
//                        s = s + c;
//                    else
//                        if (c >= 'A' && c <= 'Z')
//                            s = s + c;
//                        else
//                            if (c >= '0' && c <= '9')
//                                s = s + c;
//                            else
//                                s = s + '_';
//                }

//            return s;
//        }

//        public static String ipToString(int i)
//        {
//            return (i >> 24 & 0xff) + "." + (i >> 16 & 0xff) + "." + (i >> 8 & 0xff) + "." + (i & 0xff);
//        }

//        public static long nameToHash(String arg0)
//        {
//            String s = "";
//            for (int i = 0; i < arg0.Length; i++)
//            {
//                char c = arg0[i];
//                if (c >= 'a' && c <= 'z')
//                    s = s + c;
//                else
//                    if (c >= 'A' && c <= 'Z')
//                        s = s + (char)((c + 97) - 65);
//                    else
//                        if (c >= '0' && c <= '9')
//                            s = s + c;
//                        else
//                            s = s + ' ';
//            }

//            s = s.Trim();
//            if (s.Length > 12)
//                s = s.Substring(0, 12);
//            long l = 0L;
//            for (int j = 0; j < s.Length; j++)
//            {
//                char c1 = s[j];
//                l *= 37L;
//                if (c1 >= 'a' && c1 <= 'z')
//                    l += (1 + c1) - 97;
//                else
//                    if (c1 >= '0' && c1 <= '9')
//                        l += (27 + c1) - 48;
//            }

//            return l;
//        }

//        public static String hashToName(long arg0)
//        {
//            if (arg0 < 0L)
//                return "invalid_name";
//            String s = "";
//            while (arg0 != 0L)
//            {
//                int i = (int)(arg0 % 37L);
//                arg0 /= 37L;
//                if (i == 0)
//                    s = " " + s;
//                else
//                    if (i < 27)
//                    {
//                        if (arg0 % 37L == 0L)
//                            s = (char)((i + 65) - 1) + s;
//                        else
//                            s = (char)((i + 97) - 1) + s;
//                    }
//                    else
//                    {
//                        s = (char)((i + 48) - 27) + s;
//                    }
//            }
//            return s;
//        }

//        public static int getObjectOffset(String arg0, sbyte[] arg1)
//        {
//            int i = getShort(arg1, 0);
//            int j = 0;
//            arg0 = arg0.ToUpper();
//            for (int k = 0; k < arg0.Length; k++)
//                j = (j * 61 + arg0[k]) - 32;

//            int l = 2 + i * 10;
//            for (int i1 = 0; i1 < i; i1++)
//            {
//                int j1 = (arg1[i1 * 10 + 2] & 0xff) * 0x1000000 + (arg1[i1 * 10 + 3] & 0xff) * 0x10000 + (arg1[i1 * 10 + 4] & 0xff) * 256 + (arg1[i1 * 10 + 5] & 0xff);
//                int k1 = (arg1[i1 * 10 + 9] & 0xff) * 0x10000 + (arg1[i1 * 10 + 10] & 0xff) * 256 + (arg1[i1 * 10 + 11] & 0xff);
//                if (j1 == j)
//                    return l;
//                l += k1;
//            }

//            return 0;
//        }

//        public static int getSoundLength(String arg0, sbyte[] arg1)
//        {
//            int i = getShort(arg1, 0);
//            int j = 0;
//            arg0 = arg0.ToUpper();
//            for (int k = 0; k < arg0.Length; k++)
//                j = (j * 61 + arg0[k]) - 32;

//            for (int i1 = 0; i1 < i; i1++)
//            {
//                int j1 = (arg1[i1 * 10 + 2] & 0xff) * 0x1000000 + (arg1[i1 * 10 + 3] & 0xff) * 0x10000 + (arg1[i1 * 10 + 4] & 0xff) * 256 + (arg1[i1 * 10 + 5] & 0xff);
//                int k1 = (arg1[i1 * 10 + 6] & 0xff) * 0x10000 + (arg1[i1 * 10 + 7] & 0xff) * 256 + (arg1[i1 * 10 + 8] & 0xff);
//                if (j1 == j)
//                    return k1;
//            }

//            return 0;
//        }

//        public static sbyte[] loadData(String s, int i, sbyte[] abyte0)
//        {
//            return loadData(s, i, abyte0, null);
//        }

//        public static sbyte[] loadData(String arg0, int arg1, sbyte[] arg2, sbyte[] arg3)
//        {
//            int i = (arg2[0] & 0xff) * 256 + (arg2[1] & 0xff);
//            int j = 0;
//            arg0 = arg0.ToUpper();
//            for (int k = 0; k < arg0.Length; k++)
//                j = (j * 61 + arg0[k]) - 32;

//            int l = 2 + i * 10;
//            for (int i1 = 0; i1 < i; i1++)
//            {
//                int j1 = (arg2[i1 * 10 + 2] & 0xff) * 0x1000000 + (arg2[i1 * 10 + 3] & 0xff) * 0x10000 + (arg2[i1 * 10 + 4] & 0xff) * 256 + (arg2[i1 * 10 + 5] & 0xff);
//                int k1 = (arg2[i1 * 10 + 6] & 0xff) * 0x10000 + (arg2[i1 * 10 + 7] & 0xff) * 256 + (arg2[i1 * 10 + 8] & 0xff);
//                int l1 = (arg2[i1 * 10 + 9] & 0xff) * 0x10000 + (arg2[i1 * 10 + 10] & 0xff) * 256 + (arg2[i1 * 10 + 11] & 0xff);
//                if (j1 == j)
//                {
//                    if (arg3 == null)
//                        arg3 = new sbyte[k1 + arg1];
//                    if (k1 != l1)
//                    {
//                        BZip2Decompressor.unpackData(arg3, k1, arg2, l1, l);
//                    }
//                    else
//                    {
//                        for (int i2 = 0; i2 < k1; i2++)
//                            arg3[i2] = arg2[l + i2];

//                    }
//                    return arg3;
//                }
//                l += l1;
//            }

//            return null;
//        }

//        public static Uri codeBase = null;
//        private static int[] bitMask = {
//        0, 1, 3, 7, 15, 31, 63, 127, 255, 511, 
//        1023, 2047, 4095, 8191, 16383, 32767, 65535, 0x1ffff, 0x3ffff, 0x7ffff, 
//        0xfffff, 0x1fffff, 0x3fffff, 0x7fffff, 0xffffff, 0x1ffffff, 0x3ffffff, 0x7ffffff, 0xfffffff, 0x1fffffff, 
//        0x3fffffff, 0x7fffffff, -1
//    };
//    }
//}
