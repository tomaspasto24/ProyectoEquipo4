using System;
using System.Linq;

namespace Bot
{
    /// <summary>
    /// Clase abstracta que debe implementar cualquiera de los handlers utilizados en la Chain of Responsability.
    /// Patrones y principios: 
    /// Debido a que se indentifica una sola razón de cambio, esta clase cumple con SRP, este motivo de cambio podría ser, cambiar el método handle.
    /// También cumple con Expert, ya que posee todo lo necesario para cumplir la responsabilidad otorgada a la clase.
    /// A su vez, cumple con el patrón Chain of Responsibility.
    /// </summary>
    public abstract class AbstractHandler : IHandler
    {
        /// <summary>
        /// Crea una nueva instancia de éste handler y define su sucesor.
        /// </summary>
        /// <param name="succesor">El siguiente handler a ser invocado en caso de que el actual no cumpla la condición.</param>
        public AbstractHandler(AbstractHandler succesor)
        {
            this.Succesor = succesor;
        }

        /// <summary>
        /// El siguiente handler en caso de que el actual no cumpla la condicion.
        /// </summary>
        /// <value>Siguiente handler.</value>
        public IHandler Succesor { get; set; }

        /// <summary>
        /// Procesa el mensaje o la pasa al siguiente handler si está definido o no es nulo.
        /// </summary>
        /// <param name="request">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje.</param>
        /// <returns>Este handler si fue capaz de procesar el mensaje, en caso contrario el Succesor.</returns>
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
        /// Retorna este handler al estado inicial y cancela el próximo handler si existe. 
        /// Utilizado para que los handlers que procesan varios mensajes cambiando de estado 
        /// entre mensajes puedan volver al estado inicial en caso de error por ejemplo.
        /// </summary>
        public virtual void Cancel()
        {
            this.InternalCancel();
            if (this.Succesor != null)
            {
                this.Succesor.Cancel();
            }
        }

        /// <summary>
        /// Intenta procesar el mensaje recibido y devuelve una respuesta.
        /// </summary>
        /// <param name="request">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected abstract bool InternalHandle(Message request, out string response);

        /// <summary>
        /// Este método puede ser sobreescrito en las clases sucesores que procesan varios mensajes cambiando de estado
        /// entre mensajes deben sobreescribir este método para volver al estado inicial. En la clase base no hace nada.
        /// </summary>
        protected virtual void InternalCancel()
        {
            // Intencionalmente en blanco.
        }
    }
}