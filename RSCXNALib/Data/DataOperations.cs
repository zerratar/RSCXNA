using System.IO;
using System;
using System.Net;
using System.Collections.Generic;
namespace RSCXNALib.Data
{


    public class DataOperations
    {

        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: public static java.io.InputStream openInputStream(String objName) throws java.io.IOException
        //public static InputStream openInputStream(string objName)
        //{
        //    object obj;
        //    if (codeBase == null)
        //    {
        //        obj = new BufferedInputStream(new FileInputStream(objName));
        //    }
        //    else
        //    {
        //        URL url = new URL(codeBase, objName);
        //        obj = url.openStream();
        //    }
        //    return ((InputStream)(obj));
        //}

        public static MemoryStream openInputStream(String arg0)
        {

            //return org.moparscape.msc.client.DataOperations.getByte(byte0);

            Stream obj;
            if (codeBase == null)
            {
                obj = File.OpenRead(arg0);
            }
            else
            {
                Uri url = new Uri(codeBase, arg0);

                var req = HttpWebRequest.Create(url.ToString());

                obj = req.GetResponse().GetResponseStream();
            }

            MemoryStream memory = new MemoryStream();
            int j = 0;
            byte[] buffer = new byte[2048];



            //using (BinaryWriter binaryWriter = new BinaryWriter(outputStream))
            //{
            //    binaryWriter.Write((sbyte[])(Array)sbytes);
            //}


            while ((j = obj.Read(buffer, 0, buffer.Length)) != 0)
            {
                //memory.Write(buffer, 0, j);
                memory.Write((byte[])(Array)buffer, 0, j);
            }


            return memory;
        }

        static private sbyte[] streamToSbyte(BinaryReader stream, int length)
        {
            List<sbyte> list = new List<sbyte>();
            {
                sbyte ch;
                var i = 0;
                while ((ch = stream.ReadSByte()) != -1 && i < length)
                {
                    list.Add(ch);
                    i++;
                }
            }
            return list.ToArray();
        }

        public static void readFully(string p, sbyte[] abyte1, int i)
        {
            //org.moparscape.msc.client.DataOperations.readFully(p, abyte1, i);
            using (var stream = openInputStream(p))
            {
                abyte1 = streamToSbyte(new BinaryReader(stream), i);
                //for (int j = 0; j < i; j++) { 
                //    abyte1[j] = 
                //}
                //stream.Read(abyte1, 0, i);
            }
        }

        public static void readFully(string p, byte[] abyte1, int i) {
            using (var stream = openInputStream(p))
            {
                stream.Read(abyte1, 0, i);
            }
        }

        ////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        ////ORIGINAL LINE: public static void readFully(String s, byte abyte0[] , int i) throws java.io.IOException
        //public static void readFully(string s, sbyte[] abyte0, int i)
        //{
        //    InputStream inputstream = openInputStream(s);
        //    DataInputStream datainputstream = new DataInputStream(inputstream);
        //    try
        //    {
        //        datainputstream.readFully(abyte0, 0, i);
        //    }
        //    catch (EOFException _ex)
        //    {
        //    }
        //    datainputstream.close();
        //}


        //public static int getByte(byte byte0)
        //{
        //    //return org.moparscape.msc.client.DataOperations.getByte(byte0);
        //    return byte0 & 0xff;
        //}
        public static int getByte(sbyte byte0)
        {
            return byte0 & 0xff;
        }
        //public static int getShort(byte[] abyte0, int i)
        //{
        //    //return org.moparscape.msc.client.DataOperations.getShort(abyte0, i);
        //    var val = ((abyte0[i] & 0xff) << 8) + (abyte0[i + 1] & 0xff);
        //    var val2 = abyte0[i];
        //    var val3 = abyte0[i] & 0xff;
        //    var val4 = abyte0[i] & 0xff;
        //    var val5 = abyte0[i + 1] & 0xff;
        //    return val;
        //}


        public static int getShort(sbyte[] abyte0, int i)
        {
            //return org.moparscape.msc.client.DataOperations.getShort(abyte0, i);
            var val = ((abyte0[i] & 0xff) << 8) + (abyte0[i + 1] & 0xff);
            var val2 = abyte0[i];
            var val3 = abyte0[i] & 0xff;
            var val4 = abyte0[i] & 0xff;
            var val5 = abyte0[i + 1] & 0xff;
            return val;
        }

