using RestSharp;
using System;
using System.Data;
using System.Windows.Forms;

namespace MallMan
{
    public partial class AliAccounts : Form
    {
        private string domainName;

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

            ReloadDatatable1();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void ReloadDatatable1()
        {
            var dt = DataAccess.ExcuteDataTableReader($"select allowed_prices,today_amount,yesterday_amount,(total_amt-today_amount) remain_amount, seller_id,settle_status,is_master,account_type from ali_accounts where domain_name='{domainName}'");

            long todayAmtTotal = 0L;
            long yesterdayAmtTotal = 0L;
            long remainAmtTotal = 0L;
            int availableCount = 0;

            foreach (DataRow row in dt.Rows)
            {
                var todayAmt = long.Parse(row["today_amount"].ToString());
                var yesterdayAmt = long.Parse(row["yesterday_amount"].ToString());
                var remainAmt = long.Parse(row["remain_amount"].ToString());
                var settleStatus = int.Parse(row["settle_status"].ToString());
                var isMaster = int.Parse(row["is_master"].ToString());

                todayAmtTotal += todayAmt;
                yesterdayAmtTotal += yesterdayAmt;
                if (isMaster == 0 && settleStatus == 1 && remainAmt > 0)
                {
                    remainAmtTotal += remainAmt;
                    availableCount++;
                }

                if (remainAmt < 0 || settleStatus == 0 || isMaster == 1)
                {
                    row["remain_amount"] = 0;
                }
            }

            dataGridView1.DataSource = dt;

            toolStripStatusLabel1.Text = $"  今天交易总额：" + todayAmtTotal + "元";
            toolStripStatusLabel2.Text = $"  昨天交易总额：" + yesterdayAmtTotal + "元";
            toolStripStatusLabel3.Text = $"  可轮询下单号：" + availableCount + "个";
            toolStripStatusLabel4.Text = $"  剩余总额度：" + remainAmtTotal + "元";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "btnTurnOnOff" && e.RowIndex >= 0)
            {
                string sellerId = dataGridView1[0, e.RowIndex].Value.ToString();
                var status = Convert.ToInt32(dataGridView1[1, e.RowIndex].Value.ToString());
                var newStatus = status == 0 ? 1 : 0;
                var sql = $"update ali_accounts set settle_status={newStatus} where seller_id='{sellerId}'";

                DataAccess.ExecuteNonQuery(sql);

                ReloadDatatable1();
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name == "btnCheck" && e.RowIndex >= 0)
            {
                string sellerId = dataGridView1[0, e.RowIndex].Value.ToString();
                string orderAmt = (new Random().Next(10, 100) / 100.0).ToString();
                string outTradeNo = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 23);
                var client = new RestClient("http://122.147.254.10/shopx5/pay/createOrder");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("Connection", "keep-alive");
                request.AddHeader("Content-Length", "72");
                request.AddHeader("Accept-Encoding", "gzip, deflate");
                request.AddHeader("Host", "122.147.254.10");
                request.AddHeader("Postman-Token", "8ba43ba8-9af5-4ac0-b337-cd27122e822e,c3ab9f87-a83b-451b-9f52-8d03331fcbab");
                request.AddHeader("Cache-Control", "no-cache");
                request.AddHeader("Accept", "*/*");
                request.AddHeader("User-Agent", "PostmanRuntime/7.20.1");
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("undefined", $"outTradeNo={outTradeNo}&orderAmt={orderAmt}&account={sellerId}", ParameterType.RequestBody);
                request.Timeout = 3000;
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

            if (dataGridView1.Columns[e.ColumnIndex].Name == "account_type")
            {
                string sellerId = dataGridView1[0, e.RowIndex].Value.ToString();
                string accountType = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();

                new EditAccountType(sellerId, accountType).ShowDialog();
                ReloadDatatable1();
            }
        }
    }
}
