using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppMVC.Models
{
    public class Tache
    {
        public int Id { get; set; }
        public string Titre { get; set; }

        [Column(TypeName = "datetime2(7)")]
        [DisplayName("Date of Creation")]
        public DateTime DateCreation { get; set; }

        [Column(TypeName = "bit")]
        [DisplayName("IsClosed?")]
        public bool? Cloturee { get; set; }
    }
}
