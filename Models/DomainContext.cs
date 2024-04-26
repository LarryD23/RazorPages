using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using Vote_Final.Models;
using Voting_Final.Models;


namespace Voting_Final.Models
{
    public class DomainContext : DbContext
    {
        public DbSet<Candidate> Candidate { get; set; }
        public DbSet<Voting_Final.Models.Ballot> Ballot { get; set; } = default!;
        public DbSet<Voting_Final.Models.Race> Race { get; set; } = default!;
        public DbSet<Voting_Final.Models.BallotIssue> BallotIssue { get; set; } = default!;

        public DbSet<Voting_Final.Models.RaceCandidate> RaceCandidate { get; set; } = default!;

        public DbSet<RaceBallotSelection> RaceBallotSelection { get; set; } = default!;

        public DbSet<BallotIssueSelection> BallotIssueSelection { get; set; } = default!;

        public DbSet<Voter> Voters { get; set; } = default!;


        public DomainContext(DbContextOptions<DomainContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ballot>()
                .HasMany(b => b.BallotIssues)
                .WithOne(bi => bi.Ballot)
                .HasForeignKey(bi => bi.BallotId);



            modelBuilder.Entity<Ballot>()
                .HasMany(b => b.Races)
                .WithOne(r => r.Ballot)
                .HasForeignKey(r => r.BallotId);

            modelBuilder.Entity<RaceCandidate>()
                .HasOne(rc => rc.Candidate);
            //.WithOne() // Each Candidate belongs to one RaceCandidate
            //.HasForeignKey(c => c.RaceCandidateId); // Foreign key property in Candidate


            modelBuilder.Entity<RaceCandidate>()
                 .HasOne(rc => rc.Race); // Each RaceCandidate belongs to one Race
                                         //.WithOne() // 
                                         //.HasForeignKey(r => r.RaceCandidateId); // Foreign key property in RaceCandidate


            modelBuilder.Entity<BallotIssueSelection>(b =>

                {
                    b.HasKey(bis => bis.BallotIssueSelectionId);
                    b.Property(bi => bi.BallotId).IsRequired();
                    b.HasOne<Ballot>()
                    .WithMany()
                    .HasForeignKey(b => b.BallotId)
                    .OnDelete(DeleteBehavior.NoAction);

                    b.Property(bi => bi.BallotIssueId).IsRequired();
                    b.HasOne<BallotIssue>()
                    .WithMany()
                    .HasForeignKey(i => i.BallotIssueId);

                    b.Property(bi => bi.VoterId).IsRequired();
                    b.HasOne<Voter>()
                    .WithMany()
                    .HasForeignKey(v => v.VoterId);



                    b.ToTable("BallotIssueSelection");
                });


            modelBuilder.Entity<RaceBallotSelection>(r =>

            {
                r.HasKey(bis => bis.RaceBallotSelectionId);
                r.Property(bi => bi.BallotId).IsRequired();
                r.HasOne<Ballot>()
                .WithMany()
                .HasForeignKey(b => b.BallotId)
                .OnDelete(DeleteBehavior.NoAction);

                r.Property(bi => bi.RaceId).IsRequired();
                r.HasOne<Race>()
                .WithMany()
                .HasForeignKey(i => i.RaceId);

                r.Property(bi => bi.VoterId).IsRequired();
                r.HasOne<Voter>()
                .WithMany()
                .HasForeignKey(v => v.VoterId);

                r.Property(bi => bi.CandidateId).IsRequired();
                r.HasOne<Candidate>()
                .WithMany()
                .HasForeignKey(v => v.CandidateId);



                r.ToTable("RaceBallotSelection");
            });



            IList<Ballot> ballotList = new List<Ballot>();
            ballotList.Add(new Ballot() { BallotId = 1, BallotName = "June Primary", BallotDate = new DateTime(2024, 6, 4) });

            modelBuilder.Entity<Ballot>().HasData(ballotList);


            IList<Candidate> candidateList = new List<Candidate>();
            candidateList.Add(new Candidate() { CandidateId = 1, FirstName = "Gus", LastName = "Dawg", PartyAffiliation = "RePAWblican"});
            candidateList.Add(new Candidate() { CandidateId = 2, FirstName = "Donna", LastName = "Diamond", PartyAffiliation = "Green Party" });
            candidateList.Add(new Candidate() { CandidateId = 3, FirstName = "Javier", LastName = "Mendez", PartyAffiliation = "Socialista" });
            candidateList.Add(new Candidate() { CandidateId = 4, FirstName = "Larry", LastName = "Diamond", PartyAffiliation = "Get Down Party" });
            modelBuilder.Entity<Candidate>().HasData(candidateList);

            IList<Race> raceList = new List<Race>();
            raceList.Add(new Race() { RaceId = 1, RaceName = "United States Senate", IsPartisan = true, BallotId = 1 });
            raceList.Add(new Race() { RaceId = 2, RaceName = "Governor", IsPartisan = true , BallotId = 1});
            raceList.Add(new Race() { RaceId = 3, RaceName = "Chief Justice", IsPartisan = false, BallotId = 1 });

            modelBuilder.Entity<Race>().HasData(raceList);  

            IList<BallotIssue> ballotIssuesList = new List<BallotIssue>();
            ballotIssuesList.Add(new BallotIssue() { BallotIssueId = 1, Title = "Proposition 4543", Description = "allows all dogs over the age of 5 to vote", Type = "Proposition", BallotId = 1, NoOption = "No on Prop 4543", YesOption = "Yes on Prop 4543" });

            modelBuilder.Entity<BallotIssue>().HasData(ballotIssuesList);

          

            IList<RaceCandidate> raceCandidateList = new List<RaceCandidate>();
            raceCandidateList.Add(new RaceCandidate { RaceCandidateId = 1, CandidateId = 1, RaceId = 1 });
            raceCandidateList.Add(new RaceCandidate { RaceCandidateId = 2 , CandidateId = 2, RaceId = 1 });
            raceCandidateList.Add(new RaceCandidate {RaceCandidateId = 3, CandidateId = 3, RaceId = 1 });
            raceCandidateList.Add(new RaceCandidate {RaceCandidateId = 4, CandidateId = 4, RaceId = 2 });
            modelBuilder.Entity<RaceCandidate>().HasData(raceCandidateList);

            IList<Voter> voterList = new List<Voter>();
            voterList.Add(new Voter { VoterId = 1, VoterName = "John Smith", Address = "123 3rd St", Phone = "406-555-9854", VotingDistrict = "Ninth", VoterStatus = "Active", IsAlive = true });
            modelBuilder.Entity<Voter>().HasData(voterList);    
        }



    }
}
