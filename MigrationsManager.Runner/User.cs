using MigrationsManager.Shared.Attributes;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationsManager.Runner
{
    [Migration(true)]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Column("UserId")]
        public string UserName { get; set; }
        [DefaultValue("GETUTCDATE()")]
        public DateTime Created { get; set; }
    }
}
