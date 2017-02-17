using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace NFinal.Advanced
{
    public class EnumUtil
    {
        /// <summary>
        /// 将枚举类型的各项转换成一个可遍历的序列
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, object>> Enumerate(Type enumType)
        {
            CheckEnumType(enumType);
            return Enum.GetValues(enumType)
                .Cast<object>()
                .Select(v => new KeyValuePair<string, object>(v.ToString(), v));
        }

        /// <summary>
        /// 将枚举类型的各项转换成一个可遍历的序列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, T>> Enumerate<T>()
        {
            var type = typeof(T);
            CheckEnumType(type);
            return Enum.GetValues(type)
                .Cast<T>()
                .Select(v => new KeyValuePair<string, T>(v.ToString(), v));
        }

        /// <summary>
        /// 将枚举类型的各项转换成一个可遍历的序列
        /// </summary>
        /// <typeparam name="TUnderlying"></typeparam>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, TUnderlying>> Enumerate<TUnderlying>(Type enumType)
        {
            CheckEnumType(enumType);
            return Enum.GetValues(enumType)
                .Cast<object>()
                .Select(v => new KeyValuePair<string, TUnderlying>(v.ToString(), (TUnderlying) v));
        }

        /// <summary>
        /// 将枚举类型的各项转换成一个可遍历的序列
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <typeparam name="TUnderlying"></typeparam>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, TUnderlying>> Enumerate<TEnum, TUnderlying>()
        {
            var type = typeof(TEnum);
            return Enumerate<TUnderlying>(type);
        }

        /// <summary>
        /// 将枚举类型的各项转换成一个 <c>NameValueCollection</c>，
        /// 由参数 <c>isNameAsKey</c> 决定其 Key 是枚举名称还是枚举数值。
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="isNameAsKey"></param>
        /// <remark>当 <c>isNameAsKey == false</c> 时，可能会出现重复的 Key，
        /// 这种情况下应该谨慎使用生成的 <c>NameValueCollection</c>，
        /// 可以使用其 <c>GetValues().First()</c> 来获取唯一值则不是逗号分隔的所有值。</remark>
        /// <returns></returns>
        public static NameValueCollection ToNameValueCollection(Type enumType, bool isNameAsKey)
        {
            var all = Enumerate(enumType);
            var unType = Enum.GetUnderlyingType(enumType);

            var collection = new NameValueCollection();

            var valueGetter = new Func<object, string>(v => Convert.ChangeType(v, unType).ToString());

            return isNameAsKey
                ? all.Aggregate(collection, (c, e) => {
                    c.Add(e.Key, valueGetter(e.Value));
                    return c;
                })
                : all.Aggregate(collection, (c, e) => {
                    c.Add(valueGetter(e.Value), e.Key);
                    return c;
                });
        }

        private static void CheckEnumType(Type type)
        {
            if (!type.IsEnum)
            {
                throw new ArgumentException(string.Format("specified type [{0}] is not a enum type", type.FullName));
            }
        }
    }
}
