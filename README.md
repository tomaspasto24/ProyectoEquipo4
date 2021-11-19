# Proyecto programación 2 2021
## Equipo 4 - Segunda entrega

Esta vez si utilizamos el Readme! :grin:

**1. ¿Qué desafíos de la entrega fueron los más difíciles?**
- Coordinar los horarios entre todos para poder trabajar todos juntos fue dificil, pero gracias a GitHub, esto no fue un problema. :smile:

**2. ¿Qué cosas aprendieron enfrentándose al proyecto que no aprendieron en clase como parte de la currícula?**
- Mas que aprender, fue una buena oportunidad de poner en práctica todo lo que veniamos aprendiendo en la clase pero en un proyecto bastante serio. :books:
- La idea del bot nos encantó, esperamos con ganas la próxima entrega para poder adaptarlo a una plataforma de mensajería como Telegram, para que la experiencia de uso sea mas atractiva. :sunglasses:

**3. ¿Qué recursos (páginas web, libros, foros, etc) encontraron que les fueron valiosos para sortear los desafíos que encontraron?**
- Refactoring.Guru
- Youtube
- GitHub
- Stackoverflow
- GeeksforGeeks

**4. Comentarios sobre el trabajo en el proyecto:**
- Como ya mencionamos, el enfoque que tiene el bot nos gustó, incluso tanto, que tratamos de hacer la adaptación con Telegram antes de tiempo y tal vez nos generó una complicación mas que una ayuda, pero pudimos superarlo sin problemas. :muscle:


Patrones de Diseño: 

Las colecciones se declaran como abstracciones para que justamente no dependa de un tipo especifico (Ejemplo: List o Collections). Con esto se cumple
con el patrón de diseño DIP (Dependency Inversion Principle): "Las clases de alto nivel no deben depender de clases de bajo nivel, ambas deben depender de abstracciones". Adicionalmente también se cumple con el patrón de diseño ISP (Interface Segregation Principle) porque por ejemplo al definir al objeto con una interfaz IList reduce las opciones que tiene el cliente de utilizar métodos que no son necesarios. 

Se usa la interfaz IJsonConvertible para definir el tipo de las clases que son Serializables y Deserializables, dentro de la interfaz
se encuentra la operación para serializar el propio objeto ya que, por patrón Expert, la propia clase es la experta en serializarse a si misma porque
tiene la responsabilidad de conocer todos sus atributos. En cambio la operación para deserializar se encuentra fuera porque no necesariamente
las clases de tipo IJsonConvertible tienen que ser expertas en aplicarse una deserialización.

Las clases CompanySet, PublicationSet, SerializeManager, DeserializeManager como se puede ver implementan un constructor privado para que no sea posible
crear más de una instancia de la clase, la única opción de usar las clases es accediendo a su propiedad estática Instance en donde su get público calcula
si ya se ha creado una instancia de la clase, en este caso devuelve la misma y en caso de que no se haya creado todavía, la crea y la devuelve. Protegiendo
así al atributo instance y cumpliendo con el patrón de creación Singleton.