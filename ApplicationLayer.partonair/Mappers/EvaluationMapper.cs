﻿using ApplicationLayer.partonair.DTOs;

using DomainLayer.partonair.Entities;


namespace ApplicationLayer.partonair.Mappers
{
    internal static class EvaluationMapper
    {
        internal static Evaluation ToEntity(this EvaluationCreateDTO eval, User existingOwner, User sender)
        {
            return new Evaluation
            {
                EvaluationCommentary = eval.Commentary,
                EvaluationCreatedAt = DateTime.Now,
                EvaluationUpdatedAt = null,
                EvaluationValue = eval.Value,
                FK_Owner = existingOwner.Id,
                Owner = existingOwner,
                FK_Sender = sender.Id,
                Sender = sender
            };
        }

        internal static EvaluationViewDTO ToView(this Evaluation eval)
        {
            return new EvaluationViewDTO
                (
                    eval.Id,
                    eval.EvaluationCreatedAt,
                    eval.EvaluationUpdatedAt,
                    eval.EvaluationValue,
                    eval.EvaluationCommentary,
                    eval.Owner.Id,
                    eval.Owner.UserName,
                    eval.Sender.Id,
                    eval.Sender.UserName
                );
        }
    }
}
