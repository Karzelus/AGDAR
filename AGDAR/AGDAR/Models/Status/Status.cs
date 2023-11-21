namespace AGDAR.Models.Status
{
    public class Status
    {
        public static string GetStatus(int? id)
        {
            switch (id)
            {
                case 0:
                    return "Zgłoszone";
                case 1:
                    return "Odebrane";
                case 2:
                    return "Wycenione";
                case 3:
                    return "Odrzucone";
                case 4:
                    return "Zaakceptowane";
                default:
                    return "Nieznany Status";
            }
        }
    }
}
