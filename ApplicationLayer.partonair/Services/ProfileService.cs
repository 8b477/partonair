using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.Interfaces;
using ApplicationLayer.partonair.Mappers;

using DomainLayer.partonair.Contracts;
using DomainLayer.partonair.Entities;
using DomainLayer.partonair.Enums;
using DomainLayer.partonair.Exceptions;
using DomainLayer.partonair.Exceptions.Enums;

namespace ApplicationLayer.partonair.Services
{
    public class ProfileService(IUnitOfWork UOW) : IProfileService
    {
        private readonly IUnitOfWork _UOW = UOW;

        public async Task<ProfileViewDTO> CreateAsyncService(Guid idUser, ProfileCreateDTO profileDTO)
        {
            try
            {
                await _UOW.BeginTransactionAsync();

                var existingUser = await _UOW.Users.GetByGuidAsync(idUser);

                if (existingUser.Profile is not null || existingUser.FK_Profile is not null)
                    throw new ApplicationLayerException(ApplicationLayerErrorType.ConstraintViolationErrorException,
                        "The user already has a profile, please use Update endpoint or Delete before creating a new profile.");

                Profile profileToAdd = profileDTO.ToEntity(existingUser);
                var profilCreated = await _UOW.Profiles.CreateAsync(profileToAdd);

                existingUser.Profile = profilCreated;

                var result = await _UOW.Users.Update(existingUser);

                await _UOW.SaveChangesAsync();
                await _UOW.CommitTransactionAsync();

                return profilCreated.ToView();
            }
            catch
            {
                await _UOW.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<ProfileViewDTO> GetByGuidAsyncService(Guid id)
        {
            var result = await _UOW.Profiles.GetByGuidAsync(id);

            return result.ToView();
        }

        public async Task<ICollection<ProfileAndUserViewDTO>> GetByRoleAsyncService(string role)
        {
            if (!Enum.TryParse<Roles>(role, true, out var validRole))
                throw new ApplicationLayerException(ApplicationLayerErrorType.ConstraintViolationErrorException, "The valid Role are : Visitor, Employee, Company - NO CASE SENSITIVE");

            var users = await _UOW.Users.GetByRoleIncludeProfilAsync(role);

            var result = users.Where(u => u.Role.ToString() == role) .ToList();

            ICollection<UserViewDTO> usersView = users.Select(u => u.ToView()).ToList();
            ICollection<ProfileViewDTO?> profileView = users.Select(u => u?.Profile?.ToView()).ToList();

            ICollection<ProfileAndUserViewDTO> finalView = usersView
                .Zip(profileView, (user, profile) => new ProfileAndUserViewDTO(user, profile))
                .ToList();

            return finalView;
        }

        public async Task<ProfileViewDTO> UpdateService(Guid id, ProfileUpdateDTO entity)
        {
            var existingProfil = await _UOW.Profiles.GetByGuidAsync(id);

            entity.ToEntity(existingProfil);

            var result = await _UOW.Profiles.Update(existingProfil);

            await _UOW.SaveChangesAsync();

            return result.ToView();
        }

        public async Task DeleteService(Guid id)
        {
            try
            {
                await _UOW.BeginTransactionAsync();

                await _UOW.Profiles.Delete(id);

                var existingUser = await _UOW.Users.GetByForeignKeyProfilAsync(id);

                existingUser.FK_Profile = null;
                existingUser.Profile = null;

                await _UOW.Users.Update(existingUser);

                await _UOW.SaveChangesAsync();
                await _UOW.CommitTransactionAsync();

                return;
            }
            catch
            {
                await _UOW.RollbackTransactionAsync();
                throw;
            }
        }

    }
}
