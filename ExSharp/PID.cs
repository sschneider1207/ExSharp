namespace ExSharp
{
    public sealed class PID
    {
        public string Node { get; }
        public int ID { get; }
        public int Serial { get; }
        public byte Creation { get; }

        public PID(string node, int id, int serial, byte creation)
        {
            Node = node;
            ID = id;
            Serial = serial;
            Creation = creation;
        }

        internal PID()
        {
            Node = string.Empty;
        }
    }
}
