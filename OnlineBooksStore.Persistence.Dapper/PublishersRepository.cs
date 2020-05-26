﻿using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using OnlineBooksStore.Domain.Contracts.Models.Pages;
using OnlineBooksStore.Domain.Contracts.Repositories;
using OnlineBooksStore.Persistence.Dapper.Providers;
using OnlineBooksStore.Persistence.Entities;

namespace OnlineBooksStore.Persistence.Dapper
{
    public class PublishersRepository : IPublishersRepository
    {
        private readonly ConnectionProvider _connectionProvider;

        public PublishersRepository(ConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public PagedList<PublisherEntity> GetPublishers(QueryOptions options)
        {
            if (string.IsNullOrEmpty(options.SortPropertyName))
            {
                options.SortPropertyName = nameof(PublisherEntity.Name);
                options.DescendingOrder = false;
            }
            const string rowsCountSql = @"SELECT COUNT(*) AS [Count]
                                   FROM Publishers";

            var queryProcessing = new QueryProcessing<QueryOptions>(options);
            var sql = $@"SELECT *
                                FROM Publishers {queryProcessing.GetQueryConditions()}";
            using (var connection = _connectionProvider.OpenConnection())
            {
                var rowsCount = connection.ExecuteScalar<int>(rowsCountSql);
                var publishers = connection.Query<PublisherEntity>(sql);

                return new PagedList<PublisherEntity>(publishers, rowsCount, options);
            }
        }

        public PublisherEntity GetPublisher(long id)
        {
            const string sql = @"SELECT P.*, B.Id, B.Authors, B.Title, B.PurchasePrice, B.RetailPrice
                          FROM Publishers AS P
                                   INNER JOIN Books AS B ON P.Id = B.PublisherId
                         WHERE P.Id = @id";
            var parameter = new DynamicParameters();
            parameter.Add("@id", id, DbType.Int64, ParameterDirection.Input);
            using (var connection = _connectionProvider.OpenConnection())
            {
                var publisherDictionary = new Dictionary<long, PublisherEntity>();
                var result = connection.Query<PublisherEntity, BookEntity, PublisherEntity>(
                    sql,
                    (publisher, book) =>
                    {
                        if (!publisherDictionary.TryGetValue(publisher.Id, out var publisherEntry))
                        {
                            publisherEntry = publisher;
                            publisherEntry.Books = new List<BookEntity>();
                            publisherDictionary.Add(publisherEntry.Id, publisherEntry);
                        }
                        publisherEntry.Books.Add(book);

                        return publisherEntry;
                    }, splitOn: nameof(BookEntity.Id), param: parameter).FirstOrDefault();

                return result;
            }
        }

        public PublisherEntity AddPublisher(PublisherEntity publisher)
        {
            const string sql = @"INSERT INTO Publishers (Name, Country)
                                 VALUES @name, @country";
            var parameters = new DynamicParameters();
            parameters.Add("@name", publisher.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("@country", publisher.Name, DbType.String, ParameterDirection.Input);
            using (var connection = _connectionProvider.OpenConnection())
            {
                var affectedRows = connection.Execute(sql, parameters);

                return affectedRows > 0 ? new PublisherEntity() : null;
            }
        }

        public bool UpdatePublisher(PublisherEntity publisher)
        {
            const string sql = @"UPDATE Publishers
                                 SET Name    = @name,
                                     Country = @country,
                                     Updated = GETDATE()";
            var parameters = new DynamicParameters();
            parameters.Add("@name", publisher.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("@country", publisher.Name, DbType.String, ParameterDirection.Input);
            using (var connection = _connectionProvider.OpenConnection())
            {
                var affectedRows = connection.Execute(sql, parameters);

                return affectedRows > 0;
            }
        }

        public bool DeletePublisher(PublisherEntity publisher)
        {
            const string sql = @"DELETE FROM Publishers
                                  WHERE Id = @id";
            var parameter = new DynamicParameters();
            parameter.Add("@id", publisher.Id, DbType.Int64, ParameterDirection.Input);
            using (var connection = _connectionProvider.OpenConnection())
            {
                var affectedRows = connection.Execute(sql, parameter);

                return affectedRows > 0;
            }
        }
    }
}