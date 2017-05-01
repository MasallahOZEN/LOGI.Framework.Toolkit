using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using LOGI.Framework.Toolkit.Core.Extensions.ExtXml;
using Newtonsoft.Json;
using LOGI.Framework.Toolkit.Core.Extensions.ExtObject;
using LOGI.Framework.Toolkit.Core.Serialization;

namespace LOGI.Framework.Toolkit.Core.Common.Collections
{
    [Serializable]
    [DataContract]
    public class TreeContainer : IDisposable, IXmlSerializable
    {
        #region Const

        public TreeContainer()
        {
            this.ChildNodes = new TreeContainerCollection();
            this.Items=new TreeItemCollection();
        }

        public TreeContainer(string key)
        {
            this.ChildNodes = new TreeContainerCollection();
            this.Items = new TreeItemCollection();
            Key = key;
        }
        #endregion

        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Key { get; private set; }
        
        [DataMember]
        public TreeItemCollection Items { get; private set; }

        [DataMember]
        public TreeContainerCollection ChildNodes { get; private set; }

        public string ParentNodeKey { get; private set; }

        public TreeContainer this[string key]
        {
            get
            {
                TreeContainer findedContainer = null;

                foreach (var treeContainer in ChildNodes)
                {
                    findedContainer = FindInnerContainer(treeContainer, key);

                    if (findedContainer!=null)
                    {
                        break;
                    }
                }

                return findedContainer;
            }
        }

        public TreeContainer Add(string key)
        {
            var child = ChildNodes.Where(x => x.Key == key).FirstOrDefault();
            if (child!=null)
            {
                return child;
            }

            var newChild = new TreeContainer(key);

            ChildNodes.Add(newChild);

            return newChild;
        }

        #region Public Action
        public TreeContainer SetParentNodeKey(string key)
        {
            this.ParentNodeKey = key;
            return this;
        }
        public TreeContainer Insert(params TreeItemValue[] items)
        {
            if (items != null)
            {
                foreach (var item in items)
                {
                    var itemObj = Items.Where(x => x.Key == item.Key).FirstOrDefault();
                    var newItem = new TreeItem { Key = item.Key, Value = item.Value };

                    if (itemObj == null)
                    {
                        Items.Add(newItem);
                    }
                    else
                    {
                        if (item.Change && Items.Remove(itemObj))
                        {
                            Items.Add(newItem);
                        }
                    }

                }
            }

            return this;
        }

        public T GetItem<T>(string key)
        {
            var searchedItemList = GetItems(this);

            var itemObj = searchedItemList.Where(x => x.Key == key).FirstOrDefault();

            if (itemObj != null && itemObj.Value != null)
            {
                var item = itemObj.Value.To<T>(returnDefaultValue:true);

                var retValue = Mapping.EmitMapper.Map<T, T>(item);

                //item = retValue;

                return retValue;
            }

            return default(T);
        }

        public List<TreeItem> GetAllItems()
        {
            var retValue = GetItems(this);

            return retValue;
        }
        #endregion

        #region Private Action
        private TreeContainer FindInnerContainer(TreeContainer container, string key)
        {
            if (container != null)
            {
                if (container.Key == key)
                {
                    return container;
                }

                return container.ChildNodes.Select(treeContainer => FindInnerContainer(treeContainer, key)).FirstOrDefault();
            }

            return null;
        }

        private List<TreeItem> GetItems(TreeContainer container)
        {
            var retValue = new List<TreeItem>();

            retValue.AddRange(container.Items);

            foreach (var treeContainer in container.ChildNodes)
            {
                if (treeContainer != null)
                {
                    var findList = GetItems(treeContainer);

                    retValue.AddRange(findList);
                }
            }

            return retValue;
        }
        #endregion

        #region Disposing
        ~TreeContainer()
        {
            this.Dispose(false);
        }


        private bool isDisposed = false;

        //Dispose of the balance object
        public void Dispose()
        {
            if (!isDisposed)
                Dispose(true);
            GC.SuppressFinalize(this);
        }

