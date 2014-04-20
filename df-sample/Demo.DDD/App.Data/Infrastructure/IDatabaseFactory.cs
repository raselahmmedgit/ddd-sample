using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Domain;
using System.Data.Objects;

namespace App.Data
{
    public interface IDatabaseFactory : IDisposable
    {
        ObjectContext Get();
    }
}
