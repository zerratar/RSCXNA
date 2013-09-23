namespace RSCXNALib.Data
{

	internal class BZip2BlockEntry
	{

		internal BZip2BlockEntry()
		{
			unzftab = new int[256];
			afm = new int[257];
			afn = new int[257];
			inUse = new bool[256];
			inUse16 = new bool[16];
			seqToUnseq = new int[256];
			yy = new int[4096];
			agg = new int[16];
			selector = new sbyte[18002];
			selectorMtf = new sbyte[18002];
//ORIGINAL LINE: len = new sbyte[6][258];
//JAVA TO C# CONVERTER NOTE: The following call to the 'RectangularArrays' helper class reproduces the rectangular array initialization that is automatic in Java:
			len = RectangularArrays.ReturnRectangularSbyteArray(6, 258);
//ORIGINAL LINE: limit = new int[6][258];
//JAVA TO C# CONVERTER NOTE: The following call to the 'RectangularArrays' helper class reproduces the rectangular array initialization that is automatic in Java:
			limit = RectangularArrays.ReturnRectangularIntArray(6, 258);
//ORIGINAL LINE: _base = new int[6][258];
//JAVA TO C# CONVERTER NOTE: The following call to the 'RectangularArrays' helper class reproduces the rectangular array initialization that is automatic in Java:
			_base = RectangularArrays.ReturnRectangularIntArray(6, 258);
//ORIGINAL LINE: perm = new int[6][258];
//JAVA TO C# CONVERTER NOTE: The following call to the 'RectangularArrays' helper class reproduces the rectangular array initialization that is automatic in Java:
			perm = RectangularArrays.ReturnRectangularIntArray(6, 258);
			agn = new int[6];
		}

		internal sbyte[] inputBuffer;
		internal int offset;
		internal int compressedSize;
		internal int aeh;
		internal int aei;
		internal sbyte[] outputBuffer;
		internal int aek;
		internal int decompressedSize;
		internal int aem;
		internal int aen;
		internal sbyte afa;
		internal int afb;
		internal bool afc;
		internal int afd;
		internal int afe;
		internal int blockSize100k;
		internal int afg;
		internal int origPtr;
		internal int afi;
		internal int afj;
		internal int[] unzftab;
		internal int afl;
		internal int[] afm;
		internal int[] afn;
		public static int[] aga;
		internal int inUseOffset;
		internal bool[] inUse;
		internal bool[] inUse16;
		internal int[] seqToUnseq;
		internal int[] yy;
		internal int[] agg;
		internal sbyte[] selector;
		internal sbyte[] selectorMtf;
		internal sbyte[][] len;
		internal int[][] limit;
		internal int[][] _base;
		internal int[][] perm;
		internal int[] agn;
		internal int aha;
	}

}