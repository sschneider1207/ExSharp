using System.Linq;

namespace ExSharp
{
    public sealed class Reference
    {
        public string Node { get; }
        public byte Creation { get; }
        public int[] ID { get; }

        internal Reference(string node, byte creation, int[] id)
        {
            Node = node;
            Creation = creation;
            ID = id;
        }

        public override string ToString() => $"#Reference<{Creation}.{string.Join(".", ID.Reverse().Select(i => i.ToString()))}>";

        public override bool Equals(object obj)
        {
            if(obj == null)
            {
                return false;
            }

            var @ref = (Reference)obj;
            if(@ref == null)
            {
                return false;
            }

            return Node == @ref.Node && Creation == @ref.Creation && ID.SequenceEqual(@ref.ID);
        }

        public override int GetHashCode()
        {
            var hash = Node.GetHashCode();
            hash = hash >> Creation;
            foreach(var i in ID)
            {
                hash = hash >> i;
            }
            return hash;
        }
    }
}
