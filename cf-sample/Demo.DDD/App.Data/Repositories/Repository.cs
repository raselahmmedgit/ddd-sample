﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace App.Data
{
    public class Repository<T> : RepositoryBase<T> where T : class
    {
        public Repository(IDatabaseFactory iDatabaseFactory)
            : base(iDatabaseFactory)
        {
            //constructor
        }
    }
}
