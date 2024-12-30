//https://autolisp.ru/2024/10/29/nanocad-vyvod-komand-s-ix-opisaniem-cherez-net/ 

using System.Reflection;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#if NC

using Teigha.Runtime;

#elif AC
using Autodesk.AutoCAD.Runtime;

#endif  


namespace drz.Infrastructure.CAD.Service
{

    /// <summary>
    /// https://adn-cis.org/programmnoe-opredelenie-dublirovannyix-imen-.net-komand.html
    /// </summary>
    public class FindCmdInfo
    {
        /// <summary>
        /// Список зарегистрированных команд
        /// </summary>
        class CmdList
        {
            /// <summary>
            /// Имя метода
            /// </summary>
            internal string MethodAttr { get; set; }

            /// <summary>
            ///Описание метода
            /// </summary>
            internal string DescriptionAttr { get; set; }

            /// <summary>
            /// Имя класса
            /// </summary>
            internal string MethodInfo { get; set; }


        }

        /// <summary>
        /// Gets or sets the s command information.
        /// </summary>
        /// <value>
        /// The s command information.
        /// </value>
        public string sCmdInfo { get; set; } = "";

        /// <summary>
        /// Gets or sets the s duplicate information.
        /// </summary>
        /// <value>
        /// The s duplicate information.
        /// </value>
        public string sDuplInfo { get; set; } = "";

        /// <summary>
        /// Gets or sets the asm.
        /// </summary>
        /// <value>
        /// The asm.
        /// </value>
        Assembly asm { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FindCmdInfo"/> class.
        /// </summary>
        public FindCmdInfo()
        {
            asm = Assembly.GetExecutingAssembly();
            Reflection();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FindCmdInfo"/> class.
        /// </summary>
        /// <param name="_asm">The asm.</param>
        public FindCmdInfo(Assembly _asm)
        {
            asm = _asm;
            Reflection();
        }

        /// <summary>
        /// Reflections this instance.
        /// </summary>
        void Reflection()
        //public void FindCmdDuplicates(string asmPath)
        {
            //cmdInfo cmdinf = new cmdInfo();
            Dictionary<string, List<CmdList>> mapInfo =
                new Dictionary<string, List<CmdList>>();


            Dictionary<string, List<MethodInfo>> map =
                new Dictionary<string, List<MethodInfo>>();


            Type[] expTypes = asm.GetTypes();

            foreach (Type type in expTypes)
            {
                MethodInfo[] methods = type.GetMethods();

                //собираем методы
                foreach (MethodInfo method in methods)
                {
                    CmdList cinf = GetCmdInf(method);
                    if (cinf == null)
                        continue;

                    if (!mapInfo.ContainsKey(cinf.MethodAttr))
                    {
                        var lCinfo = new List<CmdList>();
                        mapInfo.Add(cinf.MethodAttr, lCinfo);
                    }
                    mapInfo[cinf.MethodAttr].Add(cinf);
                }
            }


            foreach (KeyValuePair<string, List<CmdList>> keyValuePair in mapInfo)
            {
                if (keyValuePair.Value.Count > 1)
                {
                    if (!string.IsNullOrEmpty(sDuplInfo)) sDuplInfo += "\n";//если дописываем, то перенос

                    sDuplInfo += "Дублированный атрибут: " + keyValuePair.Key;

                    foreach (CmdList itemList in keyValuePair.Value)
                    {
                        sDuplInfo += "\n\t[" + itemList.MethodInfo + "] " + itemList.DescriptionAttr;
                    }
                }
                else
                {
#if DEBUG
                    string sMethod = " [" + keyValuePair.Value[0].MethodInfo + "]";
#else
                    string sMethod = "";
#endif
                    if (!string.IsNullOrEmpty(sCmdInfo)) sCmdInfo += "\n";//если дописываем, то перенос

                    sCmdInfo += keyValuePair.Key + sMethod + "\t" + keyValuePair.Value[0].DescriptionAttr;
                }
            }
        }

        CmdList GetCmdInf(MethodInfo method)
        {
            object[] attributes = method.GetCustomAttributes(true);
            CmdList res = new CmdList();


            foreach (object attribute in attributes)
            {
                if (attribute is CommandMethodAttribute cmdAttr)
                {
                    res.MethodAttr = cmdAttr.GlobalName;

                    res.MethodInfo = method.Name;

                }
                else if (attribute is DescriptionAttribute descrAttr)
                {
                    if (descrAttr != null)
                    {

                        res.DescriptionAttr = descrAttr.Description;
                    }
                    else
                    {
                        res.DescriptionAttr = "";
                    }
                }
            }
            //return res;
            return res.MethodAttr == null ? null : res;
        }

    }
}

