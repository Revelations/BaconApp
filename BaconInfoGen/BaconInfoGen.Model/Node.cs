using System;
using System.Collections.Generic;
using System.Text;

namespace BaconApp
{
	public class Node
	{
		public Node Parent { get; set; }
		public string Name { get; private set; }
		public List<object> Children { get; private set; }

		public Node(string name)
		{
			Name = name ?? "";
			Children = new List<object>();
		}

		public Node(string name, object value) : this(name)
		{
			if (value != null)
			{
				if (value is Node)
				{
					((Node) value).Parent = this;
				}
				Children.Add(value);
			}

		}

		public Node(string name, IEnumerable<object> values) : this(name)
		{
			foreach (var child in values)
			{
				if (child is Node)
				{
					((Node) child).Parent = this;
				}
				Children.Add(child);
			}

			//Children = new List<object>(values);
		}

		public void AddValue(object value)
		{
			if (value is Node)
				((Node)value).Parent = this;
			Children.Add(value);
		}

		public void AddValues(IEnumerable<object> values)
		{
			foreach (var value in values)
				AddValue(value);
		}

		public override string ToString()
		{
			var builder = new StringBuilder();

			if (Name != null)
				builder.Append("<").Append(Name).Append(">");
			else
				builder.Append("null");

			foreach (var value in Children)
				builder.Append(value);

			if (Name != null)
				builder.Append("</").Append(Name).Append(">");
			else
				builder.Append("null");
			
			return builder.ToString();
		}

		public void Display()
		{
			Console.WriteLine(@"<" + Name + @"> parent: " + ( (Parent == null) ? "null" : Parent.Name));
			foreach (var child in Children)
			{
				if (child is Node)
				{
					var n = (Node) child;
					n.Display();
				} else
				{
					Console.WriteLine(child);
				}
			}

		}
	}
}