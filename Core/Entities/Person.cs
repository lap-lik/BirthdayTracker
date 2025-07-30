using Core.DTOs.Input.Person;
using Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("persons", Schema = "public")]
    public class Person
    {
        private Person(Guid id, string name, DateOnly birthday, PersonType type, Guid userId, string photoUrl)
        {
            Id = id;
            Name = name;
            Birthday = birthday;
            Type = type;
            UserId = userId;
            PhotoUrl = photoUrl;
        }

        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("name")]
        [Required]
        [MaxLength(50)]
        public string Name { get; private set; }

        [Column("birthday")]
        public DateOnly Birthday { get; private set; }

        [Column("type")]
        public PersonType Type { get; private set; }

        [Column("photo_url")]
        [MaxLength(500)]
        public string? PhotoUrl { get; private set; }

        [Column("user_id")]
        [ForeignKey(nameof(User))]
        [Required]
        public Guid UserId { get; private set; }

        public User User { get; private set; }

        public static Person Create(Guid id, string name, DateOnly birthday, Guid userId, PersonType type = PersonType.Friend, string? photoUrl = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name hash is required", nameof(name));
            }

            if (userId == Guid.Empty)
            {
                throw new ArgumentException("User ID hash is required", nameof(userId));
            }

            return new Person(id, name, birthday, type, userId, photoUrl);
        }

        public void Update(string? newName, DateOnly? newBirthday, PersonType? newType, string? newPhotoUrl = null)
        {
            if (!string.IsNullOrWhiteSpace(newName))
            {
                Name = newName;
            }

            if (newBirthday.HasValue)
            {
                Birthday = newBirthday.Value;
            }

            if (newType.HasValue)
            {
                Type = newType.Value;
            }

            if (!string.IsNullOrWhiteSpace(newPhotoUrl))
            {
                PhotoUrl = newPhotoUrl;
            }
        }
    }
}