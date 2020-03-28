﻿using Microsoft.IdentityModel.Tokens;

namespace KiraNet.AlasFx.Options
{
    /// <summary>
    /// JWT身份认证选项
    /// </summary>
    public class JwtOptions
    {
        /// <summary>
        /// 密钥
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// 发行方
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 订阅方
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// AccessToken有效期分钟数
        /// </summary>
        public double AccessExpireMins { get; set; }

        /// <summary>
        /// RefreshToken有效期分钟数
        /// </summary>
        public double RefreshExpireMins { get; set; }

        /// <summary>
        /// 加密算法
        /// </summary>
        public string Encrypt { get; set; } = SecurityAlgorithms.HmacSha256;
    }
}