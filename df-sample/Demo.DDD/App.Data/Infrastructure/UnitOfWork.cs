using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Domain;
using System.Data.Objects;

namespace App.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseFactory _iDatabaseFactory;
        private ObjectContext _dataContext;

        public UnitOfWork(IDatabaseFactory iDatabaseFactory)
        {
            this._iDatabaseFactory = iDatabaseFactory;
        }

        protected ObjectContext DataContext
        {
            get { return _dataContext ?? (_dataContext = _iDatabaseFactory.Get()); }
        }

        public int Commit()
        {
            return DataContext.SaveChanges();
        }
    }
}
