using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Domain;
using System.Data.Objects;

namespace App.Data
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private readonly dbEntities _dbEntities = new dbEntities();
        private ObjectContext _objectContext;

        public ObjectContext Get()
        {
            if (_objectContext != null)
            {
                return _objectContext;
            }
            else
            {
                _objectContext = new ObjectContext(_dbEntities.Connection.ConnectionString);
                return _objectContext;
            }
        }

        protected override void DisposeCore()
        {
            if (_objectContext != null)
                _objectContext.Dispose();
        }
    }
}
