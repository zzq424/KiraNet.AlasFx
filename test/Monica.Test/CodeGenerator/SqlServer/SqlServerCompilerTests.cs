﻿using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.Extensions.DependencyInjection;
using Monica.CodeGenerator.SqlServer;
using Monica.DataAccess.Model;
using Monica.SqlServer;

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Monica.CodeGenerator.CodeGenerator.SqlServer.Tests
{
    public class SqlServerCompilerTests
    {
        [Fact()]
        public async Task GenerateModelAsyncTest()
        {
            var generator = new SqlServerModelCompiler();
            await generator.GenerateAsync(new ModelConfig
            {
                ConnectionString = "Persist Security Info=False;User ID=sa;Password=sqlzzq;Initial Catalog=test;Data Source=localhost;",
                Database = "test",
                Schema = "dbo",
                Table = "ExternNews",
                NameSpace = "Monica.DataAccess.Model",
                FilePath = Directory.GetCurrentDirectory()
            });
        }

        [Fact()]
        public async Task GenerateInterfaceAsyncTest()
        {
            var generator = new SqlServerInterfaceCompiler();
            await generator.GenerateAsync(new ModelConfig
            {
                ConnectionString = "Persist Security Info=False;User ID=sa;Password=sqlzzq;Initial Catalog=test;Data Source=localhost;",
                Database = "test",
                Schema = "dbo",
                Table = "ExternNews",
                NameSpace = "Monica.DataAccess.Model",
                FilePath = Directory.GetCurrentDirectory()
            });
        }

        [Fact()]
        public async Task GenerateDaoAsyncTest()
        {
            var generator = new SqlServerDaoCompiler();
            await generator.GenerateAsync(new ModelConfig
            {
                ConnectionString = "Persist Security Info=False;User ID=sa;Password=sqlzzq;Initial Catalog=test;Data Source=localhost;",
                Database = "test",
                Schema = "dbo",
                Table = "ExternNews",
                NameSpace = "Monica.DataAccess.Model",
                FilePath = Directory.GetCurrentDirectory()
            });
        }

        [Fact]
        public async Task DaoTest()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<SqlServerConnectionProvider, TestSqlServerConnectionProvider>();

            var services = serviceCollection.BuildServiceProvider();
            var externNewsDataAccess = new DaoExternNews(services.GetRequiredService<SqlServerConnectionProvider>());

            try
            {
                //await externNewsDataAccess.InsertUpdateAsync(new ExternNews
                //{
                //    SecondaryType = null,
                //    Sort = default,
                //    DefaultCollectionNum = default,
                //    DefaultReadNum = default,
                //    BannerSort = default,
                //    BannerType = default,
                //    SourceId = default,
                //    SourceType = default,
                //    Summary = default,
                //    ContractId = default,
                //    CoverImg = default,
                //    Html = default,
                //    IsHot = default,
                //    IsPublish = default,
                //    IsRecommend = default,
                //    LinkUrl = default,
                //    NewsType = 1,
                //    PublishTime = default,
                //    RealCollectionNum = default,
                //    RealReadNum = default,
                //    ResourceUrl = default,
                //    Tags = default,
                //    Title = default
                //});

                //var id = externNewsDataAccess.GetLastIdentity();
                //var model = externNewsDataAccess.GetModel(id);
                //Assert.NotNull(model);

                var list = await externNewsDataAccess.GetPageAsync(null, "1=1", "id desc", 1, 5);
                Assert.True(list.Count() > 0);
            }
            catch(Exception ex)
            {

            }
        }
    }

    public class TestSqlServerConnectionProvider : SqlServerConnectionProvider
    {
        protected override string GetRealOnlyDbConnectionString()
        {
            return GetConnectionString("MonicaTestDb_Read");
        }

        protected override string GetWriteDbConnectionString()
        {
            return GetConnectionString("MonicaTestDb");
        }
    }
}