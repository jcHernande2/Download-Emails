using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CentroDeCorreos.Models;
using CentroDeCorreos.Clases;
using OpenPop.Mime;
using OpenPop.Pop3;
using System.Net.Mail;
using System.Data;
using System.Text;
using OpenPop.Mime.Header;

namespace CentroDeCorreos.Clases
{
    public class Emails
    {
        DB DataBase = new DB();
        Pop3Client client = new Pop3Client();
        public bool DescargaCorreos(long idUser)
        {
            tblUsuarioSynergia UsuarioSynergia= DataBase.GetUsuario(idUser);
            //usuario/mail de gmail
            // string username = "";
            string username = UsuarioSynergia.pop3user;//"";
            //password
            //string password = "";
            string password = UsuarioSynergia.pop3pwd;// "";
            //el puerto para pop de gmail es el 995
            int port = 995;
            //el host de pop de gmail es pop.gmail.com
            //string hostname = "";
            string hostname = UsuarioSynergia.pop3server;//"outlook.office365.com";
            //esta opción debe ir en true
            bool useSsl = true;

            // creamos instancia de mensajes
            Pop3Client client = new Pop3Client();
            
            // conectamos al servidor
            client.Connect(hostname, port, useSsl);
            if (!client.Connected)
            {
                return false;
            }
            // Autentificación
            client.Authenticate(username, password);
            // Obtenemos los Uids mensajes
            List<string> uids = client.GetMessageUids();

            int contador = 0;
            //uids.OrderByDescending()
            // Recorremos para comparar
            for (int i = 0; i < uids.Count&&contador<10; i++)
            {
                contador++;
                //obtenemos el uid actual, es él id del mensaje
                string currentUidOnServer = uids[i];
                //por medio del uid obtenemos el mensaje con el siguiente metodo
                Message oMessage = client.GetMessage(i + 1);
                MessageHeader oMessageHeader=client.GetMessageHeaders(i + 1);
                string cadhead= oMessage.Headers.ToString();

                //si no se ha realizado la descarga
                if (!DataBase.existeCorreo(oMessage.Headers.MessageId))
                {
                    //oMessage.RawMessage.
                    //agregamos el mensaje a la lista que regresa el metodo
                    //lstMessages.Add(Msg);
                    StringBuilder builder = new StringBuilder();
                    // Might include a part holding html instead
                    OpenPop.Mime.MessagePart html = oMessage.FindFirstHtmlVersion();
                    if (html != null)
                    {
                        // We found some html!
                        builder.Append(html.GetBodyAsText());
                    }
                    else
                    {
                        OpenPop.Mime.MessagePart plainText = oMessage.FindFirstPlainTextVersion();
                        if (plainText != null)
                        {
                            // We found some plaintext!
                            builder.Append(plainText.GetBodyAsText());
                        }
                    }
                    StringBuilder builderCc = new StringBuilder();
                    foreach (OpenPop.Mime.Header.RfcMailAddress cc in oMessage.Headers.Cc) // Loop through all strings
                    {
                        builderCc.Append(cc).Append(";"); // Append string to StringBuilder
                    }
                    StringBuilder builderBcc = new StringBuilder();
                    foreach (OpenPop.Mime.Header.RfcMailAddress bcc in oMessage.Headers.Bcc) // Loop through all strings
                    {
                        builderBcc.Append(bcc).Append(";"); // Append string to StringBuilder
                    }
                    StringBuilder builderTo = new StringBuilder();
                    foreach (OpenPop.Mime.Header.RfcMailAddress to in oMessage.Headers.To) // Loop through all strings
                    {
                        builderTo.Append(to).Append(";"); // Append string to StringBuilder
                    }
                    List<MessagePart> Attachments = oMessage.FindAllAttachments();
                    ///Guardar Correo
                    long id=DataBase.GuardarCorreo(oMessage.Headers.From.ToString(), oMessage.Headers.Subject.ToString()
                        , builderCc.ToString(), builderBcc.ToString()
                        , builder.ToString(), builderTo.ToString(), oMessage.Headers.ToString()
                        , oMessage.Headers.MessageId.ToString(), 78, oMessage.Headers.DateSent);
                    foreach (MessagePart Attach in Attachments)
                    {
                       // Attach.IsMultiPart
                        if (Attach.IsAttachment)
                        {
                            string path = "\\\\MDCWEBSERVER\\www.grupomdc.com\\sistema\\intranet\\mdcsistemas\\synergia\\filetemp\\"+Attach.FileName;
                          
                            System.IO.FileInfo File = new System.IO.FileInfo(path);
                            Attach.Save(File);
                            DataBase.GuardarAdjunto(id,Attach.FileName.ToString(), Attach.FileName.ToString(),false);
                        }
                        else
                        {
                            
                        }
                    }
                    
                    //string cc= string.Join(",",oMessage.Headers.Cc.ToArray());
                    //GuardarCorreo(string Remitente, string Asunto, string Cc, string Cco, string Cuerpo, string Destinatario, string EncabezadoMensaje, string idMensajeServer)
                   
                }
            }
            return true;
        }
    }
}