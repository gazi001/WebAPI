using KS.DataBase;
using KS.Model.Ticket.TicketRequest;
using KS.Model.Ticket.TicketResponse;
using Newtonsoft.Json;
using RM.Common.DotNetModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Ticket.SDK.AdvancedAPIs
{
   public class LeagueTicketApi
    {
       private static net.ksweixin.Service service = new net.ksweixin.Service();
       private static iticketDB iticketdb = DbContextFactory.Create(DBName.ITicketDB) as iticketDB;
        public static JsonReturn CreateFormula(CreateFormulaData data)
        {
            JsonReturn jsonResult = new JsonReturn();
            
            if (data != null)
            {
                try
                {
                    //var sflg = GetSflag(data.type);
                    if (data.arr != null && data.arr.Count > 0)
                    {
                        var pro_num =TicketApi.GetProNum(data.type, data.pro_num);

                        //调用webservice接口定义产品包，返回产品包id
                        //TODO:   Formulacode, fmoney,fnum, 字段没有
                        var formulaid = service.SetFormula_json("", "", data.pro_name, data.summary, "00", data.price, pro_num, data.hotelcode,data.pro_arr);
                        if (formulaid != "")
                        {
                            var formulaint = int.Parse(formulaid);
                            //本地关系型数据库存储产品信息
                            var formula = new Formula_t
                            {
                                fmoney = decimal.Parse(data.price),
                                Formulacode = "",
                                FormulaId = int.Parse(formulaid),
                                FormulaName = data.pro_name,
                                FormulaSummary = data.summary,
                                hotelcode = data.hotelcode,
                                //maxnum=int.Parse( data.pro_num),
                                flag = 0,
                            };
                            iticketdb.Formula_t.Add(formula);
                            iticketdb.SaveChanges();

                            //TODO:redis缓存记录产品信息
                            var setformulafw = service.SetFormula_fw_pl_json("", "", formulaid, data.hotelcode_fw, data.hotelcode,data.pro_arr);
                            var rangArr = data.pro_arr.Split(',');
                            foreach (var item1 in rangArr)
                            {
                                if (!iticketdb.Formula_fw_t.Any(x => x.hotelcode == data.hotelcode && x.FormulaId == formulaint))
                                {
                                    iticketdb.Formula_fw_t.Add(new Formula_fw_t
                                    {
                                        FormulaId = formulaint,
                                        hotelcode = data.hotelcode,
                                        usehotelcode = item1.ToString(),
                                    });
                                }
                            }
                            iticketdb.SaveChanges();
                            //TODO:打包产品
                            var dbstr = service.Setdbname_json("", "", data.pro_name, formulaid, data.hotelcode,data.pro_arr).Replace("(", "").Replace(")", "");
                            if (dbstr != null)
                            {
                                var dbobj = JsonConvert.DeserializeObject<List<SetdbnameResult>>(dbstr)[0];
                                if (dbobj.returncode == "success")
                                {
                                    iticketdb.dbname_t.Add(new dbname_t
                                    {
                                        dbname = data.pro_name,
                                        formulaid = formulaid,
                                        hotelcode = data.hotelcode,
                                    });
                                    iticketdb.SaveChanges();
                                    if (CreateCategory(data.arr, data, dbobj))
                                    {

                                        jsonResult.code = ApiCode.成功;
                                        jsonResult.msg = "成功";
                                    }
                                    else
                                    {
                                        jsonResult.code = ApiCode.子券定义失败;
                                        jsonResult.msg = "子券定义失败";
                                    }

                                }

                            }

                        }
                        else
                        {
                            jsonResult.code = ApiCode.产品定义失败;
                            jsonResult.msg = "产品定义失败";

                        }
                    }
                    else
                    {
                        jsonResult.code = ApiCode.参数不全;
                        jsonResult.msg = "接口调用失败";
                    }
                }
                catch (Exception ex)
                {

                    jsonResult.code = ApiCode.接口调用失败;
                    jsonResult.msg = ex.ToString();
                }
            }
            return jsonResult;
         
        }
        public static bool CreateCategory(List<CreateCategoryData> CreateCategoryData, CreateFormulaData CreateFormulaData, SetdbnameResult dbname)
        {
            var isSet = true;
            try
            {
                for (int i = 0; i < CreateCategoryData.Count; i++)
                {
                    if (CreateCategoryData[i].cate_name != null)
                    {
                        var sflg =TicketApi. GetSflag(CreateFormulaData.type, CreateCategoryData[i].cate_xz, CreateCategoryData[i].cate_type);
                        var iscz =TicketApi. GetIscz(sflg);
                        if (sflg == "4" || sflg == "5")
                        {
                            var isbool = TicketApi.GetIsBool(sflg, CreateCategoryData[i].isbool);
                            var Categoryid = service.Setcategory_cz_json
    ("", "", CreateCategoryData[i].cate_name, CreateCategoryData[i].cate_code, CreateCategoryData[i].cate_summary, CreateFormulaData.hotelcode, "", "00", "00", "0", CreateCategoryData[i].cate_type, sflg, CreateCategoryData[i].cate_start_date.ToString(), (CreateCategoryData[i].cate_start_date + CreateCategoryData[i].cate_end_date).ToString(), "00", "1", isbool, "0", "1", CreateCategoryData[i].cate_bm, CreateCategoryData[i].cate_price, "0", CreateCategoryData[i].iszs, iscz, "", CreateFormulaData.pro_arr);
                            if (Categoryid != "")
                            {
                                SaveCategory(CreateCategoryData, CreateFormulaData, dbname, i, sflg, isbool, Categoryid, iscz);
                            }
                            else
                            {
                                isSet = false;
                            }
                        }
                        else
                        {
                            var isbool =TicketApi. GetIsBool(sflg, CreateCategoryData[i].isbool);
                            var Categoryid = service.Setcategory_json("", "", CreateCategoryData[i].cate_name, CreateCategoryData[i].cate_code, CreateCategoryData[i].cate_summary, CreateFormulaData.hotelcode, "", "00", "00", "0", CreateCategoryData[i].cate_type, sflg, CreateCategoryData[i].cate_start_date.ToString(), (CreateCategoryData[i].cate_start_date + CreateCategoryData[i].cate_end_date).ToString(), "00", "1", isbool, "0", "1", CreateCategoryData[i].cate_bm, CreateCategoryData[i].cate_price == "" ? "9999" : CreateCategoryData[i].cate_price, "0", CreateCategoryData[i].iszs, iscz, CreateFormulaData.pro_arr);
                            if (Categoryid != "")
                            {
                                SaveCategory(CreateCategoryData, CreateFormulaData, dbname, i, sflg, isbool, Categoryid, iscz);
                            }
                            else
                            {
                                isSet = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isSet = false;

            }
            return isSet;

        }
        private static void SaveCategory(List<CreateCategoryData> CreateCategoryData, CreateFormulaData CreateFormulaData, SetdbnameResult dbname, int i, string sflg, string isbool, string Categoryid, string iscz)
        {
            var dbnameid = service.Setdbdata_json("", "", Categoryid, CreateFormulaData.data[i], dbname.id, CreateFormulaData.hotelcode, CreateFormulaData.pro_arr);
            if (dbnameid.IndexOf("success") > -1)
            {
                var cateint = int.Parse(Categoryid);
                iticketdb.dbdata_t.Add(new dbdata_t
                {
                    CategoryId = cateint,
                    dbnameid = int.Parse(dbname.id),
                    hotelcode = CreateFormulaData.hotelcode,
                    num = int.Parse(CreateFormulaData.data[i]),
                    oid = CreateFormulaData.usercode,
                });
                iticketdb.Category_t.Add(new Category_t
                {
                    addtime = DateTime.Now,
                    BeginString = "00",
                    bgpic = "",
                    Categorycode = CreateCategoryData[i].cate_code,
                    CategoryId = cateint,
                    CategoryName = CreateCategoryData[i].cate_name,
                    CategoryNamebm = CreateCategoryData[i].cate_bm,
                    dzpq = "1",
                    endnum = int.Parse("00"),
                    EndString = "00",
                    ExpireDate = CreateCategoryData[i].cate_start_date.ToString(),
                    ExpireDateend = (CreateCategoryData[i].cate_start_date + CreateCategoryData[i].cate_end_date).ToString(),
                    flag = 0,
                    fmoney = CreateCategoryData[i].cate_price == "" ? decimal.Parse("9999") : decimal.Parse(CreateCategoryData[i].cate_price),
                    fnum = 0,
                    hotelcode = CreateFormulaData.hotelcode,
                    HotelId = CreateFormulaData.hotelcode,
                    isbool = int.Parse(isbool),//未确定
                    iscz = int.Parse(iscz),
                    istest = 0,
                    iswxly = 0,
                    iszs = CreateCategoryData[i].iszs == null ? 1 : int.Parse(CreateCategoryData[i].iszs),
                    maxNum = int.Parse(CreateFormulaData.pro_num == "" ? "999999" : CreateFormulaData.pro_num),
                    moneytype = 0,
                    pic = "",
                    rate = 1,
                    sflag = int.Parse(sflg),
                    Summary = CreateCategoryData[i].cate_summary,
                    type = 0,
                });
                var setcatefw = service.Setcategory_fw_pl_json("", "", Categoryid, CreateFormulaData.hotelcode_fw, CreateFormulaData.pro_arr);
                var rangArr = CreateFormulaData.hotelcode_fw.Split(',');
                foreach (var item1 in rangArr)
                {
                    if (item1.ToString() != "")
                    {
                        if (!iticketdb.Category_fw_t.Any(x => x.hotelcode == CreateFormulaData.hotelcode && x.CategoryId == cateint))
                        {
                            iticketdb.Category_fw_t.Add(new Category_fw_t
                            {
                                CategoryId = cateint,
                                hotelcode = CreateFormulaData.hotelcode,
                                usehotelcode = item1.ToString(),
                                addtime = DateTime.Now,

                            });
                        }
                    }
                }
                iticketdb.SaveChanges();
            }
        }
        public static JsonReturn UpdateFormula(UpdateFormulaData data)
        {
            var res = new JsonReturn() { code = ApiCode.成功, msg = "成功" };
            var retformula = service.RetFormula_json(data.usercode, "", data.pro_name, data.summary == "" ? " " : data.summary, data.id, "00", data.price == "" ? "0" : data.price, data.pro_num == "" ? "999999" : data.pro_num, data.hotelcode,data.pro_arr);
            if (retformula.IndexOf("success") > -1)
            {
                var delformulafw = service.DelFormula_fw_json("", "", data.id, data.hotelcode,data.pro_arr);
                if (delformulafw.IndexOf("success") > -1)
                {
                    var SetFormula_fw = service.SetFormula_fw_pl_json("", "", data.id, data.pro_arr, data.hotelcode, data.pro_arr);

                    int sum = 0;
                    if (data.arr != null && data.arr.Count > 0)
                    {
                        var dbstr = service.Setdbname_json("", "", data.pro_name, data.id, data.hotelcode, data.pro_arr).Replace("(", "").Replace(")", "");
                        for (int i = 0; i < data.arr.Count; i++)
                        {
                            var sflg =TicketApi. GetSflag(data.type, data.arr[i].cate_xz, data.arr[i].cate_type);
                            var iscz =TicketApi. GetIscz(sflg);
                            if (sflg == "4" || sflg == "5")
                            {
                                var isbool = TicketApi.GetIsBool(sflg, data.arr[i].isbool);
                                var Categoryid = service.Setcategory_cz_json
        ("", "", data.arr[i].cate_name, data.arr[i].cate_code, data.arr[i].cate_summary, data.hotelcode, "", "00", "00", "0", data.arr[i].cate_type, sflg, data.arr[i].cate_start_date.ToString(), (data.arr[i].cate_start_date + data.arr[i].cate_end_date).ToString(), "00", "1", isbool, "0", "1", data.arr[i].cate_bm, data.arr[i].cate_price, "0", data.arr[i].iszs, iscz, "",data.pro_arr);
                                if (Categoryid != "")
                                {
                                    SaveCatogory(data.arr, data.hotelcode, data.data_new[i], data.usercode, data.pro_num, data.yformul.dbid, i, sflg, isbool, Categoryid, iscz,data.pro_arr,data.hotelcode_fw);
                                }
                                else
                                {
                                    sum++;
                                }
                            }
                            else
                            {
                                var isbool = TicketApi.GetIsBool(sflg, data.arr[i].isbool);
                                var Categoryid = service.Setcategory_json("", "", data.arr[i].cate_name, data.arr[i].cate_code, data.arr[i].cate_summary, data.hotelcode, "", "00", "00", "0", data.arr[i].cate_type, sflg, data.arr[i].cate_start_date.ToString(), (data.arr[i].cate_start_date + data.arr[i].cate_end_date).ToString(), "00", "1", isbool, "0", "1", data.arr[i].cate_bm, data.arr[i].cate_price, "0", data.arr[i].iszs, iscz,data.pro_arr);
                                if (Categoryid != "")
                                {
                                    SaveCatogory(data.arr, data.hotelcode, data.data_new[i], data.usercode, data.pro_num, data.yformul.dbid, i, sflg, isbool, Categoryid, iscz,data.pro_arr,data.hotelcode_fw);
                                }
                                else
                                {
                                    sum++;
                                }
                            }
                        }
                        if (sum > 0)
                        {
                            res.code = ApiCode.子券定义失败;
                            res.msg = "子券定义失败";
                        }
                    }
                }
                else
                {
                    res.code = ApiCode.删除产品范围失败;
                    res.msg = "删除产品范围失败";
                }
            }
            else
            {
                res.code = ApiCode.修改产品信息失败;
                res.msg = "修改产品信息失败";
            }
            return res;
        }
        private static void SaveCatogory(List<CreateCategoryData> CreateCategoryData, string hotelcode, string num, string usercode, string pro_num, string dbid, int i, string sflg, string isbool, string Categoryid, string iscz, string pro_arr, string hotelcode_fw)
        {
            var dbnameid = service.Setdbdata_json("", "", Categoryid, num, dbid, hotelcode, pro_arr);
            if (dbnameid.IndexOf("success") > -1)
            {
                var cateint = int.Parse(Categoryid);
                iticketdb.dbdata_t.Add(new dbdata_t
                {
                    CategoryId = cateint,
                    dbnameid = int.Parse(dbid),
                    hotelcode = hotelcode,
                    num = int.Parse(num),
                    oid = usercode,
                });
                iticketdb.Category_t.Add(new Category_t
                {
                    addtime = DateTime.Now,
                    BeginString = "00",
                    bgpic = "",
                    Categorycode = CreateCategoryData[i].cate_code,
                    CategoryId = cateint,
                    CategoryName = CreateCategoryData[i].cate_name,
                    CategoryNamebm = CreateCategoryData[i].cate_bm,
                    dzpq = "1",
                    endnum = int.Parse("00"),
                    EndString = "00",
                    ExpireDate = CreateCategoryData[i].cate_start_date.ToString(),
                    ExpireDateend = (CreateCategoryData[i].cate_start_date + CreateCategoryData[i].cate_end_date).ToString(),
                    flag = 0,
                    fmoney = CreateCategoryData[i].cate_price == null ? decimal.Parse("0") : decimal.Parse(CreateCategoryData[i].cate_price),
                    fnum = 0,
                    hotelcode = hotelcode,
                    HotelId = hotelcode,
                    isbool = int.Parse(isbool),//未确定
                    iscz = int.Parse(iscz),
                    istest = 0,
                    iswxly = 0,
                    iszs = CreateCategoryData[i].iszs == null ? 1 : int.Parse(CreateCategoryData[i].iszs),
                    maxNum = int.Parse(pro_num == "" ? "999999" : pro_num),
                    moneytype = 0,
                    pic = "",
                    rate = 1,
                    sflag = int.Parse(sflg),
                    Summary = CreateCategoryData[i].cate_summary,
                    type = 0,
                });
                var setcatefw = service.Setcategory_fw_pl_json("", "", Categoryid, hotelcode_fw, pro_arr);
                var rangArr = hotelcode_fw.Split(',');
                foreach (var item1 in rangArr)
                {
                    if (item1.ToString() != "")
                    {
                        if (!iticketdb.Category_fw_t.Any(x => x.hotelcode == hotelcode && x.CategoryId == cateint))
                        {
                            iticketdb.Category_fw_t.Add(new Category_fw_t
                            {
                                CategoryId = cateint,
                                hotelcode = hotelcode,
                                usehotelcode = item1.ToString(),
                                addtime = DateTime.Now,
                            });
                        }
                    }
                }
                iticketdb.SaveChanges();
            }
        }

       
        public static JsonReturn UpdateCategoryFw(UpdateCategoryFwModel data)
        {
            JsonReturn jsonResult = new JsonReturn();
            if (data.cate_arr != null)
            {
                var fwArr = data.cate_arr.Split(',');

                var list = iticketdb.Category_fw_t.Where(x => x.CategoryId == data.id && x.hotelcode == data.hotelcode).ToList();
                foreach (var item in list)
                {
                    iticketdb.Category_fw_t.Remove(item);
                }
                foreach (var item in fwArr)
                {
                    if (item != "")
                    {
                        iticketdb.Category_fw_t.Add(new Category_fw_t
                        {
                            CategoryId = data.id,
                            hotelcode = data.hotelcode,
                            usehotelcode = item.ToString(),
                            addtime = DateTime.Now,
                        });
                    }
                }
                iticketdb.SaveChanges();
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "成功";
            }
            else
            {
                jsonResult.code = ApiCode.参数不全;
                jsonResult.msg = "参数不全";
            }
            return jsonResult;
        }
    }
}
