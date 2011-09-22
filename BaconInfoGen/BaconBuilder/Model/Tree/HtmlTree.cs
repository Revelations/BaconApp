using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace BaconBuilder
{
	public class HtmlTree<T> : IEnumerable
	{
		public HtmlNode<T> Owner { get; private set;  }
		private readonly List<HtmlNode<T>> nodes;

		public HtmlTree(HtmlNode<T> node)
		{
			Owner = node;
			nodes = new List<HtmlNode<T>>();
		}

		public HtmlNode<T> Add(T node)
		{
			var n = new HtmlNode<T>(node);
			n.Parent = Owner;
			nodes.Add(n);
			return n;
		}

		public void AddRange(T[] p)
		{
			foreach (T item in p)
			{
				nodes.Add(new HtmlNode<T>(item));
			}
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			foreach (HtmlNode<T> item in nodes)
			{
				builder.Append(item.ToString());
				builder.AppendLine();
			}
			return builder.ToString();
		}

		public IEnumerator GetEnumerator()
		{
			return nodes.GetEnumerator();
		}
	}
}
