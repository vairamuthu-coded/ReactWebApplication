namespace ReactWebApplication.Models.Masters
{
    public class Master
    {
        public string getAbbreviatedName(int month)
        {
            DateTime date = new DateTime(System.DateTime.Now.Year, month, System.DateTime.Now.Day);
            return date.ToString("MMM");
        }

    }
}
