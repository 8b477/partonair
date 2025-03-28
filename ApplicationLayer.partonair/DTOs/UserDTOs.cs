﻿using ApplicationLayer.partonair.DTOs.ValidationAttributes;

using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.partonair.DTOs
{
    public record UserViewDTO
    (
        Guid Id,
        string UserName,
        string Email,
        bool IsPublic,
        DateTime UserCreatedAt,
        DateTime LastConnection,
        string Role,
        Guid? FK_Profile
    );

    public record UserCreateDTO
    (
        [Required(ErrorMessage = "Le champ 'Nom' est requis")]
        [MinLength(3,ErrorMessage = "Le champ 'Nom' doit contenir au minimum 3 caractères")]
        [MaxLength(200,ErrorMessage = "Le champ 'Nom' doit contenir au maximum 200 caractères")]
        string UserName,

        [Required(ErrorMessage = "Le champ 'Email' est requis")]
        [EmailAddress(ErrorMessage = "Adresse email non valide")]
        [MaxLength(250,ErrorMessage = "Le champ 'Email' doit contenir au maximum 250 caractères")]
        string Email,

        [Required(ErrorMessage = "Le champ 'Mot de passe' est requis")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Le mot de passe doit avoir au moins 8 caractères avec 1 majuscule, 1 minuscule, 1 chiffre et 1 caractère spécial")]
        string Password
    );

    public class UserUpdateNameOrMailOrPasswordDTO
    {
        [MinLength(3,ErrorMessage = "Le champ 'Nom' doit contenir au minimum 3 caractères")]
        [MaxLength(200,ErrorMessage = "Le champ 'Nom' doit contenir au maximum 200 caractères")]
        public string? UserName { get; init; }

        [EmailAddress(ErrorMessage = "Adresse email non valide")]
        [MaxLength(250,ErrorMessage = "Le champ 'Email' doit contenir au maximum 250 caractères")]
        public string? Email { get; init; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Le mot de passe doit avoir au moins 8 caractères avec 1 majuscule, 1 minuscule, 1 chiffre et 1 caractère spécial")]
        public string? OldPassword { get; init; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Le mot de passe doit avoir au moins 8 caractères avec 1 majuscule, 1 minuscule, 1 chiffre et 1 caractère spécial")]
        public string? NewPassword { get; set; }

        [Compare(nameof(NewPassword), ErrorMessage = "Le nouveau mot de passe et la confirmation de celui-ci ne corresponde pas")]
        public string? NewPasswordConfirm { get; init; }
    }

    public record UserChangeRoleDTO
    (
        [Required]
        [ValidRole]      
        string Role
    );

}
