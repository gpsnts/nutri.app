using System.ComponentModel;

namespace NutriApp.Models
{
    public class FoodConsumida
    {
        public int Id { get; set; }
        [DisplayName("Id do alimento")]
        public int idFood { get; set; }
        public int idAlimentacao { get; set; }
        [DisplayName("Quantidade")]
        public int quantidade { get; set; }

        public List<Food>? ListaDeAlimentos { get; set; }
        public Food? food { get; set; }
    }
}
