namespace ExSharp
{
    public sealed class Export
    {
        public string Module { get; }

        public string Function { get; }

        public byte Arity { get; }

        public Export(string module, string function, byte arity)
        {
            Module = module;
            Function = function;
            Arity = arity;
        }
    }
}
