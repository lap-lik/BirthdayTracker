using Core.DTOs.Output.Person;
using Core.Entities;
using Core.Intefaces.Application.Mapping;

namespace Application.Mapping
{
    public class PersonMapper : IPersonMapper
    {
        public PersonResponse MapToResponse(Person person)
        {
            if (person == null)
                throw new ArgumentNullException(nameof(person));

            return new PersonResponse(
                Id: person.Id,
                Name: person.Name,
                Age: CalculateAge(person.Birthday),
                Birthday: person.Birthday,
                Type: person.Type,
                PhotoUrl: person.PhotoUrl
            );
        }

        public IEnumerable<PersonResponse> MapToResponse(IEnumerable<Person> persons)
        {
            return persons.Select(MapToResponse);
        }

        private static int CalculateAge(DateOnly birthday)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var age = today.Year - birthday.Year;
            if (birthday > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}