﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Repository.Interface
{
    public interface IDocumentDBRepository<T> where T : class
    {
        Task<Microsoft.Azure.Documents.Document> CreateItemAsync(T item, string collectionId);
        Task DeleteItemAsync(string id, string collectionId, string partitionKey);
        Task<IEnumerable<T>> GetItemsAsync(string collectionId);
        Task<Microsoft.Azure.Documents.Document> UpdateItemAsync(string id, T item, string collectionId);
    }
}
