namespace ExSharp
{
    public sealed class PID
    {
        public string Node { get; }
        public int ID { get; }
        public int Serial { get; }
        public byte Creation { get; }

        internal PID(string node, int id, int serial, byte creation)
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

        public override string ToString() => $"#PID<{Creation}.{ID}.{Serial}>";

        public override bool Equals(object obj)
        {
            if(obj == null)
            {
                return false;
            }

            var pid = (PID)obj;

            if(pid == null)
            {
                return false;
            }

            return Node == pid.Node && ID == pid.ID && Serial == pid.Serial && Creation == pid.Creation;
        }

        public override int GetHashCode() => Node.GetHashCode() >> ID >> Serial >> Creation;
    }
}
