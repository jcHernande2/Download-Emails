using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CentroDeCorreos.Models;
using PagedList;
namespace CentroDeCorreos.Clases
{
    public class DB
    {
        MDCSynergiaEntities3 DbMDCSynergia = new MDCSynergiaEntities3();
        public long GuardarCorreo(string Remitente, string Asunto, string Cc, string Cco, string Cuerpo,string Destinatario,string EncabezadoMensaje
            , string idMensajeServer,long idUser,DateTime FechaEnvio)
        {
            tblCorreosElectronicosPruebas Correo = new tblCorreosElectronicosPruebas();
            Correo.Remitente = Remitente;
            Correo.Asunto = Asunto;
            Correo.Cc = Cc;
            Correo.Cco = Cco;
            Correo.Cuerpo = Cuerpo;
            Correo.Destinatario = Destinatario;
            Correo.EncabezadoMensaje = EncabezadoMensaje;
            Correo.idMensajeServer = idMensajeServer;
            Correo.Fecha = DateTime.Now;
            Correo.FechaEnvio = FechaEnvio;
            Correo.FechaTemporal = "";
            Correo.FechaDescarga = "";
            Correo.idContacto = Convert.ToInt64(11045);
            Correo.idUsuario = Convert.ToInt64(idUser);
            DbMDCSynergia.tblCorreosElectronicosPruebas.Add(Correo);
            DbMDCSynergia.SaveChanges();
            return Correo.id;
        }
        public long GuardarAdjunto(long idCorreo, string NombreArchivo,string RutaArchivo,bool TipoMine)
        {
            tblAdjuntosCorreoPruebas Adjuntos = new tblAdjuntosCorreoPruebas();
            Adjuntos.idCorreoElectronico = Convert.ToInt64(idCorreo);
            Adjuntos.NombreArchivo = NombreArchivo;
            Adjuntos.RutaArchivo = RutaArchivo;
            Adjuntos.idTemporal="";
            Adjuntos.TipoMime = TipoMine;
            DbMDCSynergia.tblAdjuntosCorreoPruebas.Add(Adjuntos);
            DbMDCSynergia.SaveChanges();
            return Adjuntos.id;
        }
        public IPagedList<tblCorreosElectronicosPruebas> ConsultarCorreos(int pageNumber,int pageSize)
        {
            //IPagedList<tblCorreosElectronicosPruebas> listaCorreos = new List();
            //List<DbMDCSynergia.tblCorreosElectronicosPruebas> lista=
            //.Take(pageSize)
            var listaCorreos =DbMDCSynergia.tblCorreosElectronicosPruebas.Where(x=>x.Borrado==false).OrderByDescending(x => x.FechaEnvio).ToPagedList(pageNumber,pageSize);
            return listaCorreos;
        }
        public tblUsuarioSynergia GetUsuario(long idUser)
        {
            return DbMDCSynergia.tblUsuarioSynergia.Where(u =>u.id==idUser).Single();
        }
        public bool existeCorreo(string UID)
        {
            if (DbMDCSynergia.tblCorreosElectronicosPruebas.Where(e => e.idMensajeServer == UID).Count() > 0)
                return true;
            return false;
        }
        public tblCorreosElectronicosPruebas GetEmail(string idMessage)
        {
            return DbMDCSynergia.tblCorreosElectronicosPruebas.Where(e => e.idMensajeServer == idMessage).Single();
        }

    }
}