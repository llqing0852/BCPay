using System;
using System.Windows.Forms;

namespace MallMan
{
    public partial class EditPrices : Form
    {
        private string account;
        private string prices;

        public EditPrices()
        {
            InitializeComponent();
        }

        public EditPrices(string account, string prices)
        {
            this.account = account;
            this.prices = prices;

            InitializeComponent();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbPrices.Text))
            {
                MessageBox.Show("请输入金额，逗号分隔");
                return;
            }

            string pricesTxt = tbPrices.Text;

            if (pricesTxt.Contains("，"))
            {
                pricesTxt = pricesTxt.Replace("，", ",");
            }

            try
            {
                DataAccess.ExecuteNonQuery($"update wechat_accounts set allowed_prices='{pricesTxt.Trim()}' where account='{this.account}'");
            }
            catch
            {
                MessageBox.Show("编辑金额出错");
            }

            this.Close();
        }

        private void EditCommentsForm_Load(object sender, EventArgs e)
        {
            this.tbPrices.Text = prices;
        }
    }
}
