using Microsoft.EntityFrameworkCore;
using Voting_Final.Models;

namespace Vote_Final.ViewModels
{
    public class BallotModel
    {
        public Ballot Ballot { get; set; }

        public List<RaceModel> RaceModels { get; set; }

        public List<BallotIssue> BallotIssues { get; set; }

        public static BallotModel BallotModelRefactory(Ballot ballot, DomainContext dbContext)
        {
            //retrieve ballot then retrieve races, candidates, build race models get ballotissues. create instance of this
            //ballotmodel and then retrieve, and that is what will be the return as well
        }
    }
}
