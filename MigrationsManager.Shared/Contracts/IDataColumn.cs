﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Contracts
{
    public interface IDataColumn
    {
        string Name { get; }
        string Type { get;}
        object DefaultValue { get; }
    }
}
