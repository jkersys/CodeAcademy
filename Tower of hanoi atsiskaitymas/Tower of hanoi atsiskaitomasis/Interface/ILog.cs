namespace TowerOfHanoi.Interface
{
    public interface ILog
    {
        void Log(string[] data);
        string[] CreateCsvLogText(string[] data);
        string CreateHtmlLogText(string[] data);
        string CreateTxtLogText(string[] data);
    }
}
