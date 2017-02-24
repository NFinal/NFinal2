using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;

namespace NFinal.Collections
{
    public class NameValueDynamicCollection: DynamicObject, IDictionary<string, StringContainer>
    {
        private readonly IDictionary<string, StringContainer> _obj;
        public NameValueDynamicCollection()
        {
            _obj = new Dictionary<string, StringContainer>();
        }
        public NameValueDynamicCollection(IDictionary<string, StringContainer> obj)
        {
            _obj = obj;
        }

        public StringContainer this[string key]
        {
            get
            {
                StringContainer result;
                if (_obj.TryGetValue(key, out result))
                {
                    return result;
                }
                else
                {
                    return StringContainer.Empty;
                }
            }
            set
            {
                if (_obj.ContainsKey(key))
                {
                    _obj[key] = value;
                }
                else
                {
                    _obj.Add(key, value);
                }
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "The compiler generates calls to invoke this")]
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = this[binder.Name];
            return true;
        }

        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "The compiler generates calls to invoke this")]
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (value == null)
            {
                this[binder.Name] = StringContainer.Empty;
            }
            else
            {
                this[binder.Name] =new StringContainer(value.ToString());
            }
            return true;
        }

        public void Add(string key, StringContainer value)
        {
            if (!_obj.ContainsKey(key))
            {
                _obj.Add(key, value);
            }
        }

        public bool ContainsKey(string key)
        {
            return _obj.ContainsKey(key);
        }

        public ICollection<string> Keys
        {
            get { return _obj.Keys; }
        }

        public bool Remove(string key)
        {
            return _obj.Remove(key);
        }

        public bool TryGetValue(string key, out StringContainer value)
        {
            return _obj.TryGetValue(key, out value);
        }

        public ICollection<StringContainer> Values
        {
            get { return _obj.Values; }
        }

        public void Add(KeyValuePair<string, StringContainer> item)
        {
            _obj.Add(item);
        }

        public void Clear()
        {
            _obj.Clear();
        }

        public bool Contains(KeyValuePair<string, StringContainer> item)
        {
            return _obj.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, StringContainer>[] array, int arrayIndex)
        {
            _obj.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _obj.Count; }
        }

        public bool IsReadOnly
        {
            get { return _obj.IsReadOnly; }
        }

        public bool Remove(KeyValuePair<string, StringContainer> item)
        {
            return _obj.Remove(item);
        }

        public IEnumerator<KeyValuePair<string, StringContainer>> GetEnumerator()
        {
            return _obj.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
