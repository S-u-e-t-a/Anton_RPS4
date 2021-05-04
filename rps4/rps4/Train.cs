using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rps4
{
    [Table("trains")]
    public class Train
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public string Station_dep { get; set; }
        public string Station_arr { get; set; }
        public int Cost { get; set; }
    }
}
