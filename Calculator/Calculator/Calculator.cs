using System.Data;
using System.Windows;
using System.IO;
using Newtonsoft.Json;

namespace Calculator
{
    public class CalculatorClass
    {
        private const string HistoryFile = "history.json";

        public string Expression { get; set; }
        private string Answer { get; set; }

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
            catch (Exception ex)
            {
                MessageBox.Show(caption: "Ошибка", messageBoxText: $"{ex}");
                return "долбоёб";
            }
        }

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

        public static List<Dictionary<string, string>> LoadHistory()
        {
            if (!File.Exists(HistoryFile))
                return new List<Dictionary<string, string>>();

            string json = File.ReadAllText(HistoryFile);
            return JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(json) ?? new List<Dictionary<string, string>>();
        }

        public static void ClearHistory()
        {
            File.WriteAllText(HistoryFile, "[]");
        }
    }
}