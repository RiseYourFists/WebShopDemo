using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace WebShop.Core.Data
{
    using Models.BookShop;

    public static class DatabaseSeeder
    {
        private const string DataSetPath = "../WebShop.Core/Datasets/";
        private const string FileExtension = ".json";

        public static ModelBuilder SeedDatabase(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity => entity.HasData(GetData<Author>()));
            modelBuilder.Entity<Genre>(entity => entity.HasData(GetData<Genre>()));
            modelBuilder.Entity<Book>(entity => entity.HasData(GetData<Book>()));
            modelBuilder.Entity<Promotion>(entity => entity.HasData(GetData<Promotion>()));
            modelBuilder.Entity<GenrePromotion>(entity => entity.HasData(GetData<GenrePromotion>()));
            return modelBuilder;
        }

        /// <summary>
        /// This method goes to Datasets folder and searches for a .json file with the same name as its class model and maps the json data on it
        /// </summary>
        /// <typeparam name="T">class</typeparam>
        /// <returns>List<T></returns>
        private static List<T> GetData<T>()
        where T : class
        {
            var type = typeof(T);
            var filePath = Path.Combine(DataSetPath, type.Name.ToLower() + FileExtension);
            if (!Directory.Exists(filePath))
            {
                return new();
            }

            var jsonString =  File.ReadAllText(filePath);
            var objectData = JsonConvert.DeserializeObject<T[]>(jsonString);
            return objectData.ToList();
        }
    }
}
