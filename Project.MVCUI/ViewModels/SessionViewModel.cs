using Newtonsoft.Json;

namespace Project.MVCUI.ViewModels
{
    [Serializable]
    public class SessionViewModel
    {
        [JsonProperty]
        public string? ImagePath { get; set; }
    }
}
