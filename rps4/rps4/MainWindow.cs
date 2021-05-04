using System;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace rps4
{
    public partial class MainWindow : Form
    {
        public ApplicationContext db;
        public BindingList<Train> Trains;
        public MainWindow()
        {
            InitializeComponent();
            saveFileDialog.Filter = @"Text files(*.txt)|*.txt";
            MaximizeBox = false;

            db = new ApplicationContext();
            db.Trains.Load();

            Trains = db.Trains.Local.ToBindingList();

            TrainsGrid.DataSource = Trains;
            if (Settings.Default.Show == true)
            {
                InfoToolStripMenuItem_Click(null, null);
                ShowInfoToolStripMenuItem.Checked = true;
            }
            else ShowInfoToolStripMenuItem.Checked = false;
            if (TrainsGrid.RowCount == 1)
            {
                ButtonChange.Enabled = false;
                ButtonDelete.Enabled = false;
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var newTrain = new Train();
                var newEntity = new Adding();

                int maxTrainID;

                foreach (DataGridViewRow row in TrainsGrid.Rows)
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
                newEntity.ShowDialog();
                
                if (TrainsGrid.Rows.Count != 0)
                {
                    // Нахождение ID для новой строки базы данных
                    maxTrainID = TrainsGrid.Rows.Cast<DataGridViewRow>()
                                                      .Max(r => Convert.ToInt32(r.Cells["ID"].Value)) + 1;
                }
                else
                {
                    maxTrainID = 1;
                }
                newTrain.ID = maxTrainID;
                newTrain.Name = Data.Name;
                newTrain.Departure = Data.DepartureDate + " " + Data.DepartureTime;
                newTrain.Arrival = Data.ArrivalDate + " " + Data.ArrivalTime;
                newTrain.Station_dep = Data.StationDep;
                newTrain.Station_arr = Data.StationArr;
                newTrain.Cost = Data.Cost;
                if (String.IsNullOrEmpty(newTrain.Name) || String.IsNullOrEmpty(newTrain.Departure)
                    || String.IsNullOrEmpty(newTrain.Arrival) || String.IsNullOrEmpty(newTrain.Station_dep)
                    || String.IsNullOrEmpty(newTrain.Station_arr) || String.IsNullOrEmpty(newTrain.Cost.ToString()))
                {
                    throw new NullReferenceException();
                }
                db.Trains.Add(newTrain);
                db.SaveChanges();
                int newRowIndex = TrainsGrid.Rows.Count - 1;
                TrainsGrid.Rows[newRowIndex].DefaultCellStyle.BackColor = Color.Green;
                MessageBox.Show("Данные успешно добавлены и сохранены.", "Информация",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                ButtonChange.Enabled = true;
                ButtonDelete.Enabled = true;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Вы не ввели данные.", "Ошибка!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void NewEntity_FormClosed(object sender, FormClosedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string Choose = TrainsGrid.CurrentCell.OwningColumn.Name;
                if (Choose == "ID")
                {
                    if (DialogResult.Yes == MessageBox.Show("Вы уверены, что хотите удалить направление из базы?", "Подтвердите действие",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    {
                        int deleting = int.Parse(TrainsGrid.CurrentCell.Value.ToString());
                        db.Trains.Remove(db.Trains.Find(deleting));
                        db.SaveChanges();
                    }
                }
                if (TrainsGrid.RowCount == 1)
                {
                    ButtonChange.Enabled = false;
                    ButtonDelete.Enabled = false;
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Нет строк для удаления.",
                                "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ButtonChange_Click(object sender, EventArgs e)
        {
            try
            {
                string columnNameOfChosenCell = TrainsGrid.CurrentCell.OwningColumn.Name;
                if (columnNameOfChosenCell == "ID")
                {
                    foreach (DataGridViewRow row in TrainsGrid.Rows)
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                    }
                    int changingID = int.Parse(TrainsGrid.CurrentCell.Value.ToString());
                    var changingTrain = db.Trains.SingleOrDefault(p => p.ID == changingID);

                    // Вывод вспомогательной формы
                    var newEntity = new Adding();
                    newEntity.Text = "Изменение сущности";
                    newEntity.ShowDialog();

                    // Изменение сущности
                    changingTrain.Name = Data.Name;
                    changingTrain.Departure = Data.DepartureDate + " " + Data.DepartureTime;
                    changingTrain.Arrival = Data.ArrivalDate + " " + Data.ArrivalTime;
                    changingTrain.Station_dep = Data.StationDep;
                    changingTrain.Station_arr = Data.StationArr;
                    changingTrain.Cost = Data.Cost;
                    if (String.IsNullOrEmpty(changingTrain.Name) || String.IsNullOrEmpty(changingTrain.Departure)
                    || String.IsNullOrEmpty(changingTrain.Arrival) || String.IsNullOrEmpty(changingTrain.Station_dep)
                    || String.IsNullOrEmpty(changingTrain.Station_arr) || String.IsNullOrEmpty(changingTrain.Cost.ToString()))
                    {
                        throw new NullReferenceException();
                    }
                    // Сохранение изменений
                    db.SaveChanges();
                    int changedRowIndex = TrainsGrid.CurrentCell.RowIndex;
                    TrainsGrid.Rows[changedRowIndex].DefaultCellStyle.BackColor = Color.GreenYellow;
                    MessageBox.Show("Данные успешно изменены и сохранены.", "Информация",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Вы не ввели данные.", "Ошибка!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Работа с СУБД SQLite.\n" +
                                "Данная программа хранит расписание поездов и \n" +
                                "позволяет добавлять/удалять направления в базе данных.\n" +
                                "Автор:  Гусев Антон\n" +
                                "Группа: 494\n" +
                                "Учебное заведение: СПбГТИ (ТУ)", "О программе",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ShowInfoToolStripMenuItem.Checked)
            {
                ShowInfoToolStripMenuItem.Checked = false;
                Settings.Default.Show = false;
                Settings.Default.Save();
            }
            else
            {
                ShowInfoToolStripMenuItem.Checked = true;
                Settings.Default.Show = true;
                Settings.Default.Save();

            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            string fileOutputPath = saveFileDialog.FileName;
            saveFileDialog.FileName = string.Empty;

            string text = SaveInFile.MakeResult(Trains);

            SaveInFile.SaveToFile(fileOutputPath, text);
        }
    }
}