        //public static int getInt(sbyte[] abyte0, int i)
        //{
        //    return ((abyte0[i] & 0xff) << 24) + ((abyte0[i + 1] & 0xff) << 16) + ((abyte0[i + 2] & 0xff) << 8) + (abyte0[i + 3] & 0xff);
        //}

        public static long getLong(sbyte[] abyte0, int i)
        {
            //return org.moparscape.msc.client.DataOperations.getLong(abyte0, i);
            return (((long)getInt(abyte0, i) & 0xffffffffL) << 32) + ((long)getInt(abyte0, i + 4) & 0xffffffffL);
        }

        public static long getLong(byte[] abyte0, int i)
        {
            //return org.moparscape.msc.client.DataOperations.getLong(abyte0, i);
            return (((long)getInt(abyte0, i) & 0xffffffffL) << 32) + ((long)getInt(abyte0, i + 4) & 0xffffffffL);
        }

        //public static int getShort(sbyte[] abyte0, int i)
        //{
        //    return ((abyte0[i] & 0xff) << 8) + (abyte0[i + 1] & 0xff);
        //}

        public static int getInt(sbyte[] abyte0, int i)
        {
            //return org.moparscape.msc.client.DataOperations.getInt(abyte0, i);
            return ((abyte0[i] & 0xff) << 24) + ((abyte0[i + 1] & 0xff) << 16) + ((abyte0[i + 2] & 0xff) << 8) + (abyte0[i + 3] & 0xff);
        }

        public static int getInt(byte[] abyte0, int i)
        {
            //return org.moparscape.msc.client.DataOperations.getInt(abyte0, i);
            return ((abyte0[i] & 0xff) << 24) + ((abyte0[i + 1] & 0xff) << 16) + ((abyte0[i + 2] & 0xff) << 8) + (abyte0[i + 3] & 0xff);
        }

        //public static long getLong(sbyte[] abyte0, int i)
        //{
        //    return (((long)getInt(abyte0, i) & 0xffffffffL) << 32) + ((long)getInt(abyte0, i + 4) & 0xffffffffL);
        //}
        public static int getShort2(sbyte[] abyte0, int i)
        {
            int j = getByte(abyte0[i]) * 256 + getByte(abyte0[i + 1]);
            if (j > 32767)
            {
                j -= 0x10000;
            }
            return j;
        }

        //public static int getShort2(byte[] abyte0, int i)
        //{
        //    int j = getByte(abyte0[i]) * 256 + getByte(abyte0[i + 1]);
        //    if (j > 32767)
        //    {
        //        j -= 0x10000;
        //    }
        //    return j;
        //}


        public static int getBits(sbyte[] bytes, int off, int len)
        {
            //return org.moparscape.msc.client.DataOperations.getBits(bytes, off, len);

            int bitOff = off >> 3;
            int bitMod = 8 - (off & 7);
            int k = 0;
            for (; len > bitMod; bitMod = 8)
            {
                k += (bytes[bitOff++] & bitMask[bitMod]) << len - bitMod;
                len -= bitMod;
            }

            if (len == bitMod)
            {
                k += bytes[bitOff] & bitMask[bitMod];
            }
            else
            {
                k += bytes[bitOff] >> bitMod - len & bitMask[len];
            }
            return k;
        }

        //public static int getBits(byte[] bytes, int off, int len)
        //{
        //    int bitOff = off >> 3;
        //    int bitMod = 8 - (off & 7);
        //    int k = 0;
        //    for (; len > bitMod; bitMod = 8)
        //    {
        //        k += (bytes[bitOff++] & bitMask[bitMod]) << len - bitMod;
        //        len -= bitMod;
        //    }

        //    if (len == bitMod)
        //    {
        //        k += bytes[bitOff] & bitMask[bitMod];
        //    }
        //    else
        //    {
        //        k += bytes[bitOff] >> bitMod - len & bitMask[len];
        //    }
        //    return k;
        //}

        public static string formatString(string arg0, int arg1)
        {
            string s = "";
            for (int i = 0; i < arg1; i++)
            {
                if (i >= arg0.Length)
                {
                    s = s + " ";
                }
                else
                {
                    char c = arg0[i];
                    if (c >= 'a' && c <= 'z')
                    {
                        s = s + c;
                    }
                    else
                    {
                        if (c >= 'A' && c <= 'Z')
                        {
                            s = s + c;
                        }
                        else
                        {
                            if (c >= '0' && c <= '9')
                            {
                                s = s + c;
                            }
                            else
                            {
                                s = s + '_';
                            }
                        }
                    }
                }
            }

            return s;
        }

