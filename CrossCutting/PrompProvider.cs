namespace Ai_File_Watcher;

public static class PrompProvider
{
    public static string GetPrompt(string fileText)
    {
        return "Dein Job ist es meinen Inhalt zu Kategorisieren.\ndie Kategorien sind:\n-Rechnung\n-Angebot\n-Sonstiges\n\nDer Eigene Firmenname ist \"Fantasie Webdesign GmbH\", Ermittle den Firmenname vom Kunden.\n\nDu Antwortest ausschließlich nur mit einem JSON-Datensatz:\n{\n \"category\":\"Rechnung\",\n \"company\":\"Test Gmbh\"\n}\n###\n" + fileText + "\n";
    }
}