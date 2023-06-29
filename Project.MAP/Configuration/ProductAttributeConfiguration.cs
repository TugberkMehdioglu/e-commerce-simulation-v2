using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Configuration
{
    public class ProductAttributeConfiguration : BaseConfiguration<ProductAttribute>
    {
        public override void Configure(EntityTypeBuilder<ProductAttribute> builder)
        {
            base.Configure(builder);

            HashSet<ProductAttribute> productAttributes = new()
            {
                new(){Id=1, Name="Çekirdek Sayısı", Value="16 çekirdek", ProductId=1},
                new(){Id=2, Name="Frekans Hızı", Value="4.20 GHz", ProductId=1},
                new(){Id=3, Name="Önbellek", Value="144 MB", ProductId=1},
                new(){Id=4, Name="İşlemci Markası", Value="AMD", ProductId=1},
                new(){Id=5, Name="Soket Tipi", Value="Soket AM5", ProductId=1},
                new(){Id=6, Name="İşlemci Hızı", Value="4,2 GHz", ProductId=1},
                new(){Id=7, Name="Garanti", Value="24", ProductId=1},
                new(){Id=8, Name="Menşei", Value="Çin", ProductId=1},

                new(){Id=9, Name="Çekirdek Sayısı", Value="16 çekirdek", ProductId=2},
                new(){Id=10, Name="Frekans Hızı", Value="5,2 GHz", ProductId=2},
                new(){Id=11, Name="Önbellek", Value="30 MB", ProductId=2},
                new(){Id=12, Name="İşlemci Markası", Value="INTEL®", ProductId=2},
                new(){Id=13, Name="Soket Tipi", Value="LGA 1700", ProductId=2},
                new(){Id=14, Name="İşlemci Hızı", Value="5,2 GHz", ProductId=2},
                new(){Id=15, Name="Garanti", Value="24", ProductId=2},
                new(){Id=16, Name="Menşei", Value="Çin", ProductId=2},

                new(){Id=17, Name="Ekran Kartı Chipset Marka", Value="NVIDIA®", ProductId=3},
                new(){Id=18, Name="Grafik İşlemci", Value="GeForce RTX 3090 Ti", ProductId=3},
                new(){Id=19, Name="Çekirdek Hızı Maks.", Value="1860 MHz", ProductId=3},
                new(){Id=20, Name="Çekirdek Hücre Sayısı", Value="10752 Cuda", ProductId=3},
                new(){Id=21, Name="Bellek Tipi", Value="GDDR6X", ProductId=3},
                new(){Id=22, Name="Bellek Kapasitesi", Value="24GB", ProductId=3},
                new(){Id=23, Name="Bellek Hızı", Value="21000 MHz", ProductId=3},
                new(){Id=24, Name="Bellek Arayüzü", Value="384 Bit", ProductId=3},

                new(){Id=25, Name="Ekran Kartı Chipset Marka", Value="NVIDIA®", ProductId=4},
                new(){Id=26, Name="Grafik İşlemci", Value="GeForce RTX 4090", ProductId=4},
                new(){Id=27, Name="Çekirdek Hızı Maks.", Value="2640 MHz", ProductId=4},
                new(){Id=28, Name="Çekirdek Hücre Sayısı", Value="16384 Cuda", ProductId=4},
                new(){Id=29, Name="Bellek Tipi", Value="GDDR6X", ProductId=4},
                new(){Id=30, Name="Bellek Kapasitesi", Value="24GB", ProductId=4},
                new(){Id=31, Name="Bellek Hızı", Value="21000 MHz", ProductId=4},
                new(){Id=32, Name="Bellek Arayüzü", Value="384 Bit", ProductId=4},

                new(){Id=33, Name="Soket Tipi", Value="Soket AM5", ProductId=5},
                new(){Id=34, Name="Ram Tipi", Value="DDR5", ProductId=5},
                new(){Id=35, Name="Anakart Markası", Value="MSI", ProductId=5},
                new(){Id=36, Name="Anakart Chipseti", Value="AMD® X670", ProductId=5},
                new(){Id=37, Name="Anakart Yapı", Value="ATX", ProductId=5},
                new(){Id=38, Name="Ram Slot Sayısı", Value="4", ProductId=5},
                new(){Id=39, Name="Maks. Ram Desteği", Value="128GB", ProductId=5},
                new(){Id=40, Name="Desteklenen Ram Hızı", Value="4800MHz", ProductId=5},

                new(){Id=41, Name="Soket Tipi", Value="Soket sWRX8", ProductId=6},
                new(){Id=42, Name="Ram Tipi", Value="DDR4", ProductId=6},
                new(){Id=43, Name="Anakart Markası", Value="ASUS", ProductId=6},
                new(){Id=44, Name="Maks. Ram Desteği", Value="2048GB", ProductId=6},
                new(){Id=45, Name="Anakart Yapı", Value="Extended ATX", ProductId=6},
                new(){Id=46, Name="Ram Slot Sayısı", Value="8", ProductId=6},
                new(){Id=47, Name="Maks. Ram Hızı (O.C.)", Value="3200mhz", ProductId=6},
                new(){Id=48, Name="Desteklenen Ram Hızı", Value="3200mhz", ProductId=6},

                new(){Id=49, Name="Yenileme Hızı", Value="240Hz", ProductId=7},
                new(){Id=50, Name="Ekran Boyutu", Value="45 inch", ProductId=7},
                new(){Id=51, Name="Çözünürlük (Piksel)", Value="3440 x 1440", ProductId=7},
                new(){Id=52, Name="Görüntü Formatı", Value="21:9", ProductId=7},
                new(){Id=53, Name="Ekran Renk", Value="1.07 Milyar", ProductId=7},
                new(){Id=54, Name="Piksel Aralığı", Value="84 PPI", ProductId=7},
                new(){Id=55, Name="İzleme Açısı", Value="178º(R/L), 178º(U/D)", ProductId=7},
                new(){Id=56, Name="Sync Teknolojisi", Value="FreeSync / G-Sync", ProductId=7},

                new(){Id=57, Name="Yerel açı oranı", Value="32:9", ProductId=8},
                new(){Id=58, Name="Yenileme yanıt süresi", Value="5 ms", ProductId=8},
                new(){Id=59, Name="Panel tipi", Value="AH-IPS", ProductId=8},
                new(){Id=60, Name="En boy oranı", Value="32:9", ProductId=8},
                new(){Id=61, Name="Ekran parlaklığı (tipik)", Value="350 cd/m²", ProductId=8},
                new(){Id=62, Name="Görüntüleme teknolojisi", Value="LED", ProductId=8},
                new(){Id=63, Name="HD türü", Value="UltraWide Quad HD", ProductId=8},
                new(){Id=64, Name="Kontrast oranı", Value="1000:1", ProductId=8},

                new(){Id=65, Name="Standart bağlantı", Value="PCI Express 4.0", ProductId=9},
                new(){Id=66, Name="Donanım şifreleme", Value="Var", ProductId=9},
                new(){Id=67, Name="TBW sınıfı", Value="1400", ProductId=9},
                new(){Id=68, Name="NVMe", Value="Var", ProductId=9},
                new(){Id=69, Name="Sıralı okuma hızı (CDM)", Value="7100 MB/s", ProductId=9},
                new(){Id=70, Name="Hafıza tipi", Value="3D TLC NAND", ProductId=9},
                new(){Id=71, Name="SSD form faktörü", Value="M.2", ProductId=9},
                new(){Id=72, Name="Sıralı yazma hızı (CDM)", Value="6800 MB/s", ProductId=9},

                new(){Id=73, Name="Standart bağlantı", Value="PCI Express 4.0", ProductId=10},
                new(){Id=74, Name="Donanım şifreleme", Value="Var", ProductId=10},
                new(){Id=75, Name="TBW sınıfı", Value="1400", ProductId=10},
                new(){Id=76, Name="NVMe", Value="Var", ProductId=10},
                new(){Id=77, Name="Sıralı okuma hızı (CDM)", Value="7100 MB/s", ProductId=10},
                new(){Id=78, Name="Hafıza tipi", Value="3D TLC NAND", ProductId=10},
                new(){Id=79, Name="SSD form faktörü", Value="M.2", ProductId=10},
                new(){Id=80, Name="Sıralı yazma hızı (CDM)", Value="6800 MB/s", ProductId=10},
            };

            builder.HasData(productAttributes);
        }
    }
}
