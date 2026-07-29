using System;
namespace TodoList.Config
{
    public class DatabaseSettings
    {
        public string CollectionString { get; set; } = string.Empty;
        public string DataBaseName { get; set; } = string.Empty;
        public string TodoCollectionName { get; set; } = string.Empty;
        public string UserCollectionName { get; set; } = string.Empty;
        public string CommentCollectionName { get; set; } = string.Empty;
    }
}
