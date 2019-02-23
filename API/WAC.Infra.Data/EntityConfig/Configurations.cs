using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders; 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WAC.Domain.Entities;

namespace WAC.infra.Data.EntityConfig
{ 
    public static class Configuration
    {
        public static void ApplyConfigurations(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdValue);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.IdValue);
            });
        }
    }

}
