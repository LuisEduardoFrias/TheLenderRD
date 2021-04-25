
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TheLenderRD.Domain.Dto;

namespace TheLenderRD.Domain.Entitys
{
    public class AgeRate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Age { get; set; }

        [Required]
        public decimal Rate { get; set; }
    }
}
