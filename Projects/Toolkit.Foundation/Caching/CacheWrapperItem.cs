using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOGI.Framework.Toolkit.Foundation.Caching
{
    [Serializable]
    public class CacheWrapperItem
    {
        public string Key { get; set; }

        public object Content { get; set; }

        public List<string> Tags { get; set; }

        public string Region { get; set; }

        public CacheWrapperItem(string key, object conent)
        {

            Key = key;

            Content = conent;

            Tags = new List<string>();

        }

        public CacheWrapperItem(string key, object conent, string[] tags)
        {

            Key = key;

            Content = conent;

            Tags = new List<string>();

            foreach (string tag in tags)
            {

                Tags.Add(tag);

            }

        }

        public string[] GetTags()
        {

            string[] tags = null;

            if (Tags != null && Tags.Count > 0)
            {

                tags = new string[Tags.Count];

                int i = 0;

                foreach (string tag in Tags)
                {

                    tags[i++] = tag;

                }

            }

            return tags;

        }
    }
}
