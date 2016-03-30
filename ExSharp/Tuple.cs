namespace ExSharp
{
    public sealed class Tuple
    {
        public int Arity { get; }

        private ElixirTerm[] _elements;

        public ElixirTerm this[int i] => i < Arity ? _elements[i] : null;

        internal Tuple(int arity, ElixirTerm[] elements)
        {
            Arity = arity;
            _elements = elements;
        }
    }
}
