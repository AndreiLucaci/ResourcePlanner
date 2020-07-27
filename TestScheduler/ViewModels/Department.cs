using System;
using System.Linq;

namespace TestScheduler.ViewModels
{
    public enum Department
    {
        Root = 0,
        IT = 1,
        TEMP = 2,
        USR = 3,
        ASDF = 4,
        HelloWorld = 5,
        SoManyDepartments = 6
    }

    public class DepartmentChooser
    {
        private static Random random = new Random();
        public static Department Choose()
        {
            return (Department)random.Next(1, Enum.GetValues(typeof(Department)).Length);
        }
    }
}
