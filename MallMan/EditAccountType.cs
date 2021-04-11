using System;
using System.Windows.Forms;

namespace MallMan
{
    public partial class EditAccountType : Form
    {
        private string sellerId;
        private string accountType;

        public EditAccountType()
        {
            InitializeComponent();
        }

        public EditAccountType(string sellerId, string accountType)
        {
            this.sellerId = sellerId;
            this.accountType = accountType;

            InitializeComponent();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbAccountType.Text))
            {
                MessageBox.Show("请输入账户类型");
                return;
            }

            var accountType = tbAccountType.Text.Trim().ToUpper();

            if (accountType.Equals("F2F") || accountType.Equals("WAP"))
            {
                try
                {
                    DataAccess.ExecuteNonQuery($"update ali_accounts set account_type='{accountType}' where seller_id='{this.sellerId}'");
                }
                catch
                {
                    MessageBox.Show("编辑账户类型出错");
                }

                this.Close();

            }
            else
            {
                MessageBox.Show("请输入正确的账户类型 - F2F或者WAP");
                return;
            }
        }

        private void EditCommentsForm_Load(object sender, EventArgs e)
        {
            this.tbAccountType.Text = accountType;
        }
    }
}
