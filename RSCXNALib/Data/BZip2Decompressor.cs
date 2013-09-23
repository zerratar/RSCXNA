using System;

namespace RSCXNALib.Data
{

    public class BZip2Decompressor
    {

        public static long unpackData(sbyte[] abyte0, int decompressedSize, sbyte[] abyte1, int j, int k)
        {
            BZip2BlockEntry blockEntry = new BZip2BlockEntry();
            blockEntry.inputBuffer = abyte1;
            blockEntry.offset = k;
            blockEntry.outputBuffer = abyte0;
            blockEntry.aek = 0;
            blockEntry.compressedSize = j;
            blockEntry.decompressedSize = decompressedSize;
            blockEntry.afe = 0;
            blockEntry.afd = 0;
            blockEntry.aeh = 0;
            blockEntry.aei = 0;
            blockEntry.aem = 0;
            blockEntry.aen = 0;
            blockEntry.afg = 0;
            readBlock(blockEntry);
            decompressedSize -= blockEntry.decompressedSize;
            return decompressedSize;
        }

        public static long unpackData(byte[] abyte0, int decompressedSize, byte[] abyte1, int j, int k)
        {
            BZip2BlockEntry blockEntry = new BZip2BlockEntry();
            blockEntry.inputBuffer = (sbyte[])(Array)abyte1;
            blockEntry.offset = k;
            blockEntry.outputBuffer = (sbyte[])(Array)abyte0;
            blockEntry.aek = 0;
            blockEntry.compressedSize = j;
            blockEntry.decompressedSize = decompressedSize;
            blockEntry.afe = 0;
            blockEntry.afd = 0;
            blockEntry.aeh = 0;
            blockEntry.aei = 0;
            blockEntry.aem = 0;
            blockEntry.aen = 0;
            blockEntry.afg = 0;
            readBlock(blockEntry);
            decompressedSize -= blockEntry.decompressedSize;
            return decompressedSize;
        }


