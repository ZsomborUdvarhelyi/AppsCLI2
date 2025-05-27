using AppsCLI;

List<App> apps = App.LoadFromCsv(@"..\..\..\src\apps.csv");

Console.WriteLine("6. feladat - Kiírás");
for (int i = 0; i < apps.Count; i++)
{
    Console.WriteLine($"{i + 1}. {apps[i].ToString()}");
}

var maxYear = apps.Max(a => a.UpdateYear);
var maxMonth = apps.Where(a => a.UpdateYear == maxYear).Max(a => a.UpdateMonth);

var filtered = apps
    .Where(a => a.ContentRating?.ContentRatingName == "Everyone"
             && a.Category?.CategoryName == "PHOTOGRAPHY"
             && a.UpdateYear == maxYear
             && a.UpdateMonth == maxMonth)
    .ToList();

Console.WriteLine($"\n7. feladat - Válogatás\nFrissítve: {maxYear}.{maxMonth}");
foreach (var app in filtered) Console.WriteLine($"{app.ToString()}");

Console.WriteLine("\n8. feladat - Adatok fájlba írása");
using (StreamWriter sw = new StreamWriter(@"..\..\..\src\bests.txt"))
{
    foreach (var app in apps.OrderByDescending(a => a.Rating))
    {
        string rating = app.Rating > 0 ? app.Rating.ToString() : "NO RESULT";
        sw.WriteLine($"{rating}\t{app.AppName}-{app.currentVer}");
    }
}