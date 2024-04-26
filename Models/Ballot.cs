using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voting_Final.Models
{
    public class Ballot
    {
        public int BallotId { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Ballot Name")]
        public string BallotName { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Date")]
        public DateTime BallotDate { get; set; }

        public virtual List<Race> Races { get; set; }

        [Required]
        public List<BallotIssue> BallotIssues { get; set; }

        //public ICollection<BallotLinkItem> BallotLinkItems { get; set; }


        public Ballot()
        {
            Races = new List<Race>();
            BallotIssues = new List<BallotIssue>();
        }

        public override string ToString()
        {
            return $"ID: {BallotId}, Election: {BallotName}, Date: {BallotDate}, Races: {Races}, BallotIssues: {BallotIssues}";
        }

    }
}
