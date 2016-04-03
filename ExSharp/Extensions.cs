using ExSharp.Protobuf;
using Google.Protobuf;
using System;
using System.Linq;

namespace ExSharp
{
    internal static class Extensions
    {
        private static readonly byte[] _protoHeader = new byte[] { 112, 198, 7, 27 };
        private static readonly byte[] _callbackHeader = new byte[] { 177, 229, 171, 48 };

        internal static void WriteCallback(this ElixirCallback message)
        {
            var messageBytes = message.ToByteArray();
            var totalBytes = _callbackHeader.Concat(_protoHeader)
                .Concat(messageBytes)
                .ToArray();
            using (var stream = Console.OpenStandardOutput(4 + message.CalculateSize()))
            {
                stream.Write(totalBytes, 0, totalBytes.Length);
                stream.Flush();
            }
        }
        internal static void WriteProto(this IMessage message)
        {
            var messageBytes = message.ToByteArray();
            var totalBytes = _protoHeader.Concat(messageBytes)
                .ToArray();
            using(var stream = Console.OpenStandardOutput(4 + message.CalculateSize()))
            {
                stream.Write(totalBytes, 0, totalBytes.Length);
                stream.Flush();
            }            
        }
    }
}
