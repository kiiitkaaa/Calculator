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

        public string Expression { get; set; } = "";

        /// <summary>
        /// Рассчёт выражения
        /// </summary>
        /// <returns></returns>
        public string GetAnswer()
        {
            try
            {
                var result = new DataTable().Compute(Expression, null).ToString();
                if (!string.IsNullOrEmpty(result))
                {
                    SaveToHistory(Expression, result);
                }
                return result;
            }
            catch
            {
                MessageBox.Show("Неверный ввод!", "Ошибка");
                return "Ошибка";
            }
        }

        /// <summary>
        /// Сохранение варадения в json файл
        /// </summary>
        private static void SaveToHistory(string expression, string answer)
        {
            try
            {
                var history = LoadHistory();
                history.Add(new Dictionary<string, string>
                {
                    { "Expression", expression },
                    { "Answer", answer }
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
                return [];

            string json = File.ReadAllText(HistoryFile);
            return JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(json) ?? [];
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