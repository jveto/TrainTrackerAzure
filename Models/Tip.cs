using System;
using System.ComponentModel.DataAnnotations;

namespace TrainTracker.Models
{
    public class Tip
    {
        [Key]
        public int TipId { get; set; }

        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        [MinLength(8)]
        public string Comment { get; set; }

        public DateTime Date { get; set; }
    }
}