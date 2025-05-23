﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        object Get(string key);
        void Add(string key,object value,int duration);
        void Remove(string key);
        bool IsAdded(string key);
        void RemoveByPattern(string pattern);
    }
}
