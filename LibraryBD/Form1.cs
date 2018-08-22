using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryBD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btFind.Enabled = false;
        }

        private void tbFind_TextChanged(object sender, EventArgs e)
        {
            if (tbFind.Text != string.Empty || tbFind != null)
                btFind.Enabled = true;
            if (tbFind.Text == string.Empty || tbFind == null)
                btFind.Enabled = false;
        }

        private void btFind_Click(object sender, EventArgs e)
        {
            using (Library2Entities db = new Library2Entities())
            {
                db.Database.Log = sql => Debug.Write(sql);
                if (tbFind.Text != null || tbFind.Text != string.Empty)
                {
                    
                    var listAuthorsVersion2 = db.Book.Include("Author").
                        Where(a => a.Author.LastName.ToLower().
                        StartsWith(tbFind.Text.ToLower())).
                        Select(x => new { Authors = x.Author.LastName + " " + x.Author.FirstName, Titlee = x.Title }).
                        OrderBy(au => au.Authors).ToList();

                    var listAuthors2 = db.Author.Include("Book").
                        Where(a => a.LastName.ToLower().
                        StartsWith(tbFind.Text.ToLower())).ToList();
                    List<Book> Temp = new List<Book>();
                    foreach (var item in listAuthors2)
                    {
                        Debug.WriteLine(item.LastName);
                        foreach (var item2 in item.Book)
                        {
                            Debug.WriteLine($"{item2.Title}");
                            Temp.Add(item2);
                        }
                    }
                    BindingSource bi = new BindingSource();
                    bi.DataSource = Temp.Select(t => new { TitleBook = t.Title});
                    //bi.DataSource = listAuthors3;
                    dataGridView1.DataSource = bi;
                    //dataGridView1.Refresh();
                }
            }
        }
    }
}
