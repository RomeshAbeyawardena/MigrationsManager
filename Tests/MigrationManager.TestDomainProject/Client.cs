using MigrationsManager.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.TestDomainProject
{
    [Migration(orderId: 2)]
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Reference { get; set; }
        public DateTimeOffset Created { get; set; }
    }
}
