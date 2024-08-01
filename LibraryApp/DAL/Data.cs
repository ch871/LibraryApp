
namespace LibraryApp.Models
{
    public class Data
    {
        static Data getData;
        string connectionString = "server = DESKTOP-VQMHHMF\\SQLEXPRESS; initial catalog = LubaryDb; user id=sa;password=1234; TrustServerCertificate=Yes";
        //יצירת החיבור רק פעם אחת
        private Data()
        {
            Layer = new DataLayer(connectionString);
        }
        //דרך הגישה לחיבור
        public static DataLayer Get
        {
            get
            {
                //אם החיבור לא קיים תייצר אותו
                if (getData == null)
                {
                    getData = new Data();
                }
                //אם קיים החיבור תחזיר אותו
                return getData.Layer;
            }
        }
        //משתנה שיכיל את החיבור למסד
        public DataLayer Layer { get; set; }
    }
}
