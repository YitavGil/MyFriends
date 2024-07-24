using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFriends.Models
{
    public class Friend
    {
        public Friend() { Images = new List<Image>(); }
        [Key]
        public int ID { get; set; }

        [Display(Name = "שם פרטי")]
        public string FirstName { get; set; } = "";
        [Display(Name = "שם משפחה")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "שם מלא"), NotMapped]
        public string FullName { get { return FirstName + " " + LastName; } }

        [Display(Name = "כתובת מייל")]
        [EmailAddress(ErrorMessage = "כתובת מייל אינה נכונה")]
        public string EmailAddress { get; set; }

        [Display(Name = "מספר טלפון"), Phone]
        public string Phone { get; set; } = string.Empty;

        public List<Image> Images { get; set; }

        [Display(Name = "הוספת תמונה"), NotMapped]
        public IFormFile SetImage
        {
            get { return null; }
            set
            {
                if (value == null) return;
                UploadImage(value);
            }
        }

        public void UploadImage(IFormFile image)
        {
            //יצירת מקום בזיכרון המכיל קובץ
            MemoryStream stream = new MemoryStream();
            image.CopyTo(stream);
            //הפיכת המקום בזיכרון לבייטים
            SaveImage(stream.ToArray());
        }
        public void SaveImage(byte[] image)
        {
            Images.Add(new Image { MyImage = image, Friend = this });
        }
    }
}