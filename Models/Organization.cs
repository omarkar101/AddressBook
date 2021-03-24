using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.Models
{
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [RegularExpression(@"^[0-9]*$")]
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public IEnumerable<Person> People { get; set; }
    }
}
