﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plans.Database
{
    public interface ICrud<T>
    {
        IEnumerable<T> GetAll();
        bool Delete(int id);
        T Save(T obj);
        T Get(int id);
        IEnumerable<T> GetById(int id);
    }
}
