using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace AquaMarket.Models
{
    public class Purchase
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Purchase amount")]
        [UIHint("NullableAmountTemplate")]
        [Range(typeof(decimal), "5,0", "20,6", ErrorMessage = "Min cost is 5, max cost is 20,5. Double separator is ','")]
        public decimal? Amount{ get; set; }

        public DateTime PurchaseDate { get; set; }

        

        public int[] SelectedPlantIDs { get; set; }
        public virtual ICollection<Plant> Plants { get; set; }

        public string Status { get; set; }

        public string FilePath { get; set; }

        public Purchase()
        {
            SelectedPlantIDs = new[] { 1 };
            Plants = new HashSet<Plant>();
        }
    }
}