﻿using Hangfire.Mongo.Database;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using Hangfire.Mongo.Helpers;

namespace Hangfire.Mongo.MongoUtils
{
    /// <summary>
    /// Helper utilities to work with Mongo database
    /// </summary>
    public static class MongoExtensions
    {
        /// <summary>
        /// Retreives server time in UTC zone
        /// </summary>
        /// <param name="database">Mongo database</param>
        /// <returns>Server time</returns>
        public static DateTime GetServerTimeUtc(this IMongoDatabase database)
        {
            try
            {
                dynamic serverStatus = AsyncHelper.RunSync(() => database.RunCommandAsync<dynamic>(new BsonDocument("serverStatus", 1)));
                return ((DateTime)serverStatus.localTime).ToUniversalTime();
            }
            catch (MongoException)
            {
                return DateTime.UtcNow;
            }
        }

        /// <summary>
        /// Retreives server time in UTC zone
        /// </summary>
        /// <param name="dbContext">Hangfire database context</param>
        /// <returns>Server time</returns>
        public static DateTime GetServerTimeUtc(this HangfireDbContext dbContext)
        {
            return GetServerTimeUtc(dbContext.Database);
        }
    }
}