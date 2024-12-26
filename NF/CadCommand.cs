//! Created by dRz on the WIN-CGR 21.12.2024 21:14:12
using System.ComponentModel;
using System.Reflection;
#if NC
using App = HostMgd.ApplicationServices;
using Cad = HostMgd.ApplicationServices.Application;
using Db = Teigha.DatabaseServices;
using Ed = HostMgd.EditorInput;
using Rtm = Teigha.Runtime;
using Gem = Teigha.Geometry;
using drz.Infrastructure.Service;
#elif AC
using Autodesk.AutoCAD.Windows;
using App = Autodesk.AutoCAD.ApplicationServices;
using Cad = Autodesk.AutoCAD.ApplicationServices.Application;
using Db = Autodesk.AutoCAD.DatabaseServices;
using Ed = Autodesk.AutoCAD.EditorInput;
using Gem = Autodesk.AutoCAD.Geometry;
using Rtm = Autodesk.AutoCAD.Runtime;
#endif
// Reserved template parameters
// itemname - CadCommand
// machinename - WIN-CGR
// projectname	 - Test CMD INFO
// registeredorganization - 
// rootnamespace - $rootnamespace$
// defaultnamespace - $defaultnamespace$
// safeitemname - CadCommand
// safeitemrootname - CadCommand
// safeprojectname - Test_CMD_INFO
// targetframeworkversion - 4.7.2
// time - 21.12.2024 21:14:12"
// specifiedsolutionname - nanoCADCommandsReflection
// userdomain - WIN-CGR
// username - dRz"
// webnamespace - $webnamespace$
// year - 2024



//https://learn.microsoft.com/en-us/visualstudio/ide/template-parameters?view=vs-2022
// [assembly: Rtm.CommandClass(typeof(drz.Test_CMD_INFO.CadCommand ))]
[assembly: Rtm.CommandClass(typeof(drz.test.CadCommand))]

// namespace drz.Test_CMD_INFO
namespace drz.test
{
    /// <summary> 
    /// Команды
    /// </summary>
    internal class CadCommand : Rtm.IExtensionApplication
    {
#if !DEBUG
        public void Initialize()
        {
        }
        public void Terminate()
        {
            // throw new System.NotImplementedException();
        }
#else

        #region INIT
        public void Initialize()
        {
            //выводим список команд с описаниями
            CmdInfo comInf = new CmdInfo();
            comInf.Reflection(Assembly.GetExecutingAssembly()); //отдельной сборкой
            //comInf.Reflection(); //модуль в этой сборке
        }

        public void Terminate()
        {
            // throw new System.NotImplementedException();
        }

        #endregion

        #region Command

        Msg msgService = new Msg();

        /// <summary>
        /// переключалка нанобаз
        /// </summary>
        [Rtm.CommandMethod("drz_MyCommand0", Rtm.CommandFlags.Session)]
        [Description("Test 1 : ref Описание команды адын " /*+ nameof(test_cmd)*/)]
        public void test_cmd()
        {
            msgService.ConsoleMessage("Test1 command");
        }

        [Rtm.CommandMethod("drz_MyCommand20", Rtm.CommandFlags.Session)]
        [Description("Test 2 : ref Описание команды два " /*+ nameof(test_cmd2)*/)]
        public void test_cmd2()
        {
            msgService.ConsoleMessage("Test2 command");
        }

        [Rtm.CommandMethod("drz_MyCommand30", Rtm.CommandFlags.Session)]
        [Description("Test 3 : ref Run info assembly " /*+ nameof(test_cmd2)*/)]
        public void test_cmd3()
        {
            //выводим список команд с описаниями
            CmdInfo comInf = new CmdInfo();
            comInf.Reflection(Assembly.GetExecutingAssembly()); //отдельной сборкой
            //comInf.Reflection(); //модуль в этой сборке
        }
        #endregion
#endif
    }
}
