using System.Reflection;
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

#if NET
[assembly: AssemblyInformationalVersion("MesagServise for CAD")]
#endif

namespace drz.Infrastructure.CAD.MessageService
{
    /// <summary>
    /// сервис сообщений
    /// </summary>
    public partial class Msg
    {
        /// <summary>
        /// MSGs the error.
        /// </summary>
        /// <param name="message">The message.</param>
        public void MsgError(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// MSGs the information.
        /// </summary>
        /// <param name="message">The message.</param>
        public void MsgInfo(string message)
        {
            MessageBox.Show(message, "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// MSGs the warning.
        /// </summary>
        /// <param name="message">The message.</param>
        public void MsgWarning(string message)
        {
            MessageBox.Show(message, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// MSGs the console.
        /// </summary>
        /// <param name="message">The message.</param>
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
