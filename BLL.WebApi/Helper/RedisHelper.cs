using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.WebApi.Model;
using KS.Model;
using KS.Model.Ticket.TicketRequest;
using KS.Model.Ticket.TicketResponse;
using Newtonsoft.Json;
using ServiceStack.Redis;

namespace BLL.WebApi.Helper
{
    public class RedisHelper
    {
        public static bool pushMsg(QuerymemberJsonResult hy, CAVTicketModel data)
        {
            try
            {
                var db = new MPDBEntities();
                var hotel = db.PushMsgInfoes.FirstOrDefault(x => x.hotelcode == data.hotelcode&&x.flag==1);
                if (hotel != null)
                {
                    var member = new MemberEntities();
                    var scene = member.UserScenes.FirstOrDefault(x => x.scene == data.scene);
                    using (IRedisClient publisher = new RedisClient(Config.RedisUrl, 6379))
                    {
                        var msg = new
                        {
                            title = "快顺核销系统",
                            orderno = data.jycode,
                            addtime = DateTime.Now.ToString("yyyy-MM-dd"),
                            CategoryName = data.CategoryName,
                            money = data.fmoney,
                            FormulaName = data.FormulaName,
                            ticketsn = data.ticketsn,
                            mobile = data.mobile,
                            bosscard = hy.CardNo,
                            realbosscard =hy.realbosscard,
                            scenename =scene==null?"":scene.scenename,
                        };
                        var str = JsonConvert.SerializeObject(msg);

                        var isSend = publisher.PublishMessage(hotel.channel, str);

                        if (isSend > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }

            
            }
            catch (Exception)
            {

                return false;
            }

        }
    }
}