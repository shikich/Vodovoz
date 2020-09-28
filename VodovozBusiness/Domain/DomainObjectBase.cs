using QS.DomainModel.Entity;

namespace Vodovoz.Domain {
    public class DomainObjectBase : PropertyChangedBase, IDomainObject {
        public virtual int Id { get; set; }
    }
}