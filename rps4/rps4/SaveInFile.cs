using System;
using System.ComponentModel;

namespace rps4
{
    class SaveInFile
    {
        public static void SaveToFile(string fileOutputPath, string text)
        {
            System.IO.File.WriteAllText(fileOutputPath, text);
        }


        public static string MakeResult(BindingList<Train> Trains)
        {
            string text = String.Format("{0} {1, 7} {2, 25} {3, 23} {4, 20} {5, 20} {6, 15}\n",
                "Код", "Наименование", "Дата и время отправления", "Дата и время прибытия", "Место отправления", "Место прибытия", "Стоимость");
            foreach (Train trainRow in Trains)
            {
                text += String.Format("{0} {1, 7} {2, 25} {3, 23} {4, 20} {5, 20} {6, 15}\n",
                    trainRow.ID, trainRow.Name, trainRow.Departure, trainRow.Arrival, trainRow.Station_dep, trainRow.Station_arr, trainRow.Cost);
            }
            return text;
        }
    }
}
