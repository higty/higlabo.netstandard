using System.Collections.Generic;
using System.Xml.Linq;
using HigLabo.Core;

namespace HigLabo.Rss
{
	/// <summary>
	/// 
	/// </summary>
	public class RssItem_0_92 : RssItem_0_91
	{
		/// <summary>
		/// 
		/// </summary>
		public RssEnclosure Enclosure { get; set; }
        /// <summary>
        /// 
        /// </summary>
		public List<RssCategory> CategoryList { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public RssSource Source { get; set; }
	
        /// <summary>
		/// 
		/// </summary>
		public RssItem_0_92()
		{
			
		}
	    /// <summary>
		/// 
		/// </summary>
		/// <param name="element"></param>
		public RssItem_0_92(XElement element)
			: base(element)
		{
			if (element != null)
			{
				Parse(element);
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="element"></param>
		protected new void Parse(XElement element)
		{
			var enclosure = element.ElementByNamespace("enclosure");
			if (enclosure != null)
			{
				Enclosure = new RssEnclosure(enclosure);
			}

			var source = element.ElementByNamespace("source");
			if (source != null)
			{
				Source = new RssSource(source);
			}

			foreach (var item in element.ElementsByNamespace("category"))
			{
				var r = new RssCategory(item);
				this.CategoryList.Add(r);
			}
		}
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}, Enclosure: {1}, Source: {2}", base.ToString(), Enclosure, Source);
        }
	}
}