// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using EgoPadel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EgoPadel.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<UsuarioApp> _userManager;
        private readonly SignInManager<UsuarioApp> _signInManager;

        public IndexModel(
            UserManager<UsuarioApp> userManager,
            SignInManager<UsuarioApp> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Phone number")]

            public string PhoneNumber { get; set; }
            public string Login { get; set; }
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public string Email { get; set; }
            public int Puntos { get; set; }
            public  string RutaImagen { get; set; }
        }

        private async Task LoadAsync(UsuarioApp user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var nombre = user.Nombre;
            var apellidos=user.Apellidos;
            var puntos = user.Puntos;
            var rutaImagen = user.Foto;


            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Login = userName,
                Email = email,
                Nombre = nombre,
                Apellidos = apellidos,
                Puntos=puntos,
                RutaImagen=rutaImagen
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            

            await LoadAsync((UsuarioApp)user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync((UsuarioApp)user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Error al intentar cambiar su número de teléfono.";
                    return RedirectToPage();
                }
            }
            if (Input.Login != user.UserName)
            {
                var ResultadoLogin = await _userManager.SetUserNameAsync(user, Input.Login);
                if (!ResultadoLogin.Succeeded)
                {
                    StatusMessage = "Error al intentar cambiar su nombre de usuario.";
                    return RedirectToPage();
                }
            }
            if (Input.Email != user.Email)
            {
                var ResultadoEmail = await _userManager.SetEmailAsync(user, Input.Email);
                if (!ResultadoEmail.Succeeded)
                {
                    StatusMessage = "Error al intentar cambiar su email.";
                    return RedirectToPage();
                }
            }
            if (Input.Nombre != user.Nombre)
            {
                user.Nombre = Input.Nombre;
            }
            if (Input.Apellidos != user.Apellidos)
            {
                user.Apellidos = Input.Apellidos;
            }

            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Perfil actualizado";
            return RedirectToPage();
        }
    }
}
