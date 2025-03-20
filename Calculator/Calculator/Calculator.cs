using System.Data;
using System.Windows;
using System.IO;
using Newtonsoft.Json;

namespace Calculator
{
    /// <summary>
    /// Калькулятор
    /// </summary>
    public class CalculatorClass
    {
        private const string HistoryFile = "history.json";

        public string Expression { get; set; } // Выражение
        private string Answer { get; set; } // Ответ

        /// <summary>
        /// Рассчёт выражения
        /// </summary>
        /// <returns></returns>
        public string GetAnswer()
        {
            try
            {
                Answer = new DataTable().Compute(Expression, null).ToString();
                if (!string.IsNullOrEmpty(Answer))
                {
                    SaveToHistory();
                }
                return Answer;
            }
            catch
            {
                MessageBox.Show(caption: "Ошибка", messageBoxText: "Неврный ввод!");
                return "Ошибка";
            }
        }

        /// <summary>
        /// Сохранение варадения в json файл
        /// </summary>
        private void SaveToHistory()
        {
            try
            {
                var history = LoadHistory();
                history.Add(new Dictionary<string, string>
                {
                    { "Expression", Expression },
                    { "Answer", Answer }
                });

                string json = JsonConvert.SerializeObject(history, Formatting.Indented);
                File.WriteAllText(HistoryFile, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}");
            }
        }


        /// <summary>
        /// Загрузка значений из json файла
        /// </summary>
        /// <returns></returns>
        public static List<Dictionary<string, string>> LoadHistory()
        {
            if (!File.Exists(HistoryFile))
                return new List<Dictionary<string, string>>();

            string json = File.ReadAllText(HistoryFile);
            return JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(json) ?? new List<Dictionary<string, string>>();
        }

        /// <summary>
        /// Очистка истории
        /// </summary>
        public static void ClearHistory()
        {
            File.WriteAllText(HistoryFile, "[]");
        }
    }
}