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
                var newEntity = new Adding(new Train());

                foreach (DataGridViewRow row in TrainsGrid.Rows)
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
                newEntity.ShowDialog();
             
                if (String.IsNullOrEmpty(newEntity.train.Name) || String.IsNullOrEmpty(newEntity.train.Departure)
                    || String.IsNullOrEmpty(newEntity.train.Arrival) || String.IsNullOrEmpty(newEntity.train.Station_dep)
                    || String.IsNullOrEmpty(newEntity.train.Station_arr) || String.IsNullOrEmpty(newEntity.train.Cost.ToString()))
                {
                    throw new NullReferenceException();
                }
                else
                {
                    InteractionDB.Add(newEntity.train, db);
                    int newRowIndex = TrainsGrid.Rows.Count - 1;
                    TrainsGrid.Rows[newRowIndex].DefaultCellStyle.BackColor = Color.Green;
                    MessageBox.Show("Данные успешно добавлены и сохранены.", "Информация",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ButtonChange.Enabled = true;
                    ButtonDelete.Enabled = true;
                }

            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Вы не ввели данные.", "Ошибка!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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
                        InteractionDB.Delete(deleting, db);

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
                Train columnNameOfChosenCell = TrainsGrid.CurrentRow.DataBoundItem as Train;

                foreach (DataGridViewRow row in TrainsGrid.Rows)
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }

                // Вывод вспомогательной формы
                var newEntity = new Adding(columnNameOfChosenCell)
                {
                    Text = "Изменение сущности"
                };
                newEntity.ShowDialog();

                // Изменение сущности

                if (String.IsNullOrEmpty(newEntity.train.Name) || String.IsNullOrEmpty(newEntity.train.Departure)
                || String.IsNullOrEmpty(newEntity.train.Arrival) || String.IsNullOrEmpty(newEntity.train.Station_dep)
                || String.IsNullOrEmpty(newEntity.train.Station_arr) || String.IsNullOrEmpty(newEntity.train.Cost.ToString()))
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
