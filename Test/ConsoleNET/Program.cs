

// See https://aka.ms/new-console-template for more information
using System.Reflection;

using drz.Infrastructure.CAD.MessageService;
using drz.Infrastructure.CAD.Service;
[assembly: AssemblyInformationalVersion("PlotSPDS for nanoCAD")]
internal class Program
{
    public static AsmInfo ASM { get; set; }


    [STAThread]
    static void Main(string[] args)
    {

        Assembly asm = Assembly.GetExecutingAssembly();

        string sTitle;
        var titl = (Attribute.GetCustomAttribute(asm,
                        typeof(AssemblyTitleAttribute),
            false) as AssemblyTitleAttribute);
        if(titl is not null)
        {
            sTitle = titl.Title;
        }
        else
        {
            sTitle = "Unc";
        }

         //sTitle = (Attribute.GetCustomAttribute(
         //   asm,
         //   typeof(AssemblyTitleAttribute),
         //   false) as AssemblyTitleAttribute).Title;


        ASM = new AsmInfo(Assembly.GetExecutingAssembly());

        var ff = ASM.sTitleAttribute;

        Msg msg = new();
        msg.MsgInfo(ff);

        //MessageBox.Show(message, "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);

        Console.WriteLine("Hello, World!");//! test
        Console.ReadKey();
    }


}