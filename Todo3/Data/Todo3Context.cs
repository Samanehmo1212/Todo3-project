using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Todo3;

namespace Todo3.Data
{
    public class Todo3Context : DbContext
    {
        public Todo3Context (DbContextOptions<Todo3Context> options)
            : base(options)
        {
        }

        public DbSet<Todo3.Todo> Todo { get; set; } = default!;
        public DbSet<Todo3.User> User { get; set; } = default!;
    }
}
