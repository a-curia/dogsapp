using System;
using System.Collections.Generic;
using System.Text;
using DogsApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DogsApp.Data
{
    public class DogsDbContext : IdentityDbContext<User>
    {
        public DogsDbContext(DbContextOptions<DogsDbContext> options)
            : base(options)
        {
        }
    }
}
