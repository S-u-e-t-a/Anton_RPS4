using System.Windows.Forms;
using System;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;

namespace rps4
{
    public partial class MainWindow : Form
    { 
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            var add = new Adding();
            add.Show();
            add.Focus();
        }

        private void ButtonChange_Click(object sender, EventArgs e)
        {
            var change = new Changing();
            change.Show();
            change.Focus();
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            var delete = new Deleting();
            delete.Show();
            delete.Focus();
        }

        public void MainWindow_Load(object sender, EventArgs e)
        {
            dgvViewer.Columns.Clear();
            using (SQLiteConnection Connect = new SQLiteConnection(@"Data Source=C:\Users\anton\Desktop\rps4\rps4\rps4\lab4.db; Version=3;"))
            {
                Connect.Open();
                DataTable dTable = new DataTable();
                string sqlQuery = @"SELECT * FROM trains";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, Connect);
                adapter.Fill(dTable);

                dgvViewer.Columns.Add("id", "ID Поезда"); dgvViewer.Columns["id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvViewer.Columns.Add("name", "Название"); dgvViewer.Columns["name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvViewer.Columns.Add("departure", "Время отправления"); dgvViewer.Columns["departure"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvViewer.Columns.Add("arrival", "Время прибытия"); dgvViewer.Columns["arrival"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvViewer.Columns.Add("station_dep", "Откуда"); dgvViewer.Columns["station_dep"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvViewer.Columns.Add("station_arr", "Куда"); dgvViewer.Columns["station_arr"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvViewer.Columns.Add("cost", "Цена"); dgvViewer.Columns["cost"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvViewer.ReadOnly = true;

                if (dTable.Rows.Count > 0)
                {
                    dgvViewer.Rows.Clear();

                    for (int i = 0; i < dTable.Rows.Count; i++)
                        dgvViewer.Rows.Add(dTable.Rows[i].ItemArray);
                }
                else
                    MessageBox.Show("Database is empty");
                Connect.Close();
            }
        }



        private void ButtonUpdateTable_Click(object sender, EventArgs e)
        {
            MainWindow_Load(null, null);
        }
    }
}
