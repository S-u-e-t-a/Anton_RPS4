using System;
using System.Windows.Forms;

namespace rps4
{
    public partial class Adding : Form
    {
        public Train train;
        public Adding(Train tr)
        {
            InitializeComponent();
            MaximizeBox = false;

            timePickerDep.Format = DateTimePickerFormat.Custom;
            timePickerDep.CustomFormat = "HH:mm";
            timePickerDep.ShowUpDown = true;

            timePickerArr.Format = DateTimePickerFormat.Custom;
            timePickerArr.CustomFormat = "HH:mm";
            timePickerArr.ShowUpDown = true;
            train = tr;

            textBoxName.Text = train.Name;

            textBoxDep.Text = train.Station_dep;
            textBoxArr.Text = train.Station_arr;
            costNumericUpDown.Value = train.Cost;

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrWhiteSpace(textBoxName.Text) || String.IsNullOrWhiteSpace(textBoxDep.Text) || String.IsNullOrWhiteSpace(textBoxArr.Text))
            {
                MessageBox.Show("Вы ввели не все необходимые данные", "Ошибка",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if ((datePickerDep.Value.Date < datePickerArr.Value.Date)
                || (datePickerDep.Value.Date == datePickerArr.Value.Date &&
                (timePickerDep.Value.Hour <= timePickerArr.Value.Hour && timePickerDep.Value.Minute < timePickerArr.Value.Minute)
                || (timePickerDep.Value.Hour < timePickerArr.Value.Hour && timePickerDep.Value.Minute <= timePickerArr.Value.Minute)))
            {
                train.Name = textBoxName.Text;

                train.Departure = datePickerDep.Value.ToShortDateString() + " " + timePickerDep.Value.ToShortTimeString();

                train.Arrival = datePickerArr.Value.ToShortDateString() + " " + timePickerArr.Value.ToShortTimeString();

                train.Station_dep = textBoxDep.Text;
                train.Station_arr = textBoxArr.Text;

                train.Cost = int.Parse(costNumericUpDown.Text);
                Close();
            }
            else
            {
                MessageBox.Show("Дата и время прибытия не могут быть раньше или равны дате и времени отправления!", "Ошибка",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
