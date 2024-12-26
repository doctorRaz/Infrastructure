

using Multicad.AplicationServices;

using System;
using System.Diagnostics;
using System.Windows;


namespace drz.Infrastructure.MCD
{
    /// <summary>
    /// нотифай мультикад
    /// </summary>
    class NotifaiMessag
    {
        private string sURL { get; set; }

        /// <summary>
        /// Нотифай с гиперссылкой
        /// </summary>
        /// <param name="sTextMsg">Текст сообщения</param>
        /// <param name="sTextURL">Текст для URL</param>
        /// <param name="nfe">Иконка нотифая</param>
        /// <param name="_sURL">адрес страницы</param>
        public NotifaiMessag(
            string sTextMsg,
            string sTextURL,
            NotificationEnumMgd nfe,
            string _sURL)
        {
            sURL = _sURL;
            string sURLcomplit = sTextURL
                                 + " "
                                 + "<A>"
                                 + _sURL
                                 + "</A>";

            IMcNotificatorSysLinkWindow link = McNotificator.CreateSysLink(sURLcomplit,
                                                                           OnLink);

            McNotificator.CreateMessage(sTextMsg,
                                        nfe,
                                        UIntPtr.Zero,
                                        link,
                                        link);

        }
        
        /// <summary> Открывает страницу по гиперссылке </summary>
        public void OnLink()
        {
            try
            {
                using (Process Proc = new Process())// razygraevaa on 07.01.2023 at 12:33
                {
                    Proc.StartInfo.FileName = sURL;// sOutPDF;
                    Proc.StartInfo.UseShellExecute = true;
                    Proc.Start();
                }
            }
            catch (Exception ex)
            {
                string serr = ex.Message;
                serr += "\n";
                serr = ex.StackTrace;
                serr += "\n";

                _ = System.Windows.Forms.MessageBox.Show(serr + "Неправильный URL");
            }
        }
    }

    /// <summary>
    /// вызов наносообщений
    /// </summary>
    class notyCMD
    {
        /// <summary>
        /// Пример вызова нотифай со ссылкой
        /// </summary>
        public static void RunNoti()
        {
            //Action aCom;

            string sTextMsg = "Text";

            string sTextURL = "URL";

            NotificationEnumMgd nfe = NotificationEnumMgd.neHint;

            string sURL = @"https://forum.nanocad.ru/index.php?/discover/unread/&stream_date_type=relative&before";


            NotifaiMessag noti = new NotifaiMessag(sTextMsg, sTextURL, nfe, sURL);

        }
    }
}