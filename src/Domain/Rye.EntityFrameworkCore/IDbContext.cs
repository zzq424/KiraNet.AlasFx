﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Rye.EntityFrameworkCore
{
    /// <summary>
    /// 数据上下文接口
    /// </summary>
    public interface IDbContext: IDisposable
    {
        /// <summary>
        /// 保存数据变更
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// 异步保存数据变更
        /// </summary>
        /// <param name="cancelToken"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancelToken = default);
    }
}