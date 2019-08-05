using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SDVAPNI.Gestion
{
    public partial class ReportesEquipos : System.Web.UI.Page
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

        private void GetDataForReport()
        {
            try
            {
                CreateConnection();
                OpenConnection();
                _sqlCommand.CommandText = "ListDatabyDatePcs";
                _sqlCommand.CommandType = CommandType.StoredProcedure;
                _sqlCommand.Parameters.AddWithValue("@Event", "Select");
                _sqlCommand.Parameters.AddWithValue("@fecha_inicio", Convert.ToString(TxtDateStart.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@fecha_fin", Convert.ToString(TxtDateEnd.Text.Trim()));
                _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
                _dtSet = new DataSet();
                _sqlDataAdapter.Fill(_dtSet);
                gvEquiposList.DataSource = _dtSet;
                gvEquiposList.DataBind();
            }
            catch (Exception ex)
            {
                Response.Redirect("Ocurrio un Error al Listar los Equipos: " + ex);
            }
            finally
            {
                CloseConnection();
                DisposeConnection();
            }
        }

        protected void BtnListData_Click(object sender, EventArgs e)
        {
            GetDataForReport();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }
        protected void BtnDownloadPDF_Click(object sender, EventArgs e)
        {
            PdfPTable pdfPTable = new PdfPTable(gvEquiposList.HeaderRow.Cells.Count);
            pdfPTable.DefaultCell.FixedHeight = 100f;

            foreach (TableCell headerCell in gvEquiposList.HeaderRow.Cells)
            {
                PdfPCell pdfCell = new PdfPCell(new Phrase(headerCell.Text));
                pdfPTable.AddCell(pdfCell);

            }


            foreach (GridViewRow gridViewRow in gvEquiposList.Rows)
            {
                foreach (TableCell tableCell in gridViewRow.Cells)
                {

                    PdfPCell pdfCell = new PdfPCell(new Phrase(tableCell.Text));
                    pdfPTable.AddCell(pdfCell);
                }
            }


            Document pdfDocument = new Document(PageSize.A4.Rotate(), 5f, 5f, 5f, 5f);
            PdfWriter.GetInstance(pdfDocument, Response.OutputStream);
            Paragraph titulo = new Paragraph("Reporte de Ingreso de Equipos Generado el: " + DateTime.Now);
            Paragraph fechas = new Paragraph("Con rango de fechas de: " + TxtDateStart.Text + " hasta: " + TxtDateEnd.Text);

            pdfDocument.Open();
            pdfDocument.Add(titulo);
            titulo.SpacingAfter = 200f;
            pdfDocument.Add(fechas);
            pdfDocument.Add(Chunk.NEWLINE);

            pdfDocument.Add(pdfPTable);
            pdfDocument.Close();

            Response.ContentType = "application/pdf";
            Response.AppendHeader("content-disposition", "attachment;filename=IngresoEquipos.pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            //Response.End();
        }

        protected void BtnDonwloadXLS_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.AppendHeader("content-disposition", "attachment;filename=IngresoEquipos.xls");
            Response.ContentType = "application/excel";

            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);

            gvEquiposList.HeaderRow.Style.Add("background-color", "#FFFFF");
            foreach (TableCell tableCell in gvEquiposList.HeaderRow.Cells)
            {
                tableCell.Style["background-color"] = "#A55129";
            }
            foreach (GridViewRow gridViewRow in gvEquiposList.Rows)
            {
                gvEquiposList.BackColor = System.Drawing.Color.White;
                foreach (TableCell tableCell2 in gridViewRow.Cells)
                {
                    tableCell2.Style["background-color"] = "#FFF7E7";
                }
            }

            gvEquiposList.RenderControl(htmlTextWriter);
            Response.Write(stringWriter.ToString());
            Response.End();
        }

        protected void BtnDonwloadDOC_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.AppendHeader("content-disposition", "attachment;filename=IngresoEquipos.doc");
            Response.ContentType = "application/word";

            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);

            gvEquiposList.HeaderRow.Style.Add("background-color", "#FFFFF");
            foreach (TableCell tableCell in gvEquiposList.HeaderRow.Cells)
            {
                tableCell.Style["background-color"] = "#A55129";
            }
            foreach (GridViewRow gridViewRow in gvEquiposList.Rows)
            {
                gvEquiposList.BackColor = System.Drawing.Color.White;
                foreach (TableCell tableCell2 in gridViewRow.Cells)
                {
                    tableCell2.Style["background-color"] = "#FFF7E7";
                }
            }

            gvEquiposList.RenderControl(htmlTextWriter);
            Response.Write(stringWriter.ToString());
            Response.End();
        }
    }
}