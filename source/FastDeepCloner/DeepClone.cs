﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace FastDeepCloner
{
    /// <summary>
    /// DeepCloner
    /// </summary>
    public static class DeepCloner
    {
        /// <summary>
        /// Clear cached data
        /// </summary>
        public static void CleanCachedItems()
        {
            FastDeepClonerCachedItems.CleanCachedItems();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectToBeCloned">Desire object to cloned</param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static T Clone<T>(this T objectToBeCloned, FastDeepClonerSettings settings) where T : class
        {
            return (T)new ClonerShared(settings).Clone(objectToBeCloned);
        }


        /// <summary>
        /// DeepClone object
        /// </summary>
        /// <param name="objectToBeCloned">Desire object to cloned</param>
        /// <param name="fieldType">Clone Method</param>
        /// <returns></returns>
        public static object Clone(this object objectToBeCloned, FieldType fieldType = FieldType.PropertyInfo)
        {
            return new ClonerShared(fieldType).Clone(objectToBeCloned);
        }

        /// <summary>
        /// DeepClone object
        /// </summary>
        /// <param name="objectToBeCloned">Desire object to cloned</param>
        /// <param name="fieldType">Clone Method</param>
        /// <returns></returns>
        public static T Clone<T>(this T objectToBeCloned, FieldType fieldType = FieldType.PropertyInfo) where T : class
        {
            return (T)new ClonerShared(fieldType).Clone(objectToBeCloned);
        }

        /// <summary>
        /// DeepClone dynamic(AnonymousType) object
        /// </summary>
        /// <param name="objectToBeCloned">Desire AnonymousType object</param>
        /// <returns></returns>
        public static dynamic CloneDynamic(this object objectToBeCloned)
        {
            return new ClonerShared(FieldType.PropertyInfo).Clone(objectToBeCloned);
        }

        /// <summary>
        /// Create CreateInstance()
        /// this will use ILGenerator to create new object from the cached ILGenerator
        /// This is alot faster then using Activator or GetUninitializedObject.
        /// TThe library will be using ILGenerator or Expression depending on the platform and then cach both the contructorinfo and the type,
        /// so it can be reused later on 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="args">Optional</param>
        /// <returns></returns>
        public static T CreateInstance<T>(params object[] args) where T : class
        {
            return (T)typeof(T).Creator(args);
        }

        /// <summary>
        /// Create CreateInstance()
        /// this will use ILGenerator to create new object from the cached ILGenerator
        /// This is alot faster then using Activator or GetUninitializedObject.
        /// The library will be using ILGenerator or Expression depending on the platform and then cach both the contructorinfo and the type,
        /// so it can be reused later on 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="args">Optional</param>
        /// <returns></returns>
        public static object CreateInstance(this Type type, params object[] args)
        {
            return type.Creator(args);
        }


#if !NETSTANDARD1_3

        /// <summary>
        /// This will try and load the assembly and cached
        /// then from that assembly it will load typePath and also cach it, so it will load much faster next time
        /// </summary>
        /// <param name="typePath">xxxx</param>
        /// <param name="assembly">xxx.dll</param>
        /// <returns></returns>
        public static Type GetObjectType(this string typePath, string assembly)
        {
            return typePath.GetFastType(assembly);
        }



        /// <summary>
        /// Create a type that implement INotifyPropertyChanged PropertyChanged.
        /// Note it will only include properties that are virtual.
        /// If type containe PropertyChanged(object sender, PropertyChangedEventArgs e) it will be bound automatically otherwise you will have to add it manually
        /// <returns></returns>
        public static T CreateProxyInstance<T>()
        {
            return (T)typeof(T).ProxyCreator();
        }

        /// <summary>
        /// Create a type that implement PropertyChanged.
        /// Note it will only include properties that are virtual
        /// If type containe PropertyChanged(object sender, PropertyChangedEventArgs e) it will be bound automatically otherwise you will have to add it manually
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static INotifyPropertyChanged CreateProxyInstance(this Type type)
        {
            return type.ProxyCreator();
        }

#endif
        /// <summary>
        /// will return fieldInfo from the cached fieldinfo. Get and set value is much faster.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<IFastDeepClonerProperty> GetFastDeepClonerFields(this Type type)
        {
            return FastDeepClonerCachedItems.GetFastDeepClonerFields(type).Values.ToList();
        }

        /// <summary>
        /// will return propertyInfo from the cached propertyInfo. Get and set value is much faster.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<IFastDeepClonerProperty> GetFastDeepClonerProperties(this Type type)
        {
            return FastDeepClonerCachedItems.GetFastDeepClonerProperties(type).Values.ToList();
        }

        /// <summary>
        /// Get field by Name
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IFastDeepClonerProperty GetField(this Type type, string name)
        {
            return FastDeepClonerCachedItems.GetFastDeepClonerFields(type).ContainsKey(name)
                ? FastDeepClonerCachedItems.GetFastDeepClonerFields(type)[name]
                : null;
        }

        /// <summary>
        /// Get Property by name
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IFastDeepClonerProperty GetProperty(this Type type, string name)
        {
            return FastDeepClonerCachedItems.GetFastDeepClonerProperties(type).ContainsKey(name)
                ? FastDeepClonerCachedItems.GetFastDeepClonerProperties(type)[name]
                : null;
        }
    }
}
