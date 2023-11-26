namespace AGDAR.Models.Status
{
    public class ProductType
    {
        public static string GetType(int? id)
        {
            switch (id)
            {
                case 0:
                    return "Nie określono";
                case 1:
                    return "Wiertarka";
                case 2:
                    return "Wkrętarka";
                case 3:
                    return "Szlifierka";
                case 4:
                    return "Frezarka";
                case 5:
                    return "Piła";
                case 6:
                    return "Kosiarka";
                case 7:
                    return "Piła tarczowa";
                case 8:
                    return "Wiertnica";
                case 9:
                    return "Piła szablasta";
                default:
                    return "Nie określono";
            }
        }
    }
}
