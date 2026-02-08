using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisClearApp.Model
{
    /// <summary>
    /// Redis信息实体类（对应SQLite的RedisInfo表）
    /// </summary>
    public class RedisInfo
    {
        /// <summary>
        /// 主键，自增整数，唯一标识每条记录
        /// </summary>
        [FreeSql.DataAnnotations.Column(IsPrimary =true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// Redis服务器IP地址（必填）
        /// </summary>
        public string Ip { get; set; } = "127.0.0.1"; // 初始化避免空引用

        /// <summary>
        /// Redis端口号（默认6379）
        /// </summary>
        public int Port { get; set; } = 6379; // 给默认值，和数据表默认值一致

        /// <summary>
        /// Redis密码（可选，默认为空字符串）
        /// </summary>
        public string Password { get; set; } = string.Empty; // 初始化避免空引用

        /// <summary>
        /// 连接是否成功（0=失败，1=成功，默认0）
        /// </summary>
        public int Issucess { get; set; } = 0;

        /// <summary>
        /// 连接对象/连接标识（可选）
        /// </summary>
        public string Conn { get; set; } = string.Empty;// 可空类型，明确表示该字段可选

        /// <summary>
        /// 备注信息（可选）
        /// </summary>
        public string Remark { get; set; } = string.Empty;// 可空类型，明确表示该字段可选

        // 可选：新增便捷属性，将Issucess转换为布尔值，提升代码可读性
        /// <summary>
        /// 连接是否成功（布尔值，便捷属性）
        /// </summary>
        [NotMapped]
        public bool IsSuccessBool
        {
            get => Issucess == 1;
            set => Issucess = value ? 1 : 0;
        }
    }
}
