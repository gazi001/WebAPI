using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BLL.WebApi.Controllers
{
    public class ITicketController : Controller
    {
        net.kuaishun.ticketmk.Service service = new net.kuaishun.ticketmk.Service();
        [HttpPost]
        public string SetCRM_ticketsn_new_json(System.String user, System.String token, System.String usertype, System.String bosscard, System.String name, System.String sjhm, System.String birth, System.String ident, System.String cardhotel, System.String sno, System.String saleid, System.String hotelcode, System.String salemodeid, System.String FormulaId, System.String OperatorId, System.String salehotelcode)
        {
            var result = service.SetCRM_ticketsn_new_json(user, token, usertype, bosscard, name, sjhm, birth, ident, cardhotel, sno, saleid, hotelcode, salemodeid, FormulaId, OperatorId, salehotelcode); return (result);
        }


        [HttpPost]
        public string Querymember_json(System.String hotelGroupId, System.String mode, System.String code)
        {
            var result = service.Querymember_json(hotelGroupId, mode, code); return (result);
        }


        [HttpPost]
        public string Setdbname_json(System.String user, System.String token, System.String dbname, System.String formulaid, System.String hotelcode)
        {
            var result = service.Setdbname_json(user, token, dbname, formulaid, hotelcode); return (result);
        }


        [HttpPost]
        public string Setdbdata_json(System.String user, System.String token, System.String categoryid, System.String num, System.String dbnameid, System.String hotelcode)
        {
            var result = service.Setdbdata_json(user, token, categoryid, num, dbnameid, hotelcode); return (result);
        }


        [HttpPost]
        public string Retdbdata_json(System.String user, System.String token, System.String categoryid, System.String num, System.String dbnameid, System.String hotelcode)
        {
            var result = service.Retdbdata_json(user, token, categoryid, num, dbnameid, hotelcode); return (result);
        }


        [HttpPost]
        public string Deldbdata_json(System.String user, System.String token, System.String categoryid, System.String num, System.String dbnameid, System.String hotelcode)
        {
            var result = service.Deldbdata_json(user, token, categoryid, num, dbnameid, hotelcode); return (result);
        }


        [HttpPost]
        public string Getdbname_json(System.String user, System.String token, System.String hotelcode)
        {
            var result = service.Getdbname_json(user, token, hotelcode); return (result);
        }


        [HttpPost]
        public string Getdbname_json_new(System.String user, System.String token, System.String hotelcode)
        {
            var result = service.Getdbname_json_new(user, token, hotelcode); return (result);
        }


        [HttpPost]
        public string ticket_create_new_json(System.String user, System.String token, System.String id, System.String hotelcode, System.Int32 num, System.String FormulaId)
        {
            var result = service.ticket_create_new_json(user, token, id, hotelcode, num, FormulaId); return (result);
        }


        [HttpPost]
        public string Getdbname_single_json(System.String user, System.String token, System.String id, System.String hotelcode)
        {
            var result = service.Getdbname_single_json(user, token, id, hotelcode); return (result);
        }


        [HttpPost]
        public string Getdbname_channel_single_json(System.String user, System.String token, System.String id, System.String hotelcode, System.String channel)
        {
            var result = service.Getdbname_channel_single_json(user, token, id, hotelcode, channel); return (result);
        }


        [HttpPost]
        public string Getdbname_single_other_json(System.String user, System.String token, System.String formulaid, System.String hotelcode)
        {
            var result = service.Getdbname_single_other_json(user, token, formulaid, hotelcode); return (result);
        }


        [HttpPost]
        public string Getdbname_single_new_json(System.String user, System.String token, System.String id, System.String hotelcode)
        {
            var result = service.Getdbname_single_new_json(user, token, id, hotelcode); return (result);
        }


        [HttpPost]
        public string getsl(System.String cid, System.String id, System.String hotelcode)
        {
            var result = service.getsl(cid, id, hotelcode); return (result);
        }


        [HttpPost]
        public string ticket_tp_create_json(System.String user, System.String pwd, System.String FormulaId, System.String salemode, System.Decimal tmoney, System.Int32 Num, System.String oid, System.Decimal kpmoney, System.String hotelcode)
        {
            var result = service.ticket_tp_create_json(user, pwd, FormulaId, salemode, tmoney, Num, oid, kpmoney, hotelcode); return (result);
        }


        [HttpPost]
        public string ticket_tpdata_pl_create_json(System.String user, System.String pwd, System.String FormulaId, System.String oid, System.Int32 num, System.String sessionid, System.String hotelcode)
        {
            var result = service.ticket_tpdata_pl_create_json(user, pwd, FormulaId, oid, num, sessionid, hotelcode); return (result);
        }


        [HttpPost]
        public string ticket_tpdata_create_json(System.String user, System.String pwd, System.String FormulaId, System.String oid, System.String sessionid, System.String hotelcode)
        {
            var result = service.ticket_tpdata_create_json(user, pwd, FormulaId, oid, sessionid, hotelcode); return (result);
        }


        [HttpPost]
        public string StopFormula_t(System.String user, System.String token, System.String FormulaId, System.String hotelcode)
        {
            var result = service.StopFormula_t(user, token, FormulaId, hotelcode); return (result);
        }


        [HttpPost]
        public string CheckFormulacode_t(System.String user, System.String token, System.String Formulacode, System.String hotelcode)
        {
            var result = service.CheckFormulacode_t(user, token, Formulacode, hotelcode); return (result);
        }


        [HttpPost]
        public string SetCRM_ticketsn_pl_json(System.String user, System.String token, System.String usertype, System.String bosscard, System.String name, System.String sjhm, System.String birth, System.String ident, System.String cardhotel, System.String sno, System.String saleid, System.String hotelcode, System.String salemodeid, System.String FormulaId, System.Int32 num, System.String OperatorId)
        {
            var result = service.SetCRM_ticketsn_pl_json(user, token, usertype, bosscard, name, sjhm, birth, ident, cardhotel, sno, saleid, hotelcode, salemodeid, FormulaId, num, OperatorId); return (result);
        }


        [HttpPost]
        public string SetCRM_ticketsn_json(System.String user, System.String token, System.String usertype, System.String bosscard, System.String name, System.String sjhm, System.String birth, System.String ident, System.String cardhotel, System.String sno, System.String saleid, System.String hotelcode, System.String salemodeid, System.String FormulaId, System.String OperatorId)
        {
            var result = service.SetCRM_ticketsn_json(user, token, usertype, bosscard, name, sjhm, birth, ident, cardhotel, sno, saleid, hotelcode, salemodeid, FormulaId, OperatorId); return (result);
        }


        [HttpPost]
        public string Getcategory_byhotelcode_json(System.String user, System.String token, System.String hotelcode)
        {
            var result = service.Getcategory_byhotelcode_json(user, token, hotelcode); return (result);
        }


        [HttpPost]
        public string Getcategory_single_json(System.String user, System.String token, System.String CategoryId, System.String hotelcode)
        {
            var result = service.Getcategory_single_json(user, token, CategoryId, hotelcode); return (result);
        }


        [HttpPost]
        public string Gettoken_json(System.String user, System.String pwd, System.String hotelcode, System.String systemtime)
        {
            var result = service.Gettoken_json(user, pwd, hotelcode, systemtime); return (result);
        }


        [HttpPost]
        public string ticket_create_json(System.String user, System.String pwd, System.String FormulaId, System.String CategoryId, System.String salemode, System.Decimal tmoney, System.Int32 Num, System.String oid, System.Decimal kpmoney, System.String hotelcode)
        {
            var result = service.ticket_create_json(user, pwd, FormulaId, CategoryId, salemode, tmoney, Num, oid, kpmoney, hotelcode); return (result);
        }


        [HttpPost]
        public string SetFormula_fw_json(System.String user, System.String token, System.String FormulaId, System.String hotelcode, System.String hotelcodenew)
        {
            var result = service.SetFormula_fw_json(user, token, FormulaId, hotelcode, hotelcodenew); return (result);
        }


        [HttpPost]
        public string SetFormula_fw_pl_json(System.String user, System.String token, System.String FormulaId, System.String hotelcode, System.String hotelcodenew)
        {
            var result = service.SetFormula_fw_pl_json(user, token, FormulaId, hotelcode, hotelcodenew); return (result);
        }


        [HttpPost]
        public string DelFormula_fw_json(System.String user, System.String token, System.String FormulaId, System.String hotelcode)
        {
            var result = service.DelFormula_fw_json(user, token, FormulaId, hotelcode); return (result);
        }


        [HttpPost]
        public string GetFormula_fw_json(System.String user, System.String token, System.String strwhere, System.String hotelcode, System.String hotelcodenew)
        {
            var result = service.GetFormula_fw_json(user, token, strwhere, hotelcode, hotelcodenew); return (result);
        }


        [HttpPost]
        public string SetFormula_t(System.String user, System.String token, net.kuaishun.ticketmk.Formula myFormula, System.String hotelcode)
        {
            var result = service.SetFormula_t(user, token, myFormula, hotelcode); return (result);
        }


        [HttpPost]
        public string SetFormula_json(System.String user, System.String token, System.String FormulaName, System.String FormulaSummary, System.String Formulacode, System.String fmoney, System.String fnum, System.String hotelcode)
        {
            var result = service.SetFormula_json(user, token, FormulaName, FormulaSummary, Formulacode, fmoney, fnum, hotelcode); return (result);
        }


        [HttpPost]
        public string RetFormula_json(System.String user, System.String token, System.String FormulaName, System.String FormulaSummary, System.String FormulaId, System.String Formulacode, System.String fmoney, System.String fnum, System.String hotelcode)
        {
            var result = service.RetFormula_json(user, token, FormulaName, FormulaSummary, FormulaId, Formulacode, fmoney, fnum, hotelcode); return (result);
        }


        [HttpPost]
        public string GetFormulanew_json(System.String user, System.String token, System.String strwhere, System.String hotelcode)
        {
            var result = service.GetFormulanew_json(user, token, strwhere, hotelcode); return (result);
        }


        [HttpPost]
        public string GetFormulanew_top_json(System.String user, System.String token, System.String hotelcode, System.Int32 page, System.Int32 pageSize, System.String desc)
        {
            var result = service.GetFormulanew_top_json(user, token, hotelcode, page, pageSize, desc); return (result);
        }


        [HttpPost]
        public string select_order1(System.Int32 page, System.Int32 pageSize, System.String desc, System.String hotelcode)
        {
            var result = service.select_order1(page, pageSize, desc, hotelcode); return (result);
        }


        [HttpPost]
        public string GetFormulanew_channel_json(System.String user, System.String token, System.String strwhere, System.String hotelcode, System.String channel)
        {
            var result = service.GetFormulanew_channel_json(user, token, strwhere, hotelcode, channel); return (result);
        }


        [HttpPost]
        public string GetFormula_json(System.String user, System.String token, System.String strwhere, System.String hotelcode)
        {
            var result = service.GetFormula_json(user, token, strwhere, hotelcode); return (result);
        }


        [HttpPost]
        public string GetFormula_stop_json(System.String user, System.String token, System.String hotelcode)
        {
            var result = service.GetFormula_stop_json(user, token, hotelcode); return (result);
        }


        [HttpPost]
        public string Ticketsn_fp_Getbytp_id(System.String useraccount, System.String tp_id)
        {
            var result = service.Ticketsn_fp_Getbytp_id(useraccount, tp_id); return (result);
        }


        [HttpPost]
        public string Ticket_jl_ysq_Get(System.String useraccount, System.String ticketsn)
        {
            var result = service.Ticket_jl_ysq_Get(useraccount, ticketsn); return (result);
        }


        [HttpPost]
        public string Ticket_jl_ysq_Get_new(System.String useraccount, System.String ticketsn, System.String hotelgrouptype, System.String hotelcode)
        {
            var result = service.Ticket_jl_ysq_Get_new(useraccount, ticketsn, hotelgrouptype, hotelcode); return (result);
        }


        [HttpPost]
        public string Ticket_jl_ysq_Get_new_hotelcode(System.String useraccount, System.String ticketsn, System.String hotelgrouptype, System.String hotelcode)
        {
            var result = service.Ticket_jl_ysq_Get_new_hotelcode(useraccount, ticketsn, hotelgrouptype, hotelcode); return (result);
        }


        [HttpPost]
        public string Stopcategory_t(System.String user, System.String token, System.String CategoryId, System.String hotelcode)
        {
            var result = service.Stopcategory_t(user, token, CategoryId, hotelcode); return (result);
        }


        [HttpPost]
        public string Setcategory_t(System.String user, System.String token, net.kuaishun.ticketmk.Category myCategory, System.String hotelcode)
        {
            var result = service.Setcategory_t(user, token, myCategory, hotelcode); return (result);
        }


        [HttpPost]
        public string Setcategory_fw_t(System.String user, System.String token, net.kuaishun.ticketmk.Category_fw myCategory_fw, System.String hotelcode)
        {
            var result = service.Setcategory_fw_t(user, token, myCategory_fw, hotelcode); return (result);
        }


        [HttpPost]
        public string Retcategory_json(System.String user, System.String token, System.String CategoryName, System.String Categorycode, System.String Summary, System.String hotelcode, System.String pic, System.String BeginString, System.String EndString, System.String maxNum, System.String type, System.String sflag, System.String ExpireDate, System.String ExpireDateend, System.String endnum, System.String dzpq, System.String isbool, System.String moneytype, System.String rate, System.String CategoryId, System.String CategoryNamebm, System.String fmoney, System.String iszs, System.String iscz)
        {
            var result = service.Retcategory_json(user, token, CategoryName, Categorycode, Summary, hotelcode, pic, BeginString, EndString, maxNum, type, sflag, ExpireDate, ExpireDateend, endnum, dzpq, isbool, moneytype, rate, CategoryId, CategoryNamebm, fmoney, iszs, iscz); return (result);
        }


        [HttpPost]
        public string Setcategory_json(System.String user, System.String token, System.String CategoryName, System.String Categorycode, System.String Summary, System.String HotelId, System.String pic, System.String BeginString, System.String EndString, System.String maxNum, System.String type, System.String sflag, System.String ExpireDate, System.String ExpireDateend, System.String endnum, System.String dzpq, System.String isbool, System.String moneytype, System.String rate, System.String CategoryNamebm, System.String fmoney, System.String fnum, System.String iszs, System.String iscz)
        {
            var result = service.Setcategory_json(user, token, CategoryName, Categorycode, Summary, HotelId, pic, BeginString, EndString, maxNum, type, sflag, ExpireDate, ExpireDateend, endnum, dzpq, isbool, moneytype, rate, CategoryNamebm, fmoney, fnum, iszs, iscz); return (result);
        }


        [HttpPost]
        public string Setcategory_cz_json(System.String user, System.String token, System.String CategoryName, System.String Categorycode, System.String Summary, System.String HotelId, System.String pic, System.String BeginString, System.String EndString, System.String maxNum, System.String type, System.String sflag, System.String ExpireDate, System.String ExpireDateend, System.String endnum, System.String dzpq, System.String isbool, System.String moneytype, System.String rate, System.String CategoryNamebm, System.String fmoney, System.String fnum, System.String iszs, System.String iscz, System.String oid)
        {
            var result = service.Setcategory_cz_json(user, token, CategoryName, Categorycode, Summary, HotelId, pic, BeginString, EndString, maxNum, type, sflag, ExpireDate, ExpireDateend, endnum, dzpq, isbool, moneytype, rate, CategoryNamebm, fmoney, fnum, iszs, iscz, oid); return (result);
        }


        [HttpPost]
        public string Retcategory_cz_json(System.String user, System.String token, System.String CategoryName, System.String Categorycode, System.String Summary, System.String HotelId, System.String pic, System.String BeginString, System.String EndString, System.String maxNum, System.String type, System.String sflag, System.String ExpireDate, System.String ExpireDateend, System.String endnum, System.String dzpq, System.String isbool, System.String moneytype, System.String rate, System.String CategoryNamebm, System.String fmoney, System.String fnum, System.String iszs, System.String iscz, System.String Categoryid)
        {
            var result = service.Retcategory_cz_json(user, token, CategoryName, Categorycode, Summary, HotelId, pic, BeginString, EndString, maxNum, type, sflag, ExpireDate, ExpireDateend, endnum, dzpq, isbool, moneytype, rate, CategoryNamebm, fmoney, fnum, iszs, iscz, Categoryid); return (result);
        }


        [HttpPost]
        public string Setcategory_fw_json(System.String user, System.String token, System.String CategoryId, System.String hotelcode, System.String hotelcodenew)
        {
            var result = service.Setcategory_fw_json(user, token, CategoryId, hotelcode, hotelcodenew); return (result);
        }


        [HttpPost]
        public string Setcategory_fw_pl_json(System.String user, System.String token, System.String CategoryId, System.String hotelcode, System.String hotelcodenew)
        {
            var result = service.Setcategory_fw_pl_json(user, token, CategoryId, hotelcode, hotelcodenew); return (result);
        }


        [HttpPost]
        public string Delcategory_fw_json(System.String user, System.String token, System.String CategoryId, System.String hotelcode, System.String hotelcodenew)
        {
            var result = service.Delcategory_fw_json(user, token, CategoryId, hotelcode, hotelcodenew); return (result);
        }


        [HttpPost]
        public string Getcategory_fw_json(System.String user, System.String token, System.String strwhere, System.String hotelcode, System.String hotelcodenew)
        {
            var result = service.Getcategory_fw_json(user, token, strwhere, hotelcode, hotelcodenew); return (result);
        }


        [HttpPost]
        public string Setticket_create_log_json(System.String user, System.String token, System.String categoryid, System.String cnum, System.String maxnum, System.String hotelcode)
        {
            var result = service.Setticket_create_log_json(user, token, categoryid, cnum, maxnum, hotelcode); return (result);
        }


        [HttpPost]
        public string Getticket_create_log_json(System.String user, System.String token, System.String categoryid, System.String hotelcode)
        {
            var result = service.Getticket_create_log_json(user, token, categoryid, hotelcode); return (result);
        }


        [HttpPost]
        public string Getcategorynew_json(System.String user, System.String token, System.String strwhere, System.String hotelcode)
        {
            var result = service.Getcategorynew_json(user, token, strwhere, hotelcode); return (result);
        }


        [HttpPost]
        public string Getcategorynew_channel_json(System.String user, System.String token, System.String strwhere, System.String hotelcode, System.String channel)
        {
            var result = service.Getcategorynew_channel_json(user, token, strwhere, hotelcode, channel); return (result);
        }


        [HttpPost]
        public string Getcategory_json(System.String user, System.String token, System.String strwhere, System.String hotelcode)
        {
            var result = service.Getcategory_json(user, token, strwhere, hotelcode); return (result);
        }


        [HttpPost]
        public string Getticket_user_honor_json(System.String user, System.String token, System.String starttime, System.String endtime, System.String hotelcode, System.String other)
        {
            var result = service.Getticket_user_honor_json(user, token, starttime, endtime, hotelcode, other); return (result);
        }


        [HttpPost]
        public string Getmember_month_json(System.String user, System.String token, System.String grouptype, System.String byear, System.String bmonth)
        {
            var result = service.Getmember_month_json(user, token, grouptype, byear, bmonth); return (result);
        }


        //[HttpPost]
        //public string Getmember_all_json(System.String user, System.String token, System.String grouptype)
        //{
        //    //var result = service.Getmember_all_json(user, token, grouptype); return (result);
        //}


        [HttpPost]
        public string Ticket_jl_add_notime_Set(net.kuaishun.ticketmk.process myprocess)
        {
            var result = service.Ticket_jl_add_notime_Set(myprocess); return (result);
        }


        [HttpPost]
        public string Ticket_jl_add_notime_channel_Set(net.kuaishun.ticketmk.process myprocess)
        {
            var result = service.Ticket_jl_add_notime_channel_Set(myprocess); return (result);
        }


        [HttpPost]
        public string Ticket_jl_add_Set(net.kuaishun.ticketmk.process myprocess)
        {
            var result = service.Ticket_jl_add_Set(myprocess); return (result);
        }


        [HttpPost]
        public string Ticket_jl_add_timexz_Set(net.kuaishun.ticketmk.process myprocess)
        {
            var result = service.Ticket_jl_add_timexz_Set(myprocess); return (result);
        }


        [HttpPost]
        public string Ticket_jl_add_new_Set(net.kuaishun.ticketmk.process myprocess)
        {
            var result = service.Ticket_jl_add_new_Set(myprocess); return (result);
        }


        [HttpPost]
        public string Ticket_jl_add_channel_Set(net.kuaishun.ticketmk.process myprocess)
        {
            var result = service.Ticket_jl_add_channel_Set(myprocess); return (result);
        }


        [HttpPost]
        public string Ticket_jl_add_ticketsn_Set(net.kuaishun.ticketmk.process myprocess)
        {
            var result = service.Ticket_jl_add_ticketsn_Set(myprocess); return (result);
        }


        [HttpPost]
        public string Ticket_jl_add_ticketsn_cz_Set(net.kuaishun.ticketmk.process myprocess)
        {
            var result = service.Ticket_jl_add_ticketsn_cz_Set(myprocess); return (result);
        }


        [HttpPost]
        public string Ticket_jl_jf_add_Set(net.kuaishun.ticketmk.process myprocess)
        {
            var result = service.Ticket_jl_jf_add_Set(myprocess); return (result);
        }


        [HttpPost]
        public string Jifen_user_v_Get(System.String useraccount, System.DateTime xydate)
        {
            var result = service.Jifen_user_v_Get(useraccount, xydate); return (result);
        }


        [HttpPost]
        public string Jifen_user_v_Getbyhotelcode(System.String useraccount, System.DateTime xydate, System.String hotelcode)
        {
            var result = service.Jifen_user_v_Getbyhotelcode(useraccount, xydate, hotelcode); return (result);
        }


        [HttpPost]
        public string Ticket_user_v_create_Get(System.String useraccount, System.String type, System.DateTime xydate, System.String hotelcode)
        {
            var result = service.Ticket_user_v_create_Get(useraccount, type, xydate, hotelcode); return (result);
        }


        [HttpPost]
        public string Ticket_user_v_create_Get_new(System.String useraccount, System.String type, System.DateTime xydate, System.String hotelcode, System.String grouptype)
        {
            var result = service.Ticket_user_v_create_Get_new(useraccount, type, xydate, hotelcode, grouptype); return (result);
        }


        [HttpPost]
        public string Ticketsn_search_Get(System.String ticketsn, System.DateTime xydate, System.String hotelcode)
        {
            var result = service.Ticketsn_search_Get(ticketsn, xydate, hotelcode); return (result);
        }


        [HttpPost]
        public string Ticketsn_detail_Get(System.String useraccount, System.String tp_id, System.DateTime xydate)
        {
            var result = service.Ticketsn_detail_Get(useraccount, tp_id, xydate); return (result);
        }


        [HttpPost]
        public string Ticketsn_fp_Get(System.String useraccount)
        {
            var result = service.Ticketsn_fp_Get(useraccount); return (result);
        }


        [HttpPost]
        public string Getticket_yhqtj_report_json(System.String user, System.String token, System.String hotelcode, System.String formulaid)
        {
            var result = service.Getticket_yhqtj_report_json(user, token, hotelcode, formulaid); return (result);
        }


        [HttpPost]
        public string Setsaleid_json(System.String user, System.String token, System.String ticketsn, System.String saleid, System.String addname)
        {
            var result = service.Setsaleid_json(user, token, ticketsn, saleid, addname); return (result);
        }


        [HttpPost]
        public string Ticket_user_v_create_Get_newall(System.String useraccount, System.String type, System.DateTime xydate, System.String hotelcode, System.String grouptype, System.String code)
        {
            var result = service.Ticket_user_v_create_Get_newall(useraccount, type, xydate, hotelcode, grouptype, code); return (result);
        }


        [HttpPost]
        public string Querymemberbyticketsn_json(System.String hotelGroupId, System.String hotelcode, System.String ticketsn, System.String hotelcodenew)
        {
            var result = service.Querymemberbyticketsn_json(hotelGroupId, hotelcode, ticketsn, hotelcodenew); return (result);
        }


        [HttpPost]
        public string Querymemberbytp_id_json(System.String hotelGroupId, System.String hotelcode, System.String tp_id, System.String categoryid, System.String hotelcodenew)
        {
            var result = service.Querymemberbytp_id_json(hotelGroupId, hotelcode, tp_id, categoryid, hotelcodenew); return (result);
        }


        [HttpPost]
        public string Ticket_jl_add_ticketsnnew_Set(net.kuaishun.ticketmk.process myprocess)
        {
            var result = service.Ticket_jl_add_ticketsnnew_Set(myprocess); return (result);
        }


        [HttpPost]
        public string Ticket_jl_add_ticketsnnew_bychannel_Set(net.kuaishun.ticketmk.process myprocess)
        {
            var result = service.Ticket_jl_add_ticketsnnew_bychannel_Set(myprocess); return (result);
        }


        [HttpPost]
        public string Setticket_ly_json(System.String user, System.String token, System.String lyhotelcode, System.String lydept, System.String lyr, System.String ticketsnstart, System.String ticketsnend, System.String formulaid, System.String categoryid, System.String num, System.String oid, System.String hotelcode)
        {
            var result = service.Setticket_ly_json(user, token, lyhotelcode, lydept, lyr, ticketsnstart, ticketsnend, formulaid, categoryid, num, oid, hotelcode); return (result);
        }


        [HttpPost]
        public string Getticket_bydept_json(System.String user, System.String token, System.String hotelcode, System.String dept, System.String hotelcodenew, System.String where)
        {
            var result = service.Getticket_bydept_json(user, token, hotelcode, dept, hotelcodenew, where); return (result);
        }


        [HttpPost]
        public string Getticket_bygq_json(System.String user, System.String token, System.String hotelcode, System.String starttime, System.String endtime)
        {
            var result = service.Getticket_bygq_json(user, token, hotelcode, starttime, endtime); return (result);
        }


        [HttpPost]
        public string Getticket_bydept_canuse_json(System.String user, System.String token, System.String hotelcode, System.String dept, System.String hotelcodenew, System.String where)
        {
            var result = service.Getticket_bydept_canuse_json(user, token, hotelcode, dept, hotelcodenew, where); return (result);
        }


        [HttpPost]
        public string Getticket_jl_byxftype_json(System.String user, System.String token, System.String mode, System.String code, System.String hotelcode, System.String xftype)
        {
            var result = service.Getticket_jl_byxftype_json(user, token, mode, code, hotelcode, xftype); return (result);
        }


        [HttpPost]
        public string Getticket_jl_bydate_json(System.String user, System.String token, System.String mode, System.String code, System.String hotelcode, System.String xftype, System.String starttime, System.String endtime)
        {
            var result = service.Getticket_jl_bydate_json(user, token, mode, code, hotelcode, xftype, starttime, endtime); return (result);
        }


        [HttpPost]
        public string Getticket_jl_byhotelgroup_no_json(System.String user, System.String token, System.String hotelgroupcode, System.String hotelcode, System.String starttime, System.String endtime)
        {
            var result = service.Getticket_jl_byhotelgroup_no_json(user, token, hotelgroupcode, hotelcode, starttime, endtime); return (result);
        }


        [HttpPost]
        public string Getticket_jl_by_no_json(System.String user, System.String token, System.String hotelgroupcode, System.String hotelcode, System.String starttime, System.String endtime)
        {
            var result = service.Getticket_jl_by_no_json(user, token, hotelgroupcode, hotelcode, starttime, endtime); return (result);
        }


        [HttpPost]
        public string Getticket_jl_byhotelgroup_json(System.String user, System.String token, System.String hotelgroupcode, System.String hotelcode, System.String starttime, System.String endtime)
        {
            var result = service.Getticket_jl_byhotelgroup_json(user, token, hotelgroupcode, hotelcode, starttime, endtime); return (result);
        }


        [HttpPost]
        public string Getticket_jl_hx_json(System.String user, System.String token, System.String starttime, System.String endtime, System.String hotelcode)
        {
            var result = service.Getticket_jl_hx_json(user, token, starttime, endtime, hotelcode); return (result);
        }


        [HttpPost]
        public string Getticket_jl_hx_fy_json(System.String user, System.String token, System.String starttime, System.String endtime, System.String hotelcode, System.Int32 page, System.Int32 pageSize, System.String desc)
        {
            var result = service.Getticket_jl_hx_fy_json(user, token, starttime, endtime, hotelcode, page, pageSize, desc); return (result);
        }


        [HttpPost]
        public string Getticket_jl_kk_json(System.String user, System.String token, System.String starttime, System.String endtime, System.String hotelcode, System.String type)
        {
            var result = service.Getticket_jl_kk_json(user, token, starttime, endtime, hotelcode, type); return (result);
        }


        [HttpPost]
        public string Getticket_jl_kk_fy_json(System.String user, System.String token, System.String starttime, System.String endtime, System.String hotelcode, System.Int32 page, System.Int32 pageSize, System.String desc)
        {
            var result = service.Getticket_jl_kk_fy_json(user, token, starttime, endtime, hotelcode, page, pageSize, desc); return (result);
        }


        [HttpPost]
        public string Getticket_user_json(System.String user, System.String token, System.String starttime, System.String endtime, System.String hotelcode, System.String other)
        {
            var result = service.Getticket_user_json(user, token, starttime, endtime, hotelcode, other); return (result);
        }


        [HttpPost]
        public string Getticket_sale_report_json(System.String user, System.String token, System.String starttime, System.String endtime, System.String hotelcode, System.String other)
        {
            var result = service.Getticket_sale_report_json(user, token, starttime, endtime, hotelcode, other); return (result);
        }


        [HttpPost]
        public string Getticket_jl_new_json(System.String user, System.String token, System.String mode, System.String code, System.String hotelcode, System.String starttime, System.String endtime)
        {
            var result = service.Getticket_jl_new_json(user, token, mode, code, hotelcode, starttime, endtime); return (result);
        }


        [HttpPost]
        public string Getticket_jl_json_hotelcode(System.String user, System.String token, System.String mode, System.String code, System.String hotelcode)
        {
            var result = service.Getticket_jl_json_hotelcode(user, token, mode, code, hotelcode); return (result);
        }


        [HttpPost]
        public string Getticket_jl_json_bygroup(System.String user, System.String token, System.String mode, System.String code, System.String grouptype)
        {
            var result = service.Getticket_jl_json_bygroup(user, token, mode, code, grouptype); return (result);
        }


        [HttpPost]
        public string Setticket_endtime_json(System.String user, System.String token, System.String ticketsn, System.String endtime, System.String categoryid, System.String hotelcode, System.String iswxly)
        {
            var result = service.Setticket_endtime_json(user, token, ticketsn, endtime, categoryid, hotelcode, iswxly); return (result);
        }


        [HttpPost]
        public string Setticket_starttime_json(System.String user, System.String token, System.String ticketsn, System.String starttime, System.String categoryid, System.String hotelcode, System.String iswxly)
        {
            var result = service.Setticket_starttime_json(user, token, ticketsn, starttime, categoryid, hotelcode, iswxly); return (result);
        }


        [HttpPost]
        public string Setticket_yhquse_new_json(System.String user, System.String token, System.String ticketsn, System.String resno, System.String oid, System.String tmoney, System.String hotelcode, System.String ts)
        {
            var result = service.Setticket_yhquse_new_json(user, token, ticketsn, resno, oid, tmoney, hotelcode, ts); return (result);
        }


        [HttpPost]
        public string Setticket_yhquse_json(System.String user, System.String token, System.String ticketsn, System.String resno, System.String oid, System.String tmoney, System.String hotelcode, System.String gdcode)
        {
            var result = service.Setticket_yhquse_json(user, token, ticketsn, resno, oid, tmoney, hotelcode, gdcode); return (result);
        }


        [HttpPost]
        public string Setticket_yhquse_crs_json(System.String user, System.String token, System.String ticketsn, System.String resno, System.String oid, System.String tmoney, System.String hotelcode, System.String gdcode, System.String hotelcodenew)
        {
            var result = service.Setticket_yhquse_crs_json(user, token, ticketsn, resno, oid, tmoney, hotelcode, gdcode, hotelcodenew); return (result);
        }


        [HttpPost]
        public string Smssend_ks(System.String content, System.String mobile, System.String user, System.String passwd)
        {
            var result = service.Smssend_ks(content, mobile, user, passwd); return (result);
        }


        [HttpPost]
        public string Smssend(System.String content, System.String mobile, System.String user, System.String passwd, System.String usercode)
        {
            var result = service.Smssend(content, mobile, user, passwd, usercode); return (result);
        }


        [HttpPost]
        public string Getticket_zzjl_jsonp(System.String user, System.String token, System.String startdate, System.String enddate, System.String grouptype)
        {
            var result = service.Getticket_zzjl_jsonp(user, token, startdate, enddate, grouptype); return (result);
        }


        [HttpPost]
        public string Getticket_zzjl_fy_jsonp(System.String user, System.String token, System.String startdate, System.String enddate, System.String grouptype, System.Int32 page, System.Int32 pageSize, System.String desc)
        {
            var result = service.Getticket_zzjl_fy_jsonp(user, token, startdate, enddate, grouptype, page, pageSize, desc); return (result);
        }


        [HttpPost]
        public string select_order_zzjl1(System.Int32 page, System.Int32 pageSize, System.String desc, System.String startdate, System.String enddate, System.String grouptype)
        {
            var result = service.select_order_zzjl1(page, pageSize, desc, startdate, enddate, grouptype); return (result);
        }


        [HttpPost]
        public string Getticket_user_byregedit(System.String user, System.String token, System.String mode, System.String code, System.String formulaid, System.String hotelcode)
        {
            var result = service.Getticket_user_byregedit(user, token, mode, code, formulaid, hotelcode); return (result);
        }


        [HttpPost]
        public string Setday_ye_json(System.String user, System.String token, System.String bdate, System.String dayfmoney, System.String dayxfmoney, System.String daytmoney, System.String monthfmoney, System.String monthxfmoney, System.String monthtmoney, System.String yearfmoney, System.String yearxfmoney, System.String yeartmoney, System.String addname, System.String hotelcode)
        {
            var result = service.Setday_ye_json(user, token, bdate, dayfmoney, dayxfmoney, daytmoney, monthfmoney, monthxfmoney, monthtmoney, yearfmoney, yearxfmoney, yeartmoney, addname, hotelcode); return (result);
        }


        [HttpPost]
        public string Retday_ye_json(System.String user, System.String token, System.String bdate, System.String dayfmoney, System.String dayxfmoney, System.String daytmoney, System.String monthfmoney, System.String monthxfmoney, System.String monthtmoney, System.String yearfmoney, System.String yearxfmoney, System.String yeartmoney, System.String addname, System.String hotelcode)
        {
            var result = service.Retday_ye_json(user, token, bdate, dayfmoney, dayxfmoney, daytmoney, monthfmoney, monthxfmoney, monthtmoney, yearfmoney, yearxfmoney, yeartmoney, addname, hotelcode); return (result);
        }


        [HttpPost]
        public string Getopenidtmoney(System.String openid, System.String tmoney, System.String hotelcode)
        {
            var result = service.Getopenidtmoney(openid, tmoney, hotelcode); return (result);
        }


        [HttpPost]
        public string Setticketsnchange(System.String oldmobile, System.String ticketsn, System.String newmobile, System.String grouptype, System.String hotelcode, System.String formulaid, System.String summary)
        {
            var result = service.Setticketsnchange(oldmobile, ticketsn, newmobile, grouptype, hotelcode, formulaid, summary); return (result);
        }


        [HttpPost]
        public string Sethotelsystem_json(System.String user, System.String token, System.String hotelcode, System.String systemcode)
        {
            var result = service.Sethotelsystem_json(user, token, hotelcode, systemcode); return (result);
        }


        [HttpPost]
        public string Dethotelsystem_json(System.String user, System.String token, System.String hotelcode)
        {
            var result = service.Dethotelsystem_json(user, token, hotelcode); return (result);
        }


        [HttpPost]
        public string Gethotelsystem_json(System.String user, System.String token, System.String hotelcode)
        {
            var result = service.Gethotelsystem_json(user, token, hotelcode); return (result);
        }


        [HttpPost]
        public string Setuser_json(System.String user, System.String token, System.String rolecode, System.String username, System.String usercode, System.String pwd, System.String mobile, System.String hotelcode, System.String hotelgroupcode, System.String deptcode, System.String level)
        {
            var result = service.Setuser_json(user, token, rolecode, username, usercode, pwd, mobile, hotelcode, hotelgroupcode, deptcode, level); return (result);
        }


        [HttpPost]
        public string Setuser_role_json(System.String user, System.String token, System.String rolecode, System.String usercode)
        {
            var result = service.Setuser_role_json(user, token, rolecode, usercode); return (result);
        }


        [HttpPost]
        public string Getuser_role_json(System.String user, System.String token, System.String usercode)
        {
            var result = service.Getuser_role_json(user, token, usercode); return (result);
        }


        [HttpPost]
        public string Stopornostopuser_role_json(System.String user, System.String token, System.String rolecode, System.String usercode, System.String flag)
        {
            var result = service.Stopornostopuser_role_json(user, token, rolecode, usercode, flag); return (result);
        }


        [HttpPost]
        public string Stopornostopuser_json(System.String user, System.String token, System.String usercode, System.String state)
        {
            var result = service.Stopornostopuser_json(user, token, usercode, state); return (result);
        }


        [HttpPost]
        public string Retuser_json(System.String user, System.String token, System.String rolecode, System.String username, System.String usercode, System.String pwd, System.String mobile, System.String hotelcode, System.String hotelgroupcode, System.String deptcode, System.String level)
        {
            var result = service.Retuser_json(user, token, rolecode, username, usercode, pwd, mobile, hotelcode, hotelgroupcode, deptcode, level); return (result);
        }


        [HttpPost]
        public string Getuserlist_json(System.String user, System.String token, System.String hotelgroupcode, System.String hotelcode, System.String deptcode)
        {
            var result = service.Getuserlist_json(user, token, hotelgroupcode, hotelcode, deptcode); return (result);
        }


        [HttpPost]
        public string Getuserlist_hotel_json(System.String user, System.String token, System.String hotelgroupcode, System.String hotelcode)
        {
            var result = service.Getuserlist_hotel_json(user, token, hotelgroupcode, hotelcode); return (result);
        }


        [HttpPost]
        public string Getuserlist_hotelgroup_json(System.String user, System.String token, System.String hotelgroupcode)
        {
            var result = service.Getuserlist_hotelgroup_json(user, token, hotelgroupcode); return (result);
        }


        [HttpPost]
        public string setticketsn(System.String ticketsn, System.String key)
        {
            var result = service.setticketsn(ticketsn, key); return (result);
        }


        [HttpPost]
        public string getticketsn(System.String ticketsn, System.String key)
        {
            var result = service.getticketsn(ticketsn, key); return (result);
        }


        [HttpPost]
        public string userlogin(System.String user, System.String token, System.String pwd, System.String hotelgroupcode)
        {
            var result = service.userlogin(user, token, pwd, hotelgroupcode); return (result);
        }


        [HttpPost]
        public string Setticket_fp_json(System.String fpcode, System.String taitou, System.String content, System.String jine, System.String notes, System.String bosscard, System.String useraccount, System.String oid, System.String hotelcode)
        {
            var result = service.Setticket_fp_json(fpcode, taitou, content, jine, notes, bosscard, useraccount, oid, hotelcode); return (result);
        }


        [HttpPost]
        public string Getticket_fp_json(System.String bosscard, System.String oid, System.String hotelcode)
        {
            var result = service.Getticket_fp_json(bosscard, oid, hotelcode); return (result);
        }


        [HttpPost]
        public string Getticket_jl_json(System.String user, System.String token, System.String mode, System.String code, System.String hotelcode)
        {
            var result = service.Getticket_jl_json(user, token, mode, code, hotelcode); return (result);
        }


        [HttpPost]
        public string Rethotelgroup_json(System.String user, System.String token, System.String hotelgroupname, System.String hotelgroupcode)
        {
            var result = service.Rethotelgroup_json(user, token, hotelgroupname, hotelgroupcode); return (result);
        }


        [HttpPost]
        public string Stopornostophotelgroup_json(System.String user, System.String token, System.String hotelgroupcode, System.String flag)
        {
            var result = service.Stopornostophotelgroup_json(user, token, hotelgroupcode, flag); return (result);
        }


        [HttpPost]
        public string Gethotelgroup_json(System.String user, System.String token, System.String hotelgroupcode)
        {
            var result = service.Gethotelgroup_json(user, token, hotelgroupcode); return (result);
        }


        [HttpPost]
        public string Setsystem_num_json(System.String user, System.String token, System.String hotelgroupcode, System.String systemcode, System.String num)
        {
            var result = service.Setsystem_num_json(user, token, hotelgroupcode, systemcode, num); return (result);
        }


        [HttpPost]
        public string Delsystem_num_json(System.String user, System.String token, System.String hotelgroupcode)
        {
            var result = service.Delsystem_num_json(user, token, hotelgroupcode); return (result);
        }


        [HttpPost]
        public string Getsystem_num_json(System.String user, System.String token, System.String hotelgroupcode)
        {
            var result = service.Getsystem_num_json(user, token, hotelgroupcode); return (result);
        }


        [HttpPost]
        public string Setsystem_json(System.String user, System.String token, System.String systemname)
        {
            var result = service.Setsystem_json(user, token, systemname); return (result);
        }


        [HttpPost]
        public string Stopornostopsystem_json(System.String user, System.String token, System.String systemcode, System.String flag)
        {
            var result = service.Stopornostopsystem_json(user, token, systemcode, flag); return (result);
        }


        [HttpPost]
        public string Retsystem_json(System.String user, System.String token, System.String systemname, System.String systemcode)
        {
            var result = service.Retsystem_json(user, token, systemname, systemcode); return (result);
        }


        [HttpPost]
        public string Getsystem_json(System.String user, System.String token)
        {
            var result = service.Getsystem_json(user, token); return (result);
        }


        [HttpPost]
        public string Setmodule_json(System.String user, System.String token, System.String systemcode, System.String modulename)
        {
            var result = service.Setmodule_json(user, token, systemcode, modulename); return (result);
        }


        [HttpPost]
        public string Stopornostopmodule_json(System.String user, System.String token, System.String systemcode, System.String modulecode, System.String flag)
        {
            var result = service.Stopornostopmodule_json(user, token, systemcode, modulecode, flag); return (result);
        }


        [HttpPost]
        public string Retmodule_json(System.String user, System.String token, System.String systemcode, System.String modulename, System.String modulecode)
        {
            var result = service.Retmodule_json(user, token, systemcode, modulename, modulecode); return (result);
        }


        [HttpPost]
        public string Getmodule_json(System.String user, System.String token, System.String systemcode)
        {
            var result = service.Getmodule_json(user, token, systemcode); return (result);
        }


        [HttpPost]
        public string Setpage_json(System.String user, System.String token, System.String systemcode, System.String modulecode, System.String pagename, System.String filename)
        {
            var result = service.Setpage_json(user, token, systemcode, modulecode, pagename, filename); return (result);
        }


        [HttpPost]
        public string Stopornostoppage_json(System.String user, System.String token, System.String pagecode, System.String flag)
        {
            var result = service.Stopornostoppage_json(user, token, pagecode, flag); return (result);
        }


        [HttpPost]
        public string Retpage_json(System.String user, System.String token, System.String systemcode, System.String modulecode, System.String pagename, System.String filename, System.String pagecode)
        {
            var result = service.Retpage_json(user, token, systemcode, modulecode, pagename, filename, pagecode); return (result);
        }


        [HttpPost]
        public string Getpage_json(System.String user, System.String token, System.String systemcode, System.String modulecode)
        {
            var result = service.Getpage_json(user, token, systemcode, modulecode); return (result);
        }


        [HttpPost]
        public string Getpage_all_json(System.String user, System.String token, System.String systemcode, System.String hotelcode)
        {
            var result = service.Getpage_all_json(user, token, systemcode, hotelcode); return (result);
        }


        [HttpPost]
        public string Gethotelcodefw_json(System.String user, System.String token, System.String categoryid, System.String hotelcode)
        {
            var result = service.Gethotelcodefw_json(user, token, categoryid, hotelcode); return (result);
        }


        [HttpPost]
        public string Queryticketall_bychannel_json(System.String user, System.String token, System.String hotelGroupId, System.String hotelcode, System.String mode, System.String code)
        {
            var result = service.Queryticketall_bychannel_json(user, token, hotelGroupId, hotelcode, mode, code); return (result);
        }


        [HttpPost]
        public string Changetickettmoney(System.String user, System.String token, System.String ticketsn, System.Decimal tmoney, System.Decimal kpmoney, System.String oid, System.String hotelcode)
        {
            var result = service.Changetickettmoney(user, token, ticketsn, tmoney, kpmoney, oid, hotelcode); return (result);
        }


        [HttpPost]
        public string Changetpstate(System.String user, System.String token, System.String tp_id, System.String stateid, System.String oid, System.String hotelcode)
        {
            var result = service.Changetpstate(user, token, tp_id, stateid, oid, hotelcode); return (result);
        }


        [HttpPost]
        public string Getbase_json(System.String user, System.String token, System.String mode, System.String code, System.String hotelcode)
        {
            var result = service.Getbase_json(user, token, mode, code, hotelcode); return (result);
        }


        [HttpPost]
        public string Setrole_system_json(System.String user, System.String token, System.String hotelcode, System.String rolecode, System.String systemcode, System.String hotelgroupcode, System.String rightinfo)
        {
            var result = service.Setrole_system_json(user, token, hotelcode, rolecode, systemcode, hotelgroupcode, rightinfo); return (result);
        }


        [HttpPost]
        public string Retrole_system_json(System.String user, System.String token, System.String hotelcode, System.String rolecode, System.String systemcode, System.String hotelgroupcode, System.String rightinfo)
        {
            var result = service.Retrole_system_json(user, token, hotelcode, rolecode, systemcode, hotelgroupcode, rightinfo); return (result);
        }


        [HttpPost]
        public string Getrole_system_json(System.String user, System.String token, System.String hotelcode, System.String rolecode, System.String systemcode, System.String hotelgroupcode)
        {
            var result = service.Getrole_system_json(user, token, hotelcode, rolecode, systemcode, hotelgroupcode); return (result);
        }


        [HttpPost]
        public string Setrole_json(System.String user, System.String token, System.String hotelcode, System.String rolecode, System.String roletname, System.String hotelgroupcode)
        {
            var result = service.Setrole_json(user, token, hotelcode, rolecode, roletname, hotelgroupcode); return (result);
        }


        [HttpPost]
        public string Getrole_json(System.String user, System.String token, System.String hotelcode)
        {
            var result = service.Getrole_json(user, token, hotelcode); return (result);
        }


        [HttpPost]
        public string Stopornostoprole_json(System.String user, System.String token, System.String rolecode, System.String flag)
        {
            var result = service.Stopornostoprole_json(user, token, rolecode, flag); return (result);
        }


        [HttpPost]
        public string Retrole_json(System.String user, System.String token, System.String hotelcode, System.String rolecode, System.String rolename)
        {
            var result = service.Retrole_json(user, token, hotelcode, rolecode, rolename); return (result);
        }


        [HttpPost]
        public string Setdept_json(System.String user, System.String token, System.String hotelcode, System.String deptcode, System.String deptname, System.String hotelgroupcode, System.String pxid)
        {
            var result = service.Setdept_json(user, token, hotelcode, deptcode, deptname, hotelgroupcode, pxid); return (result);
        }


        [HttpPost]
        public string Getdept_json(System.String user, System.String token, System.String hotelcode)
        {
            var result = service.Getdept_json(user, token, hotelcode); return (result);
        }


        [HttpPost]
        public string Stopornostopdept_json(System.String user, System.String token, System.String deptcode, System.String flag)
        {
            var result = service.Stopornostopdept_json(user, token, deptcode, flag); return (result);
        }


        [HttpPost]
        public string Retdept_json(System.String user, System.String token, System.String hotelcode, System.String deptcode, System.String deptname)
        {
            var result = service.Retdept_json(user, token, hotelcode, deptcode, deptname); return (result);
        }


        [HttpPost]
        public string Sethotet_json(System.String user, System.String token, System.String hotelcode, System.String hotelname, System.String hotelgroupcode, System.String pxid)
        {
            var result = service.Sethotet_json(user, token, hotelcode, hotelname, hotelgroupcode, pxid); return (result);
        }


        [HttpPost]
        public string Stopornostophotet_json(System.String user, System.String token, System.String hotelcode, System.String flag)
        {
            var result = service.Stopornostophotet_json(user, token, hotelcode, flag); return (result);
        }


        [HttpPost]
        public string Rethotet_json(System.String user, System.String token, System.String hotelcode, System.String hotelname, System.String pxid)
        {
            var result = service.Rethotet_json(user, token, hotelcode, hotelname, pxid); return (result);
        }


        [HttpPost]
        public string Gethotel_json(System.String user, System.String token, System.String hotelgroupcode)
        {
            var result = service.Gethotel_json(user, token, hotelgroupcode); return (result);
        }


        [HttpPost]
        public string Gethotel_byhotelname_json(System.String user, System.String token, System.String hotelname)
        {
            var result = service.Gethotel_byhotelname_json(user, token, hotelname); return (result);
        }


        [HttpPost]
        public string Sethotelgroup_json(System.String user, System.String token, System.String hotelgroupname, System.String hotelgroupcode)
        {
            var result = service.Sethotelgroup_json(user, token, hotelgroupname, hotelgroupcode); return (result);
        }


        [HttpPost]
        public string SetFormula_xg_json(System.String user, System.String token, System.String hotelcode, System.String formulaid, System.String num)
        {
            var result = service.SetFormula_xg_json(user, token, hotelcode, formulaid, num); return (result);
        }


        [HttpPost]
        public string Get_formula_xg_json(System.String user, System.String token, System.String hotelcode, System.String mobile, System.String formulaid, System.String num)
        {
            var result = service.Get_formula_xg_json(user, token, hotelcode, mobile, formulaid, num); return (result);
        }


        [HttpPost]
        public string Getcategory_yxq_json(System.String user, System.String token, System.String categoryid, System.String hotelcode)
        {
            var result = service.Getcategory_yxq_json(user, token, categoryid, hotelcode); return (result);
        }


        [HttpPost]
        public string Smssend_kk(System.String content, System.String mobile, System.String user, System.String passwd, System.String usercode, System.String smstype, System.String tmoney, System.String card, System.String hotelcode)
        {
            var result = service.Smssend_kk(content, mobile, user, passwd, usercode, smstype, tmoney, card, hotelcode); return (result);
        }


        [HttpPost]
        public string Sethonor_link_formula_json(System.String user, System.String token, System.String hotelcode, System.String formulaid, System.String formulaname, System.String honorid, System.String honorname, System.String oid)
        {
            var result = service.Sethonor_link_formula_json(user, token, hotelcode, formulaid, formulaname, honorid, honorname, oid); return (result);
        }


        [HttpPost]
        public string Rethonor_link_formula_json(System.String user, System.String token, System.String hotelcode, System.String formulaid, System.String honorid, System.String oid)
        {
            var result = service.Rethonor_link_formula_json(user, token, hotelcode, formulaid, honorid, oid); return (result);
        }


        [HttpPost]
        public string Gethonor_json(System.String user, System.String token, System.String honorid, System.String hotelcode)
        {
            var result = service.Gethonor_json(user, token, honorid, hotelcode); return (result);
        }


        [HttpPost]
        public string Getticketsn_gq_byhotelcode_json(System.String user, System.String token, System.String hotelcode, System.String starttime, System.String endtime, System.String bosscard)
        {
            var result = service.Getticketsn_gq_byhotelcode_json(user, token, hotelcode, starttime, endtime, bosscard); return (result);
        }


        [HttpPost]
        public string Getbosscard_tjxf_byhotelcode_json(System.String user, System.String token, System.String hotelcode, System.String starttime, System.String endtime, System.String num)
        {
            var result = service.Getbosscard_tjxf_byhotelcode_json(user, token, hotelcode, starttime, endtime, num); return (result);
        }


        [HttpPost]
        public string Getbosscard_tjcz_byhotelcode_json(System.String user, System.String token, System.String hotelcode, System.String starttime, System.String endtime, System.String num, System.String formulaid)
        {
            var result = service.Getbosscard_tjcz_byhotelcode_json(user, token, hotelcode, starttime, endtime, num, formulaid); return (result);
        }


        [HttpPost]
        public string Getproduct_xg_json(System.String user, System.String token, System.String formulaid, System.String hotelcode)
        {
            var result = service.Getproduct_xg_json(user, token, formulaid, hotelcode); return (result);
        }


        [HttpPost]
        public string Getticket_jl_byxshotelcode_json(System.String user, System.String token, System.String hotelcode, System.String starttime, System.String endtime)
        {
            var result = service.Getticket_jl_byxshotelcode_json(user, token, hotelcode, starttime, endtime); return (result);
        }


        [HttpPost]
        public string Getticket_hxjl_bycategory_json(System.String user, System.String token, System.String hotelcode, System.String starttime, System.String endtime)
        {
            var result = service.Getticket_hxjl_bycategory_json(user, token, hotelcode, starttime, endtime); return (result);
        }


        [HttpPost]
        public string Queryticket_json(System.String user, System.String token, System.String hotelGroupId, System.String hotelcode, System.String mode, System.String code)
        {
            var result = service.Queryticket_json(user, token, hotelGroupId, hotelcode, mode, code); return (result);
        }


        [HttpPost]
        public string Queryticket_wxly_json(System.String user, System.String token, System.String hotelcode, System.String starttime, System.String endtime)
        {
            var result = service.Queryticket_wxly_json(user, token, hotelcode, starttime, endtime); return (result);
        }


        [HttpPost]
        public string Queryticketnew_json(System.String user, System.String token, System.String hotelGroupId, System.String hotelcode, System.String mode, System.String code)
        {
            var result = service.Queryticketnew_json(user, token, hotelGroupId, hotelcode, mode, code); return (result);
        }


        [HttpPost]
        public string Queryticket_fw_json(System.String user, System.String token, System.String hotelGroupId, System.String hotelcode, System.String mode, System.String code)
        {
            var result = service.Queryticket_fw_json(user, token, hotelGroupId, hotelcode, mode, code); return (result);
        }


        [HttpPost]
        public string Queryticketall_json(System.String user, System.String token, System.String hotelGroupId, System.String hotelcode, System.String mode, System.String code)
        {
            var result = service.Queryticketall_json(user, token, hotelGroupId, hotelcode, mode, code); return (result);
        }


        [HttpPost]
        public string Ret_Category_bgimg_json(System.String user, System.String token, System.String hotelcode, System.String Categoryid, System.String img)
        {
            var result = service.Ret_Category_bgimg_json(user, token, hotelcode, Categoryid, img); return (result);
        }


        [HttpPost]
        public string Ret_Category_istest_json(System.String user, System.String token, System.String hotelcode, System.String Categoryid, System.String istest)
        {
            var result = service.Ret_Category_istest_json(user, token, hotelcode, Categoryid, istest); return (result);
        }


        [HttpPost]
        public string Getmember_useinfo_json(System.String user, System.String token, System.String hotelcode, System.String sflag, System.String starttime, System.String endtime)
        {
            var result = service.Getmember_useinfo_json(user, token, hotelcode, sflag, starttime, endtime); return (result);
        }


        [HttpPost]
        public string GetFormula_info_json(System.String user, System.String token, System.String hotelcode, System.String sflag)
        {
            var result = service.GetFormula_info_json(user, token, hotelcode, sflag); return (result);
        }


        [HttpPost]
        public string GetFormula_info_byformulaid_json(System.String user, System.String token, System.String hotelcode, System.String formulaid)
        {
            var result = service.GetFormula_info_byformulaid_json(user, token, hotelcode, formulaid); return (result);
        }


        [HttpPost]
        public string Retticketsn_jl_error_json(System.String user, System.String token, System.String hotelcode, System.String id)
        {
            var result = service.Retticketsn_jl_error_json(user, token, hotelcode, id); return (result);
        }


        [HttpPost]
        public string Retticketsn_tmoney_json(System.String user, System.String token, System.String hotelcode, System.String ticketsn, System.String tmoney)
        {
            var result = service.Retticketsn_tmoney_json(user, token, hotelcode, ticketsn, tmoney); return (result);
        }


        [HttpPost]
        public string Getticketsn_tmoney_json(System.String user, System.String token, System.String hotelcode, System.String ticketsn)
        {
            var result = service.Getticketsn_tmoney_json(user, token, hotelcode, ticketsn); return (result);
        }


        [HttpPost]
        public string Getticket_zzjl_json(System.String user, System.String token, System.String hotelcode, System.String type, System.String mobile)
        {
            var result = service.Getticket_zzjl_json(user, token, hotelcode, type, mobile); return (result);
        }


        [HttpPost]
        public string set_ticketsn_wxly_json(System.String user, System.String token, System.String ticketsn, System.String openid, System.String bosscard, System.String hotelcode)
        {
            var result = service.set_ticketsn_wxly_json(user, token, ticketsn, openid, bosscard, hotelcode); return (result);
        }


        [HttpPost]
        public string getzzjy(System.String ticketsn, System.String hotelcode)
        {
            var result = service.getzzjy(ticketsn, hotelcode); return (result);
        }


        [HttpPost]
        public string Queryticket_nowx_json(System.String user, System.String token, System.String hotelGroupId, System.String hotelcode, System.String mode, System.String code)
        {
            var result = service.Queryticket_nowx_json(user, token, hotelGroupId, hotelcode, mode, code); return (result);
        }


        [HttpPost]
        public string Queryticket_nowx_bychannel_json(System.String user, System.String token, System.String hotelGroupId, System.String hotelcode, System.String mode, System.String code, System.String channel)
        {
            var result = service.Queryticket_nowx_bychannel_json(user, token, hotelGroupId, hotelcode, mode, code, channel); return (result);
        }


        [HttpPost]
        public string Queryticketnew_nowx_json(System.String user, System.String token, System.String hotelGroupId, System.String hotelcode, System.String mode, System.String code)
        {
            var result = service.Queryticketnew_nowx_json(user, token, hotelGroupId, hotelcode, mode, code); return (result);
        }


        [HttpPost]
        public string Queryticketnew_nowx_bdly_json(System.String user, System.String token, System.String hotelGroupId, System.String hotelcode, System.String mode, System.String code)
        {
            var result = service.Queryticketnew_nowx_bdly_json(user, token, hotelGroupId, hotelcode, mode, code); return (result);
        }


        [HttpPost]
        public string Queryticketnew_nowx_bdly_fw_json(System.String user, System.String token, System.String hotelGroupId, System.String hotelcode, System.String xfhotelcode, System.String mode, System.String code)
        {
            var result = service.Queryticketnew_nowx_bdly_fw_json(user, token, hotelGroupId, hotelcode, xfhotelcode, mode, code); return (result);
        }


        [HttpPost]
        public string Getcategory_single_byformulaid_json(System.String user, System.String token, System.String formulaid, System.String hotelcode)
        {
            var result = service.Getcategory_single_byformulaid_json(user, token, formulaid, hotelcode); return (result);
        }


        [HttpPost]
        public string Getmember_all_byhotelcode_json(System.String user, System.String token, System.String hotelcode)
        {
            var result = service.Getmember_all_byhotelcode_json(user, token, hotelcode); return (result);
        }


        [HttpPost]
        public string Getlog_json(System.String user, System.String token, System.String hotelcode, System.String starttime, System.String endtime, System.String oid, System.String type)
        {
            var result = service.Getlog_json(user, token, hotelcode, starttime, endtime, oid, type); return (result);
        }


        [HttpPost]
        public string StartFormula_t(System.String user, System.String token, System.String FormulaId, System.String hotelcode)
        {
            var result = service.StartFormula_t(user, token, FormulaId, hotelcode); return (result);
        }


        [HttpPost]
        public string Startcategory_t(System.String user, System.String token, System.String CategoryId, System.String hotelcode)
        {
            var result = service.Startcategory_t(user, token, CategoryId, hotelcode); return (result);
        }


        [HttpPost]
        public string getalltpnum_json(System.String user, System.String token, System.String hotelcode)
        {
            var result = service.getalltpnum_json(user, token, hotelcode); return (result);
        }


        [HttpPost]
        public string Getticketsn_details_byhotelcode_json(System.String user, System.String token, System.String hotelcode, System.String starttime, System.String endtime, System.String categoryname)
        {
            var result = service.Getticketsn_details_byhotelcode_json(user, token, hotelcode, starttime, endtime, categoryname); return (result);
        }


        [HttpPost]
        public string Gethotelsystem_new_json(System.String user, System.String token, System.String hotelcode)
        {
            var result = service.Gethotelsystem_new_json(user, token, hotelcode); return (result);
        }


        [HttpPost]
        public string Gethotelsystem_new1_json(System.String user, System.String token, System.String hotelcode)
        {
            var result = service.Gethotelsystem_new1_json(user, token, hotelcode); return (result);
        }


        [HttpPost]
        public string ticket_ysq_online_json(System.String hotelGroupCode, System.String crsNo, System.String money, System.String remark, System.String formulaid, System.String tpid, System.String ticketsn, System.String categoryid, System.String no, System.String code)
        {
            var result = service.ticket_ysq_online_json(hotelGroupCode, crsNo, money, remark, formulaid, tpid, ticketsn, categoryid, no, code); return (result);
        }


        [HttpPost]
        public string Getticketsn_day_use_byhotelcode_json(System.String user, System.String token, System.String hotelcode, System.String starttime, System.String endtime)
        {
            var result = service.Getticketsn_day_use_byhotelcode_json(user, token, hotelcode, starttime, endtime); return (result);
        }


        [HttpPost]
        public string Getticketsn_ye_byhotelcode_json(System.String user, System.String token, System.String hotelcode, System.String FormulaId)
        {
            var result = service.Getticketsn_ye_byhotelcode_json(user, token, hotelcode, FormulaId); return (result);
        }


        [HttpPost]
        public string Getticketsn_everyday_byhotelcode_json(System.String user, System.String token, System.String hotelcode, System.String time)
        {
            var result = service.Getticketsn_everyday_byhotelcode_json(user, token, hotelcode, time); return (result);
        }


        [HttpPost]
        public string Setticketsn_everyday_byhotelcode_json(System.String user, System.String token, System.String hotelcode, System.String starttime, System.String endtime)
        {
            var result = service.Setticketsn_everyday_byhotelcode_json(user, token, hotelcode, starttime, endtime); return (result);
        }


        [HttpPost]
        public string Delticketsn_everyday_byhotelcode_json(System.String user, System.String token, System.String hotelcode, System.String starttime, System.String endtime)
        {
            var result = service.Delticketsn_everyday_byhotelcode_json(user, token, hotelcode, starttime, endtime); return (result);
        }


        [HttpPost]
        public string Getformulaid_byhotelcode_json(System.String user, System.String token, System.String hotelcode)
        {
            var result = service.Getformulaid_byhotelcode_json(user, token, hotelcode); return (result);
        }


        [HttpPost]
        public string Getformulaid_bychannel_json(System.String user, System.String token, System.String hotelcode, System.String channel)
        {
            var result = service.Getformulaid_bychannel_json(user, token, hotelcode, channel); return (result);
        }


        [HttpPost]
        public string Set_newhy_json(System.String user, System.String token, System.String hotelcode, System.String FormulaId, System.String mobile)
        {
            var result = service.Set_newhy_json(user, token, hotelcode, FormulaId, mobile); return (result);
        }


        [HttpPost]
        public string Set_newhy_new_json(System.String user, System.String token, System.String hotelcode, System.String FormulaId, System.String mobile, System.String salehotelcode)
        {
            var result = service.Set_newhy_new_json(user, token, hotelcode, FormulaId, mobile, salehotelcode); return (result);
        }


        [HttpPost]
        public string Ret_Formula_img_json(System.String user, System.String token, System.String hotelcode, System.String Formulaid, System.String img)
        {
            var result = service.Ret_Formula_img_json(user, token, hotelcode, Formulaid, img); return (result);
        }


        [HttpPost]
        public string Ret_Category_img_json(System.String user, System.String token, System.String hotelcode, System.String Categoryid, System.String img)
        {
            var result = service.Ret_Category_img_json(user, token, hotelcode, Categoryid, img); return (result);
        }
    }
}
