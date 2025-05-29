namespace AppsCLI
{
    internal class App
    {
        public string? AppName { get; set; }
        public Category? Category { get; set; }
        public ContentRating? ContentRating { get; set; }
        public double Rating { get; set; }
        public int Reviews { get; set; }
        public string? CurrentVer { get; set; }
        public int UpdateYear { get; set; }
        public int UpdateMonth { get; set; }

        public override string ToString() =>
            $"{AppName} {ConvertToStars()}";

        private string ConvertToStars()
        {
            if (Rating < 0.1) return "-";
            int starCount = (int)Math.Round(Rating);
            return new string('*', starCount);
        }

        public static List<App> LoadFromCsv(string filePath)
        {
            var apps = new List<App>();
            using StreamReader sr = new(filePath);
            var _ = sr.ReadLine();

            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                var values = line.Split(';');

                var ratingString = values[5].Replace('.', ',');

                var app = new App
                {
                    AppName = values[0],
                    Category = new Category
                    {
                        CategoryId = int.Parse(values[1]),
                        CategoryName = values[2],
                    },
                    ContentRating = new ContentRating
                    {
                        ContentRatingId = int.Parse(values[3]),
                        ContentRatingName = values[4],
                    },
                    Rating = double.Parse(ratingString),
                    Reviews = int.Parse(values[6]),
                    currentVer = values[7],
                    UpdateYear = int.Parse(values[8]),
                    UpdateMonth = int.Parse(values[9]),
                };
                apps.Add(app);
            }

            return apps;
        }
    }
}