        //Remove the event handlers
        private void Dispose(bool disposing)
        {
            isDisposed = true;
            if (disposing)
            {
                try
                {
                    for (int i = 0; i < this.Items.Count; i++)
                    {
                        try
                        {
                            this.Items[i].Dispose();
                        }
                        catch (Exception)
                        {
                        }
                    }

                    for (int i = 0; i < this.ChildNodes.Count; i++)
                    {
                        try
                        {
                            this.ChildNodes[i].Dispose();
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
        } 
        #endregion

        public override string ToString()
        {
            return string.Format("Key:{0},ParentNodeKey:{1},ItemCount:{2},ChildNodeCount:{3}", Key, ParentNodeKey, Items.Count, ChildNodes.Count);
        }


        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return (null);
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            //  Loop over the XML file
            while (reader.Read())
            {
                //  Here we check the type of the node, in this case we are looking for element
                if (reader.IsStartElement() && reader.NodeType == XmlNodeType.Element)
                {
                    //  If the element is "profile"
                    if (reader.Name == "Key")
                    {
                        Key = reader.ReadElementString("Key");
                        continue;
                    }
                    //  If the element is "profile"
                    if (reader.Name == "ParentNodeKey")
                    {
                        ParentNodeKey = reader.ReadElementString("ParentNodeKey");
                        continue;
                    }

                    ////  If the element is "profile"
                    //if (reader.Name == "TreeContainer")
                    //{
                    //    var otherSer = new XmlSerializer(typeof(TreeContainer));
                    //    var other = (TreeContainer)otherSer.Deserialize(reader);
                    //}
                    
                    //  If the element is "profile"
                    if (reader.Name == "ChildNodes")
                    {
                        var childNodeId = reader.GetAttribute("Id");
                        var childNodeCount = reader.GetAttribute("count").To<int>(0);
                        
                        var otherSer = new XmlSerializer(typeof(TreeContainer));

                        for (int i = 0; i < childNodeCount; i++)
                        {
                            reader.Read();

                            if (reader.IsStartElement() && reader.NodeType == XmlNodeType.Element && reader.Name == "TreeContainer")
                            {
                                var other = (TreeContainer)otherSer.Deserialize(reader);
                                ChildNodes.Add(other);

                                reader.ReadEndElement();
                            }
                        }

                        //var counter = 0;
                        //while (counter<childNodeCount && reader.Read())
                        //{
                        //    if (reader.IsStartElement() && reader.NodeType == XmlNodeType.Element && reader.Name == "TreeContainer")
                        //    {
                        //        var other = (TreeContainer)otherSer.Deserialize(reader);
                        //        ChildNodes.Add(other);
                        //        counter++;
                        //    }
                        //}
                        
                    }

                    //  If the element is "profile"
                    if (reader.Name == "Items")
                    {
                        var itemCount = reader.GetAttribute("count").To<int>(0);

                        var otherSer = new XmlSerializer(typeof(TreeItem));

                        var counter = 0;

                        while (counter < itemCount && reader.Read())
                        {
                            if (reader.IsStartElement() && reader.NodeType == XmlNodeType.Element && reader.Name == "TreeItem")
                            {
                                var other = (TreeItem)otherSer.Deserialize(reader);
                                Items.Add(other);

                                counter++;
                            }
                        }

                    }
                }
            }

        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteElementString("Key", Key);
            writer.WriteElementString("ParentNodeKey", ParentNodeKey);

            #region ChildNodes
            writer.WriteStartElement("ChildNodes");
            writer.WriteAttributeString("Id", ChildNodes.Id);
            writer.WriteAttributeString("count", ChildNodes.Count.ToString());

            var childNodeSer = new XmlSerializer(typeof(TreeContainer));
            foreach (var childNode in ChildNodes)
            {
                childNodeSer.Serialize(writer, childNode);
            }
            writer.WriteEndElement();

            #endregion

            #region Items
            writer.WriteStartElement("Items");
            writer.WriteAttributeString("count", Items.Count.ToString());

            var itemsSer = new XmlSerializer(typeof(TreeItem));
            foreach (var item in Items)
            {
                itemsSer.Serialize(writer, item);
            }
            writer.WriteEndElement();
            #endregion

        }
    }
}
