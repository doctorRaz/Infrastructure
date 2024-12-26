using System.Windows.Forms;





#if NC
using HostMgd.ApplicationServices;
using HostMgd.EditorInput;

using Application = HostMgd.ApplicationServices.Application;

#elif AC
using Autodesk.AutoCAD.ApplicationServices;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;
using Autodesk.AutoCAD.EditorInput;

#endif


namespace drz.Infrastructure.Service
{
    /// <summary>
    /// сервис сообщений делается
    /// </summary>
    public class Msg
    {
        public void ErrorMessage(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void InfoMessage(string message)
        {
            MessageBox.Show(message, "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ConsoleMessage(string message)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
            {
                InfoMessage(message);
            }

            Editor ed = doc.Editor;
            ed.WriteMessage("\n" + message);
        }
    }
}
