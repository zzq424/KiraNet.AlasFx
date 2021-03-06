﻿using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Rye.DataAccess;
using Rye.Enums;
using System.Data;

namespace Rye.EntityFrameworkCore.Sqlite
{
    public class SqliteSqlExecutor : SqlExecutorBase
    {
        public SqliteSqlExecutor(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public SqliteSqlExecutor(IDbContext dbContext) : base(dbContext)
        {
        }

        public SqliteSqlExecutor(DbContext dbContext) : base(dbContext)
        {
        }

        public SqliteSqlExecutor(string connectionString) : base(connectionString)
        {
        }

        public override DatabaseType DatabaseType => DatabaseType.Sqlite;

        protected override IDbConnection GetDbConnection(string connectionString)
        {
            var conn = new SqliteConnection(connectionString);
            if (conn.State != ConnectionState.Open)
                conn.Open();
            return conn;
        }
    }
}
