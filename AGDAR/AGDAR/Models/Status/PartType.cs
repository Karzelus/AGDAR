namespace AGDAR.Models.Status
{
    public class PartType
    {
        public static string GetType(int id)
        {
            switch (id)
            {
                case 0:
                    return "Nie określono";
                case 1:
                    return "Głowica";
                case 2:
                    return "Rączka";
                case 3:
                    return "Bateria";
                case 4:
                    return "Ładowarka";
                case 5:
                    return "Przekładnia";
                case 6:
                    return "Łożysko";
                case 7:
                    return "Wyłącznik bezpieczeństwa";
                case 8:
                    return "Uchwyt";
                case 9:
                    return "Silnik";
                default:
                    return "Nie określono";
            }
        }
    }
}
