using System;
using static ExSharp.Protobuf.ModuleSpec.Types;

namespace ExSharp
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class ExSharpFunctionAttribute : Attribute
    {
        private readonly string _fullName;
        private readonly string _name;
        private readonly int _arity;

        public ExSharpFunctionAttribute(string name, int arity)
        {
            if(arity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(arity), "Arity cannot be negative");
            }
            _name = name;
            _arity = arity;
            _fullName = GenFullName(_name, _arity);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is ExSharpFunctionAttribute))
            {
                return false;
            }

            var attr = (ExSharpFunctionAttribute)obj;
            return attr.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode() => _fullName.GetHashCode();

        public override string ToString() => _fullName;

        public override bool IsDefaultAttribute() => true;

        public override bool Match(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is ExSharpFunctionAttribute))
            {
                return false;
            }

            var attr = (ExSharpFunctionAttribute)obj;
            return attr.GetHashCode() == GetHashCode();
        }

        public override object TypeId => GetHashCode();

        internal static string GenFullName(string name, int arity) => $"{name}/{arity}";

        internal Function ToProto() => new Function { Name = _name, Arity = _arity };
    }
}
