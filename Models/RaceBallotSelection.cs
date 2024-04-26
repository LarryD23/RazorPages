using System.ComponentModel.DataAnnotations;

namespace Vote_Final.Models
{
    public class RaceBallotSelection
    {

       

        [Key]
        public int RaceBallotSelectionId { get; set; }

        [Required]
        public int BallotId { get; set; }

        [Required]
        public int RaceId { get; set; }

        [Required]
        public  int CandidateId { get; set; }

        [Required]
        public int VoterId { get; set; }

     

        [Required]
        public DateTime TimeStamp { get; set; } 
    }
}
