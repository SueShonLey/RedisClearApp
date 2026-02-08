using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisClearApp.EasyORM
{
    public static class DB
    {
        public static string initsql = @"-- 创建 RedisInfo 表，若已存在则不创建
CREATE TABLE IF NOT EXISTS RedisInfo (
    -- 主键，自增整数，唯一标识每条记录
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    -- Redis 服务器IP地址，非空（必填）
    Ip TEXT NOT NULL,
    -- Redis 端口号，整数类型，默认6379
    Port INTEGER DEFAULT 6379,
    -- Redis 密码，可选，默认为空字符串
    Password TEXT DEFAULT '',
    -- 连接是否成功，布尔型（SQLite用INTEGER表示，0=失败，1=成功），默认0
    Issucess INTEGER DEFAULT 0 CHECK (Issucess IN (0, 1)),
    -- 连接对象/连接标识，可选，可存储连接句柄相关信息
    Conn TEXT,
    -- 备注信息，可选，用于补充说明
    Remark TEXT
);

-- 可选：为Ip+Port创建唯一索引，避免重复存储同一Redis节点
CREATE UNIQUE INDEX IF NOT EXISTS idx_redis_ip_port ON RedisInfo(Ip, Port);";

        public static EasyCrud easydb = new EasyCrud(FreeSql.DataType.Sqlite,"", initsql);

        
    }
}
