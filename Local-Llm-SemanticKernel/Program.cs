using UglyToad.PdfPig;

namespace Local_Llm;

class Program
{
    private static readonly string _path = @"K:\FileWatcher\business";
    public static async Task Main(string[] args)
    {
        await Run();
    }

    private static Task Run()
    {
        using var watcher = new FileSystemWatcher();
        watcher.Path = _path;
        watcher.Created += OnCreated;

        watcher.EnableRaisingEvents = true;

        Console.WriteLine("Drücken Sie 'q' zum Beenden.");
        while (Console.Read() != 'q')
        {
        }

        return Task.CompletedTask;
    }
    private static async void OnCreated(object sender, FileSystemEventArgs e)
    {
        if (!File.Exists(e.FullPath))
        {
            return;
        }
        await Task.Delay(500);
        Console.WriteLine($"Datei erstellt: {e.FullPath}");
        string text;
        using (var pdf = PdfDocument.Open(e.FullPath))
        {
            var page = pdf.GetPage(1);
            text = page.Text;
        }

        var docHandler = new DocumentHandler();
        var doc = await docHandler.GetDocumentFromAi(text);
        
        // erstelle den ordner, wenn er nicht existiert über die property Company
        var path = Path.Combine(_path, doc.Company);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        // erstelle den ordner, wenn er nicht existiert über die property Category in den ordner Company
        path = Path.Combine(path, doc.Category);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        // verschiebe die Datei in den Ordner Category
        File.Move(e.FullPath, Path.Combine(path, Path.GetFileName(e.FullPath)));
    }
}