using NUnit.Framework;
using Server;
using System;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Linq;
using System.Text;

namespace ServerTests
{
    public class UserRequestTests
    {
        Type userRequestType = Assembly.Load("Server").GetTypes().First(ty => ty.FullName == "Server.UserRequest");

        /// <summary>
        ///  Writes Key + String value to stream 
        /// </summary>
        /// <param name="st">Stream to write in</param>
        /// <param name="key">4 bytes connection key</param>
        /// <param name="text">text from "user"</param>
        private void WriteConnKeyAndValueToStream(Stream st, int key, string text)
        {
            st.Write(BitConverter.GetBytes(key));
            var sw = new StreamWriter(st, Encoding.Unicode);
            sw.Write(text);           
            sw.Flush();
            st.Position = 0;
        }

        [SetUp]
        public void Setup()
        {
        }
        [Test]
        [TestCase(525435, "Oh, hi")]
        public void ConnectionKeyOnCorrectBodyShouldReturnIntValue(int key, string text)
        {
            // Arrange
            // Create instance of UserRequest with private constructor
            var userRequest = Activator.CreateInstance(userRequestType, BindingFlags.Instance | BindingFlags.NonPublic, null, null, null);
            // Get tested property
            PropertyInfo sendedText = userRequestType.GetProperty("ConnectionKey")!;
            // Get body stream field
            FieldInfo strBody = userRequestType.GetField("_reqBody", BindingFlags.Instance | BindingFlags.NonPublic)!;
            // Get body stream length field
            FieldInfo strBodyLen = userRequestType.GetField("_userBodyLength", BindingFlags.Instance | BindingFlags.NonPublic)!;
            // Fill Request body with data in format key[4 byte] + string data
            var stream = new MemoryStream();
            WriteConnKeyAndValueToStream(stream, key, text);

            strBody.SetValue(userRequest, stream);

            // Act
            var res = (int)sendedText.GetValue(userRequest)!;

            // Assert
            Assert.AreEqual(key, res);
        }

        [Test]
        public void SendedTextStrOnEmptyRequestBodyShouldReturnBlankString()
        {
            // Arrange
            // Create instance of UserRequest with private constructor
            var userRequest = Activator.CreateInstance(userRequestType, BindingFlags.Instance | BindingFlags.NonPublic, null, null, null);
            // Get tested property
            PropertyInfo sendedText = userRequestType.GetProperty("SendedTextString")!;
            
            // Act
            var res = (string)sendedText.GetValue(userRequest)!;

            // Assert
            Assert.AreEqual("",res);
        }
        [Test]
        [TestCase(525435, "Oh, hi")]
        public void SendedTextStrOnCorrectBodyShouldReturnStringBody(int key, string text)
        {
            // Arrange
            // Create instance of UserRequest with private constructor
            var userRequest = Activator.CreateInstance(userRequestType, BindingFlags.Instance | BindingFlags.NonPublic, null, null, null);
            // Get tested property
            PropertyInfo sendedText = userRequestType.GetProperty("SendedTextString")!;
            // Get body stream field
            FieldInfo strBody = userRequestType.GetField("_reqBody", BindingFlags.Instance | BindingFlags.NonPublic)!;
            // Get body stream length field
            FieldInfo strBodyLen = userRequestType.GetField("_userBodyLength", BindingFlags.Instance | BindingFlags.NonPublic)!;
            // Fill Request body with data in format key[4 byte] + string data
            var stream = new MemoryStream();
            WriteConnKeyAndValueToStream(stream, key, text);

            strBody.SetValue(userRequest, stream);

            // Act
            var res = (string)sendedText.GetValue(userRequest)!;

            // Assert
            Assert.AreEqual(text, res);
        }

        [Test]
        [TestCase(525435, "Oh, hi")]
        public void SendedTextBytOnCorrectBodyShouldReturnStringBody(int key, string text)
        {
            // Arrange
            // Create instance of UserRequest with private constructor
            var userRequest = Activator.CreateInstance(userRequestType, BindingFlags.Instance | BindingFlags.NonPublic, null, null, null);
            // Get tested property
            PropertyInfo sendedText = userRequestType.GetProperty("SendedTextBytes")!;
            // Get body stream field
            FieldInfo strBody = userRequestType.GetField("_reqBody", BindingFlags.Instance | BindingFlags.NonPublic)!;
            // Get body stream length field
            FieldInfo strBodyLen = userRequestType.GetField("_userBodyLength", BindingFlags.Instance | BindingFlags.NonPublic)!;
            // Fill Request body with data in format key[4 byte] + string data
            var stream = new MemoryStream();
            WriteConnKeyAndValueToStream(stream, key, text);

            strBody.SetValue(userRequest, stream);

            // Act
            var res = (byte[])sendedText.GetValue(userRequest)!;

            // Assert
            Assert.AreEqual(text, Encoding.Unicode.GetString(res, 0, res.Length));
        }
    }
}
