using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutriApp.Models
{
    public class Food
    {
        public int Id { get; set; }

        [DisplayName("Nome do alimento")]
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "campo obrigatório")]
        public string nome { get; set; }
        
        
        [DisplayName("Qtd. de Proteinas")]
        public decimal? proteinas { get; set; }

        [DisplayName("Qtd. Gorduras Totais")]
        public decimal? gordurasTotais { get; set; }

        [DisplayName("Qtd. Carboidratos")]
        public decimal? carboidrato { get; set; }

        [DisplayName("Qtd. Calcio")]
        public decimal? calico { get; set; }

        [DisplayName("Qtd. Ferro")]
        public decimal? ferro { get; set; }

        [DisplayName("Qtd. Fósforo")]
        public decimal? fosforo { get; set; }

        [DisplayName("Qtd. Vitamina A")]
        public decimal? vitaminaA { get; set; }

        [DisplayName("Valor energético")]
        public decimal? valorEnergetico { get; set; }
        
        [DisplayName("Porção em gramas")]
        public decimal? porcao { get; set; }

    }

   
}
