using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;

namespace RSCXNALib.Net
{
    public class StreamClass : PacketConstruction
    {

        Thread connectionThread = null;

        public StreamClass(/*Socket*/ TcpClient socket, GameApplet a1 = null)
        {
            socketClosing = false;
            socketClosed = true;
            this.socket = socket;

            inputStream = new BinaryReader(socket.GetStream()); // socket.getInputStream();
            outputStream = new BinaryWriter(socket.GetStream()); // socket.getOutputStream();
            socketClosed = false;

            connectionThread = new Thread(new ThreadStart(this.run));
            connectionThread.Start();
            //   a1.startThread(this);
        }

        public bool Connected
        {
            get
            {
                return this.socket != null && this.socket.Connected;
            }
        }


        private void OnRead(IAsyncResult iar)
        {
            try
            {
                var len = inputStream.BaseStream.EndRead(iar);
                if (len != 0)
                {

                }
            }
            catch { }
            try { inputStream.BaseStream.BeginRead(this.buffer, 0, this.buffer.Length, new AsyncCallback(OnRead), inputStream); }
            catch
            {
                // We have been disconnected :<
                closeStream();
            }
        }

        public override void closeStream()
        {
            base.closeStream();
            socketClosing = true;
            try
            {
                if (inputStream != null)
                    inputStream.Close();
                if (outputStream != null)
                    outputStream.Close();
                if (socket != null)
                    socket.Close();
            }
            catch (IOException _ex)
            {
                System.Console.WriteLine("Error closing stream");
            }
            socketClosed = true;
            // synchronized {
            //lock (syncLock)
            //{
            //    Monitor.Pulse(syncLock);
            //}
            //}

            connectionThread.Abort();

            buffer = null;
        }

        public override int read()
        {
            if (socketClosing)
                return 0;
            else
                return inputStream.ReadByte();
        }

        // We dont like this in C#
        //public override int available()
        //{
        //    if (socketClosing)
        //        return 0;
        //    else
        //    {
        //        //   return (int)inputStream.BaseStream.Length;//Available();
        //        return 2;
        //    }
        //}

        public override void readInputStream(int arg0, int arg1, sbyte[] arg2)
        {
            if (socketClosing)
                return;
            int i = 0;
            int j;
            try
            {
                byte[] org = new byte[arg2.Length];
                for (; i < arg0; i += j)
                {
                    if (socketClosing)
                        return;
                    if (!socket.Connected)
                        return;

                    if ((j = inputStream.Read(org, i + arg1, arg0 - i)) <= 0) ;
                    //throw new IOException("EOF"); 

                    for (int k = 0; k < arg2.Length; k++)
                    {
                        arg2[k] = (sbyte)org[k];
                    }

                }
            }
            catch
            {
                try
                {
                    //connectionThread.Suspend();
                    connectionThread.Abort();
                }
                catch { }
            }

        }

        private readonly object syncLock = new object();

        // [MethodImpl(MethodImplOptions.Synchronized)]
        public override void writeToBuffer(byte[] arg0, int arg1, int arg2)
        {
            if (socketClosing)
                return;
            if (buffer == null)
                buffer = new byte[5000];
            // lock (syncLock)
            {
                for (int i = 0; i < arg2; i++)
                {
                    buffer[offset] = arg0[i + arg1];
                    offset = (offset + 1) % 5000;
                    if (offset == (dataWritten + 4900) % 5000)
                        throw new IOException("buffer overflow");
                }
                //     Monitor.PulseAll(syncLock);
                //Monitor.Pulse(connectionThread);
            }
        }

        void OnWrite(IAsyncResult iar)
        {
            try
            {
                outputStream.BaseStream.EndWrite(iar);
                dataWritten = (dataWritten + lastWriteLen) % 5000;
                try
                {
                    if (offset == dataWritten)
                        outputStream.Flush();
                }
                catch (IOException ioexception1)
                {
                    base.error = true;
                    base.errorText = "Twriter:" + ioexception1;
                }
            }
            catch { }
        }
        int lastWriteLen = 0;
        public void run()
        {
            while (!socketClosed) //  && connectionThread.ThreadState != ThreadState.AbortRequested && connectionThread.ThreadState != ThreadState.Aborted
            {
                int i;
                int j;
                // lock (syncLock)
                {
                    if (offset == dataWritten)
                        try
                        {
                            //  wait();
                            //Monitor.Wait(syncLock);
                            // System.Threading.Thread.Sleep(10);
                        }
                        catch { }
                    if (socketClosed)
                        return;
                    j = dataWritten;
                    if (offset >= dataWritten)
                        i = offset - dataWritten;
                    else
                        i = 5000 - dataWritten;
                }
                if (i > 0)
                {
                    try
                    {


                        outputStream.Write(buffer, j, i);
                    }
                    catch (IOException ioexception)
                    {
                        base.error = true;
                        base.errorText = "Twriter:" + ioexception;
                    }
                    lastWriteLen = i;

                    {
                        dataWritten = (dataWritten + i) % 5000;
                        try
                        {
                            if (offset == dataWritten)
                                outputStream.Flush();
                        }
                        catch (IOException ioexception1)
                        {
                            base.error = true;
                            base.errorText = "Twriter:" + ioexception1;
                        }
                    }
                }
                System.Threading.Thread.Sleep(1);
            }
        }

        private BinaryReader /*InputStream*/ inputStream;
        private BinaryWriter /*OutputStream*/ outputStream;
        private TcpClient /*Socket*/ socket;
        private bool socketClosing;
        private byte[] buffer;
        private int dataWritten;
        private int offset;
        private bool socketClosed;

    }
}

