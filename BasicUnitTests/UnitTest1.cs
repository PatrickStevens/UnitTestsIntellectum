using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BasicUnitTests
{
  [TestClass]
  public class UnitTest1
  {
    [TestMethod]
    public void TestPrivateInvoke()
    {
      var a = new DemoLib.A();
      
      var methodParams = new object[] { "some stuff" };
      System.Reflection.MethodInfo reflectedMethod = 
        a.GetType().GetMethod("PrivateTestStuff", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

      object actualResultObject = reflectedMethod.Invoke(a, methodParams);
      string actualResultString = actualResultObject.ToString();
      string expectedResultString = "Hello, World! you can stop doing some stuff now!";
      Assert.AreEqual(actualResultString, expectedResultString);
    }

    private void TestGenericInvoke<T>()
    {
      var aofT = new DemoLib.A<T>();

      object[] methodParams = null;

      var genericType1 = typeof(DemoLib.A<>).MakeGenericType(typeof(T));
      var genericType2 = aofT.GetType();
      Assert.AreEqual(genericType1.ToString(), genericType2.ToString());

      var reflectedMethod = genericType1.GetMethod("GetSomeGenericStuff");

      object actualResultObject = reflectedMethod.Invoke(aofT, methodParams);
      string actualResultString = actualResultObject.ToString();
      string expectedResultString = String.Format("System.Collections.Generic.List`1[{0}]", typeof(T).ToString());
      Assert.AreEqual(actualResultString, expectedResultString);
    }

    [TestMethod]
    public void TestGenericIntInvoke()
    {
      TestGenericInvoke<int>();
    }

    [TestMethod]
    public void TestGenericDateTimeInvoke()
    {
      TestGenericInvoke<DateTime>();
    }

  }
}
