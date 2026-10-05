using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace WebbApiwithCQS.Domain.Entities;

[Table("Task")]
public class Tache
{
    public Tache()
    {

    }

    public Tache(int id, string titre, DateTime dateCreation, bool cloturee)
    {
        Id = id;
        Titre = titre;
        DateCreation = dateCreation;
        Cloturee = cloturee;
    }

    public int Id { get; set; }
    public string Titre { get; set; }

    [Column(TypeName = "datetime2(7)")]
    public DateTime DateCreation { get; set; }

    [Column(TypeName = "bit")]
    public bool? Cloturee { get; set; }
}