using System.Text;
using TowerOfHanoi.Interface;
using TowerOfHanoi.Models;

namespace TowerOfHanoi.Service
{
    public class Logger : ILog
    {
        private bool WriteLogToCsv { get; set; } = false;
        private bool WriteLogToTxt { get; set; } = false;
        private bool WriteLogToHtml { get; set; } = false;

        private string LogDir { get; set; }

        private string CsvLogPath;

        private string HtmlLogPath;

        private string TxtLogPath;

        private Dictionary<string, string> numberToSourceName = new Dictionary<string, string>
        {
            {"1", "pirmo" },
            {"2", "antro"},
            {"3", "trecio"},
            {"4", "ketvirto"}
        };

        private Dictionary<string, string> numberToDestinationName = new Dictionary<string, string>
        {
            {"1", "pirmą" },
            {"2", "antrą"},
            {"3", "trecią"},
            {"4", "ketvirtą"}
        };

        public Logger(bool logToCsv, bool logToTxt, bool logToHtml)
        {
            WriteLogToCsv = logToCsv;
            WriteLogToTxt = logToTxt;
            WriteLogToHtml = logToHtml;

            LogDir = Path.Combine(Environment.CurrentDirectory, "Logs");
            CsvLogPath = Path.Combine(LogDir, "log.csv");
            HtmlLogPath = Path.Combine(LogDir, "log.html");
            TxtLogPath = Path.Combine(LogDir, "txt.txt");
        }
                public string HtmlLog(string[] data)
        {
            StringBuilder htmlToLogData = new StringBuilder();
            htmlToLogData.AppendLine($"<tr>");
            htmlToLogData.AppendLine($"<td>{data[0]}</td>");
            htmlToLogData.AppendLine($"<td>{data[1]}</td>");
            htmlToLogData.AppendLine($"<td>{data[2]}</td>");
            htmlToLogData.AppendLine($"<td>{data[3]}</td>");
            htmlToLogData.AppendLine($"<td>{data[4]}</td>");
            htmlToLogData.AppendLine($"<td>{data[5]}</td>");
            htmlToLogData.AppendLine($"</tr>");
            htmlToLogData.Append($"</table>");
            return htmlToLogData.ToString();
        }



        public void Log(string[] data)
        {
            if (!Directory.Exists(LogDir))
            {
                Directory.CreateDirectory(LogDir);
            }
            if (WriteLogToCsv)
            {
                if (!File.Exists(CsvLogPath))
                {
                    File.WriteAllLines(CsvLogPath,  new[] { "zaidimo_pradzios_data, ejimo_nr, disko_1_vieta, disko_2_vieta, disko_3_vieta, disko_4_vieta" });
                }
                File.AppendAllLines(CsvLogPath, CreateCsvLogText(data));
            }

            if (WriteLogToHtml)
            {
                if (!File.Exists(HtmlLogPath))
                {
                    File.WriteAllLines(HtmlLogPath, new[] { "<table border>\n<tr>\n<th>ŽAIDIMO PRADŽIOS DATA</th>\n<th>ĖJIMO NR</td>\n<th>DISKO 1 VIETA</th>\n<th>DISKO 2 VIETA</th>\n<th>DISKO 3 VIETA</th>\n<th>DISKO 4 VIETA</th>\n</tr>" });
                }
                else{

                    var currentLogLines = File.ReadAllLines(HtmlLogPath);
                    if (currentLogLines.Last().Contains("</table>"))
                    {
                        File.WriteAllLines(HtmlLogPath, currentLogLines.Take(currentLogLines.Length - 1));
                    }
                }

                File.AppendAllLines(HtmlLogPath, new[] { HtmlLog(data) });
            }

            if (WriteLogToTxt)
            {
                File.AppendAllLines(TxtLogPath, new[] {  $"žaidime kuris pradėtas {data[0]}, ėjimu nr {data[1]} {data[6]} dalių diskas " +
            $"buvo paimtas iš {numberToSourceName[data[7]]} sulpelio ir padėtas į {numberToDestinationName[data[8]]}"
            });
            }
        }

      

        public string CreateHtmlLogText(string[] data)
        {
            StringBuilder htmlToLogData = new StringBuilder();
            htmlToLogData.AppendLine($"<tr>");
            htmlToLogData.AppendLine($"<td>{data[0]}</td>");
            htmlToLogData.AppendLine($"<td>{data[1]}</td>");
            htmlToLogData.AppendLine($"<td>{data[2]}</td>");
            htmlToLogData.AppendLine($"<td>{data[3]}</td>");
            htmlToLogData.AppendLine($"<td>{data[4]}</td>");
            htmlToLogData.AppendLine($"<td>{data[5]}</td>");
            htmlToLogData.AppendLine($"</tr>");
            htmlToLogData.Append($"</table>");
            return htmlToLogData.ToString();
        }

        public string CreateTxtLogText()
        {
            throw new NotImplementedException();
        }

        public string[] CreateCsvLogText(string[] data)
        {
            return new[] { $"{data[0]},{data[1]},{data[2]},{data[3]},{data[4]},{data[5]}" };
        }

        public string CreateTxtLogText(string[] data)
        {
            throw new NotImplementedException();
        }
    }
}
