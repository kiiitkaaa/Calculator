using System.Data;
using System.Windows;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Windows.Forms;

namespace Calculator
{
    /// <summary>
    /// Калькулятор
    /// </summary>
    public class CalculatorClass
    {
        private const string HistoryFile = "history.json";
        private string lastAnswer = "";

        public string Expression { get; set; } = "";

        /// <summary>
        /// Вычислить выражение и сохранить результат
        /// </summary>
        public void Evaluate()
        {
            try
            {
                lastAnswer = new DataTable().Compute(Expression, null).ToString();
                if (!string.IsNullOrEmpty(lastAnswer))
                {
                    SaveToHistory(Expression, lastAnswer);
                }
            }
            catch
            {
                lastAnswer = "Error";
            }
        }

        /// <summary>
        /// Вернуть последний результат без повторного вычисления
        /// </summary>
        public string GetAnswer() => lastAnswer;

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