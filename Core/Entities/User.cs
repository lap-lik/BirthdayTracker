using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("users", Schema = "public")]
    public class User
    {
        private User(Guid id, string userName, string email, string passwordHash, string? avatarUrl)
        {
            Id = id;
            UserName = userName;
            Email = email;
            PasswordHash = passwordHash;
            AvatarUrl = avatarUrl;
            Persons = new List<Person>();
        }

        [Key]
        [Column("id")]
        public Guid Id { get; private set; }

        [Column("user_name")]
        [Required]
        [MaxLength(50)]
        public string UserName { get; private set; }

        [Column("email")]
        [Required]
        [MaxLength(255)]
        public string Email { get; private set; }

        [Column("password_hash")]
        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; private set; }

        [Column("photo_url")]
        [MaxLength(500)]
        public string? AvatarUrl { get; private set; }

        public ICollection<Person> Persons { get; private set; }

        public static User Create(Guid id, string userName, string email, string passwordHash, string? avatarUrl = null)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("Username hash is required", nameof(userName));
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email hash is required", nameof(email));
            }

            if (string.IsNullOrWhiteSpace(passwordHash))
            {
                throw new ArgumentException("Password hash is required", nameof(passwordHash));
            }

            return new User(id, userName, email, passwordHash, avatarUrl);
        }

        public void Update(string? newUserName, string? newEmail, string? newAvatarUrl, string? newPasswordHash)
        {
            if (!string.IsNullOrWhiteSpace(newUserName))
            {
                UserName = newUserName;
            }

            if (!string.IsNullOrWhiteSpace(newEmail))
            {
                Email = newEmail;
            }

            if (newAvatarUrl != null)
            {
                AvatarUrl = newAvatarUrl;
            }

            if (!string.IsNullOrWhiteSpace(newPasswordHash))
            {
                PasswordHash = newPasswordHash;
            }
        }
    }
}