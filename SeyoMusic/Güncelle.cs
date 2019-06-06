using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;


namespace SeyoMusic
{
    public partial class Güncelle : Form
    {
        public Güncelle()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\Şarkılar.accdb");
        private void verilerigöster(string veriler)
            
        {
            
            OleDbDataAdapter da = new OleDbDataAdapter(veriler, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        

        private void Güncelle_Load(object sender, EventArgs e)
        {
            verilerigöster("Select * from Şarkılar");
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[0].Width = 35;
            dataGridView1.Columns[1].Width = 155;

        }

        

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int seçilialan = dataGridView1.SelectedCells[0].RowIndex;
            string ŞarkıSözü = dataGridView1.Rows[seçilialan].Cells[2].Value.ToString();
            string ŞarkıAdı = dataGridView1.Rows[seçilialan].Cells[1].Value.ToString();

            textBox1.Text = ŞarkıSözü;
            richTextBox3.Text = ŞarkıAdı;
            
        }   

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            ErrorProvider provider = new ErrorProvider();
            OleDbCommand cmd = new OleDbCommand ("update Şarkılar set ŞarkıAdı ='" + richTextBox3.Text + "',ŞarkıSözü ='" + textBox1.Text + "' where ŞarkıNo ="  + richTextBox4.Text + "");
            cmd.Connection = baglanti;
            if (richTextBox4.Text == "")
            {
                provider.SetError(textBox1, "Lütfen şarkı no'sunu girin");
                MessageBox.Show(provider.GetError(textBox1));
                provider.Clear();
            }
            else
            
            
            if (textBox1.Text == "" && richTextBox3.Text == "" && richTextBox4.Text == "")
            {
                provider.SetError(textBox1,  "Lütfen tüm alanları doldurun");
                MessageBox.Show(provider.GetError(textBox1));
                
                provider.Clear();

            }
            else if (textBox1.Text == "")
            {
                provider.SetError(textBox1, "Lütfen tüm alanları doldurun");
                MessageBox.Show(provider.GetError(textBox1));
                provider.Clear();
            }
            else if (richTextBox3.Text == "")
            {
                provider.SetError(richTextBox3, "Lütfen tüm alanları doldurun");
                MessageBox.Show(provider.GetError(richTextBox3));
                provider.Clear();
            }
            else if (richTextBox4.Text == "")
            {
                provider.SetError(textBox1, "Lütfen şarkı no'sunu girin");
                MessageBox.Show(provider.GetError(textBox1));
                provider.Clear();
            }

            else
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Güncelleme Başarılı!");
                textBox1.Clear();
                richTextBox3.Clear();
                richTextBox4.Clear();                
            }
            baglanti.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            verilerigöster("select * from Şarkılar where ŞarkıAdı Like '%" + richTextBox1.Text + "%'");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Columns[2].Visible = false; 
            dataGridView1.Columns[0].Width = 35; 
            dataGridView1.Columns[1].Width = 155; 
        }

        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }

        private void richTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }

        private void richTextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        
    }
}
