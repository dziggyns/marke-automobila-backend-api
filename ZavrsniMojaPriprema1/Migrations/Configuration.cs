namespace ZavrsniMojaPriprema1.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ZavrsniMojaPriprema1.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ZavrsniMojaPriprema1.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Marke.AddOrUpdate(
            new Models.Marka() { Id = 1, Naziv = "BMW", Drzava = "DE" },
            new Models.Marka() { Id = 2, Naziv = "Audi", Drzava = "DE" },
            new Models.Marka() { Id = 3, Naziv = "Porsche", Drzava = "DE" },
            new Models.Marka() { Id = 4, Naziv = "Ferrari", Drzava = "IT" },
            new Models.Marka() { Id = 5, Naziv = "Lambo", Drzava = "IT" },
            new Models.Marka() { Id = 6, Naziv = "Alpha Romeo", Drzava = "IT" },
            new Models.Marka() { Id = 7, Naziv = "Peugeot", Drzava = "FR" },
            new Models.Marka() { Id = 8, Naziv = "Renault", Drzava = "FR" },
            new Models.Marka() { Id = 9, Naziv = "Seat", Drzava = "ES" },
            new Models.Marka() { Id = 10, Naziv = "Tesla", Drzava = "US" }
        );

            context.Automobili.AddOrUpdate(
                new Models.Automobil() { Id = 1, Model = "BMW 7", Cena = 70000m, GodinaProizvodnje = 1990, KonjskihSnaga = 350, MarkaId = 1 },
                new Models.Automobil() { Id = 2, Model = "BMW 5", Cena = 55000m, GodinaProizvodnje = 1985, KonjskihSnaga = 280, MarkaId = 1 },
                new Models.Automobil() { Id = 3, Model = "BMW 3", Cena = 35000m, GodinaProizvodnje = 1980, KonjskihSnaga = 170, MarkaId = 1 },
                new Models.Automobil() { Id = 4, Model = "A8", Cena = 80000m, GodinaProizvodnje = 1983, KonjskihSnaga = 470, MarkaId = 2 },
                new Models.Automobil() { Id = 5, Model = "A6", Cena = 60000m, GodinaProizvodnje = 1988, KonjskihSnaga = 330, MarkaId = 2 },
                new Models.Automobil() { Id = 6, Model = "Q5", Cena = 55000m, GodinaProizvodnje = 2005, KonjskihSnaga = 260, MarkaId = 2 },
                new Models.Automobil() { Id = 7, Model = "Ferrari 456", Cena = 120000m, GodinaProizvodnje = 1998, KonjskihSnaga = 495, MarkaId = 4 },
                new Models.Automobil() { Id = 8, Model = "Ferrari Testarossa", Cena = 150000m, GodinaProizvodnje = 1988, KonjskihSnaga = 425, MarkaId = 4 },
                new Models.Automobil() { Id = 9, Model = "Cayenne", Cena = 92000m, GodinaProizvodnje = 2001, KonjskihSnaga = 380, MarkaId = 3 },
                new Models.Automobil() { Id = 10, Model = "Aventador SVJ", Cena = 155000m, GodinaProizvodnje = 2010, KonjskihSnaga = 500, MarkaId = 5 },
                new Models.Automobil() { Id = 11, Model = "Alpha 156", Cena = 48000m, GodinaProizvodnje = 1997, KonjskihSnaga = 190, MarkaId = 6 },
                new Models.Automobil() { Id = 12, Model = "Alpha 155", Cena = 44000m, GodinaProizvodnje = 1991, KonjskihSnaga = 170, MarkaId = 6 },
                new Models.Automobil() { Id = 13, Model = "Alpha 154", Cena = 38000m, GodinaProizvodnje = 1987, KonjskihSnaga = 150, MarkaId = 6 },
                new Models.Automobil() { Id = 14, Model = "205", Cena = 14000m, GodinaProizvodnje = 1980, KonjskihSnaga = 80, MarkaId = 7 },
                new Models.Automobil() { Id = 15, Model = "206", Cena = 16000m, GodinaProizvodnje = 1990, KonjskihSnaga = 88, MarkaId = 7 },
                new Models.Automobil() { Id = 16, Model = "207", Cena = 18000m, GodinaProizvodnje = 2003, KonjskihSnaga = 95, MarkaId = 7 },
                new Models.Automobil() { Id = 17, Model = "208", Cena = 20000m, GodinaProizvodnje = 2006, KonjskihSnaga = 105, MarkaId = 7 },
                new Models.Automobil() { Id = 18, Model = "Model S", Cena = 85000m, GodinaProizvodnje = 2015, KonjskihSnaga = 400, MarkaId = 10 },
                new Models.Automobil() { Id = 19, Model = "Renault 4", Cena = 9000m, GodinaProizvodnje = 1975, KonjskihSnaga = 60, MarkaId = 8 },
                new Models.Automobil() { Id = 20, Model = "Ibiza", Cena = 1100m, GodinaProizvodnje = 1996, KonjskihSnaga = 75, MarkaId = 9 }
            );

            context.SaveChanges();

        }
    }
}
