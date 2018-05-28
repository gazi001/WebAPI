using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace KS.DataBase
{
    public class DbContextFactory
    {
        private static readonly object locker = new object();
        public static DbContext Create(DBName dbName)
        {
            DbContext dbContext = CallContext.GetData(dbName.ToString()) as DbContext;
            if (dbContext == null)
            {
                lock (locker)
                {
                    if (dbContext == null)
                    {
                        switch (dbName)
                        {
                            case DBName.ChannelDB:
                                //dbContext = new ChannelEntities();
                                //CallContext.SetData(dbName.ToString(), dbContext);
                                break;
                            case DBName.MpConfigDB:
                                //dbContext = new MPDBEntities();
                                //CallContext.SetData(dbName.ToString(), dbContext);
                                break;
                            case DBName.ReservationDB:
                                //dbContext = new yudingEntities();
                                //CallContext.SetData(dbName.ToString(), dbContext);
                                break;
                            case DBName.ITicketDB:
                                dbContext = new iticketDB();
                                CallContext.SetData(dbName.ToString(), dbContext);
                                break;
                            default:
                                dbContext = null;
                                break;
                        }
                    }

                }
            }
            return dbContext;
        }
    }

    public enum DBName
    {
        /// <summary>
        /// 渠道数据库
        /// </summary>
        ChannelDB = 0,

        /// <summary>
        /// 预订数据库
        /// </summary>
        ReservationDB = 1,

        /// <summary>
        /// 微信维护后台数据库
        /// </summary>
        MpConfigDB = 2,

        /// <summary>
        /// 票券中间数据库
        /// </summary>
        ITicketDB = 3,
    }
}
