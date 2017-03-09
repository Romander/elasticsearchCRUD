using Elasticsearch.Net;
using Nest;

namespace ElasticSearchConsole
{
    public static class ElasticClientExtensions
    {
        public static IIndexResponse Put(this ElasticClient client, string index, string type, int id, object obj)
        {
            return client.Index(obj, i => i
                .Index(index)
                .Type(type)
                .Id(id)
                .Refresh(Refresh.True));
        }

        public static ISearchResponse<DocumentAttributes> Search(this ElasticClient client, string index, string type, string field, object value)
        {
            return client.Search<DocumentAttributes>(s => s
              .Index(index)
              .Type(type)
              .Query(q => q.Term(t => t.Field(field).Value(value))));            
        }

        public static IDeleteResponse Delete(this ElasticClient client, string index, string type, int id)
        {
            return client.Delete<DocumentAttributes>(id, d => d
                  .Index(index)
                  .Type(type).Refresh(Refresh.True));
        }

    }
}