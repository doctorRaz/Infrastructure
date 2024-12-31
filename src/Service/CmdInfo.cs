//https://autolisp.ru/2024/10/29/nanocad-vyvod-komand-s-ix-opisaniem-cherez-net/ 

using System.ComponentModel;
using System.Reflection;
using System;





#if NC

using Teigha.Runtime;

#elif AC
using Autodesk.AutoCAD.Runtime;

#endif  


namespace drz.Infrastructure.CAD.Service
{
    /// <summary>
    /// читает из сборки имена и описания команд
    /// </summary>
    public   class CmdInfo
    {
        /// <summary>
        /// Gets or sets the method attribute.
        /// </summary>
        /// <value>
        /// The method attribute.
        /// </value>
        CommandMethodAttribute MethodAttr { get; set; }

        /// <summary>
        /// Gets or sets the Name attribute.
        /// </summary>
        /// <value>
        /// The Name attribute.
        /// </value>
        string NameAttr { get; set; }

        /// <summary>
        /// Gets or sets the description attribute.
        /// </summary>
        /// <value>
        /// The description attribute.
        /// </value>
        /*public*/
        DescriptionAttribute descriptionAttr { get; set; }

        public  string smes { get; set; }  = "";

        /// <summary>
        /// Reflections this instance.
        /// <br>если подключаем как модуль к приложению</br>
        /// </summary>
        public void Reflection()
        {
            ReflectionEngine(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Reflections the specified asm.
        /// <br>если подключаем как библиотеку</br>
        /// </summary>
        /// <param name="_asm">Assembly сборки из которой надо вытащить команды</param>
        public void Reflection(Assembly asm)
        {
            ReflectionEngine(asm);
        }

        /// <summary>
        /// Reflections the engine.
        /// </summary>
        void ReflectionEngine(Assembly asm)
        {


            Type[] expTyped = asm.GetTypes();

           
            string smetod = "";
            foreach (Type t in expTyped)
            {
                MethodInfo[] methods = t.GetMethods();
                foreach (MethodInfo method in methods)
                {
                    CmdInfo temp = GetCmdInfo(method);
                    if (temp != null)
                    {
#if DEBUG
                        smetod = " [" + temp.NameAttr + "]";
#endif

                        if (temp.descriptionAttr != null)
                        {
                            //msgService.ConsoleMessage(temp.MethodAttr.GlobalName + "\t" + sDeb +
                            //temp.descriptionAttr.Description ?? "");
                            smes = smes + temp.MethodAttr.GlobalName +
                                smetod +
                                "\t" +
                            temp.descriptionAttr.Description + "\n";
                        }
                        else
                        {
                            smes = smes + temp.MethodAttr.GlobalName +
                                smetod +
                                "\n";
                            //msgService.ConsoleMessage(temp.MethodAttr.GlobalName);
                        }
                    }
                }
            }
            smes = smes + "___________________________";


        }

        /// <summary>
        /// Gets the command information.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        /*private*/
        CmdInfo GetCmdInfo(MethodInfo method)
        {
            object[] attrs = method.GetCustomAttributes(true);

            CmdInfo res = new CmdInfo();

            foreach (object attr in attrs)
            {
                res.NameAttr = method.Name;

                if (attr is CommandMethodAttribute cmdAttr)
                {
                    res.MethodAttr = cmdAttr;
                }
                else if (attr is DescriptionAttribute descrAttr)
                {
                    res.descriptionAttr = descrAttr;
                }
            }

            return res.MethodAttr == null ? null : res;
        }
    }

}

