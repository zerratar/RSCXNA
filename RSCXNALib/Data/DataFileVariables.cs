namespace RSCXNALib.Data
{

	internal class DataFileVariables
	{

		internal DataFileVariables()
		{
			afk = new int[256];
			afm = new int[257];
			afn = new int[257];
			agc = new bool[256];
			agd = new bool[16];
			age = new int[256];
			agf = new int[4096];
			agg = new int[16];
			agh = new sbyte[18002];
			agi = new sbyte[18002];
//ORIGINAL LINE: agj = new sbyte[6][258];
//JAVA TO C# CONVERTER NOTE: The following call to the 'RectangularArrays' helper class reproduces the rectangular array initialization that is automatic in Java:
			agj = RectangularArrays.ReturnRectangularSbyteArray(6, 258);
//ORIGINAL LINE: agk = new int[6][258];
//JAVA TO C# CONVERTER NOTE: The following call to the 'RectangularArrays' helper class reproduces the rectangular array initialization that is automatic in Java:
			agk = RectangularArrays.ReturnRectangularIntArray(6, 258);
//ORIGINAL LINE: agl = new int[6][258];
//JAVA TO C# CONVERTER NOTE: The following call to the 'RectangularArrays' helper class reproduces the rectangular array initialization that is automatic in Java:
			agl = RectangularArrays.ReturnRectangularIntArray(6, 258);
//ORIGINAL LINE: agm = new int[6][258];
//JAVA TO C# CONVERTER NOTE: The following call to the 'RectangularArrays' helper class reproduces the rectangular array initialization that is automatic in Java:
			agm = RectangularArrays.ReturnRectangularIntArray(6, 258);
			agn = new int[6];
		}

		internal sbyte[] aee;
		internal int aef;
		internal int aeg;
		internal int aeh;
		internal int aei;
		internal sbyte[] aej;
		internal int aek;
		internal int ael;
		internal int aem;
		internal int aen;
		internal sbyte afa;
		internal int afb;
		internal bool afc;
		internal int afd;
		internal int afe;
		internal int aff;
		internal int afg;
		internal int afh;
		internal int afi;
		internal int afj;
		internal int[] afk;
		internal int afl;
		internal int[] afm;
		internal int[] afn;
		public static int[] aga;
		internal int agb;
		internal bool[] agc;
		internal bool[] agd;
		internal int[] age;
		internal int[] agf;
		internal int[] agg;
		internal sbyte[] agh;
		internal sbyte[] agi;
		internal sbyte[][] agj;
		internal int[][] agk;
		internal int[][] agl;
		internal int[][] agm;
		internal int[] agn;
		internal int aha;
	}

}