        public static string ipToString(int i)
        {
            return (i >> 24 & 0xff) + "." + (i >> 16 & 0xff) + "." + (i >> 8 & 0xff) + "." + (i & 0xff);
        }

        public static long nameToHash(string arg0)
        {
            string s = "";
            for (int i = 0; i < arg0.Length; i++)
            {
                char c = arg0[i];
                if (c >= 'a' && c <= 'z')
                {
                    s = s + c;
                }
                else
                {
                    if (c >= 'A' && c <= 'Z')
                    {
                        s = s + (char)((c + 97) - 65);
                    }
                    else
                    {
                        if (c >= '0' && c <= '9')
                        {
                            s = s + c;
                        }
                        else
                        {
                            s = s + ' ';
                        }
                    }
                }
            }

            s = s.Trim();
            if (s.Length > 12)
            {
                s = s.Substring(0, 12);
            }
            long l = 0L;
            for (int j = 0; j < s.Length; j++)
            {
                char c1 = s[j];
                l *= 37L;
                if (c1 >= 'a' && c1 <= 'z')
                {
                    l += (1 + c1) - 97;
                }
                else
                {
                    if (c1 >= '0' && c1 <= '9')
                    {
                        l += (27 + c1) - 48;
                    }
                }
            }

            return l;
        }

        public static string hashToName(long arg0)
        {
            if (arg0 < 0L)
            {
                return "invalid_name";
            }
            string s = "";
            while (arg0 != 0L)
            {
                int i = (int)(arg0 % 37L);
                arg0 /= 37L;
                if (i == 0)
                {
                    s = " " + s;
                }
                else
                {
                    if (i < 27)
                    {
                        if (arg0 % 37L == 0L)
                        {
                            s = (char)((i + 65) - 1) + s;
                        }
                        else
                        {
                            s = (char)((i + 97) - 1) + s;
                        }
                    }
                    else
                    {
                        s = (char)((i + 48) - 27) + s;
                    }
                }
            }
            return s;
        }

        public static long getObjectOffset(string objName, sbyte[] objData)
        {
            //return org.moparscape.msc.client.DataOperations.getObjectOffset(objName, objData);

            int i = getShort(objData, 0);
            int j = 0;
            objName = objName.ToUpper();
            for (int k = 0; k < objName.Length; k++)
            {
                j = (j * 61 + objName[k]) - 32;
            }

            long l = 2 + i * 10;
            for (int i1 = 0; i1 < i; i1++)
            {
                long j1 = (objData[i1 * 10 + 2] & 0xff) * 0x1000000 + (objData[i1 * 10 + 3] & 0xff) * 0x10000 + (objData[i1 * 10 + 4] & 0xff) * 256 + (objData[i1 * 10 + 5] & 0xff);
                long k1 = (objData[i1 * 10 + 9] & 0xff) * 0x10000 + (objData[i1 * 10 + 10] & 0xff) * 256 + (objData[i1 * 10 + 11] & 0xff);
                if (j1 == j)
                {
                    return l;
                }
                l += k1;
            }

            return 0;
        }

        public static int getSoundLength(string arg0, sbyte[] arg1)
        {
            // return org.moparscape.msc.client.DataOperations.getSoundLength(objName, objData);

            int i = getShort(arg1, 0);
            int j = 0;
            arg0 = arg0.ToUpper();
            for (int k = 0; k < arg0.Length; k++)
            {
                j = (j * 61 + arg0[k]) - 32;
            }

            for (int i1 = 0; i1 < i; i1++)
            {
                int j1 = (arg1[i1 * 10 + 2] & 0xff) * 0x1000000 + (arg1[i1 * 10 + 3] & 0xff) * 0x10000 + (arg1[i1 * 10 + 4] & 0xff) * 256 + (arg1[i1 * 10 + 5] & 0xff);
                int k1 = (arg1[i1 * 10 + 6] & 0xff) * 0x10000 + (arg1[i1 * 10 + 7] & 0xff) * 256 + (arg1[i1 * 10 + 8] & 0xff);
                if (j1 == j)
                {
                    return k1;
                }
            }

            return 0;
        }

        public static byte[] loadData(string s, int i, byte[] abyte0)
        {
            return loadData(s, i, abyte0, null);
        }

