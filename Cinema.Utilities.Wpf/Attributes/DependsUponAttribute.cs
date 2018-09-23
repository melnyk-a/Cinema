using System;

namespace Cinema.Utilities.Wpf.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class DependsUponAttribute : Attribute
    {
        private readonly string propertyName;

        public DependsUponAttribute(string propertyName)
        {
            this.propertyName = propertyName;
        }

        public string PropertyName => propertyName;
    }
}