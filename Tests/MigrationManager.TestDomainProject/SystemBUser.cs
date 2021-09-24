using MigrationsManager.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.TestDomainProject
{
    [Migration(orderId: 1)]
    public class SystemBUser
    {
        public int Id { get; set; }
    }
}
