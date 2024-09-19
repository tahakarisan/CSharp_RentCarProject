using Business.Abstract;
using CoreLayer.Entities.Concrete;
using CoreLayer.Utilities.Results;
using CoreLayer.Utilities.Security.Hashing;
using CoreLayer.Utilities.Security.JWT;
using DataAccess.Concrete.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        IUserService _userService;
        ITokenHelper _tokenHelper;
        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<AccessToken> CreateToken(User user)
        {
            var result = _userService.GetClaims(user);
            if (result.Success)
            {
                return new SuccesfulDataResult<AccessToken>(_tokenHelper.CreateToken(user, result.Data));
            }
            return new ErrorDataResult<AccessToken>("Token oluşturulamadı");
        }
        public IResult Login(UserForLoginDto userForLoginDto)
        {
            var result = _userService.GetByMail(userForLoginDto.Email);

            if (result != null)
            {
                if (HashingHelper.VerifyPassword(userForLoginDto.Password, result.PasswordSalt, result.PasswordHash))
                {
                    return new SuccesfullResult();
                }
            }
            return new ErrorResult();
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            HashingHelper.CreatePassword(userForRegisterDto.Password, out byte[] passwordSalt, out byte[] passwordHash);
            User user = new User()
            {
                Email = userForRegisterDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true,
                LastName = userForRegisterDto.LastName,
                FirstName = userForRegisterDto.FirstName
            };
            _userService.Add(user);
            return new SuccesfulDataResult<User>(_userService.GetByMail(userForRegisterDto.Email));

        }


    }
}
