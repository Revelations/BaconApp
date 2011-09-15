
using System.IO;
using System.Web.UI;

namespace BaconBuilder.Model 
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

        public void SomeMethod()
        {
            HtmlTextWriter writer = new HtmlTextWriter(new StringWriter());
        }
	}
}