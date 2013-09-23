//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace RSC2DLib.Data
//{
//    public class BZip2BlockEntry
//    {
//        public BZip2BlockEntry()
//        {
//            unzftab = new int[256];
//            afm = new int[257];
//            afn = new int[257];
//            inUse = new bool[256];
//            inUse16 = new bool[16];
//            seqToUnseq = new sbyte[256];
//            yy = new sbyte[4096];
//            agg = new int[16];
//            selector = new sbyte[18002];
//            selectorMtf = new sbyte[18002];
//            len = new sbyte[6][];
//            limit = new int[6][];
//            _base = new int[6][];
//            perm = new int[6][];

//            for (int j = 0; j < 6; j++)
//            {
//                len[j] = new sbyte[258];
//                limit[j] = new int[258];
//                _base[j] = new int[258];
//                perm[j] = new int[258];
//            }

//            agn = new int[6];
//        }

//        public sbyte[] inputBuffer;
//        public int offset;
//        public int compressedSize;
//        public int aeh;
//        public int aei;
//        public sbyte[] outputBuffer;
//        public int aek;
//        public int decompressedSize;
//        public int aem;
//        public int aen;
//        public byte afa;
//        public int afb;
//        public bool afc;
//        public int afd;
//        public int afe;
//        public int blockSize100k;
//        public int afg;
//        public int origPtr;
//        public int afi;
//        public int afj;
//        public int[] unzftab;
//        public int afl;
//        public int[] afm;
//        public int[] afn;
//        public static int[] aga;
//        public int inUseOffset;
//        public bool[] inUse;
//        public bool[] inUse16;
//        public sbyte[] seqToUnseq;
//        public sbyte[] yy;
//        public int[] agg;
//        public sbyte[] selector;
//        public sbyte[] selectorMtf;
//        public sbyte[][] len;
//        public int[][] limit;
//        public int[][] _base;
//        public int[][] perm;
//        public int[] agn;
//        public int aha;
//    }
//}
