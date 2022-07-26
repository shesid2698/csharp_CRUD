using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_INSERT_UPDATE_DELETE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        void ShowData()
        {
            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.conn))
            {
                cn.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter("select * from 員工", cn);
                da.Fill(ds,"員工");
                dataGridView1.DataSource = ds.Tables[0];
                cn.Close();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.conn))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand($"delete from 員工 where 姓名='{txtName.Text}';", cn);
                cmd.ExecuteNonQuery();
                ShowData();
                cn.Close();
            }
        }
        DataSet ds = new DataSet();
        
        private void Form1_Load(object sender, EventArgs e)
        {
            using (SqlConnection cn=new SqlConnection(Properties.Settings.Default.conn))
            {
                cn.Open();
                SqlDataAdapter da=new SqlDataAdapter("select * from 員工",cn);
                da.Fill(ds,"員工");
                DataTable dt = ds.Tables[0];
                dataGridView1.DataSource = ds.Tables[0];
                label1.Text = dt.Columns[1].ColumnName;
                label2.Text = dt.Columns[2].ColumnName;
                label3.Text= dt.Columns[3].ColumnName;
                label4.Text = dt.Columns[4].ColumnName;
                cn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn=new SqlConnection(Properties.Settings.Default.conn))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand($"insert into 員工 (姓名,職稱,電話,薪資) values ('{txtName.Text}','{txtTitle.Text}','{txtPhone.Text}',{txtSalary.Text})", cn);
                cmd.ExecuteNonQuery();
                ShowData();
                cn.Close();
            }
                
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
                const string message = "確定關閉表單嗎?";
                const string caption = "關閉表單";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo,MessageBoxIcon.Question);

            // If the no button was pressed ...
            if (result == DialogResult.No)
                {
                    // cancel the closure of the form.
                    e.Cancel = true;
                }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.conn))
            {
                cn.Open();
                string sql = "";
                if (txtName.Text!="" && txtPhone.Text!="" && txtSalary.Text!="" && txtTitle.Text!="")
                {
                    sql = $"update 員工 set 姓名='{txtName.Text}',職稱 = '{txtTitle.Text}',電話='{txtPhone.Text}',薪資={txtSalary.Text} where 姓名='{txtName.Text}';";
                }else if(txtName.Text != "" && txtPhone.Text != "" && txtSalary.Text != "")
                {
                    sql = $"update 員工 set 姓名='{txtName.Text}',電話='{txtPhone.Text}',薪資={txtSalary.Text} where 姓名='{txtName.Text}';";
                }else if(txtName.Text != "" && txtPhone.Text != "")
                {
                    sql = $"update 員工 set 姓名='{txtName.Text}',電話='{txtPhone.Text}' where 姓名='{txtName.Text}';";
                }
                else if (txtName.Text != "" && txtSalary.Text != "")
                {
                    sql = $"update 員工 set 姓名='{txtName.Text}',薪資={txtSalary.Text} where 姓名='{txtName.Text}';";
                }
                else if (txtName.Text != "" && txtTitle.Text != "")
                {
                    sql = $"update 員工 set 姓名='{txtName.Text}',職稱 = '{txtTitle.Text}' where 姓名='{txtName.Text}';";
                }
                
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.ExecuteNonQuery();
                ShowData();
                cn.Close();
            }
        }
    }
}
