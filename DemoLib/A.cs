using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLib
{
    public class A
    {
      private string PrivateTestStuff(string stuff = "PrivateTestStuff")
      {
        System.Diagnostics.Debug.WriteLine(String.Format("Called Private Test and did {0}", stuff));
        return String.Format("Hello, World! you can stop doing {0} now!", stuff);
      }
    }

    public class A<T>
    {
      public List<T> GetSomeGenericStuff()
      {
        var value = default(T);
        var listStuff = new List<T>();
        listStuff.Add(value);

        return listStuff;
      }
    }   
}
