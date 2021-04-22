using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rps4
{
    public partial class Adding : Form
    {
        SQLiteCommand cmd;
        private SQLiteConnection m_dbConn = new SQLiteConnection();
        public Adding()
        {
            InitializeComponent();
            MaximizeBox = false;

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=C:\Users\anton\Desktop\rps4\rps4\rps4\lab4.db; Version=3;"))
            {
                using (cmd = new SQLiteCommand("INSERT INTO trains ('Name', 'Departure', 'Arrival', 'Station_dep', 'Station_arr', 'Cost') values ('" + textBoxName.Text + "' , '" + dateTimePickerDep.Value.ToLongDateString() + "', '" + dateTimePickerArr.Value.ToLongDateString() + "', '" + textBoxDep.Text + "', '" + textBoxArr.Text + "', '" + textBoxCost.Text + "')", conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
