using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using MyFriends.Models;

namespace MyFriends.DAL
{
    public class DataLayer : DbContext
    {
        // בנאי המקבל מחרוזת חיבור ומעביר אותה לבנאי הבסיסי
        public DataLayer(string ConnectionString) : base(GetOptions(ConnectionString))
        {
            // יוצר את מסד הנתונים אם הוא לא קיים
            Database.EnsureCreated();
            // אם אין חברים במסד הנתונים, מפעיל את פונקציית הזריעה
            if (Friends.Count() == 0) Seed();
        }

        // פונקציית זריעה ליצירת נתונים ראשוניים
        private void Seed()
        {
            Friend friend = new Friend
            {
                FirstName = "ייטב",
                LastName = "גיל-עד",
                Phone = "0255",
                EmailAddress = "mail@mail.com"
            };
            Friends.Add(friend);
            SaveChanges();
        }

        // פונקציה פרטית ליצירת אפשרויות חיבור למסד הנתונים
        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(),
                connectionString).Options;
        }

        // הגדרת טבלאות במסד הנתונים
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}