        public static byte[] loadData(string arg0, int arg1, byte[] arg2, byte[] arg3)
        {

            //return org.moparscape.msc.client.DataOperations.loadData(objName, objData, arg2, arg3);

            int i = (arg2[0] & 0xff) * 256 + (arg2[1] & 0xff);
            int j = 0;
            arg0 = arg0.ToUpper();
            for (int k = 0; k < arg0.Length; k++)
            {
                j = (j * 61 + arg0[k]) - 32;
            }

            int l = 2 + i * 10;
            for (int i1 = 0; i1 < i; i1++)
            {
                int j1 = (arg2[i1 * 10 + 2] & 0xff) * 0x1000000 + (arg2[i1 * 10 + 3] & 0xff) * 0x10000 + (arg2[i1 * 10 + 4] & 0xff) * 256 + (arg2[i1 * 10 + 5] & 0xff);
                int k1 = (arg2[i1 * 10 + 6] & 0xff) * 0x10000 + (arg2[i1 * 10 + 7] & 0xff) * 256 + (arg2[i1 * 10 + 8] & 0xff);
                int l1 = (arg2[i1 * 10 + 9] & 0xff) * 0x10000 + (arg2[i1 * 10 + 10] & 0xff) * 256 + (arg2[i1 * 10 + 11] & 0xff);
                if (j1 == j)
                {
                    if (arg3 == null)
                    {
                        arg3 = new byte[k1 + arg1];
                    }
                    if (k1 != l1)
                    {
                        DataFileDecrypter.unpackData(arg3, k1, arg2, l1, l);
                    }
                    else
                    {
                        for (long i2 = 0; i2 < k1; i2++)
                        {
                            arg3[i2] = arg2[l + i2];
                        }

                    }
                    return arg3;
                }
                l += l1;
            }

            return null;
        }

        public static sbyte[] loadData(string s, int i, sbyte[] abyte0)
        {
            return loadData(s, i, abyte0, null);
        }

        public static sbyte[] loadData(string arg0, int arg1, sbyte[] arg2, sbyte[] arg3)
        {

            //return org.moparscape.msc.client.DataOperations.loadData(objName, objData, arg2, arg3);

            int i = (arg2[0] & 0xff) * 256 + (arg2[1] & 0xff);
            int j = 0;
            arg0 = arg0.ToUpper();
            for (int k = 0; k < arg0.Length; k++)
            {
                j = (j * 61 + arg0[k]) - 32;
            }

            int l = 2 + i * 10;
            for (int i1 = 0; i1 < i; i1++)
            {
                int j1 = (arg2[i1 * 10 + 2] & 0xff) * 0x1000000 + (arg2[i1 * 10 + 3] & 0xff) * 0x10000 + (arg2[i1 * 10 + 4] & 0xff) * 256 + (arg2[i1 * 10 + 5] & 0xff);
                int k1 = (arg2[i1 * 10 + 6] & 0xff) * 0x10000 + (arg2[i1 * 10 + 7] & 0xff) * 256 + (arg2[i1 * 10 + 8] & 0xff);
                int l1 = (arg2[i1 * 10 + 9] & 0xff) * 0x10000 + (arg2[i1 * 10 + 10] & 0xff) * 256 + (arg2[i1 * 10 + 11] & 0xff);
                if (j1 == j)
                {
                    if (arg3 == null)
                    {
                        arg3 = new sbyte[k1 + arg1];
                    }
                    if (k1 != l1)
                    {
                        DataFileDecrypter.unpackData(arg3, k1, arg2, l1, l);
                    }
                    else
                    {
                        for (long i2 = 0; i2 < k1; i2++)
                        {
                            arg3[i2] = arg2[l + i2];
                        }

                    }
                    return arg3;
                }
                l += l1;
            }

            return null;
        }

        public static Uri codeBase = null;
        private static int[] bitMask = { 0, 1, 3, 7, 15, 31, 63, 127, 255, 511, 1023, 2047, 4095, 8191, 16383, 32767, 65535, 0x1ffff, 0x3ffff, 0x7ffff, 0xfffff, 0x1fffff, 0x3fffff, 0x7fffff, 0xffffff, 0x1ffffff, 0x3ffffff, 0x7ffffff, 0xfffffff, 0x1fffffff, 0x3fffffff, 0x7fffffff, -1 };

    }

}