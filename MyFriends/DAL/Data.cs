namespace MyFriends.DAL
{
    public class Data
    {
        // מחרוזת החיבור למסד הנתונים
        string ConnectionString = "server=YITAV\\SQLEXPRESS;initial catalog=MyFriends; user id=sa;" +
            "password=1234;TrustServerCertificate=Yes";

        static Data _data;

        private DataLayer DataLayer;
        // בנאי פרטי למניעת יצירת מופעים מרובים
        private Data()
        {
            DataLayer = new DataLayer(ConnectionString);
        }


        // מאפיין סטטי לקבלת מופע יחיד של DataLayer (Singleton)
        public static DataLayer GetData
        {
            get
            {
                // אם אין מופע קיים, יצור אחד חדש
                if (_data == null) _data = new Data();
                return _data.DataLayer;
            }
        }
    }
}