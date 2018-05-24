using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
namespace RM.Common.DotNetImage
{
    public class ImageHelper
    {
        /// <summary>
        /// ���ɶ�ά�룬���ض�ά��·��
        /// </summary>
        /// <param name="url"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string CreateSceneQR(string url,string filename)
        {
            using (var ms = new MemoryStream()) {
                GetQRCode(url, ms);
                Image img = Image.FromStream(ms);
                string path = HttpContext.Current.Server.MapPath("~/QRcode/" + filename+".jpg");
                img.Save(path);
                return path;
            }
        }
        public static bool GetQRCode(string strContent, MemoryStream ms)
        {
            ErrorCorrectionLevel Ecl = ErrorCorrectionLevel.M; //���У��ˮƽ 
            string Content = strContent;//����������
            QuietZoneModules QuietZones = QuietZoneModules.Two;  //�հ����� 
            int ModuleSize = 12;//��С
            var encoder = new QrEncoder(Ecl);
            QrCode qr;
            if (encoder.TryEncode(Content, out qr))//�����ݽ��б��룬���������ɵľ���
            {
                var render = new GraphicsRenderer(new FixedModuleSize(ModuleSize, QuietZones));
                render.WriteToStream(qr.Matrix, ImageFormat.Png, ms);
            }
            else
            {
                return false;
            }
            return true;
        }
        public static Hashtable UpLoad(string path, HttpContext context, string[] filetype, int size)
        {
            //�ϴ�ͼƬ
            Hashtable info = new Hashtable();
            Uploader up = new Uploader();
            info = up.upFile(context, path , filetype,size);                   //��ȡ�ϴ�״̬
            return info;
        }
    }
    public class Uploader
    {

        string state = "SUCCESS";

        string URL = null;
        string currentType = null;
        string uploadpath = null;
        string filename = null;
        string originalName = null;
        HttpPostedFile uploadFile = null;
        bool upLoad = true;
        /**
      * �ϴ��ļ�����������
      * @param HttpContext
      * @param string
      * @param  string[]
      *@param int
      * @return Hashtable
      */
        public Hashtable upFile(HttpContext cxt, string pathbase, string[] filetype, int size)
        {
            uploadpath = cxt.Server.MapPath(pathbase);//��ȡ�ļ��ϴ�·��

            try
            {
                uploadFile = cxt.Request.Files[0];
                originalName = uploadFile.FileName;

                //Ŀ¼����
                createFolder();

                //��ʽ��֤
                if (checkType(filetype))
                {
                    //��������ļ�����
                    state = "\u4e0d\u5141\u8bb8\u7684\u6587\u4ef6\u7c7b\u578b";
                }
                //��С��֤
                if (checkSize(size))
                {
                    //�ļ���С������վ����
                    state = "\u6587\u4ef6\u5927\u5c0f\u8d85\u51fa\u7f51\u7ad9\u9650\u5236";
                }
                //����ͼƬ
                if (state == "SUCCESS")
                {
                    filename = NameFormater.Format(cxt.Request["fileNameFormat"], originalName);
                    var testname = filename;
                    var ai = 1;
                    while (File.Exists(uploadpath + testname))
                    {
                        testname = Path.GetFileNameWithoutExtension(filename) + "_" + ai++ + Path.GetExtension(filename);
                    }
                    uploadFile.SaveAs(uploadpath + testname);

                    URL = pathbase + testname;

                }
            }
            catch (Exception)
            {
                // δ֪����
                state = "\u672a\u77e5\u9519\u8bef";
                URL = "";
            }
            return getUploadInfo();
        }

        /**
    * ���������Զ������洢�ļ���
    */
        private void createFolder()
        {
            if (!Directory.Exists(uploadpath))
            {
                Directory.CreateDirectory(uploadpath);
            }
        }

        /**
    * �ļ����ͼ��
    * @return bool
    */
        private bool checkType(string[] filetype)
        {
            currentType = getFileExt();
            return Array.IndexOf(filetype, currentType) == -1;
        }

        /**
    * ��ȡ�ļ���չ��
    * @return string
    */
        private string getFileExt()
        {
            string[] temp = uploadFile.FileName.Split('.');
            return "." + temp[temp.Length - 1].ToLower();
        }


        /**
         * �ļ���С���
         * @param int
         * @return bool
         */
        private bool checkSize(int size)
        {
            return uploadFile.ContentLength >= (size * 1024 * 1024);
        }

        /**
         * ��ȡ�ϴ���Ϣ
         * @return Hashtable
         */
        private Hashtable getUploadInfo()
        {
            Hashtable infoList = new Hashtable();

            infoList.Add("state", state);
            infoList.Add("url", URL);

            if (currentType != null)
                infoList.Add("currentType", currentType);
            if (originalName != null)
                infoList.Add("originalName", originalName);
            return infoList;
        }

        /**
         * �������ļ�
         * @return string
         */
        private string reName()
        {
            return System.Guid.NewGuid() + getFileExt();
        }

        /**
 * ɾ���洢�ļ���
 * @param string
 */
        public void deleteFolder(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }
    }
    public static class NameFormater
    {
        public static string Format(string format, string filename)
        {
            if (String.IsNullOrWhiteSpace(format))
            {
                format = "{filename}{rand:6}";
            }
            string ext = Path.GetExtension(filename);
            filename = Path.GetFileNameWithoutExtension(filename);
            format = format.Replace("{filename}", filename);
            format = new Regex(@"\{rand(\:?)(\d+)\}", RegexOptions.Compiled).Replace(format, new MatchEvaluator(delegate(Match match)
            {
                var digit = 6;
                if (match.Groups.Count > 2)
                {
                    digit = Convert.ToInt32(match.Groups[2].Value);
                }
                var rand = new Random();
                return rand.Next((int)Math.Pow(10, digit), (int)Math.Pow(10, digit + 1)).ToString();
            }));
            format = format.Replace("{time}", DateTime.Now.Ticks.ToString());
            format = format.Replace("{yyyy}", DateTime.Now.Year.ToString());
            format = format.Replace("{yy}", (DateTime.Now.Year % 100).ToString("D2"));
            format = format.Replace("{mm}", DateTime.Now.Month.ToString("D2"));
            format = format.Replace("{dd}", DateTime.Now.Day.ToString("D2"));
            format = format.Replace("{hh}", DateTime.Now.Hour.ToString("D2"));
            format = format.Replace("{ii}", DateTime.Now.Minute.ToString("D2"));
            format = format.Replace("{ss}", DateTime.Now.Second.ToString("D2"));
            var invalidPattern = new Regex(@"[\\\/\:\*\?\042\<\>\|]");
            format = invalidPattern.Replace(format, "");
            return format + ext;
        }
    }
}