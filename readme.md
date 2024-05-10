Nach einer Idee von [Gregor Biswanger](https://github.com/GregorBiswanger) und [myCodingZone](https://www.meetup.com/de-DE/My-Coding-Zone/)  

# Anleitung für das Ai-File-Watcher Programm

Dieses Programm verwendet Azure OpenAI, um PDF-Dokumente zu kategorisieren und entsprechend zu sortieren. Es überwacht einen bestimmten Ordner und wenn ein neues PDF-Dokument hinzugefügt wird, liest es den Text des Dokuments, sendet ihn an Azure OpenAI zur Kategorisierung und verschiebt das Dokument dann in einen entsprechenden Unterordner.

## Voraussetzungen

- .NET 5.0 oder höher
- Azure OpenAI Konto
- OpenAI Konto

## Schritte zur Einrichtung

1. Klone das Repository und öffne das Projekt in JetBrains Rider oder einer anderen .NET-kompatiblen IDE.

2. Stelle sicher, dass du die notwendigen Azure OpenAI und OpenAI Anmeldedaten hast. Du benötigst den OpenAI Endpunkt, den OpenAI Schlüssel und den Namen der Bereitstellung.

3. Setze die User Secrets in deinem Projekt. Du kannst dies tun, indem du das folgende Kommando in der Terminalansicht deiner IDE ausführst:

```bash
dotnet user-secrets set "OpenAiEndpoint" "<dein-openai-endpunkt>"
dotnet user-secrets set "OpenAiKey" "<dein-openai-schlüssel>"
dotnet user-secrets set "DeploymentName" "<dein-bereitstellungsname>"
```
Ersetze <dein-openai-endpunkt>, <dein-openai-schlüssel> und <dein-bereitstellungsname> durch deine tatsächlichen Anmeldedaten.

# Starte das Programm.
Es wird nun den angegebenen Ordner überwachen und jedes neu hinzugefügte PDF-Dokument kategorisieren und entsprechend verschieben.
Bitte beachte, dass das Programm derzeit nur mit Azure OpenAI oder OpenAI funktioniert. Es ist nicht mit anderen KI-Diensten kompatibe