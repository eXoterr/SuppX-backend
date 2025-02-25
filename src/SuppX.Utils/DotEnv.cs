using System.Diagnostics;

namespace SuppX.Utils;

public static class DotEnv
{
    const string NOT_FOUND = "file not found";

    public static void Read(string path = ".env")
    {
        if(!File.Exists(path))
        {
            Debug.Print(NOT_FOUND);
            return;
        }
        
        foreach (string line in File.ReadAllLines(path))
        {
            if(line is null)
            {
                continue;
            }

            string[] parts = line.Split("=");

            if(parts.Length != 2)
            {
                continue;
            }
            
            Environment.SetEnvironmentVariable(parts[0], parts[1]);
        }
    }
}
