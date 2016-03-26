using ExSharp.Protobuf;
using Google.Protobuf;
using System;
using System.Text;

namespace ExSharp
{
    public class ElixirTerm
    {
        private static readonly Encoding _latinEncoding = Encoding.GetEncoding("ISO8859-1"); // latin1
        private const byte _termIdentifier = 131;
        public enum TagType
        {
            BYTE = 97,
            INT = 98,
            FLOAT = 99,
            ATOM = 100,
            REFERENCE = 101,
            PID = 103,
            SMALL_TUPLE = 104,
            LARGE_TUPLE = 105,
            MAP = 116,
            EMPTY_LIST = 106,
            STRING = 107,
            LIST = 108,
            BINARY = 109,
            SMALL_BIG = 110,
            LARGE_BIG = 111,
            NEW_REFERENCE = 114,
            SMALL_ATOM = 115,
            NEW_FLOAT = 70,
            ATOM_UTF8 = 118,
            SMALL_ATOM_UTF8 = 119
        }

        private readonly byte[] _bytes;
        public TagType Tag { get; }

        private ElixirTerm(byte[] bytes)
        {
            if(bytes.Length < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(bytes), "Erlang term format requires a minimum of two bytes.");
            }

            if(bytes[0] != _termIdentifier)
            {
                throw new ArgumentException(nameof(bytes), $"Erlang term format requires an identifier of <<{_termIdentifier}>> at position 0.");
            }

            _bytes = bytes;
            Tag = (TagType) bytes[1];
        }

        internal static ElixirTerm FromByteString(ByteString bytes) => new ElixirTerm(bytes.ToByteArray());

        internal static FunctionResult ToFunctionResult(ElixirTerm term) => new FunctionResult { Value = ByteString.CopyFrom(term._bytes) };
        
        #region Get
        public static byte GetByte(ElixirTerm term) => term.Tag == TagType.BYTE ? term._bytes[2] : (byte)0;

        public static int GetInt(ElixirTerm term)
        {
            if(term.Tag != TagType.INT)
            {
                return 0;
            }

            var buf = new byte[4];
            Array.Copy(term._bytes, 2, buf, 0, 4);

            if(BitConverter.IsLittleEndian)
            {
                Array.Reverse(buf);
            }

            return BitConverter.ToInt32(buf, 0);
        }
        
        public static double GetDouble(ElixirTerm term)
        {
            if (term.Tag != TagType.NEW_FLOAT)
            {
                return 0d;
            }

            var buf = new byte[8];
            Array.Copy(term._bytes, 2, buf, 0, 8);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(buf);
            }

            return BitConverter.ToDouble(buf, 0);
        }

        public static string GetAtom(ElixirTerm term)
        {
            if(term.Tag != TagType.ATOM)
            {
                return string.Empty;
            }

            var len = new byte[2];
            Array.Copy(term._bytes, 2, len, 0, 2);

            if(BitConverter.IsLittleEndian)
            {
                Array.Reverse(len);
            }

            var atomLen = BitConverter.ToInt16(len, 0);

            var atomBuf = new byte[atomLen];
            Array.Copy(term._bytes, 4, atomBuf, 0, atomLen);

            return _latinEncoding.GetString(atomBuf, 0, atomLen);
        }
        
        public static string GetUTF8String(ElixirTerm term)
        {
            if(term.Tag != TagType.BINARY)
            {
                return string.Empty;
            }

            var len = new byte[4];
            Array.Copy(term._bytes, 2, len, 0, 4);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(len);
            }

            var stringLen = BitConverter.ToInt32(len, 0);

            var stringBuf = new byte[stringLen];
            Array.Copy(term._bytes, 6, stringBuf, 0, stringLen);

            return Encoding.UTF8.GetString(stringBuf, 0, stringLen);
        }
        #endregion Get

        #region Make
        public static ElixirTerm MakeByte(byte val) => new ElixirTerm(new byte[] { _termIdentifier, (byte)TagType.BYTE, val });

        public static ElixirTerm MakeInt(int val)
        {
            var buf = BitConverter.GetBytes(val);
            if(BitConverter.IsLittleEndian)
            {
                Array.Reverse(buf);
            }
            var termBuf = new byte[6];
            termBuf[0] = _termIdentifier;
            termBuf[1] = (byte)TagType.INT;
            Array.Copy(buf, 0, termBuf, 2, 4);
            return new ElixirTerm(termBuf);
        }

        public static ElixirTerm MakeDouble(double val)
        {
            var buf = BitConverter.GetBytes(val);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(buf);
            }
            var termBuf = new byte[10];
            termBuf[0] = _termIdentifier;
            termBuf[1] = (byte)TagType.NEW_FLOAT;
            Array.Copy(buf, 0, termBuf, 2, 8);
            return new ElixirTerm(termBuf);
        }

        public static ElixirTerm MakeAtom(string val)
        {
            var buf = _latinEncoding.GetBytes(val);
            if(buf.Length > 255)
            {
                throw new ArgumentException(nameof(val), $"Max supported byte length is 255, is {buf.Length}.");
            }
            var atomLen = (short)buf.Length;
            var len = BitConverter.GetBytes(atomLen);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(len);
            }
            var termBuf = new byte[4 + atomLen];
            termBuf[0] = _termIdentifier;
            termBuf[1] = (byte)TagType.ATOM;
            Array.Copy(len, 0, termBuf, 2, 2);
            Array.Copy(buf, 0, termBuf, 4, atomLen);
            return new ElixirTerm(termBuf);
        }

        public static ElixirTerm MakeUTF8String(string val)
        {
            var buf = Encoding.UTF8.GetBytes(val);
            var stringLen = buf.Length;
            var len = BitConverter.GetBytes(stringLen);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(len);
            }
            var termBuf = new byte[6 + stringLen];
            termBuf[0] = _termIdentifier;
            termBuf[1] = (byte)TagType.BINARY;
            Array.Copy(len, 0, termBuf, 2, 4);
            Array.Copy(buf, 0, termBuf, 6, stringLen);
            return new ElixirTerm(termBuf);
        }
        #endregion Make        
    }
}
