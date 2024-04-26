using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Voting_Final.Models
{
    public class BallotIssue
    {
        public int BallotIssueId { get; set; }

        
        [Required]
        [StringLength(255)]
        [Display(Name = "Title")]
        public string Title {  get; set; }

        [Required]
        [StringLength(5000)]//check real ballots to see actual length 
        [Display(Name = "Description")]
        public string Description { get; set; } 

        //public bool Options { get; set; }

        public string YesOption { get; set; }

        public string NoOption { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Type")]//initiative, referendum etc. 
        public string Type { get; set; }

        public Ballot Ballot { get; set; }

        public int BallotId { get; set; }

        //public ICollection<BallotLinkItem> BallotLinkItems { get; set; }


        //public BallotIssue()
        //{
        //    Options = new List<string>();
        //}

        public override string ToString()
        {
            return $"ID: {BallotIssueId}, Title: {Title}, Description: {Description}";
        }
    }
}
