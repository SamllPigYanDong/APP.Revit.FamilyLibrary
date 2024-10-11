using System.ComponentModel.DataAnnotations;

namespace Revit.Authorization.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
