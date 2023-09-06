using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace AvaTracer3.Gui.Models.Globals;

public static class CommonFiles
{
    public static string LogFilePath => Path.Combine(CommonDirectories.LogsDataPath, "activity.log");
}