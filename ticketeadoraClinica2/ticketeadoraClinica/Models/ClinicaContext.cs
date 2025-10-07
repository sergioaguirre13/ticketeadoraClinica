using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ticketeadoraClinica.Models
{
    public class ClinicaContext : DbContext
    {
        public ClinicaContext(DbContextOptions<ClinicaContext> options)
            : base(options) { }

        public DbSet<Paciente> Pacientes { get; set; }
    }
}