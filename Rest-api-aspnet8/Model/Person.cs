using System.ComponentModel.DataAnnotations.Schema;

namespace Rest_api_aspnet8.Model
{
    [Table("person")]
    public class Person
    {
        [Column("id")]
        public long Id{ get; set; }
        [Column("first_name")]
        public string FirstName { get; set; }
        [Column("last_name")]
        public string LastName{ get; set; }
        [Column("address")]
        public string Address { get; set; }
        [Column("gender")]
        public string Gender { get; set; }
        [Column("birth_date")]
        public DateTime BirthDate { get; set; }
    }
}
