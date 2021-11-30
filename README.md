# Proyecto programación 2 2021
## Equipo 4 - Tercera entrega

Esta vez si utilizamos el Readme! :grin:

**1. ¿Qué desafíos de la entrega fueron los más difíciles?**
- Coordinar los horarios entre todos para poder trabajar todos juntos fue difícil, pero gracias a GitHub, esto no fue un problema. :smile:

- Un desafío muy grande para el grupo fue la implementación de la serialización de las clases porque UserInfo, clase que contiene la información del usuario
y que interactúa con muchos objetos diferentes relacionados al usuario, contenía un atributo que era una interfaz de diferentes roles que el usuario podía tener.
Aunque el planteo estaba bien y cumplía con el patrón OCP, se tuvo que rediseñar ese apartado para que el sistema de serialización pueda ser implementado.
- Aprender a formar equipo entre desconocidos, como pasa en el ámbito laboral de la vida.

**2. ¿Qué cosas aprendieron enfrentándose al proyecto que no aprendieron en clase como parte de la currícula?**
- Mas que aprender, fue una buena oportunidad de poner en práctica todo lo que veniamos aprendiendo en la clase pero en un proyecto bastante serio. :books:
- La idea del bot nos encantó, esperamos con ganas la próxima entrega para poder adaptarlo a una plataforma de mensajería como Telegram, para que la experiencia de uso sea mas atractiva. :sunglasses:

- Pudimos aprender a manejar los comandos y opciones que ofrece Git y GitHub de forma más practica y constante, ya que casi todos los días teniamos que manejar.
conflictos en ramas, subir reformas y aprender a convivir en un repositorio con otras personas que codean diferente.
- Aprendimos la implementación real (en un programa más grande) de los patrones de diseño y el por qué de enfatizar en la refactorización de código.

**3. ¿Qué recursos (páginas web, libros, foros, etc) encontraron que les fueron valiosos para sortear los desafíos que encontraron?**
- Refactoring.Guru
- Youtube
- GitHub
- Stackoverflow
- GeeksforGeeks

**4. Comentarios sobre el trabajo en el proyecto:**
- Como ya mencionamos, el enfoque que tiene el bot nos gustó, incluso tanto, que tratamos de hacer la adaptación con Telegram antes de tiempo y tal vez nos generó una complicación mas que una ayuda, pero pudimos superarlo sin problemas. :muscle:

- Como en en un proyecto real la estructura y los objetivos del grupo cambian al igual que la forma de trabajo, la primera entrega del diagrama UML no tiene casi
ningún parecido con la entrega final, con tal fuimos aprendiendo y capacitando temas diferentes y herramientas fuimos encontrando mejores formas de diseñar y de cumplir patrones de diseño, los cuales en un proceso de creación, fuimos aplicando sobre la marcha.


**4. Notas generales:**

Desde el planteo de la primera entrega nos pusimos de acuerdo con desarrollar a profundidad el concepto de simplicidad, cumpliendo con todos los userstories pero mostrando opciones simples al usuario. Esto porque pensamos que en un proyecto que se va a ver reflejado en 
un chat, se tiene que ofrecer opciones simples y concretas para no sofocar al usuario con textos y opciones complejas.

Las colecciones se declaran como abstracciones para que justamente no dependa de un tipo especifico (Ejemplo: List o Collections). Con esto se cumple
con el patrón de diseño DIP (Dependency Inversion Principle): "Las clases de alto nivel no deben depender de clases de bajo nivel, ambas deben depender de abstracciones". Adicionalmente también se cumple con el patrón de diseño ISP (Interface Segregation Principle) porque por ejemplo al definir al objeto con una interfaz IList reduce las opciones que tiene el cliente de utilizar métodos que no son necesarios. Hay casos en los que no se puede declarar la colección de
tipo abstracto porque interviene la etiqueta de serialización JsonInclude, que incluye la colección como item a serializar, el problema de esto es que no acepta
abstracciones a serializar por esto optamos por usar tipos específicos en casos de serialización.

No se pudieron realizar Unit Tests del apartado de serialización porque la carpeta Test utiliza un path diferente al que usa la consola para lanzar el proyecto, por
esta razón era inviable diseñar una forma de ingresar un path personalizado ya que no es útil para los demás objetos que interactuan con SerializeManager.

Las clases PublicationSet, SerializeManager, SessionRelated, ConsoleBot, TelegramBot, TokenGenerator, SearchByLocation, SearchByMaterial como se puede ver implementan un constructor privado para que no sea posible crear más de una instancia de la clase, la única opción de utilizar las clases es accediendo a su propiedad estática Instance en donde su get público calcula si ya se ha creado una instancia de la clase, en este caso devuelve la misma y en caso de que no se haya creado todavía, la crea y la devuelve. Protegiendo
así al atributo instance y cumpliendo con el patrón de creación Singleton.

La implementación de Chain of Responsability, las clases Set (Ejemplo: PublicationSet), los searchs y los reportes cumplen con OCP porque en caso de querer añadir un miembro más a las implementaciones anteriormente mencionadas, solo se debería crear una nueva clase dependiendo de las abstracciones correspondientes, no se debería modificar ninguna implementación ya hecha.