using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace SuppX.Core;

public static class DotEnv
{
    const string NOT_FOUND = "file not found";

    public static void Read(string path = ".env", ILogger? logger = null)
    {
        if(!File.Exists(path))
        {
            if(logger is not null)
            {
                logger.LogError(NOT_FOUND);
            }else
            {
                Debug.Print(NOT_FOUND);
            }
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
