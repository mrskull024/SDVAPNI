using SDVAPNI.ClaseMensaje;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace SDVAPNI.Gestion
{
    public partial class EquiposRegistrados : System.Web.UI.Page
    {
        private string strConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private SqlCommand _sqlCommand;
        private SqlDataAdapter _sqlDataAdapter;
        DataSet _dtSet;

        public readonly string Nusuario = HttpContext.Current.User.Identity.Name;
        protected void Page_Load(object sender, EventArgs e)
        {
            var Credenciales = HttpContext.Current.User.Identity.IsAuthenticated;

            if (Credenciales)
            {
                if (!IsPostBack)
                {
                    BindEquiposRegistradosData();
                    lblWelcomeUser.Text = "Bienvenido/a, " + SelectNames(HttpContext.Current.User.Identity.Name) + "!";
                }
            }
            else
            {
                Response.Redirect("~/Account/LoginSDVAPNI.aspx");
            }
        }

        protected void Alerta(string Message, string type)
        {
            Mensaje mensaje = new Mensaje(this.Page);
            mensaje.ShowMessage(Message, type);
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

        private void BindEquiposRegistradosData()
        {
            try
            {
                CreateConnection();
                OpenConnection();
                _sqlCommand.CommandText = "IngresoEquiposCrud";
                _sqlCommand.CommandType = CommandType.StoredProcedure;
                _sqlCommand.Parameters.AddWithValue("@Event", "Read");
                _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
                _dtSet = new DataSet();
                _sqlDataAdapter.Fill(_dtSet);
                gvEquiposRegistrados.DataSource = _dtSet;
                gvEquiposRegistrados.DataBind();
            }
            catch (Exception ex)
            {
                Response.Redirect("Ocurrio un Error al Listar las Equipos Registrados: " + ex);
            }
            finally
            {
                CloseConnection();
                DisposeConnection();
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

        protected void gvEquiposRegistrados_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            gvEquiposRegistrados.PageSize = 10;
            gvEquiposRegistrados.PageIndex = e.NewPageIndex;
            BindEquiposRegistradosData();
        }
    }
}