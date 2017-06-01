//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : DynamicDictionary.cs
//        Description :动态字典类，通过dictionary.key的方式可以访问dictionar[key]中的值
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;

namespace NFinal
{
    /// <summary>
    /// 动态字典类，通过dictionary.key的方式可以访问dictionar[key]中的值
    /// </summary>
    public class DynamicDictionary : DynamicObject, IDictionary<string, object>
    {
        private readonly IDictionary<string, object> _obj;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="obj"></param>
        public DynamicDictionary(IDictionary<string, object> obj)
        {
            _obj = obj;
        }
        /// <summary>
        /// 属性索引
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this[string key]
        {
            get
            {
                object result;
                _obj.TryGetValue(key, out result);
                return Wrap(result);
            }
            set
            {
                _obj[key] = Unwrap(value);
            }
        }
        /// <summary>
        /// 获取某个值
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "The compiler generates calls to invoke this")]
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = this[binder.Name];
            return true;
        }
        /// <summary>
        /// 设置某个值
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "The compiler generates calls to invoke this")]
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            this[binder.Name] = value;
            return true;
        }
        /// <summary>
        /// 装箱
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object Wrap(object value)
        {
            var obj = value as IDictionary<string, object>;
            if (obj != null)
            {
                return new DynamicDictionary(obj);
            }

            return value;
        }
        /// <summary>
        /// 拆箱
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object Unwrap(object value)
        {
            var dictWrapper = value as DynamicDictionary;
            if (dictWrapper != null)
            {
                return dictWrapper._obj;
            }

            return value;
        }
        /// <summary>
        /// 添加元素
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, object value)
        {
            _obj.Add(key, value);
        }
        /// <summary>
        /// 判断某个元素是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(string key)
        {
            return _obj.ContainsKey(key);
        }
        /// <summary>
        /// 所有的key
        /// </summary>
        public ICollection<string> Keys
        {
            get { return _obj.Keys; }
        }
        /// <summary>
        /// 移除某个元素
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            return _obj.Remove(key);
        }
        /// <summary>
        /// 获取某个元素
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(string key, out object value)
        {
            return _obj.TryGetValue(key, out value);
        }
        /// <summary>
        /// 所有的value
        /// </summary>
        public ICollection<object> Values
        {
            get { return _obj.Values; }
        }
        /// <summary>
        /// 添加一个元素
        /// </summary>
        /// <param name="item"></param>
        public void Add(KeyValuePair<string, object> item)
        {
            _obj.Add(item);
        }
        /// <summary>
        /// 清空所有元素
        /// </summary>
        public void Clear()
        {
            _obj.Clear();
        }
        /// <summary>
        /// 是否包含某个元素
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(KeyValuePair<string, object> item)
        {
            return _obj.Contains(item);
        }
        /// <summary>
        /// 把所有元素复制到数组中
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            _obj.CopyTo(array, arrayIndex);
        }
        /// <summary>
        /// 包含的元素数量
        /// </summary>
        public int Count
        {
            get { return _obj.Count; }
        }
        /// <summary>
        /// 是否为只读
        /// </summary>
        public bool IsReadOnly
        {
            get { return _obj.IsReadOnly; }
        }
        /// <summary>
        /// 删除某个元素
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<string, object> item)
        {
            return _obj.Remove(item);
        }
        /// <summary>
        /// 获取枚举对象
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _obj.GetEnumerator();
        }
        /// <summary>
        /// 获取枚举对象
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}