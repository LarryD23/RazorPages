using System.ComponentModel.DataAnnotations;

namespace Vote_Final.Models
{
    public class BallotIssueSelection
    {
        [Key]
        public int BallotIssueSelectionId { get; set; }

        [Required]
        public int BallotId { get; set; }
        [Required]
        public int VoterId { get; set; }

        [Required]
        public int BallotIssueId { get; set; }

        [Required]
        public bool IsFor { get; set; }

        [Required]
        public DateTime TimeStamp { get; set; }
    }
}
