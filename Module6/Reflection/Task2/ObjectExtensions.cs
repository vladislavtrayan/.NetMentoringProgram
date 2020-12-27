using Mono.Reflection;

namespace Task2
{
    public static class ObjectExtensions
    {
        public static void SetReadOnlyProperty(this object obj, string propertyName, object newValue)
        {
            var backingField = obj.GetType().GetProperty(propertyName).GetBackingField();;
            backingField.SetValue(obj, newValue);
        }

        public static void SetReadOnlyField(this object obj, string filedName, object newValue)
        {
            obj.GetType().GetField(filedName).SetValue(obj, newValue);
        }
    }
}
