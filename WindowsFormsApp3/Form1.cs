using CrystalDecisions.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp3.DataSet1TableAdapters;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        livrooEntities db = new livrooEntities();
        BindingSource bs = new BindingSource();
        private void afficher()
        {
            var liv = from li in db.livres
                      select new
                      {
                          code_livre = li.id,
                          titre = li.titre,
                          date_publication = li.date_publication,
                          auteur = li.auteur.nom

                      };
            bs.DataSource = liv.ToList();
            dataGridView1.DataSource = bs;
            textBox1.DataBindings.Add("Text",bs, "code_livre");
            textBox2.DataBindings.Add("Text",bs, "titre");
            dateTimePicker1.DataBindings.Add("Value",bs, "date_publication");
            comboBox1.DataBindings.Add("SelectedValue",bs, "auteur");
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            afficher();
            comboBox1.DataSource = db.auteurs.ToList();
            comboBox1.DisplayMember = "nom";
            comboBox1.ValueMember = "id";


        }

        private void button1_Click(object sender, EventArgs e)
        {
            livre l1 = new livre();
            l1.id = int.Parse(textBox1.Text);
            l1.titre = textBox2.Text;
            l1.date_publication = dateTimePicker1.Value;
            l1.id_auteur = int.Parse(comboBox1.SelectedValue.ToString());
            db.livres.Add(l1);
            MessageBox.Show("bien");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            db.SaveChanges();
            MessageBox.Show("bien bein");
            Form1_Load(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            livre l1 = new livre();
            l1.id = int.Parse(textBox1.Text);
            var l2 = (from li in db.livres
                      where li.id == l1.id
                      select li).FirstOrDefault();
            l2.titre = textBox2.Text;
            l2.date_publication = dateTimePicker1.Value;
            l2.id_auteur = int.Parse(comboBox1.SelectedValue.ToString());

        }

        private void button3_Click(object sender, EventArgs e)
        {
            livre l1 = new livre();
            l1.id = int.Parse(textBox1.Text);
            var l2 = (from li in db.livres
                      where li.id == l1.id
                      select li).FirstOrDefault();
            db.livres.Remove(l2);
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            auteur a = new auteur();
            a.id = int.Parse(textBox5.Text);
            a.nom = textBox4.Text;
            db.auteurs.Add(a);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text= dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            dateTimePicker1.Text= dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            comboBox1.Text= dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bs.MoveFirst();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            bs.MoveLast();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            bs.MoveNext();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            bs.MovePrevious();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            print p = new print();
            DataSet1 ds = new DataSet1();
            livreTableAdapter da = new livreTableAdapter();
            da.Fill(ds.livre);
            CrystalReport1 report = new CrystalReport1();
            report.SetDataSource(ds);
            (p.Controls["crystalReportViewer1"] as CrystalReportViewer).ReportSource = report;
            p.Show();
        }
    }
}
