using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services.Blogging
{
    /// <summary>
    /// Article blog.
    /// </summary>
    public class BlogArticle
    {
        /// <summary>
        /// Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Publication date.
        /// </summary>
        public DateTime PublicationDate { get; set; }

        /// <summary>
        /// Author id.
        /// </summary>
        public int AuthorId { get; set; }
    }
}
