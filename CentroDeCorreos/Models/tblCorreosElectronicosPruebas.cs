//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CentroDeCorreos.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblCorreosElectronicosPruebas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblCorreosElectronicosPruebas()
        {
            this.tblAdjuntosCorreoPruebas = new HashSet<tblAdjuntosCorreoPruebas>();
        }
    
        public long id { get; set; }
        public Nullable<long> idContacto { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Asunto { get; set; }
        public string Remitente { get; set; }
        public long idUsuario { get; set; }
        public bool Tipo { get; set; }
        public System.DateTime FechaEnvio { get; set; }
        public bool Privado { get; set; }
        public bool Borrado { get; set; }
        public string FechaDescarga { get; set; }
        public bool Nuevo { get; set; }
        public string Cuerpo { get; set; }
        public string EncabezadoMensaje { get; set; }
        public Nullable<long> idTemporal { get; set; }
        public string idMensajeServer { get; set; }
        public string Cc { get; set; }
        public string Cco { get; set; }
        public string FechaTemporal { get; set; }
        public string Destinatario { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblAdjuntosCorreoPruebas> tblAdjuntosCorreoPruebas { get; set; }
        public virtual tblUsuarioSynergia tblUsuarioSynergia { get; set; }
    }
}
