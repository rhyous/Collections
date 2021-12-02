using System;
using System.Reflection;

namespace Rhyous.Collections
{
    /// <summary>This provides simple methods to get values of any object instance using reflection.</summary>
    public static class ValueAccessorExtensions
    {
        private const int MaxInheritanceLevels = 10;

        /// <summary>The <see cref="StringComparison"/>.</summary>
        public static StringComparison Comparison { get; set; } = StringComparison.OrdinalIgnoreCase;

        /// <summary>A static method to get the PropertyInfo of a property of any object.</summary>
        /// <param name="o">The object that has the property</param>
        /// <param name="propertyName">The name of the  property</param>
        /// <returns>PropertyInfo object. It has the property name and a useful GetValue() method.</returns>
        public static PropertyInfo GetPropertyInfo(this object o, string propertyName)
        {
            return o?.GetType().GetPropertyInfo(propertyName);
        }

        /// <summary>A static method to get the PropertyInfo of a property of any object.</summary>
        /// <param name="t">The Type that has the property</param>
        /// <param name="propertyName">The name of the  property</param>
        /// <returns>PropertyInfo object. It has the property name and a useful GetValue() method.</returns>
        public static PropertyInfo GetPropertyInfo(this Type t, string propertyName)
        {
            if (t is null || string.IsNullOrEmpty(propertyName)) { return null; }

                   // 1. Look for public exact name
            return t.GetProperty(propertyName)
                   // 2. Look for public case insensitive name
                   ?? t.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase)
                   // 3. Look for non-public exact name
                   ?? t.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic)
                   // 4. Look for non-public case insensitive name
                   ?? t.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.IgnoreCase);
        }

        /// <summary>A static method to get the value of a property of any object.</summary>
        /// <param name="o">The instance from which to read the value.</param>
        /// <param name="propertyName">The name of the property</param>
        /// <param name="defaultValue">The default value if the property doesn't exist. If the property exists, but the value is null, the default value will also be used.</param>
        /// <returns>The value of the property boxed as an object.</returns>
        public static object GetPropertyValue(this object o, string propertyName, object defaultValue = null)
        {
            return o?.GetType().GetPropertyInfo(propertyName)?.GetValue(o) ?? defaultValue;
        }

        /// <summary>A static method to get the value of a property of any object casted to a type or default(T) if the cast fails.</summary>
        /// <typeparam name="T">The type expected to be returned</typeparam>
        /// <param name="o">The instance from which to read the value.</param>
        /// <param name="propertyName">The name of the property</param>
        /// <param name="defaultValue">The default value if the property doesn't exist.</param>
        /// <returns>The value of the property boxed as a T.</returns>
        public static T GetPropertyValue<T>(this object o, string propertyName, T defaultValue = default(T))
        {
            var propInfo = o.GetType().GetPropertyInfo(propertyName);
            if (propInfo == null || (propInfo.PropertyType != typeof(T) && !typeof(T).IsAssignableFrom(propInfo.PropertyType)))
                return defaultValue;
            var value = (T)propInfo.GetValue(o);
            if (value == null)
                return defaultValue;
            return value;
        }

        /// <summary>A static method to get the value of a static property of any object.</summary>
        /// <param name="t">The type from which to read the static property value.</param>
        /// <param name="propertyName">The name of the property</param>
        /// <param name="defaultValue">The default value if the property doesn't exist.</param>
        /// <returns>The value of the property boxed as an object.</returns>
        public static object GetStaticPropertyValue(this Type t, string propertyName, object defaultValue = null)
        {
            var tmpType = t;
            object obj = null;
            int count = 0;
            while (obj == null && tmpType != null && count < MaxInheritanceLevels)
            {
                var propInfo = tmpType.GetProperty(propertyName);
                if (propInfo != null)
                    return tmpType.GetProperty(propertyName).GetValue(null);
                tmpType = tmpType.BaseType;
                count++;
            }
            return obj ?? defaultValue;
        }

        /// <summary>A static method to get the FieldInfo of a field of any object.</summary>
        /// <param name="o">The object that has the field</param>
        /// <param name="fieldName">The name of the field</param>
        /// <returns>FieldInfo object. It has the field name and a useful GetValue() method.</returns>
        public static FieldInfo GetFieldInfo(this object o, string fieldName)
        {
            return o?.GetType()?.GetFieldInfo(fieldName);
        }

        /// <summary>A static method to get the FieldInfo of a field of any object.</summary>
        /// <param name="t">The Type that has the field</param>
        /// <param name="fieldName">The name of the field</param>
        /// <returns>FieldInfo object. It has the field name and a useful GetValue() method.</returns>
        public static FieldInfo GetFieldInfo(this Type t, string fieldName)
        {
            if (t is null || string.IsNullOrWhiteSpace(fieldName)) { return null; }

            // 1. Look for public exact name
            return t.GetField(fieldName)
                   // 2. Look for public case insensitive name
                   ?? t.GetField(fieldName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase)
                   // 3. Look for non-public exact name
                   ?? t.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic)
                   // 4. Look for non-public case insensitive name
                   ?? t.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.IgnoreCase);
        }

        /// <summary>A static method to get the FieldInfo of a field of any object.</summary>
        /// <param name="o">The instance from which to read the value.</param>
        /// <param name="fieldName">The name of the field.</param>
        /// <param name="defaultValue">The default value if the field doesn't exist or if the value of the field is null.</param>
        /// <returns>The value of the property boxed as an object.</returns>
        public static object GetFieldValue(this object o, string fieldName, object defaultValue = null)
        {
            return GetFieldInfo(o.GetType(), fieldName)?.GetValue(o) ?? defaultValue;
        }

        /// <summary>Allows for returnig the default of any type at runtime, without knowing that type at compile time.</summary>
        /// <param name="t">The type.</param>
        /// <returns>The result of default(T) for type t.</returns>
        /// <remarks>This shouldn't be needed in newer versions of .Net as 'default' works without parameters.</remarks>
        public static object GetDefault(this Type t)
        {
            return typeof(ValueAccessorExtensions).GetMethod(nameof(Default), BindingFlags.Static | BindingFlags.NonPublic).MakeGenericMethod(t).Invoke(null, null);
        }

        internal static T Default<T>()
        {
            return default(T);
        }
    }
}