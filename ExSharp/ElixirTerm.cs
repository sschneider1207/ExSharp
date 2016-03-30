using ExSharp.Protobuf;
using Google.Protobuf;
using System;
using System.Linq;
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
            if(bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            if(bytes.Length < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(bytes), "Erlang term format requires a minimum of two bytes.");
            }

            int sourceIndex;
            int length;
            if(bytes[0] == _termIdentifier)
            {
                sourceIndex = 1;
                length = bytes.Length - 1;
            }
            else
            {
                sourceIndex = 0;
                length = bytes.Length;                
            }
            Tag = (TagType)bytes[sourceIndex];
            _bytes = new byte[length];
            Array.Copy(bytes, sourceIndex, _bytes, 0, length);
        }

        internal static ElixirTerm FromByteString(ByteString bytes) => new ElixirTerm(bytes.ToByteArray());

        internal static FunctionResult ToFunctionResult(ElixirTerm term) => new FunctionResult { Value = ByteString.CopyFrom(new byte[] { _termIdentifier }.Concat(term._bytes).ToArray()) };
        
        #region Get
        public static byte GetByte(ElixirTerm term) => term.Tag == TagType.BYTE ? term._bytes[2] : (byte)0;

        public static int GetInt(ElixirTerm term)
        {
            if(term.Tag != TagType.INT)
            {
                return 0;
            }

            var buf = new byte[4];
            Array.Copy(term._bytes, 1, buf, 0, 4);

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
            Array.Copy(term._bytes, 1, buf, 0, 8);

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
            Array.Copy(term._bytes, 1, len, 0, 2);

            if(BitConverter.IsLittleEndian)
            {
                Array.Reverse(len);
            }

            var atomLen = BitConverter.ToInt16(len, 0);

            var atomBuf = new byte[atomLen];
            Array.Copy(term._bytes, 3, atomBuf, 0, atomLen);

            return _latinEncoding.GetString(atomBuf, 0, atomLen);
        }
        
        public static string GetUTF8String(ElixirTerm term)
        {
            if(term.Tag != TagType.BINARY)
            {
                return string.Empty;
            }

            var len = new byte[4];
            Array.Copy(term._bytes, 1, len, 0, 4);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(len);
            }

            var stringLen = BitConverter.ToInt32(len, 0);

            var stringBuf = new byte[stringLen];
            Array.Copy(term._bytes, 5, stringBuf, 0, stringLen);

            return Encoding.UTF8.GetString(stringBuf, 0, stringLen);
        }
        
        public static PID GetPID(ElixirTerm term)
        {
            if(term.Tag != TagType.PID)
            {
                return new PID();
            }

            string node;
            int curIndex = 1;
            if(term._bytes[curIndex] == (byte)TagType.ATOM)
            {
                curIndex = curIndex + 1;
                var len = new byte[2];
                Array.Copy(term._bytes, curIndex, len, 0, 2);
                if(BitConverter.IsLittleEndian)
                {
                    Array.Reverse(len);
                }

                curIndex = curIndex + 2;
                var nodeLen = BitConverter.ToInt16(len, 0);
                var nodeBuf = new byte[nodeLen];
                Array.Copy(term._bytes, curIndex, nodeBuf, 0, nodeLen);

                node = _latinEncoding.GetString(nodeBuf, 0, nodeLen);
                curIndex = curIndex + nodeLen;
            }
            else
            {
                return new PID();
            }
            
            var idBuf = new byte[4];
            Array.Copy(term._bytes, curIndex, idBuf, 0, 4);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(idBuf);
            }
            var id = BitConverter.ToInt32(idBuf, 0);

            curIndex = curIndex + 4;
            var serialBuf = new byte[4];
            Array.Copy(term._bytes, curIndex, serialBuf, 0, 4);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(serialBuf);
            }
            var serial = BitConverter.ToInt32(serialBuf, 0);

            curIndex = curIndex + 4;
            var creation = term._bytes[curIndex];
            return new PID(node, id, serial, creation);
        }

        public static Reference GetReference(ElixirTerm term)
        {
            if (term.Tag != TagType.NEW_REFERENCE)
            {
                return new Reference();
            }

            var curIndex = 1;
            var lenBuf = new byte[2];
            Array.Copy(term._bytes, curIndex, lenBuf, 0, 2);
            if(BitConverter.IsLittleEndian)
            {
                Array.Reverse(lenBuf);
            }
            var len = BitConverter.ToInt16(lenBuf, 0);

            curIndex = curIndex + 2;
            string node;
            if (term._bytes[curIndex] == (byte)TagType.ATOM)
            {
                curIndex = curIndex + 1;
                var nodeLenBuf = new byte[2];
                Array.Copy(term._bytes, curIndex, nodeLenBuf, 0, 2);
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(nodeLenBuf);
                }

                curIndex = curIndex + 2;
                var nodeLen = BitConverter.ToInt16(nodeLenBuf, 0);
                var nodeBuf = new byte[nodeLen];
                Array.Copy(term._bytes, curIndex, nodeBuf, 0, nodeLen);

                node = _latinEncoding.GetString(nodeBuf, 0, nodeLen);
                curIndex = curIndex + nodeLen;
            }
            else
            {
                return new Reference();
            }

            byte creation = term._bytes[curIndex];

            curIndex = curIndex + 1;
            var id = new int[len];
            for(var i = 0; i < len; i++, curIndex = curIndex + 4)
            {
                var idBuf = new byte[4];
                Array.Copy(term._bytes, curIndex, idBuf, 0, 4);
                if(BitConverter.IsLittleEndian)
                {
                    Array.Reverse(idBuf);
                }

                id[i] = BitConverter.ToInt32(idBuf, 0);
            }

            return new Reference(node, creation, id);
        }
        #endregion Get

        #region Make
        public static ElixirTerm MakeByte(byte val) => new ElixirTerm(new byte[] {(byte)TagType.BYTE, val });

        public static ElixirTerm MakeInt(int val)
        {
            var buf = BitConverter.GetBytes(val);
            if(BitConverter.IsLittleEndian)
            {
                Array.Reverse(buf);
            }
            var termBuf = new byte[5];
            termBuf[0] = (byte)TagType.INT;
            Array.Copy(buf, 0, termBuf, 1, 4);
            return new ElixirTerm(termBuf);
        }

        public static ElixirTerm MakeDouble(double val)
        {
            var buf = BitConverter.GetBytes(val);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(buf);
            }
            var termBuf = new byte[9];
            termBuf[0] = (byte)TagType.NEW_FLOAT;
            Array.Copy(buf, 0, termBuf, 1, 8);
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
            var termBuf = new byte[3 + atomLen];
            termBuf[0] = (byte)TagType.ATOM;
            Array.Copy(len, 0, termBuf, 1, 2);
            Array.Copy(buf, 0, termBuf, 3, atomLen);
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
            var termBuf = new byte[5 + stringLen];
            termBuf[0] = (byte)TagType.BINARY;
            Array.Copy(len, 0, termBuf, 1, 4);
            Array.Copy(buf, 0, termBuf, 5, stringLen);
            return new ElixirTerm(termBuf);
        }

        public static ElixirTerm MakePID(PID pid)
        {
            var atom = MakeAtom(pid.Node);
            var len = 1 + atom._bytes.Length + 4 + 4 + 1; // tag, atom, id, serial, creation
            var termBuff = new byte[len];
            int curIndex = 0;
            termBuff[curIndex] = (byte)TagType.PID;

            curIndex = curIndex + 1;
            Array.Copy(atom._bytes, 0, termBuff, curIndex, atom._bytes.Length);

            curIndex = curIndex + atom._bytes.Length;
            var idBuf = BitConverter.GetBytes(pid.ID);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(idBuf);
            }
            Array.Copy(idBuf, 0, termBuff, curIndex, 4);

            curIndex = curIndex + 4;
            var serialBuf = BitConverter.GetBytes(pid.Serial);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(serialBuf);
            }
            Array.Copy(serialBuf, 0, termBuff, curIndex, 4);

            curIndex = curIndex + 4;
            termBuff[curIndex] = pid.Creation;
            return new ElixirTerm(termBuff);
        }
        #endregion Make        
    }
}
