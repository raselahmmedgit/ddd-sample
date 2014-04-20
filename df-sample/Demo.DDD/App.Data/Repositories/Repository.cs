using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using App.Domain;

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
