using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace MallMan
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;

            DataGridViewButtonColumn viewBtn = new DataGridViewButtonColumn();
            viewBtn.Name = "btnView";
            viewBtn.HeaderText = "查看";
            viewBtn.DefaultCellStyle.NullValue = "查看";
            viewBtn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dataGridView1.Columns.Add(viewBtn);

            DataGridViewButtonColumn deleteBtn = new DataGridViewButtonColumn();
            deleteBtn.Name = "btnDelete";
            deleteBtn.HeaderText = "删除";
            deleteBtn.DefaultCellStyle.NullValue = "删除";
            deleteBtn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dataGridView1.Columns.Add(deleteBtn);

            DataGridViewButtonColumn checkBtn = new DataGridViewButtonColumn();
            checkBtn.Name = "btnCheck";
            checkBtn.HeaderText = "检查";
            checkBtn.DefaultCellStyle.NullValue = "检查";
            checkBtn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dataGridView3.Columns.Add(checkBtn);

            ReloadDatatable1();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "btnView" && e.RowIndex >= 0)
            {
                string domainName = dataGridView1[0, e.RowIndex].Value.ToString();

                new AliAccounts(domainName).ShowDialog();
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name == "btnDelete" && e.RowIndex >= 0)
            {
                string domainName = dataGridView1[0, e.RowIndex].Value.ToString();

                if (MessageBox.Show("删除后，此商城关联的所有收款账号将会一起被删除，你确认要删除吗？", "确认提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataAccess.ExecuteNonQuery($"delete from ali_accounts where domain_name='{domainName}'");
                    ReloadDatatable1();
                }
            }
        }

        private void ReloadDatatable1()
        {
            var dt = DataAccess.ExcuteDataTableReader("select today_amount,yesterday_amount,(total_amt-today_amount) remain_amount, domain_name,comments,settle_status from ali_accounts where is_master=0 order by domain_name");
            var dataTable = new DataTable();
            dataTable.Columns.Add("today_amount");
            dataTable.Columns.Add("yesterday_amount");
            dataTable.Columns.Add("remain_amount");
            dataTable.Columns.Add("domain_name");
            dataTable.Columns.Add("comments");

            string currentDomainName = null;
            string currentComments = null;
            long todayAmtTotal = 0L;
            long yesterdayAmtTotal = 0L;
            long remainAmtTotal = 0L;
            long todayAmtSum = 0L;
            long yesterdayAmtSum = 0L;
            long remainAmtSum = 0L;
            int availableCount = 0;

            foreach (DataRow row in dt.Rows)
            {
                var domainName = row["domain_name"].ToString();
                var comments = row["comments"].ToString();
                var todayAmt = long.Parse(row["today_amount"].ToString());
                var yesterdayAmt = long.Parse(row["yesterday_amount"].ToString());
                var remainAmt = long.Parse(row["remain_amount"].ToString());
                var settleStatus = int.Parse(row["settle_status"].ToString());

                if (currentDomainName != domainName || currentComments != comments)
                {
                    if (currentDomainName != null && currentComments != null)
                    {
                        var newRow = dataTable.NewRow();
                        newRow["today_amount"] = todayAmtTotal;
                        newRow["yesterday_amount"] = yesterdayAmtTotal;
                        newRow["remain_amount"] = remainAmtTotal;
                        newRow["domain_name"] = currentDomainName;
                        newRow["comments"] = currentComments;
                        dataTable.Rows.Add(newRow);
                    }

                    currentDomainName = domainName;
                    currentComments = comments;
                    todayAmtTotal = todayAmt;
                    yesterdayAmtTotal = yesterdayAmt;
                    todayAmtSum += todayAmt;
                    yesterdayAmtSum += yesterdayAmt;
                    if (settleStatus == 1 && remainAmt > 0)
                    {
                        remainAmtTotal = remainAmt;
                        remainAmtSum += remainAmt;
                        availableCount++;
                    }
                    else { remainAmtTotal = 0; }
                }
                else
                {
                    todayAmtSum += todayAmt;
                    yesterdayAmtSum += yesterdayAmt;

                    todayAmtTotal += todayAmt;
                    yesterdayAmtTotal += yesterdayAmt;
                    if (settleStatus == 1 && remainAmt > 0)
                    {
                        remainAmtTotal += remainAmt;
                        remainAmtSum += remainAmt;
                        availableCount++;
                    }
                }
            }

            var newRow2 = dataTable.NewRow();
            newRow2["today_amount"] = todayAmtTotal;
            newRow2["yesterday_amount"] = yesterdayAmtTotal;
            newRow2["remain_amount"] = remainAmtTotal;
            newRow2["domain_name"] = currentDomainName;
            newRow2["comments"] = currentComments;
            dataTable.Rows.Add(newRow2);

            dataGridView1.DataSource = dataTable;

            toolStripStatusLabel1.Text = $"  今天交易总额：" + todayAmtSum + "元";
            toolStripStatusLabel2.Text = $"  昨天交易总额：" + yesterdayAmtSum + "元";
            toolStripStatusLabel3.Text = $"  可轮询下单号：" + availableCount + "个";
            toolStripStatusLabel4.Text = $"  剩余总额度：" + remainAmtSum + "元";
        }

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "tabPage1")
            {
                ReloadDatatable1();
            }
        }

        private void CallBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CallBackForm().ShowDialog();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage.Name == "tabPage1")
            {
                ReloadDatatable1();
            }

            if (e.TabPage.Name == "tabPage3")
            {
                ReloadDatatable3();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "comments")
            {
                string domainName = dataGridView1[e.ColumnIndex - 1, e.RowIndex].Value.ToString();
                string comments = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();

                new EditComments(domainName, comments).ShowDialog();
                ReloadDatatable1();
            }
        }

        private void ReloadDatatable3()
        {
            var today = DateTime.Today.ToString("yyyy-MM-dd");
            var dt = Db.DataAccess.ExcuteDataTableReader($"select count(*) cnt, mall_name,card_owner from trade_order where status=0 and pay_type='AliWap' and create_time>'{today} 00:00:00' group by mall_name,card_owner order by cnt desc limit 0,15");
            dataGridView3.DataSource = dt;
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView3.Columns[e.ColumnIndex].Name == "btnCheck" && e.RowIndex >= 0)
            {
                string sellerId = dataGridView3.CurrentRow.Cells["mall_name"].Value.ToString();
                string orderAmt = (new Random().Next(10, 100) / 100.0).ToString();
                string outTradeNo = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 23);
                var client = new RestClient("http://156.237.190.247/shopx5/pay/createOrder");
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("undefined", $"outTradeNo={outTradeNo}&orderAmt={orderAmt}&account={sellerId}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                var url = response.Content.Replace("\"", "");

                new QRCodeEncoderDemo.QRCodeEncoderForm(url).ShowDialog();
            }
        }

        private void RefreshTodayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var today = DateTime.Today.ToString("yyyy-MM-dd");
            var sql = $"select sum(total_fee) t,ali_account from tb_pay_log where pay_time>='{today}' and user_id='payUser' and trade_state=1 group by ali_account order by t desc";
            var todayDic = new Dictionary<string, double>();
            var yesterdayDic = new Dictionary<string, double>();

            DataAccess.ExecuteReader(sql, reader =>
            {
                while (reader.Read())
                {
                    todayDic.Add(reader.GetString(1), double.Parse(reader.GetString(0)));
                }
            });

            var yesterday = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");

            sql = $"select sum(total_fee) t,ali_account from tb_pay_log where pay_time>='{yesterday}' and pay_time<'{today}' and user_id='payUser' and trade_state=1 group by ali_account order by t desc";

            DataAccess.ExecuteReader(sql, reader =>
            {
                while (reader.Read())
                {
                    yesterdayDic.Add(reader.GetString(1), double.Parse(reader.GetString(0)));
                }
            });

            DataAccess.ExecuteReader("select seller_id from ali_accounts", reader =>
            {
                while (reader.Read())
                {
                    var sellerId = reader.GetString(0);
                    var todayAmt = 0d;
                    var yestodayAmt = 0d;

                    if (todayDic.ContainsKey(sellerId))
                    {
                        todayAmt = todayDic[sellerId];
                    }

                    if (yesterdayDic.ContainsKey(sellerId))
                    {
                        yestodayAmt = yesterdayDic[sellerId];
                    }

                    DataAccess.ExecuteNonQuery($"update ali_accounts set today_amount={todayAmt},yesterday_amount={yestodayAmt} where seller_id='{sellerId}'");
                }
            });

            ReloadDatatable1();
        }
    }
}
