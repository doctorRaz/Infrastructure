using System;
using System.IO;
using System.Reflection;

#if NET
[assembly: AssemblyInformationalVersion("AsmInfo for CAD")]
#endif

namespace drz.Infrastructure.CAD.Service
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AsmInfo
    {

        #region Служебные
        /// <summary>Домен машины</summary>
        internal string Userdomain = System.Environment.GetEnvironmentVariable("USERDOMAIN");


        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="AsmInfo"/> class.
        /// </summary>
        public AsmInfo()
        {
            asm = Assembly.GetExecutingAssembly();// (spath);
            Reflector();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsmInfo"/> class.
        /// </summary>
        /// <param name="_asm">The asm.</param>
        public AsmInfo(Assembly _asm)
        {
            asm = _asm;// (spath);
            Reflector();
        }

        void Reflector()
        {
            var title = (Attribute.GetCustomAttribute(
            asm,
            typeof(AssemblyTitleAttribute),
            false) as AssemblyTitleAttribute);
            if (title is null)
            {
                _sTitleAttribute = "";
            }
            else
            {
                _sTitleAttribute = title.Title;

            }
        }
        static Assembly asm { get; set; }


        #region ВЕРСИЯ ПРОГРАММЫ
        /// <summary>Версия программы</summary>
        public System.Version sysVersion => asm.GetName().Version;

        /// <summary>Версия мажор</summary>
        public int iMajor => sysVersion.Major;

        /// <summary>Версия минор</summary>
        internal int iMinor => sysVersion.Minor;

        /// <summary>Версия билд</summary>
        public int iBuild => sysVersion.Build;

        /// <summary>Версия ревизия</summary>
        internal int iRevision => sysVersion.Revision;

        /// <summary>Бета или нет, чет нечет</summary>
        //static string _sBeta => iMinor == 0 || iMinor >3 ? "" : iMinor == 1 ? "<alfa>" : "<beta>";
        private static string _sBeta = "";//=> iMinor == 0 || iMinor == 1 ? "<alfa>" : iMinor > 1 && iMinor < 5 ? "<beta>" : "";
        #endregion

        #region Assembly


        string _sTitleAttribute;
        /// <summary> Титул программы</summary>
        public string sTitleAttribute => _sTitleAttribute;

        /// <summary>Описание программы </summary>
        public string sDescription => (Attribute.GetCustomAttribute(
            asm,
            typeof(AssemblyDescriptionAttribute),
            false) as AssemblyDescriptionAttribute).Description;//!описание

        /// <summary>Конфигурация программы </summary>
        public string sConfiguration => (Attribute.GetCustomAttribute(
            asm,
            typeof(AssemblyConfigurationAttribute),
            false) as AssemblyConfigurationAttribute).Configuration;

        /// <summary>Компания </summary>
        public string sCompany => (Attribute.GetCustomAttribute(
            asm,
            typeof(AssemblyCompanyAttribute),
            false) as AssemblyCompanyAttribute).Company;

        /// <summary>Продукт </summary>
        public string sProduct => (Attribute.GetCustomAttribute(
            asm,
            typeof(AssemblyProductAttribute),
            false) as AssemblyProductAttribute).Product;

        /// <summary>копирайт</summary>
        public string sCopyright => (Attribute.GetCustomAttribute(
            asm,
            typeof(AssemblyCopyrightAttribute),
            false) as AssemblyCopyrightAttribute).Copyright;

        /// <summary>торговая марка</summary>
        public string sTrademark => (Attribute.GetCustomAttribute(
            asm,
            typeof(AssemblyTrademarkAttribute),
            false) as AssemblyTrademarkAttribute).Trademark;

        /// <summary>ProductVersion - Версия программы
        /// <br>для идентификации в лицензии пограмма для нк или АК</br>
        /// </summary>
        public string sInformationalVersionAttribut => (Attribute.GetCustomAttribute(
                   asm,
                   typeof(AssemblyInformationalVersionAttribute),
                   false) as AssemblyInformationalVersionAttribute).InformationalVersion;

        /// <summary>Полный путь к сборке </summary>
        public string sAsmFulPath => asm.Location;

        /// <summary>Имя сборки без расширения</summary>
        public string sAsmFileNameWithoutExtension => Path.GetFileNameWithoutExtension(sAsmFulPath);

        /// <summary>Имя сборки с расширением</summary>
        public string sAsmFileName => Path.GetFileName(sAsmFulPath);

        /// <summary>версия программы </summary>
        public string sVersionBeta => iMajor.ToString()
                                         + "."
                                         + iMinor.ToString()
                                         + "."
                                         + iBuild.ToString()
                                         + _sBeta//_sysVersion.ToString() + _sBeta;
                                         ;
        /// <summary>Полная версия с окончанием </summary>
        public string sVersionFullBeta => iMajor.ToString()
                                             + "."
                                             + iMinor.ToString()
                                             + "."
                                             + iBuild.ToString()
                                             + "."
                                             + iRevision.ToString()
                                             + _sBeta;//_sysVersion.ToString() + _sBeta;

        /// <summary>
        /// Gets the s version full.
        /// </summary>
        /// <value>
        /// The s version full.
        /// </value>
        public string sVersionFull => sysVersion.ToString();

        /// <summary>Полная версия без окончания </summary>
        public string sVersionShort => iMajor.ToString()
                                             + "."
                                             + iMinor.ToString()
                                             + "."
                                             + iBuild.ToString()
                                             + "."
                                             + iRevision.ToString();

        /// <summary>Дата сборки</summary>
        public string sDateRelis()
        {
            DateTime result = new DateTime(2000, 1, 1);
            result = result.AddDays(iBuild);
            result = result.AddSeconds(iRevision * 2);

#if DEBUG
            return result.ToString();
#else
            return result.ToLongDateString();
#endif



        }


        #endregion




        #region  ПУТИ

        /// <summary>Путь к Program DATA</summary>
        private string _sProgramData => Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

        /// <summary>Общий путь к пользовательским настройкам 
        /// <br>c:\Users\User\AppData\Roaming\ </br></summary>
        private string _sUserAppData => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        /// <summary>Путь к рабочему столу</summary>
        public string sUserDesktop => Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        /// <summary>Имя машины</summary>
        public string sMachineName => Environment.MachineName;

               

        #endregion


    }
}
