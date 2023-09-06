using System;
using System.IO;

namespace AvaTracer3.Gui.Models.Globals;

public static class CommonDirectories
{
    #if DEBUG
    public static string RootDataPath => 
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                     "AvaTracer 3", "GUI", "Debug");
    #else
    public static string RootDataPath => 
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                     "AvaTracer 3", "GUI");
    #endif
    
    public static string LogsDataPath => Path.Combine(RootDataPath, "Logs");

    public static string TempDocumentDataPath => Path.Combine(Path.GetTempPath(), "AvaTracer 3");

    public static void CreateRequiredDirectories()
    {
        // Logs data path is automatically created by ILogger. - Comment by Matt Heimlich on 07/11/2023@12:54:05
        Directory.CreateDirectory(RootDataPath);
        Directory.CreateDirectory(TempDocumentDataPath);
    }

    public static void CleanUp()
    {
        if ( Directory.Exists(TempDocumentDataPath) )
        {
            Directory.Delete(TempDocumentDataPath, true);
        }
    }
}