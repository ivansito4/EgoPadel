//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI.Services;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.Extensions.Logging;

//namespace EgoPadel.Models
//{
//    public class _LoginPartialController : Controller
//    {
       

//            /// <summary>
//            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
//            ///     directly from your code. This API may change or be removed in future releases.
//            /// </summary>
//            public string Username { get; set; }

//            /// <summary>
//            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
//            ///     directly from your code. This API may change or be removed in future releases.
//            /// </summary>
//            [TempData]
//            public string StatusMessage { get; set; }

//            /// <summary>
//            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
//            ///     directly from your code. This API may change or be removed in future releases.
//            /// </summary>
//            [BindProperty]
//            public InputModel Input { get; set; }

//            /// <summary>
//            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
//            ///     directly from your code. This API may change or be removed in future releases.
//            /// </summary>
//            public class InputModel
//            {
//                /// <summary>
//                ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
//                ///     directly from your code. This API may change or be removed in future releases.
//                /// </summary>
                
//                public string RutaImagen { get; set; }
//            }

//            private async Task LoadAsync(UsuarioApp user)
//            {
                
//                var rutaImagen = user.Foto;


//                Input = new InputModel
//                {
//                    RutaImagen = rutaImagen
//                };
//            }

//            //public async Task<IActionResult> OnGetAsync()
//            //{
//            //    var user = await _userManager.GetUserAsync(User);
//            //    if (user == null)
//            //    {
//            //        return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
//            //    }


//            //    await LoadAsync((UsuarioApp)user);
//            //    return Page();
//            //}

//    }
//}
