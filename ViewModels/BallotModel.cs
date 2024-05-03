using Microsoft.EntityFrameworkCore;
using Voting_Final.Models;

namespace Vote_Final.ViewModels
{
    public class BallotModel
    {
        public Ballot Ballot { get; set; }

        public List<RaceModel> RaceModels { get; set; }

        public List<BallotIssue> BallotIssues { get; set; }

        public Dictionary<string, bool> IsFors { get; set; }

        public static BallotModel BallotModelRefactory(Ballot ballot, DomainContext dbContext)
        {
            var ballotModel = new BallotModel();

            //Fetch ballot and related data
            var fetchedBallot = dbContext.Ballot
                .Include(b => b.Races)
                .Include(b => b.BallotIssues)
                .FirstOrDefault(b => b.BallotId == ballot.BallotId);

            if (fetchedBallot != null)
            {
                //populate ballot property
                ballotModel.Ballot = fetchedBallot;

                ballotModel.RaceModels = fetchedBallot.Races.Select(r => new RaceModel 
                {
                    Race = r,
                    Candidates = dbContext.RaceCandidate
                    .Include(rc => rc.Candidate)
                    .Where(rc => rc.RaceId == r.RaceId)
                    .Select(rc => rc.Candidate) 
                    .ToList()
                }).ToList(); 

                //populate the ballotIssues in the BallotModel
                ballotModel.BallotIssues = fetchedBallot.BallotIssues.ToList();
                //use ballotissue_yes, 

                //initialize IsFors Dictionary
                ballotModel.IsFors = new Dictionary<string, bool>();

                //loop through ballotissues and add entries to dictionary 
                foreach (var issue in fetchedBallot.BallotIssues )
                {
                    ballotModel.IsFors[$"ballotIssue{issue.BallotIssueId}_Yes" ] = true;
                    ballotModel.IsFors[$"ballotIssue{issue.BallotIssueId}_No"] = false;
                }
            }

            return ballotModel;
            
            
        }
    }
}
