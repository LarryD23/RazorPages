using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Voting_Final.Models
{
    public class RaceCandidate
    {
        public int RaceCandidateId {  get; set; }

        public Race Race { get; set; }//added
        public int RaceId { get; set; } //unnulled

        public Candidate Candidate { get; set; }
        public int CandidateId { get; set; }    //unnulled
    }
}
