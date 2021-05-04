using System;
using System.Windows.Forms;

namespace rps4
{
    public partial class Adding : Form
    {
        public Adding()
        {
            InitializeComponent();
            MaximizeBox = false;

            timePickerDep.Format = DateTimePickerFormat.Custom;
            timePickerDep.CustomFormat = "HH:mm";
            timePickerDep.ShowUpDown = true;

            timePickerArr.Format = DateTimePickerFormat.Custom;
            timePickerArr.CustomFormat = "HH:mm";
            timePickerArr.ShowUpDown = true;

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
                Data.Name = textBoxName.Text;

                Data.DepartureDate = datePickerDep.Value.ToShortDateString();
                Data.DepartureTime = timePickerDep.Value.ToShortTimeString();

                Data.ArrivalDate = datePickerArr.Value.ToShortDateString();
                Data.ArrivalTime = timePickerArr.Value.ToShortTimeString();

                Data.StationDep = textBoxDep.Text;
                Data.StationArr = textBoxArr.Text;

                Data.Cost = int.Parse(costNumericUpDown.Text);
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
