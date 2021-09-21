using MigrationsManager.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Runner
{
    [Migration(true)]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column("UserId")]
        public string UserName { get; set; }
        [DefaultValue("GETUTCDATE()")]
        public DateTime Created { get; set; }
    }
}