        private static void glk(BZip2BlockEntry arg0)
        {
            sbyte sbyte4 = arg0.afa;
            int i = arg0.afb;
            int j = arg0.afl;
            int k = arg0.afj;
            int[] ai = BZip2BlockEntry.aga;
            int l = arg0.afi;
            sbyte[] abyte0 = arg0.outputBuffer;
            int i1 = arg0.aek;
            int j1 = arg0.decompressedSize;
            int k1 = j1;
            int l1 = arg0.aha + 1;
            // label0:
            do
            {
                if (i > 0)
                {
                    do
                    {
                        if (j1 == 0)
                            goto done;
                        if (i == 1)
                            break;
                        abyte0[i1] = sbyte4;
                        i--;
                        i1++;
                        j1--;
                    } while (true);
                    if (j1 == 0)
                    {
                        i = 1;
                        break;
                    }
                    abyte0[i1] = sbyte4;
                    i1++;
                    j1--;
                }
                bool flag = true;
                while (flag)
                {
                    flag = false;
                    if (j == l1)
                    {
                        i = 0;
                        goto done;
                    }
                    sbyte4 = (sbyte)k;
                    l = ai[l];
                    byte sbyte0 = (byte)(l & 0xff);
                    l >>= 8;
                    j++;
                    if (sbyte0 != k)
                    {
                        k = sbyte0;
                        if (j1 == 0)
                        {
                            i = 1;
                        }
                        else
                        {
                            abyte0[i1] = sbyte4;
                            i1++;
                            j1--;
                            flag = true;
                            continue;
                        }
                        goto done;
                    }
                    if (j != l1)
                        continue;
                    if (j1 == 0)
                    {
                        i = 1;
                        goto done;
                    }
                    abyte0[i1] = sbyte4;
                    i1++;
                    j1--;
                    flag = true;
                }
                i = 2;
                l = ai[l];
                byte sbyte1 = (byte)(l & 0xff);
                l >>= 8;
                if (++j != l1)
                    if (sbyte1 != k)
                    {
                        k = sbyte1;
                    }
                    else
                    {
                        i = 3;
                        l = ai[l];
                        byte sbyte2 = (byte)(l & 0xff);
                        l >>= 8;
                        if (++j != l1)
                            if (sbyte2 != k)
                            {
                                k = sbyte2;
                            }
                            else
                            {
                                l = ai[l];
                                byte sbyte3 = (byte)(l & 0xff);
                                l >>= 8;
                                j++;
                                i = (sbyte3 & 0xff) + 4;
                                l = ai[l];
                                k = (l & 0xff);
                                l >>= 8;
                                j++;
                            }
                    }
            } while (true);

        done:

            int i2 = arg0.aem;
            arg0.aem += k1 - j1;
            if (arg0.aem < i2)
                arg0.aen++;
            arg0.afa = sbyte4;
            arg0.afb = i;
            arg0.afl = j;
            arg0.afj = k;
            BZip2BlockEntry.aga = ai;
            arg0.afi = l;
            arg0.outputBuffer = abyte0;
            arg0.aek = i1;
            arg0.decompressedSize = j1;
        }

     
        private static void readBlock(BZip2BlockEntry blockEntry)
        {
            int minLens_zt = 0;
            int[] limit_zt = null;
            int[] base_zt = null;
            int[] perm_zt = null;
            blockEntry.blockSize100k = 1;
            if (BZip2BlockEntry.aga == null)
            {
                BZip2BlockEntry.aga = new int[blockEntry.blockSize100k * 0x186a0];
            }
            bool flag19 = true;
            while (flag19)
            {
                sbyte tmpRegister = getUByte(blockEntry);
                if (tmpRegister == 23)
                {
                    return;
                }
                tmpRegister = getUByte(blockEntry);
                tmpRegister = getUByte(blockEntry);
                tmpRegister = getUByte(blockEntry);
                tmpRegister = getUByte(blockEntry);
                tmpRegister = getUByte(blockEntry);
                blockEntry.afg++;
                tmpRegister = getUByte(blockEntry);
                tmpRegister = getUByte(blockEntry);
                tmpRegister = getUByte(blockEntry);
                tmpRegister = getUByte(blockEntry);
                tmpRegister = getBit(blockEntry);
                if (tmpRegister != 0)
                {
                    blockEntry.afc = true;
                }
                else
                {
                    blockEntry.afc = false;
                }
                if (blockEntry.afc)
                {
                    Console.WriteLine("PANIC! RANDOMISED BLOCK!");
                }
                blockEntry.origPtr = 0;
                tmpRegister = getUByte(blockEntry);
                blockEntry.origPtr = blockEntry.origPtr << 8 | tmpRegister & 0xff;
                tmpRegister = getUByte(blockEntry);
                blockEntry.origPtr = blockEntry.origPtr << 8 | tmpRegister & 0xff;
                tmpRegister = getUByte(blockEntry);
                blockEntry.origPtr = blockEntry.origPtr << 8 | tmpRegister & 0xff;
                for (int j = 0; j < 16; j++)
                {
                    sbyte sbyte1 = getBit(blockEntry);
                    if (sbyte1 == 1)
                    {
                        blockEntry.inUse16[j] = true;
                    }
                    else
                    {
                        blockEntry.inUse16[j] = false;
                    }
                }

                for (int k = 0; k < 256; k++)
                {
                    blockEntry.inUse[k] = false;
                }

                for (int l = 0; l < 16; l++)
                {
                    if (blockEntry.inUse16[l])
                    {
                        for (int i3 = 0; i3 < 16; i3++)
                        {
                            sbyte sbyte2 = getBit(blockEntry);
                            if (sbyte2 == 1)
                            {
                                blockEntry.inUse[l * 16 + i3] = true;
                            }
                        }

                    }
                }

                createMaps(blockEntry);
                int alphaSize = blockEntry.inUseOffset + 2;
                int groupCount = getBits(3, blockEntry);
                int selectorCount = getBits(15, blockEntry);
                for (int i1 = 0; i1 < selectorCount; i1++)
                {
                    int j3 = 0;
                    do
                    {
                        sbyte sbyte3 = getBit(blockEntry);
                        if (sbyte3 == 0)
                        {
                            break;
                        }
                        j3++;
                    } while (true);
                     blockEntry.selectorMtf[i1] = (sbyte)j3;
                    //blockEntry.selectorMtf[i1] = (byte)j3;
                }

                sbyte[] pos = new sbyte[6];
                for (sbyte sbyte16 = 0; sbyte16 < groupCount; sbyte16++)
                {
                    pos[sbyte16] = sbyte16;
                }

                for (int j1 = 0; j1 < selectorCount; j1++)
                {
                    sbyte sbyte17 = blockEntry.selectorMtf[j1];
                    sbyte sbyte15 = pos[sbyte17];
                    for (; sbyte17 > 0; sbyte17--)
                    {
                        pos[sbyte17] = pos[sbyte17 - 1];
                    }

                    pos[0] = sbyte15;
                    blockEntry.selector[j1] = sbyte15;
                }

                for (int k3 = 0; k3 < groupCount; k3++)
                {
                    int l6 = getBits(5, blockEntry);
                    for (int k1 = 0; k1 < alphaSize; k1++)
                    {
                        do
                        {
                            sbyte sbyte4 = getBit(blockEntry);
                            if (sbyte4 == 0)
                            {
                                break;
                            }
                            sbyte4 = getBit(blockEntry);
                            if (sbyte4 == 0)
                            {
                                l6++;
                            }
                            else
                            {
                                l6--;
                            }
                        } while (true);
                        blockEntry.len[k3][k1] = (sbyte)l6;
                    }

                }

                for (int l3 = 0; l3 < groupCount; l3++)
                {
                    sbyte minlen = 32;
                    int maxlen = 0;
                    for (int l1 = 0; l1 < alphaSize; l1++)
                    {
                        if (blockEntry.len[l3][l1] > maxlen)
                        {
                            maxlen = blockEntry.len[l3][l1];
                        }
                        if (blockEntry.len[l3][l1] < minlen)
                        {
                            minlen = (sbyte)blockEntry.len[l3][l1];
                        }
                    }

                    createDecodeTables(blockEntry.limit[l3], blockEntry._base[l3], blockEntry.perm[l3], blockEntry.len[l3], minlen, maxlen, alphaSize);
                    blockEntry.agn[l3] = minlen;
                }

                int l4 = blockEntry.inUseOffset + 1;
                int lastShadow = -1;
                int groupPos = 0;
                for (int i2 = 0; i2 <= 255; i2++)
                {
                    blockEntry.unzftab[i2] = 0;
                }

                int j9 = 4095;
                for (int l8 = 15; l8 >= 0; l8--)
                {
                    for (int i9 = 15; i9 >= 0; i9--)
                    {
                        blockEntry.yy[j9] = (byte)(l8 * 16 + i9);
                        j9--;
                    }

                    blockEntry.agg[l8] = j9 + 1;
                }

                int i6 = 0;
                if (groupPos == 0)
                {
                    lastShadow++;
                    groupPos = 50;
                    sbyte sbyte12 = blockEntry.selector[lastShadow];
                    minLens_zt = blockEntry.agn[sbyte12];
                    limit_zt = blockEntry.limit[sbyte12];
                    perm_zt = blockEntry.perm[sbyte12];
                    base_zt = blockEntry._base[sbyte12];
                }
                groupPos--;
                int i7 = minLens_zt;
                int l7;
                sbyte sbyte9;
                for (l7 = getBits(i7, blockEntry); l7 > limit_zt[i7]; l7 = l7 << 1 | sbyte9)
                {
                    i7++;
                    sbyte9 = getBit(blockEntry);
                }

                for (int nextSym = perm_zt[l7 - base_zt[i7]]; nextSym != l4; )
                {
                    if (nextSym == 0 || nextSym == 1)
                    {
                        int j6 = -1;
                        int k6 = 1;
                        do
                        {
                            if (nextSym == 0)
                            {
                                j6 += k6;
                            }
                            else
                            {
                                if (nextSym == 1)
                                {
                                    j6 += 2 * k6;
                                }
                            }
                            k6 *= 2;
                            if (groupPos == 0)
                            {
                                lastShadow++;
                                groupPos = 50;
                                sbyte sbyte13 = blockEntry.selector[lastShadow];
                                minLens_zt = blockEntry.agn[sbyte13];
                                limit_zt = blockEntry.limit[sbyte13];
                                perm_zt = blockEntry.perm[sbyte13];
                                base_zt = blockEntry._base[sbyte13];
                            }
                            groupPos--;
                            int j7 = minLens_zt;
                            int i8;
                            sbyte sbyte10;
                            for (i8 = getBits(j7, blockEntry); i8 > limit_zt[j7]; i8 = i8 << 1 | (byte)sbyte10)
                            {
                                j7++;
                                sbyte10 = getBit(blockEntry);
                            }

                            nextSym = perm_zt[i8 - base_zt[j7]];
                        } while (nextSym == 0 || nextSym == 1);
                        j6++;
                        sbyte sbyte5 = (sbyte)blockEntry.seqToUnseq[blockEntry.yy[blockEntry.agg[0]] & 0xff];
                        blockEntry.unzftab[sbyte5 & 0xff] += j6;
                        for (; j6 > 0; j6--)
                        {
                            BZip2BlockEntry.aga[i6] = sbyte5 & 0xff;
                            i6++;
                        }

                    }
                    else
                    {
                        int j11 = nextSym - 1;
                        sbyte sbyte6;
                        if (j11 < 16)
                        {
                            int j10 = blockEntry.agg[0];
                            sbyte6 = (sbyte)blockEntry.yy[j10 + j11];
                            for (; j11 > 3; j11 -= 4)
                            {
                                int k11 = j10 + j11;
                                blockEntry.yy[k11] = blockEntry.yy[k11 - 1];
                                blockEntry.yy[k11 - 1] = blockEntry.yy[k11 - 2];
                                blockEntry.yy[k11 - 2] = blockEntry.yy[k11 - 3];
                                blockEntry.yy[k11 - 3] = blockEntry.yy[k11 - 4];
                            }

                            for (; j11 > 0; j11--)
                            {
                                blockEntry.yy[j10 + j11] = blockEntry.yy[(j10 + j11) - 1];
                            }

                            blockEntry.yy[j10] = sbyte6;
                        }
                        else
                        {
                            int l10 = j11 / 16;
                            int i11 = j11 % 16;
                            int k10 = blockEntry.agg[l10] + i11;
                            sbyte6 = (sbyte)blockEntry.yy[k10];
                            for (; k10 > blockEntry.agg[l10]; k10--)
                            {
                                blockEntry.yy[k10] = blockEntry.yy[k10 - 1];
                            }

                            blockEntry.agg[l10]++;
                            for (; l10 > 0; l10--)
                            {
                                blockEntry.agg[l10]--;
                                blockEntry.yy[blockEntry.agg[l10]] = blockEntry.yy[(blockEntry.agg[l10 - 1] + 16) - 1];
                            }

                            blockEntry.agg[0]--;
                            blockEntry.yy[blockEntry.agg[0]] = sbyte6;
                            if (blockEntry.agg[0] == 0)
                            {
                                int i10 = 4095;
                                for (int k9 = 15; k9 >= 0; k9--)
                                {
                                    for (int l9 = 15; l9 >= 0; l9--)
                                    {
                                        blockEntry.yy[i10] = blockEntry.yy[blockEntry.agg[k9] + l9];
                                        i10--;
                                    }

                                    blockEntry.agg[k9] = i10 + 1;
                                }

                            }
                        }
                        blockEntry.unzftab[blockEntry.seqToUnseq[sbyte6 & 0xff] & 0xff]++;
                        BZip2BlockEntry.aga[i6] = blockEntry.seqToUnseq[sbyte6 & 0xff] & 0xff;
                        i6++;
                        if (groupPos == 0)
                        {
                            lastShadow++;
                            groupPos = 50;
                            sbyte sbyte14 = blockEntry.selector[lastShadow];
                            minLens_zt = blockEntry.agn[sbyte14];
                            limit_zt = blockEntry.limit[sbyte14];
                            perm_zt = blockEntry.perm[sbyte14];
                            base_zt = blockEntry._base[sbyte14];
                        }
                        groupPos--;
                        int k7 = minLens_zt;
                        int j8;
                        sbyte sbyte11;
                        for (j8 = getBits(k7, blockEntry); j8 > limit_zt[k7]; j8 = j8 << 1 | sbyte11)
                        {
                            k7++;
                            sbyte11 = getBit(blockEntry);
                        }

                        nextSym = perm_zt[j8 - base_zt[k7]];
                    }
                }

                blockEntry.afb = 0;
                blockEntry.afa = 0;
                blockEntry.afm[0] = 0;
                for (int j2 = 1; j2 <= 256; j2++)
                {
                    blockEntry.afm[j2] = blockEntry.unzftab[j2 - 1];
                }

                for (int k2 = 1; k2 <= 256; k2++)
                {
                    blockEntry.afm[k2] += blockEntry.afm[k2 - 1];
                }

                for (int l2 = 0; l2 < i6; l2++)
                {
                    int sbyte7 = ((BZip2BlockEntry.aga[l2]) & 0xff);
                    BZip2BlockEntry.aga[blockEntry.afm[sbyte7 & 0xff]] |= l2 << 8;
                    blockEntry.afm[sbyte7 & 0xff]++;
                }

                blockEntry.afi = BZip2BlockEntry.aga[blockEntry.origPtr] >> 8;
                blockEntry.afl = 0;
                blockEntry.afi = BZip2BlockEntry.aga[blockEntry.afi];
                blockEntry.afj = (blockEntry.afi & 0xff);
                blockEntry.afi >>= 8;
                blockEntry.afl++;
                blockEntry.aha = i6;
                glk(blockEntry);
                if (blockEntry.afl == blockEntry.aha + 1 && blockEntry.afb == 0)
                {
                    flag19 = true;
                }
                else
                {
                    flag19 = false;
                }
            }
        }

