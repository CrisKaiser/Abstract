using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace AbstractApp
{
    public static class ProjektManager
    {

        private static string dateipfad = Path.Combine(
        Properties.Settings.Default.Speicherort,
        "projekte.json"
        );

        static ProjektManager()
        {
            // Erstelle das Verzeichnis, falls es nicht existiert
            string verzeichnis = Path.GetDirectoryName(dateipfad);
            if (!Directory.Exists(verzeichnis))
            {
                Directory.CreateDirectory(verzeichnis);
            }
        }

        public static List<Projekt> LadeProjekte()
        {
            if (!File.Exists(dateipfad))
                return new List<Projekt>();

            string json = File.ReadAllText(dateipfad);
            return JsonSerializer.Deserialize<List<Projekt>>(json) ?? new List<Projekt>();
        }

        public static void SpeichereProjekte(List<Projekt> projekte)
        {
            string json = JsonSerializer.Serialize(projekte, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(dateipfad, json);
        }

        public static void NeuesProjektHinzufuegen(string name)
        {
            var projekte = LadeProjekte();
            projekte.Add(new Projekt { Name = name, Erstellungsdatum = DateTime.Now });
            SpeichereProjekte(projekte);
        }
    }
}
