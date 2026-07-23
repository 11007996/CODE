using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User();
            Department dept = new Department();
            IFactory factory = new AccessFactory();

            IUser iu = factory.CreateUser();

            iu.Insert(user);
            iu.GetUser(1);
            IDepartment id = factory.CreateDepartment();
            id.Insert(dept);
            id.GetDepartment(1);

            Console.Read();
        }
    }
}
