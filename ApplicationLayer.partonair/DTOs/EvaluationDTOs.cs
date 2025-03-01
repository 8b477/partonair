

using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.partonair.DTOs
{
    public record EvaluationCreateDTO
        (   
            [Required(ErrorMessage = "The field is required")]
            [MinLength(5,ErrorMessage = "The field must contain 5 characters")]
            [MaxLength(255,ErrorMessage = "The field should contain max 255 characters")]
            string Commentary,

            [Required(ErrorMessage = "The field is required")]
            [Range(1,5,ErrorMessage = "The attempt value is between 1 and 5 'integer'")]
            int Value
        );

    public record EvaluationUpdateDTO
        (
            string? Commentary,
            int? Value
        );

    public record EvaluationViewDTO(Guid Id, DateTime CreatedAt, DateTime? UpdateAt, int Value, Guid Id_Owner, Guid Id_Sender);
}