        private static sbyte getUByte(BZip2BlockEntry o1)
        {
            return (sbyte)getBits(8, o1);
        }

        private static sbyte getBit(BZip2BlockEntry o1)
        {
            return (sbyte)getBits(1, o1);
        }

        private static int getBits(int arg0, BZip2BlockEntry arg1)
        {
            int i;
            do
            {
                if (arg1.afe >= arg0)
                {
                    int j = arg1.afd >> arg1.afe - arg0 & (1 << arg0) - 1;
                    arg1.afe -= arg0;
                    i = j;
                    break;
                }
                arg1.afd = arg1.afd << 8 | arg1.inputBuffer[arg1.offset] & 0xff;
                arg1.afe += 8;
                arg1.offset++;
                arg1.compressedSize--;
                arg1.aeh++;
                if (arg1.aeh == 0)
                {
                    arg1.aei++;
                }
            } while (true);
            return i;
        }

        private static void createMaps(BZip2BlockEntry arg0)
        {
            arg0.inUseOffset = 0;
            for (int i = 0; i < 256; i++)
            {
                if (arg0.inUse[i])
                {
                    arg0.seqToUnseq[arg0.inUseOffset] = (byte)i;
                    arg0.inUseOffset++;
                }
            }

        }

        private static void createDecodeTables(int[] limit, int[] _base, int[] perm, sbyte[] length, int minlen, int maxlen, int alphasize)
        {
            int i = 0;
            for (int j = minlen; j <= maxlen; j++)
            {
                for (int i2 = 0; i2 < alphasize; i2++)
                {
                    if (length[i2] == j)
                    {
                        perm[i] = i2;
                        i++;
                    }
                }

            }

            for (int k = 0; k < 23; k++)
            {
                _base[k] = 0;
            }

            for (int l = 0; l < alphasize; l++)
            {
                _base[length[l] + 1]++;
            }

            for (int i1 = 1; i1 < 23; i1++)
            {
                _base[i1] += _base[i1 - 1];
            }

            for (int j1 = 0; j1 < 23; j1++)
            {
                limit[j1] = 0;
            }

            int j2 = 0;
            for (int k1 = minlen; k1 <= maxlen; k1++)
            {
                j2 += _base[k1 + 1] - _base[k1];
                limit[k1] = j2 - 1;
                j2 <<= 1;
            }

            for (int l1 = minlen + 1; l1 <= maxlen; l1++)
            {
                _base[l1] = (limit[l1 - 1] + 1 << 1) - _base[l1];
            }

        }
    }

}