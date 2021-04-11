using System;
using System.Windows.Forms;

namespace MallMan
{
    public partial class EditComments : Form
    {
        private string domainName;
        private string comments;

        public EditComments()
        {
            InitializeComponent();
        }

        public EditComments(string domainName, string comments)
        {
            this.domainName = domainName;
            this.comments = comments;

            InitializeComponent();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbComments.Text))
            {
                MessageBox.Show("请输入备注");
                return;
            }

            try
            {
                DataAccess.ExecuteNonQuery($"update wechat_accounts set comments='{tbComments.Text}' where domain_name='{this.domainName}'");
            }
            catch
            {
                MessageBox.Show("编辑备注出错");
            }

            this.Close();
        }

        private void EditCommentsForm_Load(object sender, EventArgs e)
        {
            this.tbComments.Text = comments;
        }
    }
}
