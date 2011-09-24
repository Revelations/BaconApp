using System.Text;

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
			StringBuilder builder = new StringBuilder();
			if (Name.ToString() == "text")
			{
				if (Value != null)
				{
					builder.Append(Value).AppendLine();
				}
				foreach (HtmlNode<T> node in Children)
				{
					builder.Append(node.ToString());
				}
			}
			else
			{
				builder.Append("<").Append(Name);
				//Attributes here.

				if (IsEmpty)
				{
					builder.Append(" />").AppendLine();
				}
				else
				{
					builder.Append(">").AppendLine();
					if (Value != null)
					{
						builder.Append(Value).AppendLine();
					}
					foreach (HtmlNode<T> node in Children)
					{
						builder.Append(node.ToString());
					}

					builder.Append("</").Append(Name).Append(">").AppendLine();
				}
			}

			return builder.ToString();
		}
	}
}