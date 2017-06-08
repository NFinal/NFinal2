using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace NFinal.Ioc
{
    /// <summary>
    /// IOC容器
    /// </summary>
    public class Container
    {
        private IDictionary<Tuple<Type, string>, IList<Func<object>>> Factories { get; set; }

        public Container()
        {
            Factories = new Dictionary<Tuple<Type, string>, IList<Func<object>>>();
        }

        public void Register<TImplementation, TService>(string serviceKey = null)
            where TImplementation : TService
        {
            var key = Tuple.Create(typeof(TService), serviceKey);
            IList<Func<object>> factories;
            if (!Factories.TryGetValue(key, out factories))
            {
                factories = new List<Func<object>>();
                Factories[key] = factories;
            }
            var factory = Expression.Lambda<Func<object>>(Expression.New(typeof(TImplementation))).Compile();
            factories.Add(factory);
        }

        public TService Resolve<TService>(string serviceKey = null)
        {
            var key = Tuple.Create(typeof(TService), serviceKey);
            var factory = Factories[key].Single();
            return (TService)factory();
        }

        public IEnumerable<TService> ResolveMany<TService>(string serviceKey = null)
        {
            var key = Tuple.Create(typeof(TService), serviceKey);
            IList<Func<object>> factories;
            if (!Factories.TryGetValue(key, out factories))
            {
                yield break;
            }
            foreach (var factory in factories)
            {
                yield return (TService)factory();
            }
        }
    }
}

