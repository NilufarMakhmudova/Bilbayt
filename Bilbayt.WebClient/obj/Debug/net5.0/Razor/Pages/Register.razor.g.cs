#pragma checksum "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\Pages\Register.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "361ba746f811decc1900d82be9f8dd53a29fe415"
// <auto-generated/>
#pragma warning disable 1591
namespace Bilbayt.WebClient.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
#nullable restore
#line 1 "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\_Imports.razor"
using Bilbayt.WebClient;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\_Imports.razor"
using Bilbayt.WebClient.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\_Imports.razor"
using Bilbayt.WebClient.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\_Imports.razor"
using Blazored.LocalStorage;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\Pages\Register.razor"
using Microsoft.AspNetCore.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\Pages\Register.razor"
using Bilbayt.WebClient.Models;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/register")]
    public partial class Register : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
#nullable restore
#line 6 "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\Pages\Register.razor"
 if (ShowSpinner)
{

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(0, "<div class=\"spinner\"></div>");
#nullable restore
#line 9 "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\Pages\Register.razor"
}
else
{

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(1, "<h1>Register</h1>");
#nullable restore
#line 14 "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\Pages\Register.razor"
     if (ShowErrors)
    {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(2, "div");
            __builder.AddAttribute(3, "class", "alert alert-danger");
            __builder.AddAttribute(4, "role", "alert");
            __builder.OpenElement(5, "p");
            __builder.AddContent(6, 
#nullable restore
#line 17 "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\Pages\Register.razor"
                Error

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 19 "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\Pages\Register.razor"
    }

#line default
#line hidden
#nullable disable
            __builder.OpenElement(7, "div");
            __builder.AddAttribute(8, "class", "card");
            __builder.OpenElement(9, "div");
            __builder.AddAttribute(10, "class", "card-body");
            __builder.AddMarkupContent(11, "<h5 class=\"card-title\">Please enter your details</h5>\r\n            ");
            __builder.OpenComponent<Microsoft.AspNetCore.Components.Forms.EditForm>(12);
            __builder.AddAttribute(13, "Model", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Object>(
#nullable restore
#line 24 "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\Pages\Register.razor"
                             RegisterModel

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(14, "OnValidSubmit", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<Microsoft.AspNetCore.Components.Forms.EditContext>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Forms.EditContext>(this, 
#nullable restore
#line 24 "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\Pages\Register.razor"
                                                           HandleRegistration

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(15, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment<Microsoft.AspNetCore.Components.Forms.EditContext>)((context) => (__builder2) => {
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Forms.DataAnnotationsValidator>(16);
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(17, "\r\n                ");
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Forms.ValidationSummary>(18);
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(19, "\r\n\r\n                ");
                __builder2.OpenElement(20, "div");
                __builder2.AddAttribute(21, "class", "form-group");
                __builder2.AddMarkupContent(22, "<label for=\"email\">First name</label>\r\n                    ");
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Forms.InputText>(23);
                __builder2.AddAttribute(24, "Id", "firstName");
                __builder2.AddAttribute(25, "class", "form-control");
                __builder2.AddAttribute(26, "Value", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 30 "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\Pages\Register.razor"
                                                                                RegisterModel.FirstName

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(27, "ValueChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.String>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.String>(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => RegisterModel.FirstName = __value, RegisterModel.FirstName))));
                __builder2.AddAttribute(28, "ValueExpression", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Linq.Expressions.Expression<System.Func<System.String>>>(() => RegisterModel.FirstName));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(29, "\r\n                    ");
                __Blazor.Bilbayt.WebClient.Pages.Register.TypeInference.CreateValidationMessage_0(__builder2, 30, 31, 
#nullable restore
#line 31 "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\Pages\Register.razor"
                                              () => RegisterModel.FirstName

#line default
#line hidden
#nullable disable
                );
                __builder2.CloseElement();
                __builder2.AddMarkupContent(32, "\r\n                ");
                __builder2.OpenElement(33, "div");
                __builder2.AddAttribute(34, "class", "form-group");
                __builder2.AddMarkupContent(35, "<label for=\"email\">Last name</label>\r\n                    ");
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Forms.InputText>(36);
                __builder2.AddAttribute(37, "Id", "lastName");
                __builder2.AddAttribute(38, "class", "form-control");
                __builder2.AddAttribute(39, "Value", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 35 "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\Pages\Register.razor"
                                                                               RegisterModel.LastName

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(40, "ValueChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.String>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.String>(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => RegisterModel.LastName = __value, RegisterModel.LastName))));
                __builder2.AddAttribute(41, "ValueExpression", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Linq.Expressions.Expression<System.Func<System.String>>>(() => RegisterModel.LastName));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(42, "\r\n                    ");
                __Blazor.Bilbayt.WebClient.Pages.Register.TypeInference.CreateValidationMessage_1(__builder2, 43, 44, 
#nullable restore
#line 36 "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\Pages\Register.razor"
                                              () => RegisterModel.LastName

#line default
#line hidden
#nullable disable
                );
                __builder2.CloseElement();
                __builder2.AddMarkupContent(45, "\r\n                ");
                __builder2.OpenElement(46, "div");
                __builder2.AddAttribute(47, "class", "form-group");
                __builder2.AddMarkupContent(48, "<label for=\"email\">Email address</label>\r\n                    ");
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Forms.InputText>(49);
                __builder2.AddAttribute(50, "Id", "email");
                __builder2.AddAttribute(51, "class", "form-control");
                __builder2.AddAttribute(52, "Value", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 40 "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\Pages\Register.razor"
                                                                            RegisterModel.Username

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(53, "ValueChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.String>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.String>(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => RegisterModel.Username = __value, RegisterModel.Username))));
                __builder2.AddAttribute(54, "ValueExpression", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Linq.Expressions.Expression<System.Func<System.String>>>(() => RegisterModel.Username));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(55, "\r\n                    ");
                __Blazor.Bilbayt.WebClient.Pages.Register.TypeInference.CreateValidationMessage_2(__builder2, 56, 57, 
#nullable restore
#line 41 "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\Pages\Register.razor"
                                              () => RegisterModel.Username

#line default
#line hidden
#nullable disable
                );
                __builder2.CloseElement();
                __builder2.AddMarkupContent(58, "\r\n                ");
                __builder2.OpenElement(59, "div");
                __builder2.AddAttribute(60, "class", "form-group");
                __builder2.AddMarkupContent(61, "<label for=\"password\">Password</label>\r\n                    ");
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Forms.InputText>(62);
                __builder2.AddAttribute(63, "Id", "password");
                __builder2.AddAttribute(64, "type", "password");
                __builder2.AddAttribute(65, "class", "form-control");
                __builder2.AddAttribute(66, "Value", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 45 "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\Pages\Register.razor"
                                                                                               RegisterModel.Password

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(67, "ValueChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.String>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.String>(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => RegisterModel.Password = __value, RegisterModel.Password))));
                __builder2.AddAttribute(68, "ValueExpression", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Linq.Expressions.Expression<System.Func<System.String>>>(() => RegisterModel.Password));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(69, "\r\n                    ");
                __Blazor.Bilbayt.WebClient.Pages.Register.TypeInference.CreateValidationMessage_3(__builder2, 70, 71, 
#nullable restore
#line 46 "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\Pages\Register.razor"
                                              () => RegisterModel.Password

#line default
#line hidden
#nullable disable
                );
                __builder2.CloseElement();
                __builder2.AddMarkupContent(72, "\r\n                ");
                __builder2.OpenElement(73, "div");
                __builder2.AddAttribute(74, "class", "form-group");
                __builder2.AddMarkupContent(75, "<label for=\"password\">Confirm Password</label>\r\n                    ");
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Forms.InputText>(76);
                __builder2.AddAttribute(77, "Id", "password");
                __builder2.AddAttribute(78, "type", "password");
                __builder2.AddAttribute(79, "class", "form-control");
                __builder2.AddAttribute(80, "Value", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 50 "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\Pages\Register.razor"
                                                                                               RegisterModel.ConfirmedPassword

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(81, "ValueChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.String>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.String>(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => RegisterModel.ConfirmedPassword = __value, RegisterModel.ConfirmedPassword))));
                __builder2.AddAttribute(82, "ValueExpression", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Linq.Expressions.Expression<System.Func<System.String>>>(() => RegisterModel.ConfirmedPassword));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(83, "\r\n                    ");
                __Blazor.Bilbayt.WebClient.Pages.Register.TypeInference.CreateValidationMessage_4(__builder2, 84, 85, 
#nullable restore
#line 51 "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\Pages\Register.razor"
                                              () => RegisterModel.ConfirmedPassword

#line default
#line hidden
#nullable disable
                );
                __builder2.CloseElement();
                __builder2.AddMarkupContent(86, "\r\n                ");
                __builder2.AddMarkupContent(87, "<button type=\"submit\" class=\"btn btn-primary\">Submit</button>");
            }
            ));
            __builder.CloseComponent();
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 57 "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\Pages\Register.razor"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
#nullable restore
#line 58 "C:\Users\mahmu\source\repos\Bilbayt\Bilbayt.WebClient\Pages\Register.razor"
           

        private CreateUserModel RegisterModel = new CreateUserModel();
        private bool ShowErrors;
        private bool ShowSpinner;
        private string Error;

        private async Task HandleRegistration()
        {
            ShowSpinner = true;
            ShowErrors = false;

            var result = await AuthService.Register(RegisterModel);

            if (result.IsSuccess)
            {
                NavigationManager.NavigateTo("/login");
            }
            else
            {
                Error = result.ErrorMessage;
                ShowErrors = true;
            }
            ShowSpinner = false;
        }

    

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager NavigationManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IAuthService AuthService { get; set; }
    }
}
namespace __Blazor.Bilbayt.WebClient.Pages.Register
{
    #line hidden
    internal static class TypeInference
    {
        public static void CreateValidationMessage_0<TValue>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, global::System.Linq.Expressions.Expression<global::System.Func<TValue>> __arg0)
        {
        __builder.OpenComponent<global::Microsoft.AspNetCore.Components.Forms.ValidationMessage<TValue>>(seq);
        __builder.AddAttribute(__seq0, "For", __arg0);
        __builder.CloseComponent();
        }
        public static void CreateValidationMessage_1<TValue>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, global::System.Linq.Expressions.Expression<global::System.Func<TValue>> __arg0)
        {
        __builder.OpenComponent<global::Microsoft.AspNetCore.Components.Forms.ValidationMessage<TValue>>(seq);
        __builder.AddAttribute(__seq0, "For", __arg0);
        __builder.CloseComponent();
        }
        public static void CreateValidationMessage_2<TValue>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, global::System.Linq.Expressions.Expression<global::System.Func<TValue>> __arg0)
        {
        __builder.OpenComponent<global::Microsoft.AspNetCore.Components.Forms.ValidationMessage<TValue>>(seq);
        __builder.AddAttribute(__seq0, "For", __arg0);
        __builder.CloseComponent();
        }
        public static void CreateValidationMessage_3<TValue>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, global::System.Linq.Expressions.Expression<global::System.Func<TValue>> __arg0)
        {
        __builder.OpenComponent<global::Microsoft.AspNetCore.Components.Forms.ValidationMessage<TValue>>(seq);
        __builder.AddAttribute(__seq0, "For", __arg0);
        __builder.CloseComponent();
        }
        public static void CreateValidationMessage_4<TValue>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, global::System.Linq.Expressions.Expression<global::System.Func<TValue>> __arg0)
        {
        __builder.OpenComponent<global::Microsoft.AspNetCore.Components.Forms.ValidationMessage<TValue>>(seq);
        __builder.AddAttribute(__seq0, "For", __arg0);
        __builder.CloseComponent();
        }
    }
}
#pragma warning restore 1591
