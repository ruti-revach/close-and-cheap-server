using Newtonsoft.Json;


namespace close_and_cheap.Data.DTO
{
    public class CategoryDTO
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
