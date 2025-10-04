using AutoMapper;
using DataAccessLayer.Entites.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ServicesLayer.HandleResponses.CommanResponse;
using ServicesLayer.Services.TokenService;
using ServicesLayer.Services.UserService.Dtos;

namespace ServicesLayer.Services.UserService
{

    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager; 
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        public UserService(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService,
            ILogger<UserService> logger,
            IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<CommonResponse<UserDto>> LoginAsync(LoginDto loginDto)
        {
            var response = new CommonResponse<UserDto>();
            var mappedUser = new UserDto();

            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null)
            {
                response.IsSuccess = false;
                response.Errors = new Error { Code = "404", Message = "Invalid credentials" };
                return response;
            }
            try
            {
                var result = _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
                if (!result.Result.Succeeded)
                {
                    response.IsSuccess = false;
                    response.Errors = new Error { Code = "401", Message = "Invalid credentials" };
                    return response;
                }
            }
            catch (Exception err)
            {
                _logger.LogError(err.Message);
                throw;
            }

           
            response.IsSuccess = true;
            response.Data = _mapper.Map<UserDto>(user);
            response.Data.Token = _tokenService.GenerateToken(user);
            
            return response;
        }
        public async Task<CommonResponse<UserDto>> RegisterAync(RegisterDto registerDto)
        {
            var response = new CommonResponse<UserDto>();
            var isUserExist = await _userManager.FindByEmailAsync(registerDto.Email);
            if(isUserExist is not null)
            {
                response.IsSuccess = false; 
                response.Errors = new Error { Code = "400", Message = "User already exists" };
                return response;
            }

            var user = _mapper.Map<ApplicationUser>(registerDto);

            try
            {
                var result = await _userManager.CreateAsync(user, registerDto.Password);
                if (!result.Succeeded)
                {
                    response.IsSuccess = false;
                    response.Errors = new Error { Code = "400", Message = "User creation failed" };
                    return response;
                }
            }
            catch (Exception err)
            {
                _logger.LogError(err.Message);
                throw;
            }
            

            response.IsSuccess = true;
            response.Data = _mapper.Map<UserDto>(user);
            return response;
        }
    }
}
