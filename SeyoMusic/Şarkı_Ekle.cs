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
    public partial class Şarkı_Ekle : Form
    {
        public Şarkı_Ekle()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\Şarkılar.accdb");
        private void Şarkı_Ekle_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            ErrorProvider provider = new ErrorProvider();
            OleDbCommand cmd = new OleDbCommand("insert into Şarkılar (ŞarkıAdı,ŞarkıSözü) values ('" + textBox1.Text.ToString() +"','" + textBox2.Text.ToString() + "')", baglanti);
            
            if (textBox1.Text == "" && textBox2.Text == "")
            {
                provider.SetError(textBox1, "Lütfen tüm alanları doldurun");
                MessageBox.Show(provider.GetError(textBox1));
                provider.Clear();

            }
            else if (textBox1.Text == "")
            {
                provider.SetError(textBox1, "Lütfen tüm alanları doldurun");
                MessageBox.Show(provider.GetError(textBox1));
                provider.Clear();
            }
            else if (textBox2.Text == "")
            {
                provider.SetError(textBox2, "Lütfen tüm alanları doldurun");
                MessageBox.Show(provider.GetError(textBox2));
                provider.Clear();
            }
            else
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Kayıt Başarılı!");
                textBox1.Clear();
                textBox2.Clear();                
            }
            baglanti.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

       
    }
}
