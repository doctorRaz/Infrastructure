//! Created by dRz on the WIN-CGR 21.12.2024 21:14:12
using System.ComponentModel;
using System.Reflection;

using drz.Infrastructure.CAD.MessageService;
using drz.Infrastructure;
using drz.Infrastructure.CAD.Service;


#if NC
using App = HostMgd.ApplicationServices;
using Cad = HostMgd.ApplicationServices.Application;
using Db = Teigha.DatabaseServices;
using Ed = HostMgd.EditorInput;
using Rtm = Teigha.Runtime;
using Gem = Teigha.Geometry;

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

        Msg msgService;

        internal static CmdInfo CDI;//эта сборка вывод имен классов
        internal static AsmInfo AI;
        #region INIT
        public void Initialize()
        {
            msgService = new Msg();
            CDI = new CmdInfo(Assembly.GetExecutingAssembly(), true);//эта сборка вывод имен классов
            AI = new AsmInfo(Assembly.GetExecutingAssembly());


            ListCMD();

            //выводим список команд с описаниями и их дубликаты если есть

            //Class1.  ListCMD();
        }

        public void Terminate()
        {
            // throw new System.NotImplementedException();
        }

        #endregion

        #region Command

        /// <summary>
        /// Lists the command.
        /// </summary>
        [Rtm.CommandMethod("t1drz_info", Rtm.CommandFlags.Session)]
        [Description("Информация о командах сборки" /*+ nameof(test_cmd)*/)]
        public void ListCMD()
        {
            //выводим список команд с описаниями
            //CmdDuplInfo CDI = new CmdDuplInfo(Assembly.GetExecutingAssembly(), bMethod);

            //System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<CmdDuplInfo.CmdList>> cdi = CadCommand.CDI.mapInfo;
            string sTitleAttribute = AI.sTitleAttribute;
            string sVersion = AI.sDateRelis();

            Msg msgService = new Msg();
            msgService.MsgConsole(sTitleAttribute + " " + sVersion);

            if (!string.IsNullOrEmpty(CadCommand.CDI.sCmdInfo))
            {
                msgService.MsgConsole(CadCommand.CDI.sCmdInfo);
            }
            else
            {
                msgService.MsgConsole("Нет зарегистрированных команд");
            }

            if (!string.IsNullOrEmpty(CadCommand.CDI.sDuplInfo))
            {
                //msgService.MsgConsole("_____________________");
                msgService.MsgConsole(CadCommand.CDI.sDuplInfo);
                //msgService.MsgConsole("_____________________");

            }
        }

        /// <summary>
        /// Tests the command.
        /// </summary>
        [Rtm.CommandMethod("t1drz_true", Rtm.CommandFlags.Session)]
        [Description("Test 1 : Получение списка команд с описаниями" /*+ nameof(test_cmd)*/)]
        public void test_cmd()
        {
            ListCMD();
            var tt = CDI;
        }

        [Rtm.CommandMethod("t1drz_false", Rtm.CommandFlags.Session)]
        [Description("Test 1 : Получение списка команд с описаниями" /*+ nameof(test_cmd)*/)]
        public void test_cmd1()
        {
            ListCMD();
        }

        [Rtm.CommandMethod("t1drz_default", Rtm.CommandFlags.Session)]
        [Description("Test 1 : Получение списка команд с описаниями" /*+ nameof(test_cmd)*/)]
        public void test_cmddef()
        {
            ListCMD();
        }

        //---

        [Rtm.CommandMethod("t2_drz_MyCommand2", Rtm.CommandFlags.Session)]
        [Description("Test 2 : дубль 1 " /*+ nameof(test_cmd2)*/)]
        public void test_cmd2()
        {
            msgService.MsgConsole("Test2 command");
        }




        #endregion



    }
}
