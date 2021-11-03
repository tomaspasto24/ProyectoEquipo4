using System;
using System.Linq;

namespace Bot
{
    /*
    Patrones y principios: 
    Debido a que se indentifica una sola razón de cambio, esta clase cumple con SRP, este motivo de cambio podría ser, cambiar el método handle.
    También cumple con Expert, ya que posee todo lo necesario para cumplir la responsabilidad otorgada a la clase.
    A su vez, cumple con el patrón Chain of Responsability.
    */
    /// <summary>
    /// Clase abstracta para los distintos bots.
    /// </summary>
    public abstract class AbstractHandler : IHandler
    {
        /// <summary>
        /// Constructor de la clase AbstractHandlers
        /// </summary>
        /// <param name="succesor">Condicion que se tiene que cumplir para que se ejecute el handler</param>
        public AbstractHandler(AbstractHandler succesor)
        {
            this.Succesor = succesor;
        }

        /// <summary>
        /// Handler sucesor
        /// </summary>
        /// <value></value>
        public IHandler Succesor { get; set; }

        /// <summary>
        /// Este método debe ser sobreescrito por las clases sucesores. La clase sucesora procesa el mensaje y retorna
        /// true o no lo procesa y retorna false.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario</returns>
        protected abstract bool InternalHandle(Message message, out string response);

        /// <summary>
        /// Este método puede ser sobreescrito en las clases sucesores que procesan varios mensajes cambiando de estado
        /// entre mensajes deben sobreescribir este método para volver al estado inicial. En la clase base no hace nada.
        /// </summary>
        protected virtual void InternalCancel()
        {
            // Intencionalmente en blanco.
        }

        /// <summary>
        /// Metodo para manejar las peticiones. Si se cumple la condicion, se ejecuta el handler asociado. Sino lo delega a su sucesor.
        /// </summary>
        /// <param name="request">Mensaje del usuario</param>
        /// <param name="response">Respuesta del bot</param>
        /// <returns></returns>
        public IHandler Handle(Message request, out string response)
        {
            if (this.InternalHandle(request, out response))
            {
                return this;
            }
            else if (this.Succesor != null)
            {
                return this.Succesor.Handle(request, out response);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Retorna este "handler" al estado inicial. En los "handler" sin estado no hace nada. Los "handlers" que
        /// procesan varios mensajes cambiando de estado entre mensajes deben sobreescribir este método para volver al
        /// estado inicial.
        /// </summary>
        public virtual void Cancel()
        {
            this.InternalCancel();
            if (this.Succesor != null)
            {
                this.Succesor.Cancel();
            }
        }
    }
}