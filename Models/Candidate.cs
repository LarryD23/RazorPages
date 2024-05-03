using System.ComponentModel.DataAnnotations;


namespace Voting_Final.Models
{
    public class Candidate
    {
        public int CandidateId { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Party")]
        public string PartyAffiliation { get; set; }


        

        public override string ToString()
        {
           return $"ID: {CandidateId}, Name: {FirstName} {LastName}, Party: {PartyAffiliation}";
        }
    }
}
