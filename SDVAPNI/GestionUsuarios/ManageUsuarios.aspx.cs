using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SDVAPNI.ClaseMensaje;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SDVAPNI.GestionUsuarios
{
    public partial class ManageUsuarios : System.Web.UI.Page
    {
        private string strConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private readonly string NombreUsuario = HttpContext.Current.User.Identity.Name;
        private SqlCommand _sqlCommand;
        private SqlDataAdapter _sqlDataAdapter;
        DataSet _dtSet;

        protected void Page_Load(object sender, EventArgs e)
        {
            var Credenciales = HttpContext.Current.User.Identity.IsAuthenticated;       

            if (Credenciales)
            {
                if (!IsPostBack)
                {
                    ObtenerRoles();
                    ListarUsuariosyRoles();
                    lblWelcomeUser.Text = "Bienvenido/a, " + SelectNames(NombreUsuario) + "!";

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

        private void ObtenerRoles()
        {
            var TipoRol = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            var rol = TipoRol.Roles;
            this.DpdRoleId.DataSource = rol.ToList();
            DpdRoleId.DataTextField = "Name";
            DpdRoleId.DataValueField = "Name";
            DpdRoleId.DataBind();
            DpdRoleId.Items.Add(new ListItem("--- Seleccione un Rol ---", "0"));
            DpdRoleId.SelectedValue = "0";
        }

        protected void Alerta(string Message, string type)
        {
            Mensaje mensaje = new Mensaje(this.Page);
            mensaje.ShowMessage(Message, type);
        }

        private void LimpiarDatos()
        {
            TxtUsuario.Text = string.Empty;
            TxtPassword1.Text = string.Empty;
            TxtEmail.Text = string.Empty;
            TxtPhoneNumber.Text = string.Empty;
            DpdRoleId.SelectedValue = "0";
            TxtUsuario.Focus();
        }

        private void ListarUsuariosyRoles()
        {
            try
            {
                CreateConnection();
                OpenConnection();
                _sqlCommand.CommandText = "ObtenerUsuarios";
                _sqlCommand.CommandType = CommandType.StoredProcedure;
                _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
                _dtSet = new DataSet();
                _sqlDataAdapter.Fill(_dtSet);
                gvUserRols.DataSource = _dtSet;
                gvUserRols.DataBind();
            }
            catch (Exception ex)
            {
                Response.Redirect("Ocurrio un Error al Listar los Usuarios: " + ex);
            }
            finally
            {
                CloseConnection();
                DisposeConnection();
            }
        }

        protected void ChangeName(string Id)
        {
            CreateConnection();
            OpenConnection();           
            _sqlCommand.CommandText = "AddCompleteName";
            _sqlCommand.Parameters.AddWithValue("@Event", "Update");
            _sqlCommand.Parameters.AddWithValue("@CompleteName", TxtFullName.Text.Trim());
            _sqlCommand.Parameters.AddWithValue("@Id", Id);
            _sqlCommand.CommandType = CommandType.StoredProcedure;
            int result = Convert.ToInt32(_sqlCommand.ExecuteNonQuery());
            if (result > 0)
            {
                //Alerta("El Nombre fue Removido Exitosamente", "Success");
            }
            else
            {
                Alerta("Ocurrio un Error al Eliminar el Registro", "Primary");
            }
        }

        public string SelectNames(string UserName)
        {
            string resultName = string.Empty;
            CreateConnection();
            OpenConnection();
            _sqlCommand.CommandText = "SelectUserNames";
            _sqlCommand.Parameters.AddWithValue("@Event", "Select");
            _sqlCommand.Parameters.AddWithValue("@UserName", UserName);
            _sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = _sqlCommand.ExecuteReader();
            string name = string.Empty;
            while (reader.Read())
            {
                name = reader["CompleteName"].ToString();
            }

            return name;
        }
        protected void BtnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtUsuario.Text.Trim() == string.Empty)
                {
                    Alerta("El campo Usuario es requerido", "Warning");
                    return;
                }
                if (TxtPassword1.Text.Trim() == string.Empty)
                {
                    Alerta("Es necesario una contraseña para registrarte", "Warning");
                    return;
                }
                if (TxtPassword2.Text.Trim() == string.Empty)
                {
                    Alerta("Es necesario una contraseña para registrarte", "Warning");
                    return;
                }
                if (TxtPassword1.Text.Trim() != TxtPassword2.Text.Trim())
                {
                    Alerta("Las Contraseñas deben coincidir", "Warning");
                    return;
                }
                if (TxtEmail.Text.Trim() == string.Empty)
                {
                    Alerta("El Campo Correo es requerido", "Warning");
                    return;
                }
                if (TxtPhoneNumber.Text.Trim() == string.Empty)
                {
                    Alerta("El campo Telefono es requerido", "Warning");
                    return;
                }
                if(TxtFullName.Text.Trim()== string.Empty)
                {
                    Alerta("El campo Nombres es requerido", "Warning");
                    return;
                }

                var GuardarUsuario = new UserStore<IdentityUser>();
                var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
                var Manager = new UserManager<IdentityUser>(GuardarUsuario);
                IdentityUser Usuario = new IdentityUser() { UserName = TxtUsuario.Text.Trim(), Email = TxtEmail.Text.Trim(), PhoneNumber = TxtPhoneNumber.Text.Trim()  };
                var Resultado = Manager.Create(Usuario, TxtPassword1.Text.Trim());

                if (Resultado.Succeeded)
                {
                    var CurrentUser = Manager.FindByName(Usuario.UserName);
                    var CurrentRol = RoleManager.FindByName(DpdRoleId.SelectedValue);
                    var ResultadoInsert = Manager.AddToRole(CurrentUser.Id, CurrentRol.Name);

                    if (ResultadoInsert.Succeeded)
                    {
                        Alerta("Se ha registrado Exitosamente", "Success");
                        ChangeName(CurrentUser.Id);

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "MostrarModalUsuario();", true);
                        LimpiarDatos();
                        return;
                    }
                }
                else
                {
                    Alerta(Resultado.Errors.FirstOrDefault(), "Warning");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "MostrarModalUsuario();", true);
                }

            }

            catch (Exception ex)
            {
                Alerta("Ocurrio un error!!!", "Error");
                //sClsLog.guardarLog(Context.User.Identity.GetUserName(), "", "Error al registrar usuario", ex.Message + " StactkTrace: " + ex.StackTrace, "Registrar.aspx");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "MostrarModalUsuario();", true);
                string texto = ex.Message;
            }
        }

        public class Users_in_Role_ViewModel
        {
            public string UserId { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string PhoneN { get; set; }
            public string Role { get; set; }
            public string CompleteName { get; set; }
        }

        protected void gvUserRols_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "Seleccionar")
            {
                string Usuario = gvUserRols.Rows[index].Cells[1].Text;
                string Rol = gvUserRols.Rows[index].Cells[0].Text;

                var userStore = new UserStore<IdentityUser>();
                var Manager = new UserManager<IdentityUser>(userStore);
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
                var UsuarioActual = Manager.FindByName(Usuario);
                var RolActual = roleManager.FindByName(Rol);

                if (UsuarioActual != null && RolActual != null)
                {
                    TxtUsuario.Text = UsuarioActual.UserName;
                    TxtFullName.Text = SelectNames(UsuarioActual.UserName);
                    TxtEmail.Text = UsuarioActual.Email;
                    TxtPhoneNumber.Text = UsuarioActual.PhoneNumber;
                    DpdRoleId.SelectedValue = RolActual.Name;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "MostrarModalUsuario();", true);
                }
                else
                {
                    Alerta("Ocurrio un error al Seleccionar el Usuario", "Error");
                }
            }

            if(e.CommandName == "Desactivar")
            {
                var userStore = new UserStore<IdentityUser>();
                var manager = new UserManager<IdentityUser>(userStore);

                var currentUser = manager.FindByName(gvUserRols.Rows[index].Cells[1].Text);
                var result = manager.Delete(currentUser);
                if (result.Succeeded)
                {
                    Alerta("Se eliminó exitosamente", "Success");
                    ListarUsuariosyRoles();
                }
                else
                {
                    Alerta(result.Errors.FirstOrDefault(), "Info");
                }

            }

            if (e.CommandName == "Editar")
            {
                string user = gvUserRols.Rows[index].Cells[1].Text;
                TxtUsuarioNew.Text = user;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "MostrarModalResetearCredenciales();", true);
            }
        }

        protected void BtnModificarCredenciales_Click(object sender, EventArgs e)
        {
            try
            {
                string user = TxtUsuarioNew.Text.Trim();

                if (user == string.Empty)
                {
                    Alerta("El nombre de usuario es requerido!", "Info");
                    return;
                }
                if (TxtPassword1New.Text.Trim() == string.Empty)
                {
                    Alerta("La contraseña es requerido!", "Info");
                    return;
                }
                var userStore = new UserStore<IdentityUser>();
                var manager = new UserManager<IdentityUser>(userStore);
                var currentUser = manager.FindByName(user);

                if (currentUser != null)
                {
                    var result = manager.RemovePassword(currentUser.Id);
                    if (result.Succeeded)
                    {
                        var result2 = manager.AddPassword(currentUser.Id, TxtPassword1New.Text.Trim());
                        if (result2.Succeeded)
                        {
                            Alerta("Se actualizó exitosamente la credenciales", "Success");
                        }
                        else
                        {
                            Alerta(result.Errors.FirstOrDefault(), "Info");
                        }
                    }
                    else
                    {
                        Alerta(result.Errors.FirstOrDefault(), "Info");
                    }
                }
                else
                {
                    Alerta("Ocurrio un error al editar el usuario", "Info");
                }
            }
            catch (Exception ex)
            {
                Alerta("Ocurrio un error!!!" + ex, "Error");
            }
        }
    }
}