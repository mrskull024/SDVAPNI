using SDVAPNI.ClaseMensaje;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SDVAPNI.Gestion
{
    public partial class Visitas : System.Web.UI.Page
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
                    BindVisitData();
                    BtnEditarVisita.Enabled = false;
                    TxtRegistradoPor.Text = Nusuario;
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

        private void BindVisitData()
        {
            try
            {
                CreateConnection();
                OpenConnection();
                _sqlCommand.CommandText = "VisitasCrud";
                _sqlCommand.CommandType = CommandType.StoredProcedure;
                _sqlCommand.Parameters.AddWithValue("@Event", "Read");
                _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
                _dtSet = new DataSet();
                _sqlDataAdapter.Fill(_dtSet);
                gvVisitsList.DataSource = _dtSet;
                gvVisitsList.DataBind();
            }
            catch (Exception ex)
            {
                Response.Redirect("Ocurrio un Error al Listar las Visitas: " + ex);
            }
            finally
            {
                CloseConnection();
                DisposeConnection();
            }
        }

        private void LimpiarDatos()
        {
            TxtCedula.Text = string.Empty;
            TxtNombres.Text = string.Empty;
            TxtEmpresa.Text = string.Empty;
            DpdOpcionesAreas.SelectedValue = "-1";
            TxtReferencia.Text = string.Empty;
            TxtReferencia.Text = string.Empty;
            TxtRegistradoPor.Text = string.Empty;
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

        protected void gvVisitsList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());

                if (e.CommandName == "Seleccionar")
                {
                    DateTime FechaSinHora = new DateTime();
                    BtnEditarVisita.Enabled = true;

                    LinkButton lnkBtn = (LinkButton)e.CommandSource;
                    GridViewRow viewRow = (GridViewRow)lnkBtn.Parent.Parent;
                    GridView grid = (GridView)sender;
                    string Id = grid.DataKeys[viewRow.RowIndex].Value.ToString();
                    Session["id"] = Id;

                    string Cedula = gvVisitsList.Rows[index].Cells[1].Text;
                    string Nombre = gvVisitsList.Rows[index].Cells[2].Text;
                    string Empresa = gvVisitsList.Rows[index].Cells[3].Text;
                    string AreaVisita = gvVisitsList.Rows[index].Cells[4].Text;
                    string ReferenciaVisita = gvVisitsList.Rows[index].Cells[5].Text;
                    FechaSinHora = Convert.ToDateTime(gvVisitsList.Rows[index].Cells[6].Text);
                    string RegistradoPor = gvVisitsList.Rows[index].Cells[7].Text;

                    TxtCedula.Text = Cedula;
                    TxtNombres.Text = Nombre;
                    TxtEmpresa.Text = Empresa;
                    DpdOpcionesAreas.SelectedItem.Text = AreaVisita;
                    TxtReferencia.Text = ReferenciaVisita;
                    TxtFechaRegistro.Text = FechaSinHora.ToString("yyyy-MM-dd");
                    TxtRegistradoPor.Text = RegistradoPor;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "MostrarModalVisita();", true);
                }
                else
                {
                    Alerta("Ha Ocurrido Un Error al Listar al Usuario Seleccionado", "Warning");
                }

                if (e.CommandName == "Eliminar")
                {
                    try
                    {
                        LinkButton lnkBtn = (LinkButton)e.CommandSource;
                        GridViewRow viewRow = (GridViewRow)lnkBtn.Parent.Parent;
                        GridView grid = (GridView)sender;
                        string id = grid.DataKeys[viewRow.RowIndex].Value.ToString();

                        CreateConnection();
                        OpenConnection();
                        //string Id = gvVisitsList.Rows[index].Cells[0].Text;
                        _sqlCommand.CommandText = "VisitasCrud";
                        _sqlCommand.Parameters.AddWithValue("@Event", "Delete");
                        _sqlCommand.Parameters.AddWithValue("@id", Convert.ToInt32(id));
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        int result = Convert.ToInt32(_sqlCommand.ExecuteNonQuery());
                        if (result > 0)
                        {
                            Alerta("El registro fue Removido Exitosamente", "Success");
                            gvVisitsList.EditIndex = -1;
                            BindVisitData();
                        }
                        else
                        {
                            Alerta("Ocurrio un Error al Eliminar el Registro", "Primary");
                            BindVisitData();
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
            catch (Exception ex)
            {
                Alerta("Ha Ocurrido Un Error: " + ex, "Warning");
            }
        }

        protected void BtnBuscarXCedula_Click(object sender, EventArgs e)
        {
            try
            {
                CreateConnection();
                _sqlCommand.CommandType = CommandType.StoredProcedure;
                _sqlCommand.CommandText = "FindByCedula";
                _sqlCommand.Parameters.AddWithValue("@cedula", SqlDbType.VarChar).Value = TxtCedula.Text.Trim();
                OpenConnection();
                SqlDataReader Reader = _sqlCommand.ExecuteReader();

                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        TxtNombres.Text = Convert.ToString(Reader["nombre"]);
                        TxtEmpresa.Text = Convert.ToString(Reader["empresa"]);

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "MostrarModalVisita();", true);
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

        protected void BtnRegistrarVisita_Click(object sender, EventArgs e)
        {
            try
            {
                CreateConnection();
                OpenConnection();
                _sqlCommand.CommandText = "VisitasCrud";
                _sqlCommand.CommandType = CommandType.StoredProcedure;
                _sqlCommand.Parameters.AddWithValue("@Event", "Create");
                _sqlCommand.Parameters.AddWithValue("@cedula", Convert.ToString(TxtCedula.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@nombre", Convert.ToString(TxtNombres.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@empresa", Convert.ToString(TxtEmpresa.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@area_visita", Convert.ToString(DpdOpcionesAreas.SelectedItem.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@ref_visita", Convert.ToString(TxtReferencia.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@fecha_registro", Convert.ToString(TxtFechaRegistro.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@registrado_por", Convert.ToString(TxtRegistradoPor.Text.Trim()));
                int Resultado = Convert.ToInt32(_sqlCommand.ExecuteNonQuery());
                if (Resultado > 0)
                {
                    Alerta("El registro ha sido Guardado Exitosamente", "Success");
                    BindVisitData();
                    LimpiarDatos();
                }
                else
                {
                    Alerta("Ha Ocurrido un error al insertar los datos", "Warning");
                }
            }
            catch (Exception ex)
            {
                Alerta("Info: " + ex, "Info");
            }
            finally
            {
                CloseConnection();
                DisposeConnection();
            }
        }

        protected void BtnEditarVisita_Click(object sender, EventArgs e)
        {
            try
            {
                CreateConnection();
                OpenConnection();
                _sqlCommand.CommandText = "VisitasCrud";
                _sqlCommand.CommandType = CommandType.StoredProcedure;
                _sqlCommand.Parameters.AddWithValue("@Event", "Create");
                _sqlCommand.Parameters.AddWithValue("@cedula", Convert.ToString(TxtCedula.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@nombre", Convert.ToString(TxtNombres.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@empresa", Convert.ToString(TxtEmpresa.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@area_visita", Convert.ToString(DpdOpcionesAreas.SelectedItem.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@ref_visita", Convert.ToString(TxtReferencia.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@fecha_registro", Convert.ToString(TxtFechaRegistro.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@registrado_por", Convert.ToString(TxtRegistradoPor.Text.Trim()));
                int Resultado = Convert.ToInt32(_sqlCommand.ExecuteNonQuery());
                if (Resultado > 0)
                {
                    Alerta("El registro ha sido Guardado Exitosamente", "Success");
                    BindVisitData();
                    LimpiarDatos();
                }
                else
                {
                    Alerta("Ha Ocurrido un error al insertar los datos", "Warning");
                }
            }
            catch (Exception ex)
            {
                Alerta("Info: " + ex, "Info");
            }
            finally
            {
                CloseConnection();
                DisposeConnection();
            }
        }

        protected void gvVisitsList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvVisitsList.PageSize = 5;
            gvVisitsList.PageIndex = e.NewPageIndex;
            BindVisitData();
        }
    }
}