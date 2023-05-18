using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevEventos.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevEventos.API.Persistencia
{
    public class EventoDbContext : DbContext
    {
        public EventoDbContext(DbContextOptions<EventoDbContext> options) 
            : base(options)
        {
        }
        public DbSet<Evento> Eventos { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Evento>()
                .HasKey(e => e.Id);
        }
    }
}