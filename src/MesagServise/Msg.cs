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


namespace drz.Infrastructure.CAD.MessageService
{
    /// <summary>
    /// сервис сообщений
    /// </summary>
    public partial class Msg
    {
        public void MsgError(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void MsgInfo(string message)
        {
            MessageBox.Show(message, "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void MsgWarning(string message)
        {
            MessageBox.Show(message, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void MsgConsole(string message)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null)
            {
                MsgInfo(message);
            }

            Editor ed = doc.Editor;
            ed.WriteMessage("\n" + message);
        }
    }
}
