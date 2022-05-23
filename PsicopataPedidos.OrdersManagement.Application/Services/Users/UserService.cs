using AutoMapper;
using PsicopataPedidos.OrdersManagement.Application.Contracts.Authorization;
using PsicopataPedidos.OrdersManagement.Application.Contracts.Persistence;
using PsicopataPedidos.OrdersManagement.Application.Contracts.Services;
using PsicopataPedidos.OrdersManagement.Application.Exceptions;
using PsicopataPedidos.OrdersManagement.Domain.Entities;

namespace PsicopataPedidos.OrdersManagement.Application.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILoggedInUserService _loggedUserService;

        public UserService(IUserRepository userRepository, IMapper mapper, ILoggedInUserService loggedUserService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _loggedUserService = loggedUserService;
        }
        
        public async Task FundClientWallet(int userId, decimal amount)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
                throw new NotFoundException(nameof(User), userId);

            if (user.IsAdmin)
                throw new ApplicationException($"{nameof(User)} ({userId}) is not a client.");

            if (amount < 1)
                throw new ApplicationException($"Invalid Amount ({amount}).");

            if (user.Wallet == null)
                user.Wallet = 0;

            user.Wallet += amount;

            await _userRepository.UpdateAsync(user);
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();

            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto> GetUserProfile()
        {
            var user = await _userRepository.GetByIdAsync(_loggedUserService.UserId);

            return _mapper.Map<UserDto>(user);
        }
    }
}
