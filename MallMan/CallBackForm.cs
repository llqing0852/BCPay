using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace MallMan
{
    public partial class CallBackForm : Form
    {
        public CallBackForm()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbSysTradeNo.Text.Trim()))
            {
                MessageBox.Show("请输入系统订单号");
                return;
            }

            if (string.IsNullOrEmpty(tbTradeNo.Text.Trim()))
            {
                MessageBox.Show("请输入支付宝流水号");
                return;
            }

            if (string.IsNullOrEmpty(tbOrderAmt.Text.Trim()))
            {
                MessageBox.Show("请输入订单金额");
                return;
            }

            var client = new RestClient($"http://47.242.231.136:8010/pay-admin/api/v1/ali_notify?outTradeNo={tbSysTradeNo.Text.Trim()}&tradeNo={tbTradeNo.Text.Trim()}&orderAmt={tbOrderAmt.Text.Trim()}");
            var request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            if (response.Content == "success")
            {
                MessageBox.Show("回调成功");
            }
            else
            {
                MessageBox.Show(response.Content);
            }
        }

        public string GetMD5Str(string strToMd5)
        {
            MD5 md5 = MD5.Create();
            // 将字符串转换成字节数组
            byte[] byteOld = Encoding.UTF8.GetBytes(strToMd5);
            // 调用加密方法
            byte[] byteNew = md5.ComputeHash(byteOld);
            // 将加密结果转换为字符串
            StringBuilder sb = new StringBuilder();
            foreach (byte b in byteNew)
            {
                // 将字节转换成16进制表示的字符串，
                sb.Append(b.ToString("x2"));
            }
            // 返回加密的字符串
            return sb.ToString();
        }

        private void BatchCallBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var path = openFileDialog1.FileName;

                var orders = GetTradeNos(path);
                var whereClause = "";
                foreach (var order in orders)
                {
                    whereClause += $"'{order}',";
                }
                whereClause = whereClause.Trim(',');

                DataAccess.ExecuteNonQuery($"update trade_order set status=1,pay_time=now() where trade_no in({whereClause}) and status=0");

                MessageBox.Show("批量回调已完成");
            }
        }

        private List<string> GetTradeNos(string path)
        {
            var tradeNos = new List<string>();
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        tradeNos.Add(line.Trim());
                    }
                }
            }
            return tradeNos;
        }
    }
}
