

// See https://aka.ms/new-console-template for more information
using System.Reflection;

using drz.Infrastructure.CAD.MessageService;
[assembly: AssemblyInformationalVersion("PlotSPDS for nanoCAD")]
internal class Program
{




    [STAThread]
    static void Main(string[] args)
    {
        //по шагам корректно отладить не получится
        //потому что если переместить сборку MessageService.dll в другой каталог до начала отладки, студия ее опять создаст,
        //и на первом шаге эта сборка уже будет загружена в домен

        //поэтому компилируем как есть
        //сборку MessageService.dll перемещаем в каталог рядом  еxe
        //запускаем и видим, что нифига не работает
        try
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }
        catch { };


        //класс Msg в другой сборке и эта сборка, не рядом с аддоном
        //сборка не загрузится, 
        //AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve; не сработает
        //брякпойнт на входе в Init не сработает
        //****ТАК ДЕЛАТЬ НЕЛЬЗЯ*****
        try
        {
            //комментируем код ниже и все работает
            Msg msg = new();
            msg.MsgInfo("Сборка в одном каталоге с аддоном");

        }
        catch { }

        //****ТАК ДЕЛАТЬ МОЖНО*****
        test();

        Console.ReadKey();
    }

    static void test()
    {
        Msg msg = new();
        msg.MsgInfo("Ok");
    }

    #region System.Reflection            
    static  private System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
    {
        string sPath = string.Empty;

        if (args.Name.IndexOf(",") > -1)
        {
            sPath = AssemblFulNameDll(args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll");
        }
        else
        {
            sPath = AssemblFulNameDll(args.Name + ".dll");
        }


        if (sPath != string.Empty)
        {
            return System.Reflection.Assembly.LoadFile(sPath);
        }
        return null;
    }

    /// <summary>
    /// Получить полный путь к файлу загружаемой dll
    /// </summary>
    /// <param name="sDllName">имя dll</param>
    /// <returns>Путь и имя к библиотеке</returns>
    /*static*/
    static string AssemblFulNameDll(string sDllName)
    {
        Assembly asm = Assembly.GetExecutingAssembly();
        string sAsmFileFullName = asm.Location;//каталог DLL
        string asmPath = String.Empty;

        // string sAsmFileFullName = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
        string sPath = Directory.GetParent(sAsmFileFullName).FullName;


        string[] asmPaths = GetFilesOfDir(sPath, true, sDllName);
        if (asmPaths.Length > 0)
        {
            string asmPathTmp = asmPaths[0];//хватаем первую в списке

            if (File.Exists(asmPathTmp)) asmPath = asmPathTmp;//присваиваем ее значение
        }
        return asmPath;
    }

    #endregion

    /// <summary>Получить список путей фалов в директории</summary>
    /// <param name="sPath">Директория с файлами</param>
    /// <param name="WithSubfolders">Учитывать поддиректории</param>
    /// <param name="sSerchPatern">Маска поиска</param>
    /// <returns>Пути к файлам</returns>
    internal static string[] GetFilesOfDir(string sPath, bool WithSubfolders, string sSerchPatern = "*.dwg")
    {
        try
        {
            return Directory.GetFiles(sPath,
                                        sSerchPatern,
                                        (WithSubfolders
                                        ? SearchOption.AllDirectories
                                        : SearchOption.TopDirectoryOnly));
        }
        catch (System.Exception ex)
        {
#if NC || AC
                Cad.DocumentManager.MdiActiveDocument.Editor.WriteMessage("\n" + ex.Message);
#endif
            return new string[0];
        }
    }

}