using NUnit.Framework;

namespace Bot
{
    public class RegisterHandlerTest
    {
        RegisterHandler handler;
        Message message;

        [SetUp]
        public void Setup()
        {
            handler = new RegisterHandler(new RegisterCondition());
            message = new Message("Console", string.Empty);
        }

        [Test]
        public void TestHandle()
        {
            message.Text = handler.Keywords[0];
            string response;

            AbstractHandler result = handler.HandleRequest(message);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("¡Hola! ¿Cómo estás?"));
        }

        [Test]
        public void TestDoesNotHandle()
        {
            message.Text = "adios";
            string response;

            IHandler result = handler.Handle(message, out response);

            Assert.That(result, Is.Null);
            Assert.That(response, Is.Empty);
        }
    }
}