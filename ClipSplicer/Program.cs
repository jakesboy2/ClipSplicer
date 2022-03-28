using System.Diagnostics;
using System.Text;

Console.WriteLine("Enter file path with today's videos");
string filePath = Console.ReadLine();
if (String.IsNullOrEmpty(filePath))
{
    Console.WriteLine("File Path null");
    return;
}

string[] videoFilePaths = Directory.GetFiles(filePath);
string txtFileName = filePath + "/list.txt";
if (File.Exists(txtFileName))
{
    File.Delete(txtFileName);
}

using(FileStream fs = File.Create(txtFileName))
{
    foreach (string videoFile in videoFilePaths)
    {
        var splits = videoFile.Split('\\');
        Byte[] line = new UTF8Encoding(true).GetBytes("file " + splits.Last() + "\n");
        fs.Write(line, 0, line.Length);
    }
}

string command = $"/C ffmpeg -safe 0 -f concat -i list.txt -c copy champclips.mp4";
ProcessStartInfo processStartInfo = new ProcessStartInfo();
processStartInfo.UseShellExecute = false;
processStartInfo.WorkingDirectory = filePath;
processStartInfo.FileName = "cmd.exe";
processStartInfo.Arguments = command;
Process.Start(processStartInfo);