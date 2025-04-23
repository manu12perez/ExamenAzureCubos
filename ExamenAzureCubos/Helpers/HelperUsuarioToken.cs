﻿using ExamenAzureCubos.Models;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ExamenAzureCubos.Helpers
{
    public class HelperUsuarioToken
    {
        private IHttpContextAccessor contextAccessor;

        public HelperUsuarioToken(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        public UsuarioModel GetUsuario()
        {
            Claim claim = this.contextAccessor.HttpContext.User.FindFirst(x => x.Type == "UserData");
            string json = claim.Value;
            string jsonUsuario = HelperCryptography.DecryptString(json);
            UsuarioModel model = JsonConvert.DeserializeObject<UsuarioModel>(jsonUsuario);
            return model;
        }
    }
}
