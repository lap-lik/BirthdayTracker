using Core.DTOs.Output.Person;
using Core.Entities;

namespace Core.Intefaces.Application.Mapping
{
    public interface IPersonMapper
    {
        PersonResponse MapToResponse(Person person);
        IEnumerable<PersonResponse> MapToResponse(IEnumerable<Person> persons);
    }
}