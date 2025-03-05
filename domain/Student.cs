using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBA.domain
{
    internal class Student: Entity<long>
    {
        private string? name;
        private string? school;

        public Student(){ }

        public Student(string name, string school)
        {
            this.name = name;
            this.school = school;
        }

        public string Name
        {
            get { return name!; } 
        }

        public string School
        {  get { return school!; } }

        public override string ToString()
        {
            return Name + " | " + School;
        }

    }
}
