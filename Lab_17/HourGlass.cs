using System.Xml.Serialization;

namespace Labs.Lab17
{
	[Serializable]
	public class HourGlass
	{
		public double Width { get; set; }
		public double Height { get; set; }

		public HourGlass() : this(0, 0) { }
		public HourGlass(double width, double height)
		{
			Width = width;
			Height = height;
		}

		public static HourGlass? XmlDeserialize(string xml)
		{
			XmlSerializer serializer = new(typeof(HourGlass));
			using StringReader reader = new(xml);
			return serializer.Deserialize(reader) as HourGlass;
		}

		public string XmlSerialize()
		{
			XmlSerializer serializer = new(typeof(HourGlass));
			using StringWriter writer = new();
			serializer.Serialize(writer, this);
			return writer.ToString();
		}
	}
}