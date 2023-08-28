using Demo.Repository.Interface;
using DocumentFormat.OpenXml.Office.CustomUI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using NPOI.SS.Formula.Eval;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.Excel;
using Demo.Models;

namespace Demo.Repository.Implementation
{
    public class DocumentDBRepository<T> : IDocumentDBRepository<T> where T : class
    {
        private readonly string Endpoint = "https://localhost:8081";
        private readonly string Key = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        private readonly string DatabaseId = "EmployeeDetaills";
        private DocumentClient client;

        public DocumentDBRepository()
        {
            client = new DocumentClient(new Uri(Endpoint), Key);
        }

        public async Task<IEnumerable<T>> GetItemsAsync(string collectionId)
        {
            IDocumentQuery<T> query = client.CreateDocumentQuery<T>(
                UriFactory.CreateDocumentCollectionUri(DatabaseId, collectionId),
                new FeedOptions { MaxItemCount = -1 })
                .AsDocumentQuery();

            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }

            return results;
        }

        public async Task<Microsoft.Azure.Documents.Document> CreateItemAsync(T item, string collectionId)
        {
            return await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, collectionId), item);
        }

        public async Task<Microsoft.Azure.Documents.Document> UpdateItemAsync(string id, T item, string collectionId)
        {
            return await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, collectionId, id), item);
        }

        public async Task DeleteItemAsync(string id, string collectionId, string partitionKey)
        {
           

            try
            {
                var documentUri = UriFactory.CreateDocumentUri(DatabaseId, collectionId, id);
                var requestOptions = new RequestOptions { PartitionKey = new PartitionKey(id) };
                var response = await client.DeleteDocumentAsync(documentUri, requestOptions);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

      

    }
}

