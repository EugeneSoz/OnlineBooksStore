using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using OnlineBooksStore.App.WebApi.Data;
using OnlineBooksStore.Domain.Contracts.Models.Categories;
using OnlineBooksStore.Persistence.EF;

namespace OnlineBooksStore.App.WebApi.Models.Database
{
    public class DataRW
    {
        //создать файл с данными
        public void CreateJsonData(StoreSavedData data, string fileName = "data")
        {
            string json = JsonConvert.SerializeObject(data);

            using (StreamWriter writer = File.CreateText($"Files\\SavedData\\{fileName}.json"))
            {
                writer.Write(json);
            }
        }

        public void SeedDataFromFile(StoreDbContext dataContext)
        {
            string categoryJson = GetDataFromJson("savedData");
            StoreSavedData data = JsonConvert.DeserializeObject<StoreSavedData>(categoryJson);
            Dictionary<long, long> parentIds = new Dictionary<long, long>();
            Dictionary<long, long> categoriesIds = new Dictionary<long, long>();
            Dictionary<long, long> publishersIds = new Dictionary<long, long>();
            List<long> oldCategoriesIds = new List<long>();
            List<long> oldPublishersIds = new List<long>();
            data.ParentCategories.ForEach((Category p) =>
            {
                long oldId = p.Id;
                p.Id = 0;
                //dataContext.Categories.Add(p);
                dataContext.SaveChanges();
                parentIds.Add(oldId, p.Id);
            });

            data.Categories.ForEach(c =>
            {
                c.ParentId = parentIds[c.ParentId.Value];
                oldCategoriesIds.Add(c.Id);
                c.Id = 0;
                //dataContext.Categories.Add(c);
            });

            dataContext.SaveChanges();
            for (int i = 0; i < data.Categories.Count; i++)
            {
                categoriesIds.Add(oldCategoriesIds[i], data.Categories[i].Id);
            }

            data.Publishers.ForEach(p =>
            {
                oldPublishersIds.Add(p.Id);
                p.Id = 0;
                //dataContext.Publishers.Add(p);
            });

            dataContext.SaveChanges();
            for (int i = 0; i < data.Publishers.Count; i++)
            {
                publishersIds.Add(oldPublishersIds[i], data.Publishers[i].Id);
            }

            data.Books.ForEach(b =>
            {
                //b.CategoryID = categoriesIds[b.CategoryID.Value];
                //b.PublisherID = publishersIds[b.PublisherID.Value];
            });

            //dataContext.Books.AddRange(data.Books);
        }

        private string GetDataFromJson(string fileName)
        {
            StringBuilder result = new StringBuilder();

            using (StreamReader reader = File.OpenText($"Files\\DataForUpload\\{fileName}.json"))
            {
                while (reader.Peek() != -1)
                {
                    result.Append(reader.ReadLine());
                    result.AppendLine();
                }
            }

            result.Replace("\t", "    ");

            return result.ToString();
        }
    }
}
