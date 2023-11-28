namespace AGDAR.Models.Status
{
    public class ProductState
    {
        public static string GetState(int id)
        {
            switch (id)
            {
                case 0:
                    return "Inne";
                case 1:
                    return "Stworzone";
                case 2:
                    return "Wycenione";
                case 3:
                    return "Zaakceptowane";
                case 4:
                    return "Ukończone";
                default:
                    return "Nieznany Status";
            }
        }
    }
}
