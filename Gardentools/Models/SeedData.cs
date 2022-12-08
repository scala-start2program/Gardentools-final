using Gardentools.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using static System.Formats.Asn1.AsnWriter;

namespace Gardentools.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new GardentoolsContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<GardentoolsContext>>()))
            {
                if (!context.Category.Any() 
                    && !context.Brand.Any() 
                    && !context.Article.Any())
                {
                    Brand gardena = new Brand { BrandName = "Gardena" };
                    Brand wolf = new Brand { BrandName = "Wolf" };
                    Brand makita = new Brand { BrandName = "Makita" };
                    Brand blackAndDecker = new Brand { BrandName = "Black & Decker" };
                    Brand husqvarna = new Brand { BrandName = "Husqvarna" };
                    Brand stihl = new Brand { BrandName = "Stihl" };
                    context.Brand.AddRange(
                        gardena, wolf, makita, blackAndDecker, husqvarna, stihl
                    );
                    context.SaveChanges();

                    Category grasmaaiers = new Category { CategoryName = "Grasmaaiers" };
                    Category heggenscharen = new Category { CategoryName = "Heggenscharen" };
                    Category boomzagen = new Category { CategoryName = "Boomzagen" };
                    Category snoeischaren = new Category { CategoryName = "Snoeischaren" };
                    context.Category.AddRange(
                        grasmaaiers, heggenscharen, boomzagen, snoeischaren
                    );
                    context.SaveChanges();
                    context.Article.AddRange(
                        new Article { Brand = stihl, Category = boomzagen, ArticleName = "MS 180", EnergySupply = "Benzine", Price = 243.47M, Warranty=2,Description= "Vermogen: 1,4 kW / 1,9 pk\r\nLengte zaagblad: 30 / 35 cm\r\nGewicht: 4,1 kg", ImagePath = "stihl-stihl-ms-180-benzine-kettingzaag.png" },
                        new Article { Brand = stihl, Category = boomzagen, ArticleName = "MS 170", EnergySupply = "Benzine", Price = 182.36M, Warranty = 2, Description = "Vermogen 1,2 kW / 1,6 pk\r\nLengte zaagblad: 30 / 35 cm\r\nGewicht: 4,1 kg", ImagePath = "stihl-stihl-ms-170-benzine-kettingzaag.png" },
                        new Article { Brand = stihl, Category = boomzagen, ArticleName = "MS 881", EnergySupply = "Benzine", Price = 1939.31M, Warranty = 2, Description = "Vermogen: 6,4 kW / 8,7 pk\r\nLengte zaagblad: 63 - 90 cm\r\nGewicht: 9,9 kg", ImagePath = "stihl-stihl-ms-881-benzine-kettingzaag.jpg" },
                        new Article { Brand = stihl, Category = boomzagen, ArticleName = "MS 261 C-M", EnergySupply = "Benzine", Price = 868.15M, Warranty = 2, Description = "Vermogen: 3,0 kW / 4,1 pk\r\nLengte zaagblad: 37 - 45 cm\r\nGewicht: 4,9 kg", ImagePath = "stihl-stihl-ms-261-c-m-benzine-kettingzaag.png" },
                        new Article { Brand = stihl, Category = boomzagen, ArticleName = "MSE 210 C-B", EnergySupply = "Elektriciteit", Price = 345.32M, Warranty = 2, Description = "Vermogen: 2,1 kW\r\nLengte zaagblad: 30 - 40 cm\r\nGewicht: 4,5 kg", ImagePath = "stihl-stihl-mse-210-c-b-elektrische-kettingzaag.png" },
                        new Article { Brand = stihl, Category = boomzagen, ArticleName = "MSE 250", EnergySupply = "Elektriciteit", Price = 687.732M, Warranty = 2, Description = "Vermogen: 2,5 kW\r\nLengte zaagblad: 40 / 45 cm\r\nGewicht: 5,7 kg", ImagePath = "stihl-stihl-mse-250-elektrische-kettingzaag.png" },
                        new Article { Brand = stihl, Category = boomzagen, ArticleName = "MSE 141", EnergySupply = "Elektriciteit", Price = 177.51M, Warranty = 2, Description = "Vermogen: 1,4 kW\r\nLengte zaagblad: 30 / 35 cm\r\nGewicht: 4,1 kg", ImagePath = "stihl-stihl-mse-141-elektrische-kettingzaag.png" },
                        new Article { Brand = stihl, Category = boomzagen, ArticleName = "GTA 26", EnergySupply = "Batterij", Price = 159.08M, Warranty = 2, Description = "Vermogen: Lithium 10,8 V\r\nLengte zaagblad: 10 cm\r\nGewicht: 1,2 kg", ImagePath = "stihl-stihl-msa-140-c-b-accu-kettingzaag.png" },
                        new Article { Brand = stihl, Category = boomzagen, ArticleName = "MSA 220C", EnergySupply = "Batterij", Price = 440.38M, Warranty = 2, Description = "Vermogen: Lithium 36 V\r\nLengte zaagblad: 35 / 40 cm\r\nGewicht: 3,6 kg", ImagePath = "stihl-stihl-msa-160-c-b-accu-kettingzaag.jpg" },
                        new Article { Brand = wolf, Category = grasmaaiers, ArticleName = "Lycos 40/340 M", EnergySupply = "Batterij", Price = 449.99M, Warranty = 2, Description = "Centrale maaihoogteinstelling in 6 stappen: 2,5-7,5 cm\r\nMaaibreedte: 34 cm\r\n3-in-1 functie: maaien, opvangen en mulchen zonder hulpstukken", ImagePath = "Lycos_40_340M.jpg" },
                        new Article { Brand = wolf, Category = grasmaaiers, ArticleName = "LYCOS 40/400 M", EnergySupply = "Batterij", Price = 554.99M, Warranty = 2, Description = "Centrale maaihoogte-instelling in 6 stappen: 2,5-7,5 cm\r\nMaaibreedte: 40 cm\r\n3-in-1 functie: maaien, opvangen en mulchen zonder hulpstukken", ImagePath = "Lycos_40_400M.jpg" },
                        new Article { Brand = wolf, Category = grasmaaiers, ArticleName = "A 4600", EnergySupply = "Benzine", Price = 419.99M, Warranty = 2, Description = "Motor: WOLF-Garten OHV 45, 140 cm³, 2,1 kW, 2.800/min\r\nTankinhoud: 1,0 l\r\nChassis: staal", ImagePath = "A4600.jpg" },
                        new Article { Brand = wolf, Category = grasmaaiers, ArticleName = "A 5300 A", EnergySupply = "Benzine", Price = 619.49M, Warranty = 2, Description = "Motor: WOLF-Garten OHV 55, 159 cm³, 2,5 kW, 2.600 tpm\r\nTankinhoud: 1,0 l\r\nChassis: staal", ImagePath = "A5300A.jpg" }
                        );
                    context.SaveChanges();
                }
            }
        }
    }
}
