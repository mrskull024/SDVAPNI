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
    public partial class Reportes : System.Web.UI.Page
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
                    BindVisitData();
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

        private void GetDataForReport()
        {
            try
            {
                CreateConnection();
                OpenConnection();
                _sqlCommand.CommandText = "ListDatabyDate";
                _sqlCommand.CommandType = CommandType.StoredProcedure;
                _sqlCommand.Parameters.AddWithValue("@Event", "Select");
                _sqlCommand.Parameters.AddWithValue("@fecha_inicio", Convert.ToString(TxtDateStart.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@fecha_fin", Convert.ToString(TxtDateEnd.Text.Trim()));
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

        protected void BtnDownloadPDF_Click(object sender, EventArgs e)
        {
            PdfPTable pdfPTable = new PdfPTable(gvVisitsList.HeaderRow.Cells.Count);
            pdfPTable.DefaultCell.FixedHeight = 100f;

            foreach (TableCell headerCell in gvVisitsList.HeaderRow.Cells)
            {
                PdfPCell pdfCell = new PdfPCell(new Phrase(headerCell.Text));
                pdfPTable.AddCell(pdfCell);

            }


            foreach (GridViewRow gridViewRow in gvVisitsList.Rows)
            {
                foreach (TableCell tableCell in gridViewRow.Cells)
                {

                    PdfPCell pdfCell = new PdfPCell(new Phrase(tableCell.Text));
                    pdfPTable.AddCell(pdfCell);
                }
            }


            Document pdfDocument = new Document(PageSize.A4.Rotate(), 5f, 5f, 5f, 5f);
            PdfWriter.GetInstance(pdfDocument, Response.OutputStream);
            Paragraph titulo = new Paragraph("Reporte de Visitas Generado el: " + DateTime.Now);
            Paragraph fechas = new Paragraph("Con rango de fechas de: " + TxtDateStart.Text + " hasta: " + TxtDateEnd.Text);
    
            pdfDocument.Open();
            pdfDocument.Add(titulo);
            titulo.SpacingAfter = 200f;
            pdfDocument.Add(fechas);
            pdfDocument.Add(Chunk.NEWLINE);

            pdfDocument.Add(pdfPTable);
            pdfDocument.Close();

            Response.ContentType = "application/pdf";
            Response.AppendHeader("content-disposition", "attachment;filename=Visitas.pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            //Response.End();
        }

        protected void BtnDonwloadXLS_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.AppendHeader("content-disposition", "attachment;filename=Visitas.xls");
            Response.ContentType = "application/excel";

            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);

            gvVisitsList.HeaderRow.Style.Add("background-color", "#FFFFF");
            foreach(TableCell tableCell in gvVisitsList.HeaderRow.Cells)
            {
                tableCell.Style["background-color"] = "#A55129";
            }
            foreach(GridViewRow gridViewRow in gvVisitsList.Rows)
            {
                gvVisitsList.BackColor = System.Drawing.Color.White;
                foreach(TableCell tableCell2 in gridViewRow.Cells)
                {
                    tableCell2.Style["background-color"] = "#FFF7E7";
                }
            }

            gvVisitsList.RenderControl(htmlTextWriter);
            Response.Write(stringWriter.ToString());
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            
        }

        protected void BtnDonwloadDOC_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.AppendHeader("content-disposition", "attachment;filename=Visitas.doc");
            Response.ContentType = "application/word";

            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);

            gvVisitsList.HeaderRow.Style.Add("background-color", "#FFFFF");
            foreach (TableCell tableCell in gvVisitsList.HeaderRow.Cells)
            {
                tableCell.Style["background-color"] = "#A55129";
            }
            foreach (GridViewRow gridViewRow in gvVisitsList.Rows)
            {
                gvVisitsList.BackColor = System.Drawing.Color.White;
                foreach (TableCell tableCell2 in gridViewRow.Cells)
                {
                    tableCell2.Style["background-color"] = "#FFF7E7";
                }
            }

            gvVisitsList.RenderControl(htmlTextWriter);
            Response.Write(stringWriter.ToString());
            Response.End();
        }

        protected void BtnListData_Click(object sender, EventArgs e)
        {
            GetDataForReport();
        }
    }
}