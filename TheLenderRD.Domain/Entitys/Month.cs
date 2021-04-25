
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TheLenderRD.Domain.Dto;

namespace TheLenderRD.Domain.Entitys
{
    public class Month
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]        
        public int Id { get; set; }

        [Column(TypeName = "varchar(15)")]
        [Required]
        public string Description { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Value { get; set; }
    }
}
