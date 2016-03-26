using System;

namespace ExSharp
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExSharpModuleAttribute : Attribute
    {
        private readonly string _moduleName;

        public ExSharpModuleAttribute(string moduleName)
        {
            _moduleName = moduleName.StartsWith("Elixir.") ? moduleName : $"Elixir.{moduleName}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is ExSharpModuleAttribute))
            {
                return false;
            }

            var attr = (ExSharpModuleAttribute)obj;
            return attr.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode() => _moduleName.GetHashCode();

        public override string ToString() => _moduleName;

        public override bool IsDefaultAttribute() => true;

        public override bool Match(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is ExSharpModuleAttribute))
            {
                return false;
            }

            var attr = (ExSharpModuleAttribute)obj;
            return attr.GetHashCode() == GetHashCode();
        }

        public override object TypeId => GetHashCode();        
    }
}
