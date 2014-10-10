using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOGI.Framework.Toolkit.Core.Common.Collections
{
    public class TreeContainerBuilder
    {
        public TreeContainer CurrentContainer { get; private set; }

        public TreeContainerBuilder(string key)
        {
            CurrentContainer =new TreeContainer(key);
            CurrentContainer.Id = Guid.NewGuid().ToString();
        }

        public TreeContainer this[string key]
        {
            get
            {
                var findedContainer = FindInnerContainer(CurrentContainer, key);

                if (findedContainer==null)
                {
                    findedContainer = this.CurrentContainer.Add(key);
                }

                return findedContainer;
            }
        }

        #region Disposing
        ~TreeContainerBuilder()
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
                    this.CurrentContainer.Dispose();
                }
                catch (Exception)
                {

                }
            }
        } 
        #endregion

        #region Action
        public List<TreeItem> GetAllItem()
        {
            var retValue = CurrentContainer.GetAllItems();

            return retValue;
        }

        public TreeContainer Add(string key)
        {
            return CurrentContainer.Add(key);
        }

        private TreeContainer FindInnerContainer(TreeContainer container, string key)
        {
            if (container != null )
            {
                if (container.Key == key)
                {
                    return container;
                }

                return container.ChildNodes.Select(treeContainer => FindInnerContainer(treeContainer, key)).FirstOrDefault();
            }

            return null;
        }
        #endregion
    }
}
