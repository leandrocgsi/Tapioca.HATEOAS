using System.Runtime.Serialization;

namespace RESTFulSampleServer.Model.Base
{
    // Contrato entre atributos
    // e a estrutura da tabela
    // [DataContract]
    public class BaseEntity
    {

        public long? Id { get; set; }
    }
}
