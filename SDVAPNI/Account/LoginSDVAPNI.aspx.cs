using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using SDVAPNI.ClaseMensaje;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;

namespace SDVAPNI.Account
{
    public partial class LoginSDVAPNI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (User.Identity.IsAuthenticated)
                {
                    Response.Redirect("~/Gestion/Visitas.aspx");
                }
            }
        }

        protected void Alerta(string Message, string type)
        {
            Mensaje utileria = new Mensaje(this.Page);
            utileria.ShowMessage(Message, type);
        }
        protected async void BtnSigIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtUser.Text.Trim() == string.Empty)
                {
                    Alerta("Favor Validar Usuario y Password!", "Warning");
                    return;
                }
                if (TxtPassword.Text.Trim() == string.Empty)
                {
                    Alerta("Favor Validar Usuario y Password!", "Warning");
                    return;
                }

                var UserStore = new UserStore<IdentityUser>();
                var Manager = new UserManager<IdentityUser>(UserStore);
                var Usuario = Manager.Find(TxtUser.Text.Trim(), TxtPassword.Text.Trim());
                if (Usuario != null)
                {
                    var RoldeUsuario = Usuario.Roles;
                    if (RoldeUsuario.Count > 0)
                    {
                        try
                        {
                            Task<bool> SigInWork = TareaDeIncioSesion(Usuario, Manager);
                            await Task<bool>.WhenAll(SigInWork);
                            if (await SigInWork)
                            {
                                Response.Redirect("~/Gestion/Visitas.aspx", false);
                            }
                        }
                        catch (Exception ex)
                        {
                            Alerta("Ha Ocurrido un error al iniciar sesion: " + ex, "Warning");
                        }
                    }
                    else
                    {
                        Alerta("El Usuario no tiene Rol Asignado", "Warning");
                    }
                }
                else
                {
                    Alerta("Las Credenciales No Son Correctas", "Warning");
                }
            }
            catch (Exception ex)
            {
                Alerta("Ocurrio un error: " + ex, "Warning");
            }

        }

        public Task<bool> TareaDeIncioSesion(IdentityUser User, UserManager<IdentityUser> manager)
        {
            var UserIdentity = manager.CreateIdentity(User, DefaultAuthenticationTypes.ApplicationCookie);
            var AuthManager = HttpContext.Current.GetOwinContext().Authentication;
            AuthManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, UserIdentity);
            return Task.FromResult<bool>(true);
        }
    }
}