using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SDVAPNI.GestionUsuarios
{
    public partial class ManageRoles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var Credenciales = HttpContext.Current.User.Identity.IsAuthenticated;
            if (Credenciales)
            {
                if (!IsPostBack)
                {
                    ListarUsuarios();
                    ListarRoles();
                }
            }
            else
            {
                Response.Redirect("~/Account/LoginSDVAPNI.aspx");
            }
        }

        private void ListarUsuarios()
        {
            try
            {
                var userStore = new UserStore<IdentityUser>();
                var manager = new UserManager<IdentityUser>(userStore);
                var user = manager.Users;
                ddlUsuario.DataSource = user.ToList();
                ddlUsuario.DataTextField = "UserName";
                ddlUsuario.DataValueField = "UserName";
                ddlUsuario.DataBind();
                ddlUsuario.Items.Add(new ListItem("-- Seleccione Usuario --", "0"));
                ddlUsuario.SelectedValue = "0";
            }
            catch
            {

            }
        }

        private void ListarRoles()
        {
            try
            {
                var roleManger = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
                var role = roleManger.Roles;
                this.ddlRoles.DataSource = role.ToList();
                ddlRoles.DataTextField = "Name";
                ddlRoles.DataValueField = "Name";
                ddlRoles.DataBind();
                ddlRoles.Items.Add(new ListItem("-- Seleccione Rol --", "0"));
                ddlRoles.SelectedValue = "0";
            }
            catch
            {

            }
        }

        protected void BtnCrearRol_Click(object sender, EventArgs e)
        {
            try
            {
                var roleManger = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
                roleManger.Create(new IdentityRole(txtNombreRol.Text));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}