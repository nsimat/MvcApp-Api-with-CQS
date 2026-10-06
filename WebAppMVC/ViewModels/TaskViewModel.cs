using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppMVC.ViewModels
{
    public class TaskViewModel
    {
        public int Id { get; set; }

        [DisplayName("Title")]
        [StringLength(100, ErrorMessage = "The Title field cannot exceed 100 characters.")]
        [Required(ErrorMessage = "The Title field is required.")]
        public string Title { get; set; }

        [Column(TypeName = "datetime2(7)")]
        [DisplayName("Date of Creation")]
        public DateTime DateCreation { get; set; }

        [Column(TypeName = "bit")]
        [DisplayName("IsClosed?")]
        public bool IsClosed { get; set; }

        public void DateCreationValidation(DateTime dateCreation)
        {
            if (dateCreation > DateTime.Now)
            {
                throw new ArgumentOutOfRangeException(nameof(dateCreation), "The date of creation cannot be in the future.");
            }
        }
    }
}
