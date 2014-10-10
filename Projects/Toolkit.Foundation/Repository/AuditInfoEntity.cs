using System;
using System.Runtime.Serialization;

namespace LOGI.Framework.Toolkit.Foundation.Repository
{
    [DataContract]
    public abstract class AuditInfoEntity : EntityBase
    {
        private string _CreatedBy;
        [DataMember]
        public virtual string CreatedBy
        {
            get
            {
                return _CreatedBy;
            }
            set
            {
                if ((this._CreatedBy != value))
                {
                    this.SendPropertyChanging();
                    this._CreatedBy = value;
                    this.SendPropertyChanged("CreatedBy");
                }
            }
        }

        private DateTime? _CreatedDate;
        [DataMember]
        public virtual DateTime? CreatedDate
        {
            get
            {
                return _CreatedDate;
            }
            set
            {
                if ((this._CreatedDate != value))
                {
                    this.SendPropertyChanging();
                    this._CreatedDate = value;
                    this.SendPropertyChanged("CreatedDate");
                }
            }
        }

        private string _ModifiedBy;
        [DataMember]
        public virtual string ModifiedBy
        {
            get
            {
                return _ModifiedBy;
            }
            set
            {
                if ((this._ModifiedBy != value))
                {
                    this.SendPropertyChanging();
                    this._ModifiedBy = value;
                    this.SendPropertyChanged("ModifiedBy");
                }
            }
        }

        private DateTime? _ModifiedDate;
        [DataMember]
        public virtual DateTime? ModifiedDate
        {
            get
            {
                return _ModifiedDate;
            }
            set
            {
                if ((this._ModifiedDate != value))
                {
                    this.SendPropertyChanging();
                    this._ModifiedDate = value;
                    this.SendPropertyChanged("ModifiedDate");
                }
            }
        }

    }
}
