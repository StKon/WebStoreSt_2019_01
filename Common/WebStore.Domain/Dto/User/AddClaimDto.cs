using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace WebStore.Domain.Dto.User
{
    public abstract class IdentityModelDto
    {
        public Entities.User User { get; set; }
    }

    //добавить клаймы
    public class AddClaimsDto : IdentityModelDto
    {
        public IEnumerable<Claim> Claims { get; set; }
    }

    //удалить клаймы
    public class RemoveClaimsDto : IdentityModelDto
    {
        public IEnumerable<Claim> Claims { get; set; }
    }

    //заменить клайм
    public class ReplaceClaimDto : IdentityModelDto
    {
        public Claim OldClaim { get; set; }
        public Claim NewClaim { get; set; }
    }

    //вход пользователя в систему
    public class AddLoginDto : IdentityModelDto
    {
        public UserLoginInfo UserLoginInfo { get; set; }
    }

    //передавать хеш пароля пользователя
    public class PasswordHashDto : IdentityModelDto
    {
        public string Hash { get; set; }
    }

    //блокировка пльзователя
    public class SetLockoutDto : IdentityModelDto
    {
        public DateTimeOffset? LockoutEnd { get; set; }
    }
}
