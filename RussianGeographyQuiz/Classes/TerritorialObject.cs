namespace RussianGeographyQuiz.Classes
{
    internal class TerritorialObject
    {
        public string RussianName { get; set; }
        public string EnglishName { get; set; }
        public bool IfClicked { get; set; }
        public TerritorialObject(string russianName, string englishName)
        {
            RussianName = russianName;
            EnglishName = englishName;
            IfClicked = false;
        }
    }
}