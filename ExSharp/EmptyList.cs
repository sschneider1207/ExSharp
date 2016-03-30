namespace ExSharp
{
    public sealed class EmptyList
    {
        private const string _emptyList = "[]";
        public override string ToString() => _emptyList;

        internal EmptyList() { }

        public override bool Equals(object obj)
        {
            if(obj == null)
            {
                return false;
            }

            return (EmptyList)obj != null;
        }

        public override int GetHashCode() => _emptyList.GetHashCode();
    }
}
