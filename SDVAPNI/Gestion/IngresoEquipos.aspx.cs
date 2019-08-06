using SDVAPNI.ClaseMensaje;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace SDVAPNI.Gestion
{
    public partial class IngresoEquipos : System.Web.UI.Page
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

                    lblWelcomeUser.Text = "Bienvenido/a, " + SelectNames(HttpContext.Current.User.Identity.Name) + "!";
                    TxtFirma.Text = SelectNames(HttpContext.Current.User.Identity.Name);
                    TxtFirmaQuienRevisa.Text = SelectNames(HttpContext.Current.User.Identity.Name);
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

        private void LimpiarDatos()
        {
            TxtFecha.Text = string.Empty;
            TxtNombres.Text = string.Empty;
            TxtCompania.Text = string.Empty;
            DpdMarca.SelectedValue = "-1";
            TxtModelo.Text = string.Empty;
            TxtColor.Text = string.Empty;
            TxtSerie.Text = string.Empty;
            TxtPersonaVisita.Text = string.Empty;
            TxtFirma.Text = string.Empty;
            TxtFirmaPortador.Text = string.Empty;
            DpdOption1.SelectedValue = "-1";
            DpdOption2.SelectedValue = "-1";
            DpdOption3.SelectedValue = "-1";
            DpdOption4.SelectedValue = "-1";
            TxtFechaSalida.Text = string.Empty;
            TxtHoraSalida.Text = string.Empty;
            TxtFirmaQuienRevisa.Text = string.Empty;
            TxtFirmaPortadorDelEquipo.Text = string.Empty;
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

        protected void BtnRegistraEquipo_Click(object sender, EventArgs e)
        {
            try
            {
                CreateConnection();
                OpenConnection();
                _sqlCommand.CommandText = "ingresoEquiposCrud";
                _sqlCommand.CommandType = CommandType.StoredProcedure;
                _sqlCommand.Parameters.AddWithValue("@Event", "Create");
                _sqlCommand.Parameters.AddWithValue("@fecha_inicial", Convert.ToString(TxtFecha.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@nombres", Convert.ToString(TxtNombres.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@compania", Convert.ToString(TxtCompania.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@cedula", Convert.ToString(TxtCompania.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@marca", Convert.ToString(DpdMarca.SelectedItem.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@modelo", Convert.ToString(TxtModelo.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@color", Convert.ToString(TxtColor.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@serie", Convert.ToString(TxtSerie.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@referencia_visita", Convert.ToString(TxtPersonaVisita.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@firma_revision", Convert.ToString(TxtFirma.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@firma_portador", Convert.ToString(TxtFirmaPortador.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@misma_marca", Convert.ToString(DpdOption1.SelectedItem.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@mismo_color", Convert.ToString(DpdOption2.SelectedItem.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@mismo_modelo", Convert.ToString(DpdOption3.SelectedItem.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@misma_serie", Convert.ToString(DpdOption4.SelectedItem.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@fecha_salida", Convert.ToString(TxtFechaSalida.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@hora_salida", Convert.ToString(TxtHoraSalida.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@firma_revision_salida", Convert.ToString(TxtFirmaQuienRevisa.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@firma_portador_salida", Convert.ToString(TxtFirmaPortadorDelEquipo.Text.Trim()));

                int Resultado = Convert.ToInt32(_sqlCommand.ExecuteNonQuery());
                if(Resultado > 0)
                {
                    Alerta("El registro ha sido Guardado Exitosamente", "Success");
                    LimpiarDatos();
                }
                else
                {
                    Alerta("Ha Ocurrido un error al insertar los datos", "Warning");
                }

            }
            catch(Exception ex)
            {
                Alerta("Info: " + ex, "Info");
            }
            finally
            {
                CloseConnection();
                DisposeConnection();
            }
        }

        protected void BtnLimpiaEquipo_Click(object sender, EventArgs e)
        {
            LimpiarDatos();
        }

        protected void BtnFindByCedula_Click(object sender, EventArgs e)
        {
            try
            {
                CreateConnection();
                _sqlCommand.CommandType = CommandType.StoredProcedure;
                _sqlCommand.CommandText = "FindPersonPcByCed";
                _sqlCommand.Parameters.AddWithValue("@cedula", SqlDbType.VarChar).Value = TxtFindByCedula.Text.Trim();
                OpenConnection();
                SqlDataReader Reader = _sqlCommand.ExecuteReader();

                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        TxtNombres.Text = Convert.ToString(Reader["nombres"]);
                        TxtCompania.Text = Convert.ToString(Reader["compania"]);
                        TxtCedula.Text = Convert.ToString(Reader["cedula"]);
                        DpdMarca.SelectedItem.Text = Convert.ToString(Reader["marca"]);
                        TxtModelo.Text = Convert.ToString(Reader["modelo"]);
                        TxtColor.Text = Convert.ToString(Reader["color"]);
                        TxtSerie.Text = Convert.ToString(Reader["serie"]);
                        TxtPersonaVisita.Text = Convert.ToString(Reader["referencia_visita"]);
                    }
                }
                else
                {
                    Alerta("No se encontraron registro con la cedula especificada: " + TxtCedula.Text.Trim(), "Info");
                }
            }
            catch
            {

            }
            finally
            {
                CloseConnection();
                DisposeConnection();
            }
        }
    }
}