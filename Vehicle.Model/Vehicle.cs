using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle.Model
{
    public class vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage ="Registration No is required!!")]
        public string RegNo { get; set; }
        [Required(ErrorMessage = "Making year is required!!")]
        public string Make { get; set; }
        [Required(ErrorMessage = "Model No is required!!")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Vehicle Color is required!!")]
        public string Color { get; set; }
        [Required(ErrorMessage = "Engine No is required!!")]

        public string EngineNo { get; set; }
        [Required(ErrorMessage = "Chasis No is required!!")]

        public string ChasisNo { get; set; }

        public String DateOfPurchase { get; set; }

        public bool Active { get; set; }


    }
}
