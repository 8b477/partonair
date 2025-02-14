using DomainLayer.partonair.Enums;

using System.ComponentModel.DataAnnotations;


namespace ApplicationLayer.partonair.DTOs.ValidationAttributes
{
    internal class ValidRoleAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult("Le rôle est requis");
            }

            if (Enum.TryParse<Roles>(value.ToString(),true, out _))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Le rôle spécifié n'est pas valide. Les rôles valides sont : Employé ou Entreprise.");
        }
    }
}
