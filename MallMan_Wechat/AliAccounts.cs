using RestSharp;
using System;
using System.Data;
using System.Windows.Forms;

namespace MallMan
{
    public partial class AliAccounts : Form
    {
        private string domainName;
        private DataTable dataTable;

        public AliAccounts()
        {
            InitializeComponent();
        }

        public AliAccounts(string domainName)
        {
            InitializeComponent();
            this.domainName = domainName;
        }

        private void AliAccounts_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;

            DataGridViewButtonColumn turnOnOffBtn = new DataGridViewButtonColumn();
            turnOnOffBtn.Name = "btnTurnOnOff";
            turnOnOffBtn.HeaderText = "操作";
            turnOnOffBtn.DefaultCellStyle.NullValue = "开/关";
            dataGridView1.Columns.Add(turnOnOffBtn);
            turnOnOffBtn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            DataGridViewButtonColumn checkBtn = new DataGridViewButtonColumn();
            checkBtn.Name = "btnCheck";
            checkBtn.HeaderText = "检查";
            checkBtn.DefaultCellStyle.NullValue = "检查";
            checkBtn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dataGridView1.Columns.Add(checkBtn);

            //DataGridViewButtonColumn checkBtn2 = new DataGridViewButtonColumn();
            //checkBtn2.Name = "btnCheck2";
            //checkBtn2.HeaderText = "检查(大额)";
            //checkBtn2.DefaultCellStyle.NullValue = "检查(大额)";
            //checkBtn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            //dataGridView1.Columns.Add(checkBtn2);

            ReloadDatatable1();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ReloadDatatable1()
        {
            var dt = DataAccess.ExcuteDataTableReader($"select allowed_prices,today_amount,yesterday_amount,(total_amt-today_amount) remain_amount, account,settle_status from wechat_accounts where domain_name='{domainName}' order by today_amount");
            dataTable = new DataTable();
            dataTable.Columns.Add("account");
            dataTable.Columns.Add("allowed_prices");
            dataTable.Columns.Add("today_amount");
            dataTable.Columns.Add("yesterday_amount");
            dataTable.Columns.Add("remain_amount");
            dataTable.Columns.Add("settle_status");
            dataTable.Columns.Add("pass_rate");

            long todayAmtTotal = 0L;
            long yesterdayAmtTotal = 0L;
            long remainAmtTotal = 0L;
            int availableCount = 0;
            var today = DateTime.Today.ToString("yyyy-MM-dd");

            foreach (DataRow row in dt.Rows)
            {
                var todayAmt = long.Parse(row["today_amount"].ToString());
                var yesterdayAmt = long.Parse(row["yesterday_amount"].ToString());
                var remainAmt = long.Parse(row["remain_amount"].ToString());
                var settleStatus = int.Parse(row["settle_status"].ToString());
                var account = row["account"].ToString();

                todayAmtTotal += todayAmt;
                yesterdayAmtTotal += yesterdayAmt;
                if (settleStatus == 1 && remainAmt > 0)
                {
                    remainAmtTotal += remainAmt;
                    availableCount++;
                }

                if (remainAmt < 0 || settleStatus == 0)
                {
                    row["remain_amount"] = 0;
                }

                int count = 0;
                int passCount3 = 0;
                int passCount5 = 0;
                int passCount10 = 0;
                bool hasTrade = false;
                DataAccess.ExecuteReader($"select trade_state from tb_pay_log where ali_account='{account}' and create_time>'{today}' and total_fee>100 and buyer_ip is not null order by create_time desc limit 0,10", reader =>
                {
                    while (reader.Read())
                    {
                        hasTrade = true;
                        count++;
                        if (reader.GetString(0) == "1")
                        {
                            if (count <= 3) { passCount3++; passCount5++; }
                            else if (count <= 5) { passCount5++; }
                            passCount10++;
                        }
                    }
                });

                var newRow = dataTable.NewRow();

                newRow["account"] = row["account"].ToString();
                newRow["allowed_prices"] = row["allowed_prices"].ToString();
                newRow["today_amount"] = row["today_amount"].ToString();
                newRow["yesterday_amount"] = row["yesterday_amount"].ToString();
                newRow["remain_amount"] = row["remain_amount"].ToString();
                newRow["settle_status"] = row["settle_status"].ToString();
                var passRateTxt = "";
                if (hasTrade)
                {
                    if (count <= 3) { passRateTxt = $"{passCount3}/{count}"; }
                    else if (count <= 5) { passRateTxt = $"{passCount3}/3 {passCount5}/{count}"; }
                    else { passRateTxt = $"{passCount3}/3 {passCount5}/5 {passCount10}/{count}"; }
                }
                else
                {
                    passRateTxt = "未开始交易";
                }
                newRow["pass_rate"] = passRateTxt;
                dataTable.Rows.Add(newRow);
            }

            dataGridView1.DataSource = dataTable;

            toolStripStatusLabel1.Text = $"  今天交易总额：" + todayAmtTotal + "元";
            toolStripStatusLabel2.Text = $"  昨天交易总额：" + yesterdayAmtTotal + "元";
            toolStripStatusLabel3.Text = $"  可轮询下单号：" + availableCount + "个";
            toolStripStatusLabel4.Text = $"  剩余总额度：" + remainAmtTotal + "元";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "btnTurnOnOff" && e.RowIndex >= 0)
            {
                string account = dataGridView1[0, e.RowIndex].Value.ToString();
                var status = Convert.ToInt32(dataGridView1[1, e.RowIndex].Value.ToString());
                var newStatus = status == 0 ? 1 : 0;
                var sql = $"update wechat_accounts set settle_status={newStatus} where account='{account}'";

                DataAccess.ExecuteNonQuery(sql);

                foreach (DataRow dr in dataTable.Rows)
                {
                    if (dr["account"].ToString() == account)
                    {
                        dr["settle_status"] = newStatus;
                        break;
                    }
                }
                dataGridView1.DataSource = dataTable;
                //ReloadDatatable1();
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name == "btnCheck" && e.RowIndex >= 0)
            {
                string sellerId = dataGridView1[0, e.RowIndex].Value.ToString();
                string orderAmt = (new Random().Next(10, 100) / 100.0).ToString();
                string outTradeNo = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 23);
                var client = new RestClient("http://47.242.231.136/shopx5/pay/createWxOrder");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("Connection", "keep-alive");
                request.AddHeader("Content-Length", "72");
                request.AddHeader("Accept-Encoding", "gzip, deflate");
                request.AddHeader("Host", "47.242.231.136");
                request.AddHeader("Postman-Token", "8ba43ba8-9af5-4ac0-b337-cd27122e822e,c3ab9f87-a83b-451b-9f52-8d03331fcbab");
                request.AddHeader("Cache-Control", "no-cache");
                request.AddHeader("Accept", "*/*");
                request.AddHeader("User-Agent", "PostmanRuntime/7.20.1");
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("undefined", $"outTradeNo={outTradeNo}&orderAmt={orderAmt}&account={sellerId}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                var url = response.Content.Replace("\"", "");

