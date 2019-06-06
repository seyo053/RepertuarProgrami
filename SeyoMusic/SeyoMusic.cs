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
    public partial class SeyoMusic : Form
    {
        public SeyoMusic()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\Şarkılar.accdb");

        public void verilerigöster(string veriler)
        {
            
            OleDbDataAdapter da = new OleDbDataAdapter(veriler,baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }   
        private void Form1_Load(object sender, EventArgs e)
        {
            verilerigöster("select * from Şarkılar");
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[0].Width = 35;
            dataGridView1.Columns[1].Width = 155;
        }

        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int seçilialan = dataGridView1.SelectedCells[0].RowIndex;
            string ŞarkıSözü = dataGridView1.Rows[seçilialan].Cells[2].Value.ToString();
            
            textBox2.Text = ŞarkıSözü;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            verilerigöster("select * from Şarkılar where ŞarkıAdı Like '%"+textBox1.Text+"%'");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Şarkı_Ekle frm2 = new Şarkı_Ekle();
            frm2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = " ";
            textBox2.Text = " ";
            verilerigöster("select * from Şarkılar");
        }

        private void button3_Click(object sender, EventArgs e)
        {   
            
            baglanti.Open();
            ErrorProvider provider = new ErrorProvider();
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = "delete from Şarkılar where ŞarkıNo = " + textBox3.Text + "";
            cmd.Connection = baglanti;
            if (textBox3.Text == "")
            {
                provider.SetError(textBox3, "Lütfen şarkı no giriniz");
                MessageBox.Show(provider.GetError(textBox3));
                provider.Clear();

            }
            else
            {                
                cmd.ExecuteNonQuery();                
                MessageBox.Show("Silme Başarılı!");                
                verilerigöster("select * from Şarkılar");                
                textBox3.Clear();
                textBox2.Clear();                
            }
            baglanti.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            
                Güncelle frm2 = new Güncelle();
                frm2.Show();
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }
    }
    }

