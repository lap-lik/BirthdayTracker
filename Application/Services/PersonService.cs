using Application.Services.Options;
using Core.DTOs.Input.Person;
using Core.DTOs.Output.Person;
using Core.Entities;
using Core.Enums;
using Core.Exceptions;
using Core.Intefaces.Application.Mapping;
using Core.Intefaces.Application.Service;
using Core.Intefaces.Infrastructure.Repositories;
using Core.Intefaces.Infrastructure.Service;
using Microsoft.Extensions.Options;

namespace Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMediaService _mediaService;
        private readonly IPersonMapper _personMapper;
        private readonly BirthdayOptions _birthdayOptions;
        
        public PersonService(
            IPersonRepository personRepository,
            IMediaService mediaService,
            IPersonMapper personMapper,
            IOptions<BirthdayOptions> birthdayOptions)
        {
            _personRepository = personRepository;
            _mediaService = mediaService;
            _personMapper = personMapper;
            _birthdayOptions = birthdayOptions.Value;
        }

        public async Task<PersonResponse> AddAsync(Guid userId, CreatePersonRequest request)
        {
            string? photoUrl = request.Photo != null
                ? await _mediaService.SaveImageAsync(request.Photo, MediaType.PersonPhoto)
                : null;

            var person = Person.Create(Guid.NewGuid(), request.Name, request.Birthday, userId, request.Type.Value, photoUrl);

            await _personRepository.AddAsync(person);

            return _personMapper.MapToResponse(person);
        }

        public async Task UpdateAsync(Guid userId, Guid personId, UpdatePersonRequest request)
        {
            var person = await _personRepository.GetByIdAsync(personId);
            ValidatePersonOwnership(userId, person);

            string? photoUrl = request.Photo != null
                ? await _mediaService.UpdateImageAsync(person.PhotoUrl, request.Photo, MediaType.PersonPhoto)
                : null;

            person.Update(request.Name, request.Birthday, request.Type, photoUrl);
            await _personRepository.UpdateAsync(person);
        }

        public async Task DeleteAsync(Guid userId, Guid personId)
        {
            var person = await _personRepository.GetByIdAsync(personId);
            ValidatePersonOwnership(userId, person);
            
            await _personRepository.DeleteAsync(person);
            await _mediaService.DeleteImageAsync(person.PhotoUrl);
        }

        public async Task<PersonResponse> GetPersonAsync(Guid userId, Guid personId)
        {
            var person = await _personRepository.GetByIdAsync(personId);
            ValidatePersonOwnership(userId, person);

            return _personMapper.MapToResponse(person);
        }

        public async Task<IEnumerable<PersonResponse>> GetAllPersonsAsync(Guid userId)
        {
            var persons = await _personRepository.GetAllForUserAsync(userId);

            return _personMapper.MapToResponse(persons);
        }

        public async Task<IEnumerable<PersonResponse>> GetUpcomingBirthdaysAsync(Guid userId)
        {
            var today = DateTime.Today;
            var targetDate = today.AddDays(_birthdayOptions.UpcomingDays);

            var persons = await _personRepository.GetUpcomingBirthdaysAsync(
                userId,
                today.Month, 
                today.Day,
                targetDate.Month, 
                targetDate.Day);

            return _personMapper.MapToResponse(persons);
        }

        private void ValidatePersonOwnership(Guid userId, Person? person)
        {
            if (person == null)
            {
                throw new NotFoundException($"Person with not found");
            }
            
            if (userId != person.UserId)
            {
                throw new ForbiddenException("You don't have permission to access this person");
            }
        }
    }
}