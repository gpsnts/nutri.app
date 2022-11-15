using System.ComponentModel;

namespace NutriApp.Models
{
    public class AlimentacaoDia
    {
        public int Id { get; set; }

        [DisplayName("UserName")]
        public string? UserName { get; set; }

        [DisplayName("Total Calorias")]
        public string? TotalCalorias { get; set; }
        [DisplayName("Dia da semana")]
        public string? Dia { get; set; }

        [DisplayName("Data")]
        public DateTime? DiaDate { get; set; } = DateTime.Now;
        
    }
  
}
