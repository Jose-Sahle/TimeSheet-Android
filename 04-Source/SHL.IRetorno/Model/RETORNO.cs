using System;

namespace SHL.IRetorno.Model
{
    /// <summary>
    /// Objeto contendo informações respondidas pelo serivço de Meios de Pagamento
    /// </summary>
    public class RETORNO
    {
        /// <summary>
        /// Tipo: Alfanumérico
        /// Tamanho: 100
        /// Obrigatoriadade: Sim
        /// Descrição: Identificador único da transação
        /// Regra: Será retornado o mesmo valor enviado ao micro serviço de recebimento da transação
        /// </summary>
        public string transacao { get; set; }

        /// <summary>
        /// Tipo: Numérico
        /// Tamanho: 10
        /// Obrigatoriadade: Sim
        /// Descrição: Status da transação 
        /// Regra:  Valor numérico indicando a operação.
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// Tipo: Alfanumérico
        /// Tamanho: 8000
        /// Obrigatoriadade: Sim
        /// Descrição: Mensagem indicativa de como foi o processamento da transação
        /// Regra:  Mensagem indicando resultado
        /// </summary>
        public string mensagem { get; set; }

        /// <summary>
        /// Tipo: Data
        /// Tamanho: -
        /// Obrigatoriadade: Sim
        /// Descrição: Data da mensagem de retorno da transação
        /// Regra:  Formato: 2018-11-22 13:00:41.863
        /// </summary>
        public DateTime dt_retorno { get; set; }

        /// <summary>
        /// Tipo: Alfanumérico
        /// Tamanho: 8000
        /// Obrigatoriadade: Não
        /// Descrição: Mensagem com o trace técnico
        /// Regra:  N/A
        /// </summary>
        public string trace { get; set; }
    }
}
