using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Collections.FastSearch
{
    /// <summary>
    /// 查找代理
    /// </summary>
    /// <param name="keyChar"></param>
    /// <returns></returns>
    public unsafe delegate int FindDelegate(char* keyChar);
    /// <summary>
    /// 字符串对比代理
    /// </summary>
    /// <param name="keyChar"></param>
    /// <param name="CompareKeyChar"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public unsafe delegate bool CompareDelegate(char* keyChar, char* CompareKeyChar,int length);
    /// <summary>
    /// 字符串分组查找代理
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public delegate int FindGroupIndexDelegate(int length);
    
    /// <summary>
    /// 分组数据
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class GroupData<TValue>
    {
        //字符串长度
        public int length;
        //优化的二分查找函数,长度固定
        public FindDelegate findDelegate;
        //优化过的字符串比较函数
        public CompareDelegate compareDelegate;
        public List<KV<TValue>> list;
    }
    /// <summary>
    /// 快速查找类，速度是传统Dictionary的两倍以上。
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class FastSearch<TValue>
    {
        GroupData<TValue>[] groupArray;
        public FindGroupIndexDelegate findGroupIndexDelegate;
        public FastSearch()
        { }
        /// <summary>
        /// 快速查找类
        /// </summary>
        /// <param name="originalDictionary"></param>
        public unsafe FastSearch(IEnumerable<KeyValuePair<string, TValue>> originalDictionary)
        {
            GroupData<TValue> groupData;
            int index = 0;
            SortedList<int, GroupData<TValue>> groupList = new SortedList<int, GroupData<TValue>>();
            foreach (var kv in originalDictionary)
            {
                if (groupList.TryGetValue(kv.Key.Length, out groupData))
                {
                    index = groupList.Count;
                    groupData.list.Add(new KV<TValue>(kv.Key, kv.Value, index));
                }
                else
                {
                    groupData = new GroupData<TValue>();
                    //快速比较函数，应用long指针，一次比较4个字符
                    groupData.compareDelegate = CompareDelegateHelper.GetCompareDelegate(kv.Key.Length);
                    groupData.length = kv.Key.Length;
                    groupData.list = new List<KV<TValue>>();
                    groupData.list.Add(new KV<TValue>(kv.Key, kv.Value, index));
                    groupList.Add(kv.Key.Length, groupData);
                }
            }
            groupArray = groupList.Values.ToArray();
            groupList.Clear();
            //二分查长相同长度的字符串组下标
            findGroupIndexDelegate = FindGroupIndexDelegateHelper.GetFindGroupIndexDelegate(groupArray);
            for (int i = 0; i < groupArray.Length; i++)
            {
                //生成在长度相同的字符串中快速查找的函数
                groupArray[i].findDelegate = FastFindSameLengthStringHelper.GetFastFindSameLengthStringDelegate(groupArray[i].list, groupArray[i].length);
            }
            //originalDictionary.Clear();
        }
        public unsafe bool TryGetValue(string key, int length,out TValue value)
        {
            GroupData<TValue> group = groupArray[findGroupIndexDelegate(length)];
            int index;
            fixed (char* keyChar = key)
            {
                index = group.findDelegate(keyChar);
                value = group.list[index].value;
                string comparekey = group.list[index].key;
                fixed (char* complareKeyChar = comparekey)
                {
                    return group.compareDelegate(keyChar, complareKeyChar, length);
                }
            }
        }
        /// <summary>
        /// 获取相关值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValue this[string key]
        {
            get {
                TValue value;
                if (this.TryGetValue(key, key.Length, out value))
                {
                    return value;
                }
                else
                {
                    throw new KeyNotFoundException("找不到key:"+key);
                }
            }
        }

    }
}
