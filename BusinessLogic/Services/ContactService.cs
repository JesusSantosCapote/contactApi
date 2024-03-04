using AutoMapper;
using BusinessLogic.DTO;
using BusinessLogic.Result;
using DataAccess.Entitys;
using DataAccess.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BusinessLogic.Services
{
    public class ContactService : IContactService
    {
        private readonly IMapper _mapper;
        private readonly IContactRepository _contactRepo;
        private readonly IUserRepository _userRepo;
        private readonly IValidator<ContactDto> _validator;

        public ContactService(IMapper mapper, IContactRepository contactRepository, IUserRepository userRepository, IValidator<ContactDto> validator)
        {
            _contactRepo = contactRepository;
            _userRepo = userRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result<ContactDetailDto>> AddContactAsync(ContactDto contact, string username)
        {
            var validationResult = await _validator.ValidateAsync(contact);

            if (!validationResult.IsValid)
            {
                return new InvalidResult<ContactDetailDto>(validationResult.ToString());
            }

            bool existEmail = await _contactRepo.EmailExistsAsync(contact.Email);

            if (existEmail)
            {
                return new InvalidResult<ContactDetailDto>("There is already a contact with that email");
            }

            try
            {
                UserEntity user = await _userRepo.GetUserByNameAsync(username);
                var newContact = _mapper.Map<ContactEntity>(contact);
                newContact.Owner = user.Id;
                newContact.OwnerUser = user;
                var result = await _contactRepo.AddAsync(newContact);
                var addedContact = _mapper.Map<ContactDetailDto>(result);

                return new SuccessResult<ContactDetailDto>(addedContact);
            }
            catch (Exception ex)
            {
                return new UnexpectedResult<ContactDetailDto>(ex.Message);
            }
        }

        public async Task<Result<ContactDetailDto>> DeleteAsync(Guid id, string username)
        {
            try
            {
                ContactEntity contact = await _contactRepo.GetAsync(id);
                UserEntity user = await _userRepo.GetUserByNameAsync(username);

                if (user == null)
                {
                    return new UnexpectedResult<ContactDetailDto>($"User with User Name: {username} not registered");
                }

                if (contact == null)
                {
                    return new NotFoundResult<ContactDetailDto>($"Contact with id: {id} not found");
                }

                if (contact.Owner != user.Id)
                {
                    return new UnauthorizedResult<ContactDetailDto>("The user is not authorized to delete the contact because it does not belong to them");
                }

                var deleteResult = await _contactRepo.DeleteAsync(id);
                return new NoContentResult<ContactDetailDto>();
            }
            catch (Exception ex)
            {
                return new UnexpectedResult<ContactDetailDto>(ex.Message);
            }
        }

        public async Task<Result<List<ContactDetailDto>>> GetAllContactsAsync(string username)
        {
            try
            {
                var contacts = await _contactRepo.GetAllAsync(username);
                List<ContactDetailDto> contactDetailDtos = contacts.Select(c => _mapper.Map<ContactDetailDto>(c)).ToList();
                return new SuccessResult<List<ContactDetailDto>>(contactDetailDtos);
            }
            catch (Exception ex)
            {
                return new UnexpectedResult<List<ContactDetailDto>>(ex.Message);
            }
        }

        public async Task<Result<ContactDetailDto>> GetContactAsync(Guid id, string username)
        {
            try
            {
                ContactEntity contact = await _contactRepo.GetAsync(id);
                UserEntity user = await _userRepo.GetUserByNameAsync(username);

                if (user == null)
                {
                    return new UnexpectedResult<ContactDetailDto>($"User with User Name: {username} not registered");
                }

                if (contact == null)
                {
                    return new NotFoundResult<ContactDetailDto>($"Contact with id: {id} not found");
                }

                if (contact.Owner != user.Id)
                {
                    return new UnauthorizedResult<ContactDetailDto>("The user is not authorized to get the contact because it does not belong to them");
                }

                ContactDetailDto contactResponse = _mapper.Map<ContactDetailDto>(contact);
                return new SuccessResult<ContactDetailDto>(contactResponse);
            }
            catch (Exception ex)
            {
                return new UnexpectedResult<ContactDetailDto>(ex.Message);
            }
        }

        public async Task<Result<ContactDetailDto>> UpdateContactAsync(Guid id, ContactDto contact, string username)
        {
            try
            {
                var validationResult = _validator.Validate(contact);
                if (!validationResult.IsValid)
                {
                    return new InvalidResult<ContactDetailDto>(validationResult.ToString());
                }

                UserEntity user = await _userRepo.GetUserByNameAsync(username);
                if (user == null)
                {
                    return new UnexpectedResult<ContactDetailDto>($"User with User Name: {username} not registered");
                }

                ContactEntity contactToUpdate = await _contactRepo.GetAsync(id);
                if (contactToUpdate == null)
                {
                    return new NotFoundResult<ContactDetailDto>($"Contact with id: {id} not found");
                }

                if (contact.Email != contactToUpdate.Email)
                {
                    bool emailExists = await _contactRepo.EmailExistsAsync(contact.Email);
                    if (emailExists)
                    {
                        return new InvalidResult<ContactDetailDto>("The email already exists in the database");
                    }
                }

                if (contactToUpdate.Owner != user.Id)
                {
                    return new UnauthorizedResult<ContactDetailDto>("The user is not authorized to get the contact because it does not belong to them");
                }

                ContactEntity updatedContact = _mapper.Map<ContactEntity>(contact);
                // Me estoy repitiendo aqui
                await _contactRepo.UpdateAsync(id, updatedContact);
                return new NoContentResult<ContactDetailDto>();
            }
            catch (Exception ex)
            {
                return new UnexpectedResult<ContactDetailDto>(ex.Message);
            }
        }
    }
}
