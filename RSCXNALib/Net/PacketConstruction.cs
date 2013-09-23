using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RSCXNALib.Net
{
    public class PacketConstruction
    {

        public virtual void closeStream()
        {
        }

        public void createPacket(int id)
        {
            if (packetStart > (maxPacketLength * 4) / 5)
                try
                {
                    writePacket(0);
                }
                catch (IOException ioexception)
                {
                    error = true;
                    errorText = ioexception.ToString();//ioexception.getMessage();
                }
            if (packetData == null)
                packetData = new byte[maxPacketLength];
            packetData[packetStart + 2] = (byte)id;
            packetData[packetStart + 3] = 0;
            packetOffset = packetStart + 3;
            skipOffset = 8;
        }

        public void writePacket(int packetId)
        {
            if (error)
            {
                packetStart = 0;
                packetOffset = 3;
                error = false;
                throw new IOException(errorText);
            }
            packetCount++;
            if (packetCount < packetId)
                return;
            if (packetStart > 0)
            {
                packetCount = 0;
                writeToBuffer(packetData, 0, packetStart);
            }
            packetStart = 0;
            packetOffset = 3;
        }

        public void addByte(int i)
        {
            packetData[packetOffset++] = (byte)i;
        }

        public void addString(String s)
        {
            var bytes = Encoding.UTF8.GetBytes(s);

            //s.getBytes(0, s.length(), packetData, packetOffset);

            Array.Copy(bytes, 0, packetData, packetOffset, bytes.Length);

            packetOffset += bytes.Length;//s.length();
        }

        public void addLong(long l)
        {
            addInt((int)(l >> 32));
            addInt((int)(l & -1L));
        }

        public virtual void writeToBuffer(byte[] abyte0, int i, int j)
        {
        }

        public virtual void readInputStream(int i, int j, sbyte[] abyte0)
        {
        }

        public int readShort()
        {
            int i = readByte();
            int j = readByte();
            return i * 256 + j;
        }

        public virtual int read()
        {
            return 0;
        }

        public void read(int i, sbyte[] abyte0)
        {
            readInputStream(i, 0, abyte0);
        }

        public void addInt(int i)
        {
            packetData[packetOffset++] = (byte)(i >> 24);
            packetData[packetOffset++] = (byte)(i >> 16);
            packetData[packetOffset++] = (byte)(i >> 8);
            packetData[packetOffset++] = (byte)i;
        }

        public void flush(bool format = true)
        {
            if (format)
                formatPacket();
            writePacket(0);
        }

        // bad
        //public virtual int available()
        //{
        //    Console.WriteLine("packetconstruction.available WRONG");
        //    return 0;
        //}

        public void addShort(int i)
        {
            packetData[packetOffset++] = (byte)(i >> 8);
            packetData[packetOffset++] = (byte)i;
        }

        public long readLong()
        {
            long l = readShort();
            long l1 = readShort();
            long l2 = readShort();
            long l3 = readShort();
            return (l << 48) + (l1 << 32) + (l2 << 16) + l3;
        }

        public void formatPacket()
        {
            if (skipOffset != 8)
                packetOffset++;
            int j = packetOffset - packetStart - 2;
            packetData[packetStart] = (byte)(j >> 8);
            packetData[packetStart + 1] = (byte)j;
            if (maxPacketLength <= 10000)
            {
                int k = packetData[packetStart + 2] & 0xff;
                packetCommandCount[k]++;
                packetLengthCount[k] += packetOffset - packetStart;
            }
            packetStart = packetOffset;
#warning formatPacket shouldnt flush

            flush(false);
            // mudclient.sendingPing = false;
        }

        public void addBytes(byte[] data, int off, int len)
        {
            for (int i = 0; i < len; i++)
                packetData[packetOffset++] = data[off + i];

        }

        public bool hasData()
        {
            return packetStart > 0;
        }

        public int readPacket(sbyte[] arg0)
        {
            try
            {
                _read++;
                if (maxPacketReadCount > 0 && _read > maxPacketReadCount)
                {
                    error = true;
                    errorText = "time-out";
                    maxPacketReadCount += maxPacketReadCount;
                    return 0;
                }
                if (length == 0 /*&& available() >= 2*/)
                {
                    sbyte[] buf = new sbyte[2];
                    readInputStream(2, 0, buf);
                    length = ((short)((buf[0] & 0xff) << 8) | (short)(buf[1] & 0xff)) + 1;
                }
                if (length > 0 /*&& available() >= length*/)
                {
                    read(length, arg0);
                    int i = length;
                    length = 0;
                    _read = 0;
                    return i;
                }
            }
            catch (IOException ioexception)
            {
                error = true;
                errorText = ioexception.ToString();//ioexception.getMessage();
            }
            return 0;
        }

        public int readByte()
        {
            return read();
        }

        public PacketConstruction()
        {
            packetOffset = 3;
            skipOffset = 8;
            maxPacketLength = 5000;
            errorText = "";
            error = false;
        }

        public int length;
        public int _read;
        public int maxPacketReadCount;
        public int packetStart;
        private int packetOffset;
        private int skipOffset;
        public byte[] packetData;
        public static int[] packetCommandCount = new int[256];
        public int maxPacketLength;
        public static int[] packetLengthCount = new int[256];
        public int packetCount;
        public String errorText;
        public bool error;
    }

}
