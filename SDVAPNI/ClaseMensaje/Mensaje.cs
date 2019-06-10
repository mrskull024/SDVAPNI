using System.Web.UI;

namespace SDVAPNI.ClaseMensaje
{
    public class Mensaje
    {
        public enum MessageType { Success, Error, Info, Warning };
        private Page page;
        public Mensaje(Page pagina)
        {
            page = pagina;
        }
        public void ShowMessage(string Message, string type)
        {
            try
            {
                ScriptManager.RegisterStartupScript(page, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
            }
            catch
            {

            }
        }
    }
}