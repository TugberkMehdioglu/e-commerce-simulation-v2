using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Configuration
{
    public class CategoryConfiguration : BaseConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);

            HashSet<Category> categories = new()
            {
                new(){ Id=1, Name="İşlemci", Description="İşlemci fiyatları, modelleri ve güvenilir işlemci markaları uygun ödeme seçenekleriyle şimdi inceleyin ve hemen satın alın."},
                new(){Id=2, Name="Ekran Kartı", Description="Ekran kartı fiyatları, modelleri ve güvenilir ekran kartı markaları uygun ödeme seçenekleriyle şimdi inceleyin ve hemen satın alın."},
                new(){Id=3, Name="Anakart", Description="Anakart fiyatları, modelleri ve güvenilir ekran kartı markaları uygun ödeme seçenekleriyle şimdi inceleyin ve hemen satın alın."},
                new(){Id=4, Name="Monitör", Description="Monitör fiyatları, modelleri ve güvenilir ekran kartı markaları uygun ödeme seçenekleriyle şimdi inceleyin ve hemen satın alın."},
                new(){Id=5, Name="SSD", Description="SSD fiyatları, modelleri ve güvenilir ekran kartı markaları uygun ödeme seçenekleriyle şimdi inceleyin ve hemen satın alın."},
                new(){Id=6, Name="Harici Disk", Description="Harici Disk fiyatları, modelleri ve güvenilir ekran kartı markaları uygun ödeme seçenekleriyle şimdi inceleyin ve hemen satın alın."},
                new(){Id=7, Name="Bilgisayar Kasası", Description="Bilgisayar Kasası fiyatları, modelleri ve güvenilir ekran kartı markaları uygun ödeme seçenekleriyle şimdi inceleyin ve hemen satın alın."}
            };

            builder.HasData(categories);
        }
    }
}
