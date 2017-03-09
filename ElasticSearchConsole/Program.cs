using System;
using Nest;

namespace ElasticSearchConsole
{
    class Program
    {
        static void Main()
        {
            // создаём клиент
            var client = GetClient();

            // добавить запись
            var myJson = new DocumentAttributes
            {
                Name = "pig",
                OriginalVoiceActor = "master",
                AnimatedDebut = "Smith"
            };
            var indexResponse = client.Put("disney", "character", 1, myJson);
            Console.WriteLine(indexResponse);

            // найти запись по полю id
            SearchCharacterById(client, 1);
            SearchCharacterById(client, 2);

            //удалить по id
            var deleteResponse = client.Delete("disney", "character", 1);
            Console.WriteLine(deleteResponse);

            // найти запись по полю id
            SearchCharacterById(client, 1);
            SearchCharacterById(client, 2);
        }

        private static void SearchCharacterById(ElasticClient client, int id)
        {
           
            var searchResponse = client.Search("disney", "character", "_id", id);
            foreach (var hit in searchResponse.Hits)
            {
                Console.WriteLine($"id:{hit.Id}\n Source:{hit.Source}");
            }
        } 

        private static ElasticClient GetClient()
        {
            return new ElasticClient();
        }
    }

    public class DocumentAttributes
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string OriginalVoiceActor { get; set; }
        public string AnimatedDebut { get; set; }

        public override string ToString()
        {
            return $"id: {Id}, name: {Name}, original_voice_actor: {OriginalVoiceActor}, animated_debut: {AnimatedDebut}";
        }
    }
}
