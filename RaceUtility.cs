using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Voting_Final.Models;

namespace Vote_Final.Utilities
{
    public static class RaceUtility
    {
        //public static (Ballot ballot, List<Race> races) GetBallotAndRaces(DomainContext context, int ballotId)
        //{
        //    var ballot = context.Ballot.FirstOrDefault(b => b.BallotId == ballotId);
        //    if (ballot == null)
        //    {
        //        throw new ArgumentException("Invalid BallotId");
        //    }

        //    var races = context.Race.Where(r => r.BallotId == ballotId).ToList();

        //    return (ballot, races);
        //}

        public static Ballot GetBallotById(DomainContext context, int ballotId)
        {
            return context.Ballot.FirstOrDefault(b => b.BallotId == ballotId);
        }

        public static async Task<int> GetBallotIssueId(DomainContext context, int ballotId, int raceId)
        {
            // Get the race associated with the raceId
            var race = await context.Race.FirstOrDefaultAsync(r => r.RaceId == raceId && r.BallotId == ballotId);
            if (race == null)
            {
                throw new ArgumentException("Invalid RaceId or BallotId");
            }

            // Get the corresponding ballot issue for the race
            var ballotIssue = await context.BallotIssue.FirstOrDefaultAsync(bi => bi.BallotId == ballotId && bi.Type == race.RaceName);
            if (ballotIssue == null)
            {
                throw new ArgumentException("Corresponding BallotIssue not found");
            }

            return ballotIssue.BallotIssueId;
        }

        public static Dictionary<int, List<int>> GetCandidatesForRaces(DomainContext context, int ballotId)
        {
            // Get the ballot using GetBallotById
            var ballot = GetBallotById(context, ballotId);
            if (ballot == null)
            {
                throw new ArgumentException("Invalid BallotId");

            }

            var races = context.Race.Where(r => r.BallotId == ballot.BallotId).ToList();

            var raceIds = races.Select(r => r.RaceId).ToList();


            var raceCandidates = context.RaceCandidate
            .Where(rc => raceIds.Contains(rc.RaceId) && rc.CandidateId != null)
            .GroupBy(rc => rc.RaceId)
            .ToDictionary(
                group => group.Key,
                group => group.Select(rc => rc.CandidateId).ToList() // Select CandidateId from each group
            );

            return raceCandidates;
        }


        public static Dictionary<int, List<BallotIssue>> GetBallotIssuesForBallots(DomainContext context, int ballotId)
        {
            // Get the ballot using GetBallotById
            var ballot = GetBallotById(context, ballotId);
            if (ballot == null)
            {
                throw new ArgumentException("Invalid BallotId");
            }

            // Get issues associated with the given ballotId
            var ballotIssues = context.BallotIssue.Where(bi => bi.BallotId == ballot.BallotId).ToList();

            // Initialize the dictionary to hold BallotIssues for each ballot
            var ballotIssuesForBallots = new Dictionary<int, List<BallotIssue>>();

            // Loop through each issue to get its associated BallotIssues
            foreach (var issue in ballotIssues)
            {
                // Get BallotIssues for the current issue
                var issuesForBallot = context.BallotIssue
                    .Where(bi => bi.BallotId == issue.BallotId)
                    .ToList();

                // Check if the key already exists before adding
                if (!ballotIssuesForBallots.ContainsKey(issue.BallotId))
                {
                    ballotIssuesForBallots.Add(issue.BallotId, issuesForBallot);
                }
            }

            return ballotIssuesForBallots;
        }





        //public static Candidate GetCandidateDetails(DomainContext context, int candidateId)
        //{
        //    return context.Candidate.FirstOrDefault(c => c.CandidateId == candidateId);
        //}

    }
}
