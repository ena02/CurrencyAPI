using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurrencyAPI.Model
{
    public class Currency
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string Title { get; set; }

        [Required]
        [MaxLength(3)]
        public string Code { get; set; }

        [Required]
        [Column(TypeName = "numeric(18,2)")]
        public decimal Value { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime ADate { get; set; }
    }
}
