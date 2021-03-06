﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Clients.Base;

namespace WebStore.Clients.Users
{
    public class RolesClient : BaseClient, IRoleStore<IdentityRole>
    {
        public RolesClient(IConfiguration configuration) : base(configuration) => ServicesAddress = "api/roles";

        #region IRoleStore<IdentityRole>

        public async Task<IdentityResult> CreateAsync(IdentityRole role, CancellationToken cancel) =>
            await (await PostAsync(ServicesAddress, role, cancel))
                .Content
                .ReadAsAsync<bool>(cancel)
                ? IdentityResult.Success
                : IdentityResult.Failed();

        public async Task<IdentityResult> UpdateAsync(IdentityRole role, CancellationToken cancel) =>
            await (await PutAsync(ServicesAddress, role, cancel))
                .Content
                .ReadAsAsync<bool>(cancel)
                ? IdentityResult.Success
                : IdentityResult.Failed();

        public async Task<IdentityResult> DeleteAsync(IdentityRole role, CancellationToken cancel) =>
            await (await PostAsync($"{ServicesAddress}/Delete", role, cancel))
                .Content
                .ReadAsAsync<bool>(cancel)
                ? IdentityResult.Success
                : IdentityResult.Failed();

        public async Task<string> GetRoleIdAsync(IdentityRole role, CancellationToken cancel) =>
            await (await PostAsync($"{ServicesAddress}/GetRoleId", role, cancel))
                .Content
                .ReadAsAsync<string>(cancel)
                .ConfigureAwait(false);

        public async Task<string> GetRoleNameAsync(IdentityRole role, CancellationToken cancel) =>
            await (await PostAsync($"{ServicesAddress}/GetRoleName", role, cancel))
                .Content
                .ReadAsAsync<string>(cancel)
                .ConfigureAwait(false);

        public async Task SetRoleNameAsync(IdentityRole role, string name, CancellationToken cancel)
        {
            role.Name = name;
            await PostAsync($"{ServicesAddress}/SetRoleName/{name}", role, cancel);
        }

        public async Task<string> GetNormalizedRoleNameAsync(IdentityRole role, CancellationToken cancel) =>
            await (await PostAsync($"{ServicesAddress}/GetnormalizedRoleName", role, cancel))
                .Content
                .ReadAsAsync<string>(cancel)
                .ConfigureAwait(false);

        public async Task SetNormalizedRoleNameAsync(IdentityRole role, string name, CancellationToken cancel)
        {
            role.NormalizedName = name;
            await PostAsync($"{ServicesAddress}/SetNormalizedRoleName/{name}", role, cancel);
        }

        public async Task<IdentityRole> FindByIdAsync(string id, CancellationToken cancel) =>
            await GetAsync<IdentityRole>($"{ServicesAddress}/FindById/{id}", cancel);

        public async Task<IdentityRole> FindByNameAsync(string name, CancellationToken cancel) =>
            await GetAsync<IdentityRole>($"{ServicesAddress}/FindByName/{name}", cancel);

        #endregion
    }
}
