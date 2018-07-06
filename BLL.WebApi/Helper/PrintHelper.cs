using BLL.WebApi.Model;
using KS.Model.Ticket.TicketRequest;
using KS.Model.Ticket.TicketResponse;
using Newtonsoft.Json;
using StriveEngine;
using StriveEngine.Core;
using StriveEngine.Tcp.Passive;
using StriveEngine.Tcp.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.WebApi.Helper
{
    public class PrintHelper
    {
        //public bool SendPrintMsg(QuerymemberJsonResult hy, CAVTicketModel data)
        //{

        //    var member = new MemberEntities();
        //    var scene = member.UserScenes.FirstOrDefault(x => x.scene == data.scene);
        //    var msg = new
        //    {
        //        title = "快顺核销系统",
        //        orderno = data.jycode,
        //        addtime = DateTime.Now.ToString("yyyy-MM-dd"),
        //        CategoryName = data.CategoryName,
        //        money = data.fmoney,
        //        FormulaName = data.FormulaName,
        //        ticketsn = data.ticketsn,
        //        mobile = data.mobile,
        //        bosscard = hy.CardNo,
        //        realbosscard = hy.realbosscard,
        //        scenename = scene == null ? "" : scene.scenename,
        //        scene = scene.Id,
        //        Type="1"
        //    };
        //    var str = JsonConvert.SerializeObject(msg);
        //    return true;
        //}
        private ITcpPassiveEngine tcpPassiveEngine;

        public void SendPrintMsg(QuerymemberJsonResult hy, CAVTicketModel data)
        {
            try
            {
                this.tcpPassiveEngine = NetworkEngineFactory.CreateTextTcpPassiveEngine("127.0.0.1", int.Parse("9000"), new DefaultTextContractHelper("\0"));
                //this.tcpPassiveEngine.MessageReceived += new CbDelegate<System.Net.IPEndPoint, byte[]>(tcpPassiveEngine_MessageReceived);
                this.tcpPassiveEngine.AutoReconnect = false;//启动掉线自动重连                

                this.tcpPassiveEngine.Initialize();

                if (tcpPassiveEngine.Connected)
                {
                    var member = new MemberEntities();
                    var scene = member.UserScenes.FirstOrDefault(x => x.scene == data.scene);
                    //var str = JsonConvert.SerializeObject(data);
                    var json = new
                    {
                        hotelcode=data.hotelcode,
                        title = "快顺核销系统",
                        orderno = data.jycode,
                        addtime = DateTime.Now.ToString("yyyy-MM-dd"),
                        CategoryName = data.CategoryName,
                        money = data.fmoney,
                        FormulaName = data.FormulaName,
                        ticketsn = data.ticketsn,
                        mobile = data.mobile,
                        bosscard = hy.CardNo,
                        realbosscard = hy.realbosscard,
                        scenename = scene == null ? "" : scene.scenename,
                        scene = scene.Id,
                        Type = "1"
                    };
                    var str = JsonConvert.SerializeObject(json);
                    string msg = str + "\0";// "\0" 表示一个消息的结尾
                    byte[] bMsg = System.Text.Encoding.UTF8.GetBytes(msg);//消息使用UTF-8编码
                    this.tcpPassiveEngine.SendMessageToServer(bMsg);
                    this.tcpPassiveEngine.CloseConnection();
                }
            }
            catch (Exception ee)
            {
                
                throw;
            }
        }
    }
}