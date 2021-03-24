using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressBook.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AddressBook.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AddressBookContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AddressBookContext>>()))
            {
                if (context.Organization.Any())
                {
                    return;
                }

                context.Organization.AddRange(
                    new Organization
                    {
                        Name = "Administrate",
                        Address = "Beirut",
                        PhoneNumber = "00000000"
                    }
                    
                );
                context.SaveChanges();
            }
        }
    }
}
