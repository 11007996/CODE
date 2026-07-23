using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component
{
    class Composite:Component
    {
        private List<Component> children = new List<Component>();
        public Composite(string name) : base(name) { }
        public override void Add(Component c)
        {
            children.Add(c);
        }
        public override void Remove(Component c)
        {
            children.Remove(c);
        }
        public override void Display(int depth)
        {
            Console.WriteLine(new String('_', depth) + name);
            foreach(Component component in children)
            {
                component.Display(depth + 2);
            }
        }
    }
}
