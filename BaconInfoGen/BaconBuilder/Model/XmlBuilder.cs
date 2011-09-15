namespace BaconApp
{
	public class XmlBuilder
	{
		private readonly Node _node;

		public XmlBuilder(string rootTagName)
		{
			_node = new Node(rootTagName);
		}

		public string ToXml()
		{
			return _node.ToString();
		}

		public void AddValue(string value)
		{
			_node.AddValue(value);
			
		}
	}
}