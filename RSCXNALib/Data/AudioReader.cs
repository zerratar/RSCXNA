using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSCXNALib.Data
{
    public class AudioReader
    {
        public AudioReader()
        {
           // AudioPlayer.player.start(this);
        }

        public void stop()
        {
          //  AudioPlayer.player.stop(this);
        }

        public void play(sbyte[] abyte0, int i, int j)
        {
            data = abyte0;
            offset = i;
            length = i + j;
        }

        public int read(sbyte[] arg0, int arg1, int arg2)
        {
            for (int i = 0; i < arg2; i++)
                if (offset < length)
                    arg0[arg1 + i] = data[offset++];
                else
                    arg0[arg1 + i] = 0;

            return arg2;
        }

        public int read()
        {
            sbyte[] abyte0 = new sbyte[1];
            read(abyte0, 0, 1);
            return abyte0[0];
        }

        sbyte[] data;
        int offset;
        int length;
    }
}
