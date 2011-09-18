using System;
using System.Collections.Generic;
using System.Text;
using BaconBuilder.Model;

namespace BaconBuilder
{
	public class HtmlNode<T>
	{
		public bool IsEmpty { get; set; }
		public HtmlNode<T> Parent { get; set; }
		public T Name { get; private set; }
		public T Value { get; set; }
		public HtmlTree<T> Children { get; private set; }

		public HtmlNode(T name)
		{
			Name = name;
			Children = new HtmlTree<T>(this);
		}

		public override string ToString()
		{
			//return string.Format("Name:{0} Value:{1} Children:{2}",
			//	Name, Value, Children.ToString());
			string openTag = string.Format("<{0}>", Name);
			return openTag;
		}
	}
}