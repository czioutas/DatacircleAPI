using DatacircleAPI.Models;
using DatacircleAPI.ViewModel;
using DatacircleAPI.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using DatacircleAPI.Application;
using System;
// using System;

namespace DatacircleAPI.Services
{
    public class AccountService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountService(
            ICompanyRepository companyRepository,
            IRoleRepository roleRepository,
            IUserRepository userRepository,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailSender emailSender)
        {
            this._companyRepository = companyRepository;
            this._roleRepository = roleRepository;
            this._userManager = userManager;
            this._userRepository = userRepository;
            this._signInManager = signInManager;
            this._emailSender = emailSender;
        }

        public async Task Register(RegisterViewModel registerVm)
        {
            Company _company = _companyRepository.GetByName(registerVm.Company.ToLower());
            
            if (_company != null) {
                throw new ResponseException("A company already exists.");
            } 
            
            Company newCompanyEntity = _companyRepository.Create(new Company { Name = registerVm.Company });

            Role newRole = _roleRepository.getDefaultNewRole();
            newRole.ComapnyFk = newCompanyEntity.ID;

            Role newRoleEntity = _roleRepository.Create(newRole);
            
            User newUser = getDefaultNewUser(
                registerVm,
                newCompanyEntity.ID,
                newRoleEntity.Id
            );            

            IdentityResult result = await _userManager.CreateAsync(newUser, registerVm.Password);
            if (result.Succeeded) {
                _companyRepository.Save();
                User newUserEntity = await _userManager.FindByEmailAsync(newUser.Email);

                await _emailSender.SendEmailAsync(new Email { RecipientEmailAddress = registerVm.Email, Template = "accountRegistration"});
                return;
            } else {                
                string message = "";
                foreach (IdentityError error in result.Errors) {
                    message += error.Description.Replace("User name", "Emai") + Environment.NewLine;
                }
                throw new ResponseException(message);
            }
        }

        public bool ConfirmEmail(string verificationKey)
        {
            User _user = this._userRepository.FindByVerificationToken(verificationKey);
            _user.IsVerified = true;
            
            int result = this._userRepository.Save();
            return Convert.ToBoolean(result); 
        }

        private User getDefaultNewUser(RegisterViewModel vm, int CompanyFk, int RoleFk)
        {
            DateTime now = DateTime.Now;

            return new User {
                Email = vm.Email,
                UserName = vm.Email,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                CompanyFk = CompanyFk,
                IsCompanyOwner = true,
                IsActive = false,
                VerificationKey = "VerificationKey",
                Token = "token",
                IsVerified = false,
                RoleFk = RoleFk,
                CreatedAt = now,
                UpdatedAt = now
            };
        }
    }
}