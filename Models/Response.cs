using System.ComponentModel.DataAnnotations;

namespace BeautifulNumbers.Models
{
    public class Response
    {
        [Required(ErrorMessage = "Please select a number system")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a number system")]
        public int Radix { get; set; }

        [Required(ErrorMessage = "Please select a number of symbols")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a number of symbols")]
        public int Extent { get; set; }

        public string Name { get; set; }

        public Number BeautifulNumber { get; set; }
    }
}
