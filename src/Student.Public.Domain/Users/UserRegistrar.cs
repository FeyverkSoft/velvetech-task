using System;
using System.Threading;
using System.Threading.Tasks;
using Student.Public.Domain.Users.Entity;
using Student.Public.Domain.Users.Exceptions;

namespace Student.Public.Domain.Users
{
    public sealed class UserRegistrar
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;

        public UserRegistrar(
            IPasswordHasher passwordHasher,
            IUserRepository userRepository
        )
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
        }

        public async Task<User> Registrate(String login, String name, String password, CancellationToken cancellationToken)
        {
            // в идеале надо отправить емайл, с кодом подтверждения итд, но так как это надо искать SMTP а так же в задании про это нечего нет
            // тем более сервис отправки писем должен быть отдельно, и реагировать  на сообщение от этого сервиса через шину, чего опять же не будет (убить пару дней на неоплачиваемое тестовое задание, такое себе занятие)
            // то не буду усложнять себе жизнь, и сделаю наивную регистрацию без подтверждения
            if (String.IsNullOrEmpty(password))
                throw new WrongPasswordException("Password can't b empty!");

            var existingUser = await _userRepository.Get(login, cancellationToken);
            var passwordHash = await _passwordHasher.HashPassword(password);

            if (existingUser != null)
                if (existingUser.Login == login &&
                    existingUser.Name == name &&
                    existingUser.Password == passwordHash){
                    return existingUser;
                }
                else{
                    throw new UserAlreadyExistsException();
                }

            var user = new User(login, name, passwordHash);
            return user;
        }
    }
}