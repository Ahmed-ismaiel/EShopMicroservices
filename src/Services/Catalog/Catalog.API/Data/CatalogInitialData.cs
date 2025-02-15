using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        // Store data in the database

        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {

            // Hena h create session 3shan 22dar ata3aml m3 l data 
            using var session = store.LightweightSession();

            // Check if there are any products in the database

            if (await session.Query<Product>().AnyAsync())
                return;

            // Add products UPSERT to the database by passing object array
            session.Store<Product>(GetPreConfiguredProducts());
            await session.SaveChangesAsync(cancellation);


        }

        private static IEnumerable<Product> GetPreConfiguredProducts() => new List<Product>
        {

            new Product()
            {   Id = new Guid("20f7a37e-61ce-4958-b28d-19d4bc50b1df"),
                Name = "IPhone 12",
                Category = new List<string>() {"Smart Phones" },
                Description = "IPhone 12",
                ImageFile = "product-1.png",
                Price = 950
            },
              new Product()
        {
            Id = new Guid("8f2e9176-35ee-4f0a-ae8c-4474d301d9ce"),
            Name = "iPhone 12",
            Category = new List<string>() { "Smart Phones" },
            Description = "Latest iPhone 12 with A14 Bionic chip and dual camera system",
            ImageFile = "product-1.png",
            Price = 950.00M
        },
        new Product()
        {
            Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
            Name = "Samsung Galaxy S21",
            Category = new List<string>() { "Smart Phones" },
            Description = "Samsung Galaxy S21 with Snapdragon 888 and 8K video recording",
            ImageFile = "product-2.png",
            Price = 899.99M
        },
        new Product()
        {
            Id = new Guid("3e2d2750-5f78-40e5-9ddb-90a27c8991c1"),
            Name = "Google Pixel 6",
            Category = new List<string>() { "Smart Phones" },
            Description = "Google Pixel 6 featuring Google Tensor chip and advanced AI capabilities",
            ImageFile = "product-3.png",
            Price = 799.00M
        },
        new Product()
        {
            Id = new Guid("b47d4c3c-3e29-49b9-b6be-28e5ee77ea31"),
            Name = "OnePlus 9 Pro",
            Category = new List<string>() { "Smart dump Phones" },
            Description = "OnePlus 9 Pro with Hasselblad camera system and 120Hz display",
            ImageFile = "product-4.png",
            Price = 869.00M
        },
        new Product()
        {
            Id = new Guid("e8ae5421-7c5f-45d5-92eb-f483375a9b57"),
            Name = "Xiaomi Mi 11",
            Category = new List<string>() { "white appliance" },
            Description = "Xiaomi Mi 11 with Snapdragon 888 and 108MP camera",
            ImageFile = "product-5.png",
            Price = 749.99M
        }








            };
       
    }
}
