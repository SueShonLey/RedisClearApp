using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisClearApp.Helper
{
    public class RedisEasyHelper
    {

        public static string GetRedisString(string ip, int port, string pwd = "")
        {
            pwd = string.IsNullOrEmpty(pwd) ? string.Empty : $",password={pwd}";
            return $"{ip}:{port},defaultDatabase=0,allowAdmin=true{pwd}";
        }


        /// <summary>
        /// 尝试连接到 Redis
        /// </summary>
        /// <param name="redisConnectionString">Redis 连接字符串</param>
        /// <returns>是否连接成功</returns>
        public static async Task<bool> TryConnectAsync(string redisConnectionString)
        {
            try
            {
                // 设置连接超时为1秒
                var config = ConfigurationOptions.Parse(redisConnectionString);
                config.SyncTimeout = 1000;  // 设置同步操作的超时时间（毫秒）
                config.AsyncTimeout = 1000; // 设置异步操作的超时时间（毫秒）

                // 启动连接任务和超时计时器任务
                var connectTask = ConnectionMultiplexer.ConnectAsync(config);
                var timeoutTask = Task.Delay(500);  // 500ms超时计时器

                // 等待连接任务和超时计时器任务中较早完成的一个
                var completedTask = await Task.WhenAny(connectTask, timeoutTask);

                if (completedTask == timeoutTask)
                {
                    // 如果超时任务先完成，返回false
                    return false;
                }

                // 等待连接任务完成并获取连接实例
                var redis = await connectTask;
                var db = redis.GetDatabase();

                // 使用 Ping 命令检测 Redis 是否可用
                var pingResult = await db.PingAsync();
                return pingResult.TotalMilliseconds > 0; // 如果 Ping 返回有效结果
            }
            catch (Exception)
            {
                return false; // 连接失败
            }
        }

        /// <summary>
        /// 执行 FLUSHALL 命令
        /// </summary>
        /// <returns>FLUSHALL 命令的执行结果</returns>
        public static async Task<bool> FLUSHALL(string redisConnectionString)
        {
            try
            {
                if (await TryConnectAsync(redisConnectionString))//如果连接成功
                {
                    var redis = await ConnectionMultiplexer.ConnectAsync(redisConnectionString);   // 连接 Redis
                    await redis.GetServer(redis.GetEndPoints()[0]).FlushAllDatabasesAsync();
                    Debug.WriteLine($"{redisConnectionString}清空成功！");
                    return true;
                }
                Debug.WriteLine($"{redisConnectionString}清空失败！");
                return false;
            }
            catch (Exception)
            {
                Debug.WriteLine($"{redisConnectionString}清空失败！");
                return false; // 返回错误信息
            }
        }
    }
}
