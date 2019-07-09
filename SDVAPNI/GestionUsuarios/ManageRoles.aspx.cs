using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SDVAPNI.GestionUsuarios
{
    public partial class ManageRoles : System.Web.UI.Page
    {
        private string strConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private SqlCommand _sqlCommand;
        private SqlDataAdapter _sqlDataAdapter;
        DataSet _dtSet;

        public readonly string Nusuario = HttpContext.Current.User.Identity.Name;
        protected void Page_Load(object sender, EventArgs e)
        {
            var Credenciales = HttpContext.Current.User.Identity.IsAuthenticated;

            CreateConnection();
            OpenConnection();
            _sqlCommand.CommandText = "SelectUserNames";
            _sqlCommand.Parameters.AddWithValue("@Event", "Select");
            _sqlCommand.Parameters.AddWithValue("@UserName", HttpContext.Current.User.Identity.Name);
            _sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = _sqlCommand.ExecuteReader();
            string name = string.Empty;
            while (reader.Read())
            {
                name = reader["CompleteName"].ToString();
                lblWelcomeUser.Text = "Bienvenido /a, " + name.ToString() + "!";
            }

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

        private void CreateConnection()
        {
            SqlConnection _sqlConnection = new SqlConnection(strConnectionString);
            _sqlCommand = new SqlCommand();
            _sqlCommand.Connection = _sqlConnection;
        }

        private void OpenConnection()
        {
            _sqlCommand.Connection.Open();
        }

        private void CloseConnection()
        {
            _sqlCommand.Connection.Close();
        }

        private void DisposeConnection()
        {
            _sqlCommand.Connection.Dispose();
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