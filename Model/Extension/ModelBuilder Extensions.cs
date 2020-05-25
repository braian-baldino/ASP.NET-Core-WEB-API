using Microsoft.EntityFrameworkCore;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Extension
{
    public static class ModelBuilderExtensions
    {
        public static void SeedMonths(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Month>()
            .HasData(
                new Month { Id = 1, Name = "Enero" },
                new Month { Id = 2, Name = "Febrero" },
                new Month { Id = 3, Name = "Marzo" },
                new Month { Id = 4, Name = "Abril" },
                new Month { Id = 5, Name = "Mayo" },
                new Month { Id = 6, Name = "Junio" },
                new Month { Id = 7, Name = "Julio" },
                new Month { Id = 8, Name = "Agosto" },
                new Month { Id = 9, Name = "Septiembre" },
                new Month { Id = 10, Name = "Octubre" },
                new Month { Id = 11, Name = "Noviembre" },
                new Month { Id = 12, Name = "Diciembre" }
            );
        }

        public static void SeedCategories(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IncomeType>()
            .HasData(
                new IncomeType { Id = 1, Name = "Salario" },
                new IncomeType { Id = 2, Name = "Aguinaldo" },
                new IncomeType { Id = 3, Name = "Bonus" },
                new IncomeType { Id = 4, Name = "Intereses Ganados" },
                new IncomeType { Id = 5, Name = "Comision" },
                new IncomeType { Id = 6, Name = "Prestamo" },
                new IncomeType { Id = 7, Name = "Venta" },
                new IncomeType { Id = 8, Name = "Honorarios" },
                new IncomeType { Id = 9, Name = "Otro" }
            );

            modelBuilder.Entity<SpendingType>()
            .HasData(
                new SpendingType { Id = 1, Name = "Servicio" },
                new SpendingType { Id = 2, Name = "Impuesto" },
                new SpendingType { Id = 3, Name = "Alquiler" },
                new SpendingType { Id = 4, Name = "Educacion" },
                new SpendingType { Id = 5, Name = "Vehiculo" },
                new SpendingType { Id = 6, Name = "Combustible" },
                new SpendingType { Id = 7, Name = "Documentacion" },
                new SpendingType { Id = 8, Name = "Intereses" },
                new SpendingType { Id = 9, Name = "Tarjeta de Credito" },
                new SpendingType { Id = 10, Name = "Debito" },
                new SpendingType { Id = 11, Name = "Perdida" },
                new SpendingType { Id = 12, Name = "Productos Alimenticios" },
                new SpendingType { Id = 13, Name = "Productos Varios" },
                new SpendingType { Id = 14, Name = "Ocio" },
                new SpendingType { Id = 15, Name = "Otro" }
            );
        }
    }
    
}
