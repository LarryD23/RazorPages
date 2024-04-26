using System.ComponentModel.DataAnnotations;
using Voting_Final.Models;

namespace Voting_Final.Models
{
    public class Race
    {
        public int RaceId { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Race")]
        public string RaceName { get; set; }

        public bool IsPartisan {  get; set; }

        //public virtual Ballot Ballot { get; set; }

        //public virtual List<Candidate> Candidates { get; set; }

        //public ICollection<BallotLinkItem> BallotLinkItems { get; set; }

        //public RaceCandidate RaceCandidate { get; set; }    
        //public ICollection<RaceCandidate> RaceCandidates { get; set; }
        
        
        //[Display(Name = "RaceCandidateID")]
        //public int RaceCandidateId { get; set; }

        public Ballot Ballot { get; set; }

        [Required]
        public int BallotId { get; set; }   //un-nulled



        //public Race()
        //{
        //    Candidates = new List<Candidate>();
        //}

        public override string ToString()
        {
            return $"ID: {RaceId}, Race: {RaceName},";
        }


    }
}
