namespace LawOfficeDesktopApp.Models.Entities
{
    public partial class User
    {
        /// <summary>
        /// https://www.cyberforum.ru/csharp-beginners/thread1830031.html
        /// </summary>
        public string ExperienceInYearsAsString
        {
            get
            {
                if (!ExperienceInYears.HasValue)
                {
                    return null;
                }
                string result = ExperienceInYears.ToString();
                int ageRemainder = ExperienceInYears.Value % 10;
                if (ageRemainder == 0) result += " лет";
                else if (ageRemainder == 1) result += " год";
                else result += " года";
                return result;
            }
        }
    }
}
