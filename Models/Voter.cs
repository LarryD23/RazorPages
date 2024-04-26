namespace Vote_Final.Models
{
    public class Voter
    {
        public int VoterId { get; set; }
        
        public string VoterName { get; set;}

        public string VotingDistrict { get; set;}   

        public string Address { get; set;}  

        public string Phone { get; set;}

        public bool IsAlive { get; set;}

        public string VoterStatus { get; set;}  
    }
}