                new QRCodeEncoderDemo.QRCodeEncoderForm(url).ShowDialog();
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name == "btnCheck2" && e.RowIndex >= 0)
            {
                string sellerId = dataGridView1[0, e.RowIndex].Value.ToString();
                var amts = dataGridView1[2, e.RowIndex].Value.ToString().Split(',');
                string orderAmt = amts[new Random().Next(0, amts.Length)];
                string outTradeNo = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 23);
                var client = new RestClient("http://47.242.231.136/shopx5/pay/createWxOrder");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("Connection", "keep-alive");
                request.AddHeader("Content-Length", "72");
                request.AddHeader("Accept-Encoding", "gzip, deflate");
                request.AddHeader("Host", "47.242.231.136");
                request.AddHeader("Postman-Token", "8ba43ba8-9af5-4ac0-b337-cd27122e822e,c3ab9f87-a83b-451b-9f52-8d03331fcbab");
                request.AddHeader("Cache-Control", "no-cache");
                request.AddHeader("Accept", "*/*");
                request.AddHeader("User-Agent", "PostmanRuntime/7.20.1");
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("undefined", $"outTradeNo={outTradeNo}&orderAmt={orderAmt}&account={sellerId}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                var url = response.Content.Replace("\"", "");

                new QRCodeEncoderDemo.QRCodeEncoderForm(url).ShowDialog();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "allowed_prices")
            {
                string sellerId = dataGridView1[0, e.RowIndex].Value.ToString();
                string prices = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();

                new EditPrices(sellerId, prices).ShowDialog();
                ReloadDatatable1();
            }
        }
    }
}
