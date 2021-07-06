using Newtonsoft.Json;

namespace Bilbayt.Core.Entities
{
    public abstract class BaseEntity
    {
        [JsonProperty(PropertyName = "id")]
        public virtual string Id { get; set; }
    }
}
